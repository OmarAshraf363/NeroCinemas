using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nero.Data;
using Nero.Models;
using Nero.Repository.IRepository;
using Nero.Repository.ModelsRepository.ActorModel;
using Nero.Repository.ModelsRepository.ActorMoviesModel;
using Nero.ViewModel;

namespace Nero.Controllers
{
    public class ActorController : Controller
    {

        private readonly IUnitOfWork unitOfWork;

        public ActorController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

  


        public IActionResult GetActor(int id)
        {
            var actor = unitOfWork.ActorRepository.Get(e=>e.Id==id).SingleOrDefault();
            var movies=unitOfWork.ActiveMoviesRepository.Get(e=>e.ActorId==id,e=>e.Movie).Select(e=>e.Movie).ToList();
            ViewBag.movies = movies;
            return View(actor);
        }
    }
}
