using Microsoft.EntityFrameworkCore;
using Nero.Models;

namespace Nero.Data
{
    public class AppDbContext:DbContext
    {
        //DbSets
        public DbSet<Cinema>Cinemas { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<ActorMovie> ActorsMovie { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var connection = builder.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connection);
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

            modelBuilder.Entity<Category>().HasMany(e=>e.Movies)
                .WithOne(e=>e.Category).HasForeignKey(e=>e.CategoryId);
        }
    }
}
