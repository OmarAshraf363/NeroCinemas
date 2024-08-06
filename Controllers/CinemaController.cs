using Microsoft.AspNetCore.Mvc;
using Nero.Repository.ModelsRepository.CinemaModel;
using Nero.Repository.ModelsRepository.MovieModel;

namespace Nero.Controllers
{
    public class CinemaController : Controller
    {
        private readonly CinemaRepository _cinemaRepository;
        private readonly MovieRepository movieRepository;

        public CinemaController(CinemaRepository cinemaRepository, MovieRepository movieRepository)
        {
            _cinemaRepository = cinemaRepository;
            this.movieRepository = movieRepository;
        }

        public IActionResult Index()
        {
            var items=_cinemaRepository.GetAll();
            return View(items);
        }
        public IActionResult Movies(int id)
        {
            ViewBag.name=_cinemaRepository.GetById(id)?.Name;
            var movies = movieRepository.GetAll().Where(e => e.CinemaId == id).ToList();
            return View(movies);
        }
    }
}
