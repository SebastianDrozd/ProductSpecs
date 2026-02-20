
using Microsoft.EntityFrameworkCore;
using ProductSpecs.Data;
using ProductSpecs.Data.Dapper;
using ProductSpecs.Services;
using Scalar.AspNetCore;

namespace ProductSpecs
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();


            builder.Services.AddDbContext<MysqlDbContext>(options =>
                options.UseMySql(
                                 builder.Configuration.GetConnectionString("Mysql"),
                                ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("Mysql"))));

            builder.Services.AddScoped <AuthService>() ;
            builder.Services.AddScoped<AuthQueries>();
            builder.Services.AddScoped<LdapService>();
            builder.Services.AddScoped<SessionQueries>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseExceptionHandler("/error");

            app.MapControllers();

            app.Run();
        }
    }
}
