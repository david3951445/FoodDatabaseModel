using Microsoft.EntityFrameworkCore;
using DatabaseBuilder.Models;

namespace DatabaseBuilder.Data;

public class FoodsContext : DbContext
{
    private readonly string _connectionString;

    public DbSet<FoundationFood> FoundationFoods { get; set; }
    public DbSet<FoodNutrient> FoodNutrients { get; set; }
    public DbSet<Nutrient> Nutrients { get; set; }
    public DbSet<FoodCategory> FoodCategories { get; set; }
    // public DbSet<FoodNutrientSource> FoodNutrientSources { get; set; }

    public FoodsContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder
        .UseSqlite($"Data Source={_connectionString}")
        .EnableSensitiveDataLogging();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // From DatabaseBuilder.Services
        // modelBuilder.Entity<FoodCategory>()
        //     .Ignore(b => b.AdditionalProperties);
        // modelBuilder.Entity<FoodComponent>()
        //     .Ignore(b => b.AdditionalProperties);
        // modelBuilder.Entity<FoodNutrient>()
        //     .Ignore(b => b.AdditionalProperties);
        // modelBuilder.Entity<FoodPortion>()
        //     .Ignore(b => b.AdditionalProperties);
        // modelBuilder.Entity<InputFoodFoundation>()
        //     .Ignore(b => b.AdditionalProperties);
        // modelBuilder.Entity<NutrientConversionFactors>()
        //     .Ignore(b => b.AdditionalProperties)
        //     .HasKey(b => b.Type); // Not sure if Type can be a primary key
        // modelBuilder.Entity<FoodNutrientDerivation>()
        //     .Ignore(b => b.AdditionalProperties);
        // modelBuilder.Entity<FoodNutrientSource>()
        //     .Ignore(b => b.AdditionalProperties);
        // modelBuilder.Entity<MeasureUnit>()
        //     .Ignore(b => b.AdditionalProperties);
        // modelBuilder.Entity<Nutrient>()
        //     .Ignore(b => b.AdditionalProperties);
        // modelBuilder.Entity<NutrientAcquisitionDetails>()
        //     .Ignore(b => b.AdditionalProperties)
        //     .HasKey(b => b.SampleUnitId);
        // modelBuilder.Entity<NutrientAnalysisDetails>()
        //     .Ignore(b => b.AdditionalProperties)
        //     .HasKey(b => b.SubSampleId);
        // modelBuilder.Entity<SampleFoodItem>()
        //     .Ignore(b => b.AdditionalProperties)
        //     .HasKey(b => b.FdcId);
        // modelBuilder.Entity<FoundationFoodItem>()
        //     .Ignore(b => b.AdditionalProperties)
        //     .HasKey(b => b.FdcId);
    }
}
