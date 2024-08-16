using Nero.Repository.ModelsRepository.ActorModel;
using Nero.Repository.ModelsRepository.ActorMoviesModel;
using Nero.Repository.ModelsRepository.CategoryModel;
using Nero.Repository.ModelsRepository.CinemaModel;
using Nero.Repository.ModelsRepository.MovieModel;
using Nero.Repository.ModelsRepository.OrderItemModel;
using Nero.Repository.ModelsRepository.OrderModel;

namespace Nero.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository CategoryRepository { get; }
        ICinemaRepository CinemaRepository { get; }
        IMovieRepository MovieRepository { get; }
        ActiveMoviesRepository ActiveMoviesRepository { get; }
        IActorRepository ActorRepository { get; }
        IOrderItemRepository OrderItemRepository { get; }
        IOrderRepository OrderRepository { get; }
       
    }
}
