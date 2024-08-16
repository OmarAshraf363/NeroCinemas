using Nero.Data;
using Nero.Repository.ModelsRepository.ActorModel;
using Nero.Repository.ModelsRepository.ActorMoviesModel;
using Nero.Repository.ModelsRepository.CategoryModel;
using Nero.Repository.ModelsRepository.CinemaModel;
using Nero.Repository.ModelsRepository.MovieModel;
using Nero.Repository.ModelsRepository.OrderItemModel;
using Nero.Repository.ModelsRepository.OrderModel;

namespace Nero.Repository.IRepository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext context;

        public UnitOfWork(AppDbContext context)
        {
            this.context = context;
            CategoryRepository =new CategoryRepository(context);
            CinemaRepository = new CinemaRepository(context);
            MovieRepository = new MovieRepository(context);
            ActiveMoviesRepository = new ActorMoviesRepository(context);
            ActorRepository = new ActorRepository(context);
            OrderItemRepository = new OrderItemRepository(context);
            OrderRepository = new OrderRepository(context);
            
        }

        public ICategoryRepository CategoryRepository { get; set; }

        public ICinemaRepository CinemaRepository { get; set; }

        public IMovieRepository MovieRepository { get; set; }

        public ActiveMoviesRepository ActiveMoviesRepository { get; set; }

        public IActorRepository ActorRepository { get; set; }

        public IOrderItemRepository OrderItemRepository { get; set; }

        public IOrderRepository OrderRepository { get; set; }
    }
}
