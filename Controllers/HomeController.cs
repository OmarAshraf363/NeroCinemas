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

        private readonly IUnitOfWork unitOfWork;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            this.unitOfWork = unitOfWork;
            
        }

        public IActionResult Index()
        {
            var allMovies =unitOfWork.MovieRepository.GetAll().Include(e => e.Category);

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
            ViewBag.Categories= unitOfWork.CategoryRepository.GetAll();
            unitOfWork.CategoryRepository.Save();
           
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
