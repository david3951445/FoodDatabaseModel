using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace DatabaseBuilder.Models;

public class FoundationFood
{
    public int FdcId { get; set; }
    public string DataType { get; set; }
    public string Description { get; set; }
    public string? FoodClass { get; set; }
    public string? FootNote { get; set; }
    public bool IsHistoricalReference { get; set; }
    public int NdbNumber { get; set; }
    public string? PublicationDate { get; set; }
    public string? ScientificName { get; set; }

    public FoodCategory FoodCategory { get; set; }
    public ICollection<FoodComponent> FoodComponents { get; set; }
    public ICollection<FoodNutrient>? FoodNutrients { get; set; }
    public ICollection<FoodPortion> FoodPortions { get; set; }
    public ICollection<InputFoodFoundation> InputFoods { get; set; }
    public ICollection<NutrientConversionFactors> NutrientConversionFactors { get; set; }
}

public class FoodCategory
{
    public int Id { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
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
    public float Amount { get; set; }
    public int DataPoints { get; set; }
    public float Min { get; set; }
    public float Max { get; set; }
    public float Median { get; set; }
    public string Type { get; set; }
    public Nutrient Nutrient { get; set; }
    public FoodNutrientDerivation FoodNutrientDerivation { get; set; }
    public NutrientAnalysisDetails NutrientAnalysisDetails { get; set; }

    public int FoundationFoodId { get; set; }
    // public int NutrientId { get; set; }
}

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
/// </summary>
public class InputFoodFoundation
{
    public int Id { get; set; }
    public string FoodDescription { get; set; }
    public SampleFoodItem InputFood { get; set; }
}

public class NutrientConversionFactors
{
    public string Type { get; set; }
    public float Value { get; set; }
}

/// <summary>
/// a food nutrient
/// </summary>
public class Nutrient
{
    public int Id { get; set; }
    public string Number { get; set; }
    public string Name { get; set; }
    public int Rank { get; set; }
    public string UnitName { get; set; }
}

public class FoodNutrientDerivation
{
    public int Id { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    public FoodNutrientSource FoodNutrientSource { get; set; }
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
    public System.Collections.Generic.ICollection<NutrientAcquisitionDetails> NutrientAcquisitionDetails { get; set; }
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
    public System.Collections.Generic.ICollection<FoodCategory> FoodAttributes { get; set; }
}

public class FoodNutrientSource
{
    public int Id { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
}

public class NutrientAcquisitionDetails
{
    [Key]
    public int SampleUnitId { get; set; }
    public string PurchaseDate { get; set; }
    public string StoreCity { get; set; }
    public string StoreState { get; set; }
}
