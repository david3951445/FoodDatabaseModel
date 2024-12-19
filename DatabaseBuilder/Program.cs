using DatabaseBuilder.Models;
using DatabaseBuilder.Data;

string? projectRoot = Directory.GetParent(AppContext.BaseDirectory)?.Parent?.Parent?.Parent?.Parent?.FullName;
string folderPath = projectRoot == null
    ? throw new ArgumentException("Could not find project root.")
    : Path.Combine(projectRoot, "doc");
if (!Directory.Exists(folderPath))
{
    Console.WriteLine($"Creating folder '{folderPath}'...");
    Directory.CreateDirectory(folderPath);
}

string filePath = Path.Join(folderPath, "foundationDownload.json");
if (!File.Exists(filePath))
{
    string zipFileName = "FoodData_Central_foundation_food_json_2024-10-31.zip";
    string zipFilePath = Path.Join(folderPath, zipFileName);

    await DownloadFoodData(zipFileName, zipFilePath);

    Console.WriteLine($"Extracting file '{zipFilePath}' to '{folderPath}'...");
    System.IO.Compression.ZipFile.ExtractToDirectory(zipFilePath, folderPath);
}

Console.WriteLine("Deserializing json file...");
string test = Path.Join(folderPath, "one_object.json");
// string json = File.ReadAllText(test);
string json = File.ReadAllText(filePath);
var settings = new Newtonsoft.Json.JsonSerializerSettings
{
    MissingMemberHandling = Newtonsoft.Json.MissingMemberHandling.Error
};
var foodItems = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, DatabaseBuilder.Services.FoundationFoodItem[]>>(json)!["FoundationFoods"];

Console.WriteLine("Building database...");
string connectionString = Path.Join(folderPath, "foods.db");
using FoodsContext context = new(connectionString);

// Recreate database
context.Database.EnsureDeleted();
context.Database.EnsureCreated();

// FoodCategory[] foodCategories = foodItems
//     .SelectMany(i => i.InputFoods)
//     .Select(f => f.InputFood.AdditionalProperties.TryGetValue("foodCategory", out var category) && category is JObject jObject ? jObject.ToObject<DatabaseBuilder.Services.FoodCategory>() : null)
//     .Where(c => c != null)
//     .Select(a => a.ToEntity())
//     .DistinctBy(e => e.Id)
//     .Where(e => e.Id != default)
//     .ToArray();

// context.FoodCategories.AddRange(foodCategories);
// context.SaveChanges();

FoundationFood[] foundationFoods = foodItems
    .Select(i => i.ToEntity())
    .ToArray();

context.FoundationFoods.AddRange(foundationFoods);
context.SaveChanges();

Nutrient[] nutrients = foodItems
    .SelectMany(i => i.FoodNutrients)
    .Select(fn => fn.Nutrient.ToEntity())
    .DistinctBy(e => e.Id)
    .ToArray();

context.Nutrients.AddRange(nutrients);
context.SaveChanges();

FoodNutrient[] foodNutrients = foodItems
    .SelectMany(i => i.FoodNutrients
        .Select(n => 
        {
            var e = n.ToEntity(); // Mapping step
            e.FoundationFoodId = i.FdcId;
            return e;
        }))
    .DistinctBy(e => e.Id) // Remove duplicates
    .ToArray();

context.FoodNutrients.AddRange(foodNutrients);
context.SaveChanges();

async Task DownloadFoodData(string zipFileName, string zipFilePath)
{
    Console.WriteLine("Downloading food data...");

    Uri baseAddress = new("https://fdc.nal.usda.gov");
    UriBuilder builder = new(baseAddress) { Path = $"fdc-datasets/{zipFileName}" };

    using HttpClient client = new() { Timeout = TimeSpan.FromSeconds(3) };
    using var stream = await client.GetStreamAsync(builder.Uri);
    using var fileStream = File.Create(zipFilePath);
    await stream.CopyToAsync(fileStream);
}
