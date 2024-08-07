using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nero.Data;
using Nero.Repository.ModelsRepository.ActorModel;
using Nero.Repository.ModelsRepository.ActorMoviesModel;

namespace Nero.Controllers
{
    public class ActorController : Controller
    {
        private readonly IActorRepository actorRepository;
        private readonly IActiveMoviesRepository actorMoviesRepository;

        public ActorController(IActorRepository actorRepository, IActiveMoviesRepository actorMoviesRepository)
        {
            this.actorRepository = actorRepository;
          
            this.actorMoviesRepository = actorMoviesRepository;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetActor(int id)
        {
            var actor = actorRepository.GetById(id);
            var movies=actorMoviesRepository.GetAll().Include(e=>e.Movie).Where(e=>e.ActorId==id).Select(e=>e.Movie).ToList();
            ViewBag.movies = movies;
            return View(actor);
        }
    }
}
