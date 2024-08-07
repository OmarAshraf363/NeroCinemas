using Microsoft.EntityFrameworkCore;
using Nero.Data;
using Nero.Models;
using Nero.Repository.IRepository;
using Nero.Repository.ModelsRepository.ActorModel;
using Nero.Repository.ModelsRepository.ActorMoviesModel;
using Nero.Repository.ModelsRepository.CategoryModel;
using Nero.Repository.ModelsRepository.CinemaModel;
using Nero.Repository.ModelsRepository.MovieModel;
using System.Configuration;

namespace Nero
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IMovieRepository, MovieRepository>();
            builder.Services.AddScoped<IActorRepository, ActorRepository>();
            builder.Services.AddScoped<ICinemaRepository, CinemaRepository>();
            builder.Services.AddScoped<IActiveMoviesRepository,ActorMoviesRepository>();

            //builder.Services.AddDbContext<AppDbContext>();
            //builder.Services.AddScoped(typeof(IGenralRepository<>), typeof(GenralRepository<>));
            //builder.Services.AddScoped<CategoryRepository>();
            //builder.Services.AddScoped<ActorRepository>();
            //builder.Services.AddScoped< MovieRepository>();
            //builder.Services.AddScoped< CinemaRepository>();
            //builder.Services.AddScoped<ActorMoviesRepository>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

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
