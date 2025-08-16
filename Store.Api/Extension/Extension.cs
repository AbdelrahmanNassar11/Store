using Domain.Contracts;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Persistencies;
using Persistencies.Identity;
using Services;
using Shared.ErrorModels;
using Store.Api.Middlewares;


namespace Store.Api.Extension
{
    public static class Extension
    {
        public static IServiceCollection RegisterAllServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddBulidInServices();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            services.AddSwaggerServices();

            services.AddInfrastructureServices(configuration);

            services.AddApplicationServices();
            services.AddIdentityServices();
            services.ConfigerServices();
            return services;
        }
        private static IServiceCollection AddBulidInServices(this IServiceCollection services)
        {
            services.AddControllers();
            return services;
        }

        private static IServiceCollection AddIdentityServices(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, IdentityRole>()
                 .AddEntityFrameworkStores<StoreIdentityDbContext>();


            return services;
        }

        private static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            return services;
        }

        private static IServiceCollection ConfigerServices(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(config =>
            {
                config.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(m => m.Value.Errors.Any())
                                 .Select(m => new ValidationError
                                 {
                                     Field = m.Key,
                                     Errors = m.Value.Errors.Select(error => error.ErrorMessage)
                                 });
                    var responce = new ValidationErrorResponse
                    {
                        Error = errors,
                    };
                    return new BadRequestObjectResult(responce);
                };
            });
            return services;
        }

        public static async Task<WebApplication> ConfigureMiddlewares(this WebApplication app)
        {
            await app.InitializeDataBaseAsync();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseGloblErrorHandling();

            app.MapControllers();
            return app;
        }

        private static async Task <WebApplication> InitializeDataBaseAsync(this WebApplication app)
        {
            #region Seeding
            using var scope = app.Services.CreateScope();
            var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            await dbInitializer.InitializeAsync();
            await dbInitializer.InitializeIdentityAsync();
            #endregion
            return app;
        }
        private static WebApplication UseGloblErrorHandling(this WebApplication app)
        {
            app.UseMiddleware<GloblErrorHandlingMiddleware>();
            return app;
        }
    }
}
