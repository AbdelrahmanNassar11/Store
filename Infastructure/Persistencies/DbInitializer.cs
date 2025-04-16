using Domain.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistencies.Data.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistencies
{
    public class DbInitializer : IDbInitializer
    {
        private readonly StoreDbContext _Context;

        public DbInitializer(StoreDbContext Context)
        {
            _Context = Context;
        }
        public async Task InitializeAsync()
        {
            //Create the database if it doesn't exist && Apply to Any Building Migrations
            if(_Context.Database.GetPendingMigrations().Any())
            {
                await _Context.Database.MigrateAsync();
            }

            try
            {
                // Data Seeding
                //1. Seeding Product Types From Json File
                if (!_Context.ProductTypes.Any())
                {
                    // - Read all Data in the Json File As String
                    var typesData = await File.ReadAllTextAsync(@"..\Infastructure\Persistencies\Data\Seeding\types.json");

                    // - Transform the String to C# Object [List<ProductTypes>]
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                    // - Add [List<ProductTypes>] to DataBase
                    if (types is not null && types.Any())
                    {
                        await _Context.ProductTypes.AddRangeAsync(types);
                        await _Context.SaveChangesAsync();
                    }
                }

                //2. Seeding Product Brands From Json File
                if (!_Context.ProductBrands.Any())
                {
                    // - Read all Data in the Json File As String
                    var brandsData = await File.ReadAllTextAsync(@"..\Infastructure\Persistencies\Data\Seeding\brands.json");

                    // - Transform the String to C# Object [List<ProductBrand>]
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                    // - Add [List<ProductBrand>] to DataBase
                    if (brands is not null && brands.Any())
                    {
                        await _Context.ProductBrands.AddRangeAsync(brands);
                        await _Context.SaveChangesAsync();
                    }
                }

                //3. Seeding Products From Json File
                if (!_Context.Products.Any())
                {
                    // - Read all Data in the Json File As String
                    var ProductsData = await File.ReadAllTextAsync(@"..\Infastructure\Persistencies\Data\Seeding\products.json");

                    // - Transform the String to C# Object [List<Product>]
                    var Products = JsonSerializer.Deserialize<List<Product>>(ProductsData);

                    // - Add [List<Product>] to DataBase
                    if (Products is not null && Products.Any())
                    {
                        await _Context.Products.AddRangeAsync(Products);
                        await _Context.SaveChangesAsync();
                    }
                }
            }
            catch
            {
                throw;
            }
          
        }
    }
}
//..\Infastructure\Persistencies\Data\Seeding\types.json
