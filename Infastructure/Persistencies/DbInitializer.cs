using Domain.Contracts;
using Domain.Entities;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistencies.Data.DbContexts;
using Persistencies.Identity;
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
        private readonly StoreIdentityDbContext _identityDbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(
            StoreDbContext Context,
            StoreIdentityDbContext identityDbContext,
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager
            )
        {
            _Context = Context;
            _identityDbContext = identityDbContext;
            _userManager = userManager;
            _roleManager = roleManager;
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

        public async Task InitializeIdentityAsync()
        {
            //Create the database if it doesn't exist && Apply to Any Building Migrations
            if (_identityDbContext.Database.GetPendingMigrations().Any())
            {
                await _identityDbContext.Database.MigrateAsync(); 
            }
            //Create Roles
            if(!_roleManager.Roles.Any())
            {

                await _roleManager.CreateAsync(new IdentityRole()
                {
                    Name = "Admin",     
                });
                await _roleManager.CreateAsync(new IdentityRole()
                {
                    Name = "SuperAdmin",
                });

            }
            if (!_userManager.Users.Any())
            {
                var superAdminUser = new AppUser()
                {
                    DisplayName = "Super Admin",
                    Email ="superadmin@gmail.com",
                    UserName = "SuperAdmin",
                    PhoneNumber = "01012345678",
                };
                var adminUser = new AppUser()
                {
                    DisplayName = "Admin",
                    Email = "admin@gmail.com",
                    UserName = "Admin",
                    PhoneNumber = "01034678965",
                };
                //P@ssword
                await _userManager.CreateAsync(superAdminUser, "P@ssword");
                await _userManager.CreateAsync(adminUser, "P@ssword");
                await _userManager.AddToRoleAsync(superAdminUser, "SuperAdmin");
                await _userManager.AddToRoleAsync(adminUser, "Admin");
            }

        }
    }
}
//..\Infastructure\Persistencies\Data\Seeding\types.json
