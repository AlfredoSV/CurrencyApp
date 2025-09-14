using CurrencyApp.Application.IServices;
using CurrencyApp.Application.Services;
using CurrencyApp.Domain.Entities;
using CurrencyApp.Domain.IRepositories;
using CurrencyApp.Infrastructure.Data;
using CurrencyApp.Infrastructure.Repositories;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace CurrencyApp.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddHttpClient("FrankFurterApi", (http) =>
            {
                http.BaseAddress = new Uri(builder.Configuration.GetSection("Api:ApiBaseUrl").Value??string.Empty);
            });

            //DbContext
            builder.Services.AddDbContext<DataDbContext>(options =>
            options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")
                )
            );

            builder.Services.AddScoped<ICurrencyRepository, CurrencyRepository>();
            builder.Services.AddScoped<ICurrencyService, CurrencyService>();

            builder.Services.AddScoped<IRequestRepository, RequestRepository>();
            builder.Services.AddScoped<IRequestService, RequestService>();

            builder.Services.AddScoped<ILogBookRepository, LogBookRepository>();
            builder.Services.AddScoped<ILogBookService, LogBookService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseHsts();
            }

            //Log errors for Controllers
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    IExceptionHandlerPathFeature exceptionHandlerPathFeature =
                        context.Features.Get<IExceptionHandlerPathFeature>()!;

                    ILogBookService serviceLog = context.RequestServices.GetService<ILogBookService>()!;

                    LogBook log = LogBook.Create(exceptionHandlerPathFeature?.Error ?? new Exception());

                    await serviceLog?.SaveLog(log)!;

                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = "text/html;charset=utf-8";

                    await context.Response.WriteAsync($"<h2>Ocurrió un error, favor de consultar el siguiente ticket con soporte: {log.Id} </h2>");
                });
            });

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Management}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
