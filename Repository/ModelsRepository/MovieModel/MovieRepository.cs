using Microsoft.EntityFrameworkCore;
using Nero.Data;
using Nero.Models;
using Nero.Repository.IRepository;
using Nero.ViewModel;

namespace Nero.Repository.ModelsRepository.MovieModel
{
    public class MovieRepository:GenralRepository<Movie>, IMovieRepository
    {

        public MovieRepository(AppDbContext context) : base(context) { }
        public Movie? GetMovieandCategoryById(int id)
        {
            return context.Movies.Include(e=>e.Category).ThenInclude(e=>e.Movies).Include(e=>e.Cinema).ThenInclude(e=>e.Movies)
                .Include(e=>e.ActorMovies).ThenInclude(e=>e.Actor)
                .Where(e => e.Id == id).SingleOrDefault();
        }
      
        public void updateFromVm(Movie movie, MovieVM model,bool fromMtoVm)
        {
            if (fromMtoVm==true) { 
            movie.MovieStatus = model.MovieStatus;
            movie.StartDate = model.StartDate;
            movie.EndDate = model.EndDate;
            movie.ImgUrl = model.ImgUrl;
            movie.TrailerUrl = model.TrailerUrl;
            movie.CategoryId = model.CategoryId;
            movie.CinemaId = model.CinemaId;
            movie.Price = model.Price;
            movie.Name = model.Name;
            movie.Description = model.Description;
            Update(movie);
            Save();
            }
            else
            {
                model.MovieStatus = movie.MovieStatus;
                model.StartDate = movie.StartDate;
                model.EndDate = movie.EndDate;
                model.ImgUrl = movie.ImgUrl;
                model.TrailerUrl = movie.TrailerUrl;
                model.CategoryId = movie.CategoryId;
                model.CinemaId = movie.CinemaId;
                model.Price = movie.Price;
                model.Name = movie.Name;
                model.Description = movie.Description;
                model.Categories=context.Categories.ToList();
                model.CinemaList=context.Cinemas.ToList();
            }
        }
    }
}
