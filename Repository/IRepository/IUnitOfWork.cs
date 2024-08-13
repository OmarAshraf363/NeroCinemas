using Nero.Repository.ModelsRepository.ActorModel;
using Nero.Repository.ModelsRepository.ActorMoviesModel;
using Nero.Repository.ModelsRepository.CategoryModel;
using Nero.Repository.ModelsRepository.CinemaModel;
using Nero.Repository.ModelsRepository.MovieModel;

namespace Nero.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository CategoryRepository { get; }
        ICinemaRepository CinemaRepository { get; }
        IMovieRepository MovieRepository { get; }
        ActiveMoviesRepository ActiveMoviesRepository { get; }
        IActorRepository ActorRepository { get; }
       
    }
}
