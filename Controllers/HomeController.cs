using Microsoft.AspNetCore.Mvc;
using Nero.Models;
using Nero.Repository.IRepository;
using Nero.Repository.ModelsRepository.ActorModel;
using Nero.Repository.ModelsRepository.CategoryModel;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Nero.Repository.ModelsRepository.MovieModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Nero.Controllers
{
    [Authorize(Roles ="Admin,Customar")]
    public class HomeController : Controller
    {
        
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<AppUser> userManager;

        private readonly ILogger<HomeController> _logger;
        private readonly SignInManager<AppUser> signInManager;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _logger = logger;
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public IActionResult Index()
        {
            
            var user = signInManager.IsSignedIn(principal:signInManager.Context.User);
            if (user)
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
            else
            {
                return RedirectToAction("Login","Account");
            }
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
