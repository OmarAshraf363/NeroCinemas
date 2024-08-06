using Microsoft.EntityFrameworkCore;
using Nero.Data;
using Nero.Models;
using Nero.Repository.IRepository;

namespace Nero.Repository.ModelsRepository.MovieModel
{
    public class MovieRepository:GenralRepository<Movie>
    {

        public MovieRepository(AppDbContext context) : base(context) { }
        public Movie? GetMovieandCategoryById(int id)
        {
            return context.Movies.Include(e=>e.Category).ThenInclude(e=>e.Movies).Include(e=>e.Cinema).ThenInclude(e=>e.Movies)
                .Include(e=>e.ActorMovies).ThenInclude(e=>e.Actor)
                .Where(e => e.Id == id).SingleOrDefault();
        }

    }
}
