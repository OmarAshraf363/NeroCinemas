using Microsoft.AspNetCore.Mvc;
using Nero.Models;
using Nero.Repository.IRepository;
using Nero.Repository.ModelsRepository.MovieModel;

namespace Nero.Controllers
{
    public class MovieController : Controller
    {
        private readonly MovieRepository _movieRepo;
        public MovieController(MovieRepository movieRepo)
        {
            _movieRepo = movieRepo;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Details(int id)
        {
            var item=_movieRepo.GetMovieandCategoryById(id);
            return View(item);
        }
        public IActionResult Search(string text)
        {

            if (string.IsNullOrEmpty(text))
            {
                return NotFound();
            }
            else
            {
                var movies = _movieRepo.GetAll().Where(e => e.Name.Contains(text));
                return View(movies);

            }
        }
    }
}
