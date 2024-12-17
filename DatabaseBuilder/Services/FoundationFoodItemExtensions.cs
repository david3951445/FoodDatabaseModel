namespace DatabaseBuilder.Services;

public static class FoundationFoodItemExtensions
{

}

public partial class FoundationFoodItem
{
    public Models.FoundationFood ToEntity() => new()
    {
        FdcId = FdcId,
        FoodNutrients = FoodNutrients.Select(x => x.ToEntity()).ToArray(),
    };
}

public partial class FoodNutrient
{
    public Models.FoodNutrient ToEntity() => new()
    {
        Id = Id,
        Amount = Amount,
    };
}

public partial class Nutrient
{
    public Models.Nutrient ToEntity() => new()
    {
        Id = Id,
        Number = Number,
        Name = Name,
        Rank = Rank,
        UnitName = UnitName
    };
}