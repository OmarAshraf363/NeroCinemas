using Microsoft.AspNetCore.Mvc;
using Nero.Models;
using Nero.Repository.IRepository;
using Nero.Repository.ModelsRepository.ActorModel;
using Nero.Repository.ModelsRepository.CategoryModel;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Nero.Repository.ModelsRepository.MovieModel;

namespace Nero.Controllers
{
    public class HomeController : Controller
    {

        private readonly IMovieRepository _movieRepo;
        private readonly ICategoryRepository _categoryRepo;


        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IMovieRepository movieRepo, ICategoryRepository categoryRepo)
        {
            _logger = logger;


            _movieRepo = movieRepo;
            _categoryRepo = categoryRepo;
        }

        public IActionResult Index()
        {
            var allMovies = _movieRepo.GetAll().Include(e => e.Category);
            


            foreach (var movie in allMovies)
            {
                if (movie.StartDate <= DateTime.Now && movie.EndDate > movie.StartDate && movie.EndDate >=DateTime.Now)
                {
                    movie.MovieStatus = MovieStatus.Available;

                }
                else if (movie.StartDate > DateTime.Now && movie.EndDate > movie.StartDate)
                {
                    movie.MovieStatus = MovieStatus.Upcoming;

                }
                else
                {
                    movie.MovieStatus = MovieStatus.Expired;

                }

            }
            var allCategories = _categoryRepo.GetAll();
            ViewBag.Categories= allCategories;
            _movieRepo.Save();
           
            return View(allMovies);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
