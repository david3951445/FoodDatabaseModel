# Database Model for USDA Foundation Foods

## Download food data
By powershell 7
```pwsh
curl -o doc/FoodData_Central_foundation_food_json_2024-10-31.zip "https://fdc.nal.usda.gov/fdc-datasets/FoodData_Central_foundation_food_json_2024-10-31.zip"
```
or from [website](https://fdc.nal.usda.gov/download-datasets) (Foundation Foods data type)

## Create client
1. Download json from https://app.swaggerhub.com/apis/fdcnal/food-data_central_api/1.0.1
2. Create C# client from open API json file
```pwsh
cd .\doc\
nswag openapi2csclient /input:fdcnal-food-data_central_api-1.0.1-resolved.json /classname:FoodDataCentralClient /namespace:DatabaseBuilder.Services /output:../doc/DatabaseBuilder/Services/FoodDataCentralClient.cs
```