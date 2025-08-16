
using Domain.Contracts;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistencies;
using Persistencies.Data.DbContexts;
using Services;
using Services.Abstractions;
using Shared.ErrorModels;
using Store.Api.Extension;
using Store.Api.Middlewares;
using System.Threading.Tasks;

namespace Store.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.RegisterAllServices(builder.Configuration);


            var app = builder.Build();

            await app.ConfigureMiddlewares();

            app.Run();
        }
    }
}
