using Nero.Data;
using Nero.Repository.ModelsRepository.ActorModel;
using Nero.Repository.ModelsRepository.ActorMoviesModel;
using Nero.Repository.ModelsRepository.CategoryModel;
using Nero.Repository.ModelsRepository.CinemaModel;
using Nero.Repository.ModelsRepository.MovieModel;

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
        }

        public ICategoryRepository CategoryRepository { get; set; }

        public ICinemaRepository CinemaRepository { get; set; }

        public IMovieRepository MovieRepository { get; set; }

        public ActiveMoviesRepository ActiveMoviesRepository { get; set; }

        public IActorRepository ActorRepository { get; set; }
    }
}
