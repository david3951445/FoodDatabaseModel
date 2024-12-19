// doc: https://fdc.nal.usda.gov/docs/Download_Field_Descriptions_Oct2020.pdf
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace DatabaseBuilder.Models;

/// <summary>
/// Foods whose nutrient and food component values are derived primarily by 
/// chemical analysis. Foundation data also include extensive underlying 
/// metadata, such as the number of samples, the location and dates on which 
/// samples were obtained, analytical approaches used, and if appropriate, 
/// cultivar, genotype, and production practices.
/// </summary>
public class FoundationFood
{
    /// <summary>
    /// ID of the food in the food table
    /// </summary>
    public int Id { get; set; }
    public string DataType { get; set; }
    public string Description { get; set; }
    public string? FoodClass { get; set; }
    /// <summary>
    /// Comments on any unusual aspects. These are released to the public.
    /// Examples might include unusual aspects of the food overall.
    /// </summary>
    public string? FootNote { get; set; }
    public bool IsHistoricalReference { get; set; }
    /// <summary>
    /// Unique number assigned for the food, different from fdc_id, assigned in SR
    /// </summary>
    public int NdbNumber { get; set; }
    public string? PublicationDate { get; set; }
    public string? ScientificName { get; set; }

    public int FoodCategoryId { get; set; }
    public FoodCategory FoodCategory { get; set; } = null!;
    // public ICollection<FoodComponent> FoodComponents { get; set; }
    public ICollection<FoodNutrient> FoodNutrients { get; set; } = [];
    // public ICollection<FoodPortion> FoodPortions { get; set; }
    // public ICollection<InputFoodFoundation> InputFoods { get; set; }
    // public ICollection<NutrientConversionFactors> NutrientConversionFactors { get; set; }
}

public class FoodCategory
{
    public int Id { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }

    public FoundationFood? FoundationFood{ get; set; }
}

public class FoodComponent
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int DataPoints { get; set; }
    public double GramWeight { get; set; }
    public bool IsRefuse { get; set; }
    public int MinYearAcquired { get; set; }
    public double PercentWeight { get; set; }
}

public class FoodNutrient
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public int DataPoints { get; set; }
    public decimal Min { get; set; }
    public decimal Max { get; set; }
    public decimal Median { get; set; }
    public string Type { get; set; }

    public int NutrientId { get; set; }
    public Nutrient Nutrient { get; set; } = null!;
    // public int? FoodNutrientDerivationId { get; set; }
    // public FoodNutrientDerivation? FoodNutrientDerivation { get; set; }
    // public NutrientAnalysisDetails NutrientAnalysisDetails { get; set; }
    public int FoundationFoodId { get; set; }
    public FoundationFood FoundationFood { get; set; } = null!;
}

/// <summary>
///  Discrete amount of food
/// </summary>
public class FoodPortion
{
    public int Id { get; set; }
    public float Amount { get; set; }
    public int DataPoints { get; set; }
    public float GramWeight { get; set; }
    public int MinYearAcquired { get; set; }
    public string Modifier { get; set; }
    public string PortionDescription { get; set; }
    public int SequenceNumber { get; set; }
    public MeasureUnit MeasureUnit { get; set; }
}

/// <summary>
/// applies to Foundation foods. Not all inputFoods will have all fields.
///  A food that is an ingredient (for survey (FNDDS) foods) or a source food (for foundation foods or their source foods) to another food.
/// </summary>
public class InputFoodFoundation
{
    public int Id { get; set; }
    public string FoodDescription { get; set; }
    public SampleFoodItem InputFood { get; set; }
}

/// <summary>
/// Top level type for all types of nutrient conversion factors.
/// A separate row is stored for each of these 3 types of conversion factor.
/// </summary>
public class NutrientConversionFactors
{
    /// <summary>
    /// food_calorie_conversion_factor: The multiplication factors to be used when calculating energy from macronutrients for a specific food
    /// </summary>
    public string Type { get; set; }
    public float Value { get; set; }
}

/// <summary>
/// a food nutrient
/// </summary>
public class Nutrient
{
    public int Id { get; set; }
    public string? Number { get; set; }
    public string? Name { get; set; }
    public int Rank { get; set; }
    public string? UnitName { get; set; }

    public FoodNutrient? FoodNutrient { get; set; }
}

public class FoodNutrientDerivation
{
    public int Id { get; set; }
    public string? Code { get; set; }
    public string? Description { get; set; }

    public FoodNutrient? FoodNutrient { get; set; }
    // public int? FoodNutrientSourceId { get; set; }
    // public FoodNutrientSource? FoodNutrientSource { get; set; }
}

public class NutrientAnalysisDetails
{
    public int SubSampleId { get; set; }
    public float Amount { get; set; }
    [Key]
    public int NutrientId { get; set; }
    public string LabMethodDescription { get; set; }
    public string LabMethodOriginalDescription { get; set; }
    public string LabMethodLink { get; set; }
    public string LabMethodTechnique { get; set; }
    public ICollection<NutrientAcquisitionDetails> NutrientAcquisitionDetails { get; set; }

    public int FoodNutrientId { get; set; }
    public FoodNutrient FoodNutrient { get; set; }
}

public class MeasureUnit
{
    public int Id { get; set; }
    public string Abbreviation { get; set; }
    public string Name { get; set; }
}

public class SampleFoodItem
{
    public int FdcId { get; set; }
    public string Datatype { get; set; }
    public string Description { get; set; }
    public string FoodClass { get; set; }
    public string PublicationDate { get; set; }
    public ICollection<FoodCategory> FoodAttributes { get; set; }
}

public class FoodNutrientSource
{
    public int Id { get; set; }
    public string? Code { get; set; }
    public string? Description { get; set; }

    public FoodNutrientDerivation? FoodNutrientDerivation { get; set; }
}

public class NutrientAcquisitionDetails
{
    [Key]
    public int SampleUnitId { get; set; }
    public string PurchaseDate { get; set; }
    public string StoreCity { get; set; }
    public string StoreState { get; set; }
}
