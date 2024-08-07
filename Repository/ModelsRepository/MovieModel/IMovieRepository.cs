using Nero.Models;
using Nero.Repository.IRepository;

namespace Nero.Repository.ModelsRepository.MovieModel
{
    public interface IMovieRepository:IGenralRepository<Movie>
    {
        Movie? GetMovieandCategoryById(int id);
    }
}
