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
string json = File.ReadAllText(test);
// string json = File.ReadAllText(filePath);
var foodItems = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, DatabaseBuilder.Services.FoundationFoodItem[]>>(json)!["FoundationFoods"];

Console.WriteLine("Building database...");
string connectionString = Path.Join(folderPath, "foods.db");
using FoodsContext context = new(connectionString);

// Recreate database
context.Database.EnsureDeleted();
context.Database.EnsureCreated();

FoundationFood[] foundationFoods = foodItems
    .Select(x => x.ToEntity())
    .ToArray();

Nutrient[] nutrients = foodItems
    .SelectMany(x => x.FoodNutrients)
    .Select(x => x.Nutrient.ToEntity())
    .DistinctBy(x => x.Id)
    .ToArray();

context.AddRange(nutrients);
context.SaveChanges();

// FoodNutrientSource[] foodNutrientSources = foodItems
//     .SelectMany(x => x.FoodNutrients)
//     .Select(x => x.FoodNutrientDerivation.FoodNutrientSource)
//     .DistinctBy(x => x.Id)
//     .ToArray();

// context.FoodNutrientSources.AddRange(foodNutrientSources);
// context.SaveChanges();

// FoodCategory[] foodAttributes = foodItems
//     .SelectMany(x => x.InputFoods)
//     .SelectMany(x => x.InputFood.FoodAttributes)
//     .DistinctBy(x => x.Id)
//     .ToArray();

// context.AddRange(foodNutrientSources);
// context.SaveChanges();

// FoodNutrient[] foodNutrients = foodItems
//     .SelectMany(x => x.FoodNutrients)
//     .Select(x => x.ToEntity())
//     .ToArray();

// context.FoodNutrients.AddRange(foodNutrients);
// context.SaveChanges();

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
