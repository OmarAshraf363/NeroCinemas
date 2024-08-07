using Microsoft.AspNetCore.Mvc;
using Nero.Models;
using Nero.Repository.IRepository;
using Nero.Repository.ModelsRepository.MovieModel;

namespace Nero.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieRepository _movieRepo;
        public MovieController(IMovieRepository movieRepo)
        {
            _movieRepo = movieRepo;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Details(int id)
        {
            var item = _movieRepo.GetMovieandCategoryById(id);
            return View(item);
        }
        public IActionResult Search(string text)
        {

            if (string.IsNullOrEmpty(text))
            {
                return View("NotFound");
            }
            else
            {
                var movies = _movieRepo.GetAll().Where(e => e.Name.Contains(text)).ToList();
                if (movies.Count == 0)
                {
                    return View("NotFound");
                }
                else
                {
                    return View(movies);

                }

            }
        }
    }
}
