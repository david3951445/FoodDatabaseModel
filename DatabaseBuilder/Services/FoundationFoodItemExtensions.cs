namespace DatabaseBuilder.Services;

public static class FoundationFoodItemExtensions
{

}

public partial class FoundationFoodItem
{
    public Models.FoundationFood ToEntity() => new()
    {
        Id = FdcId,
        DataType = DataType,
        Description = Description,
        FoodClass = FoodClass,
        FootNote = FootNote,
        IsHistoricalReference = IsHistoricalReference,
        NdbNumber = NdbNumber,
        PublicationDate = PublicationDate,
        ScientificName = ScientificName,
        FoodCategoryId = FoodCategory.Id,
    };
}

public partial class FoodNutrient
{
    public Models.FoodNutrient ToEntity() => new()
    {
        Id = Id,
        Amount = (decimal)Amount,
        DataPoints = DataPoints,
        Min = (decimal)Min,
        Max = (decimal)Max,
        Median = (decimal)Median,
        Type = Type,
        NutrientId = Nutrient.Id,
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

public partial class FoodCategory
{
    public Models.FoodCategory ToEntity() => new()
    {
        Id = Id,
        Code = Code,
        Description = Description
    };
}

public partial class FoodNutrientDerivation
{
    public Models.FoodNutrientDerivation ToEntity() => new()
    {
        Id = Id,
        Code = Code,
        Description = Description,
        // FoodNutrientSourceId = FoodNutrientSource.Id
    };
}

public partial class FoodNutrientSource
{
    public Models.FoodNutrientSource ToEntity() => new()
    {
        Id = Id,
        Code = Code,
        Description = Description
    };
}
