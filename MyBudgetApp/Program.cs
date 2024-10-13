using Microsoft.EntityFrameworkCore;
using MyBudgetApp.Models;
using System;
using System.Data;
using System.IO;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using IDbConnection = System.Data.IDbConnection;

namespace MyBudgetApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Config and connection string for connecting to Mysql db
           //var config = new ConfigurationBuilder()
                //.SetBasePath(Directory.GetCurrentDirectory())
                //.AddJsonFile("appsettings.json")
               // .Build();
           // string connString = config.GetConnectionString("DefaultConnection");
            //IDbConnection conn = new MySqlConnection(connString);
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<IDbConnection>((s) =>
            {
                IDbConnection conn =
                    new MySqlConnection(builder.Configuration.GetConnectionString("DefaultConnection"));
                conn.Open();
                return conn;
            });

            // Add services to the container.
            
           // builder.Services.AddDbContext<ExpensesDbContext>(options =>
           // options.UseInMemoryDatabase("ExpenseDb"));
           builder.Services.AddScoped<IExpenseRepo, DapperExpenseRepo>();
         

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
           // var builder = WebApplication.CreateBuilder(args);

// Add services to the container
            

           

            //var app = builder.Build();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
