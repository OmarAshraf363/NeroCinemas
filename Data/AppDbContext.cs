using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Nero.Models;

namespace Nero.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        //DbSets
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<ActorMovie> ActorsMovie { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Cinema>().HasMany(e => e.Movies)
                .WithOne(e => e.Cinema).HasForeignKey(e => e.CinemaId);
            modelBuilder.Entity<Movie>()
                 .HasMany(m => m.ActorMovies)
                 .WithOne(am => am.Movie)
                 .HasForeignKey(am => am.MovieId);

            modelBuilder.Entity<Actor>()
                .HasMany(a => a.ActorMovies)
                .WithOne(am => am.Actor)
                .HasForeignKey(am => am.ActorId);

            modelBuilder.Entity<ActorMovie>()
                .HasKey(am => new { am.ActorId, am.MovieId });

            modelBuilder.Entity<Category>().HasMany(e => e.Movies)
                .WithOne(e => e.Category).HasForeignKey(e => e.CategoryId);


            // Seed data for Cinemas
            modelBuilder.Entity<Cinema>().HasData(
                new Cinema { Id = 1, Name = "Cinema A", Description = "Cinema in City Center", CinemaLogo = "cinema-a-logo.png", Address = "123 City Center Blvd" },
                new Cinema { Id = 2, Name = "Cinema B", Description = "Downtown Cinema", CinemaLogo = "cinema-b-logo.png", Address = "456 Downtown St" }
            );

            // Seed data for Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action" },
                new Category { Id = 2, Name = "Comedy" },
                new Category { Id = 3, Name = "Drama" }
            );
            modelBuilder.Entity<AppUser>().HasData(
        new AppUser
        {
            Id = "123548",
            UserName = "Admin",
            Email = "Admin@gmail.com",
            NormalizedUserName = "ADMIN",
            NormalizedEmail = "ADMIN@GMAIL.COM",
            EmailConfirmed = true,
            PasswordHash = new PasswordHasher<AppUser>().HashPassword(null, "Admin@123"),
            SecurityStamp = string.Empty,
        }
    );

            //role
            modelBuilder.Entity<IdentityRole>().HasData(
                   new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
                   new IdentityRole { Id = "2", Name = "Customer", NormalizedName = "CUSTOMER" }
                      );
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
       new IdentityUserRole<string>
       {
           UserId = "123548",
           RoleId = "1" // Admin role
       }
   );
            // Seed data for Movies
            modelBuilder.Entity<Movie>().HasData(
                new Movie
                {
                    Id = 1,
                    Name = "Action Movie 1",
                    Description = "Exciting action movie",
                    Price = 10.99,
                    ImgUrl = "movie1.png",
                    TrailerUrl = "https://trailer-url.com/action-movie-1",
                    StartDate = new DateTime(2024, 8, 1),
                    EndDate = new DateTime(2024, 8, 31),
                    MovieStatus = MovieStatus.Available,
                    NumOfVisit = 100,
                    CategoryId = 1,
                    CinemaId = 1
                },
                new Movie
                {
                    Id = 2,
                    Name = "Comedy Movie 1",
                    Description = "Hilarious comedy movie",
                    Price = 8.99,
                    ImgUrl = "movie2.png",
                    TrailerUrl = "https://trailer-url.com/comedy-movie-1",
                    StartDate = new DateTime(2024, 8, 25),
                    EndDate = new DateTime(2024, 9, 5),
                    MovieStatus = MovieStatus.Upcoming,
                    NumOfVisit = 50,
                    CategoryId = 2,
                    CinemaId = 2
                }
            );

            // Seed data for Actors
            modelBuilder.Entity<Actor>().HasData(
                new Actor { Id = 1, FirstName = "John", LastName = "Doe", Bio = "Action star", ProfilePucture = "john-doe.jpg", News = "Starring in Action Movie 1" },
                new Actor { Id = 2, FirstName = "Jane", LastName = "Smith", Bio = "Comedy queen", ProfilePucture = "jane-smith.jpg", News = "Leading role in Comedy Movie 1" }
            );

            // Seed data for ActorMovies (many-to-many relationship)
            modelBuilder.Entity<ActorMovie>().HasData(
                new ActorMovie { ActorId = 1, MovieId = 1 },
                new ActorMovie { ActorId = 2, MovieId = 2 }
            );
        }
    }
}
