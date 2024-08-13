using Nero.Models;
using Nero.Repository.IRepository;
using Nero.ViewModel;

namespace Nero.Repository.ModelsRepository.MovieModel
{
    public interface IMovieRepository:IGenralRepository<Movie>
    {
        Movie? GetMovieandCategoryById(int id);

        void updateFromVm(Movie movie,MovieVM model, bool fromMtoVm);

    }
}
