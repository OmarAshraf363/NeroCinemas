using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nero.Repository.ModelsRepository.CategoryModel;
using Nero.Repository.ModelsRepository.MovieModel;

namespace Nero.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryRepository _repository;
        private readonly MovieRepository _movieRepository;


        public CategoryController(CategoryRepository repository, MovieRepository movieRepository)
        {
            _repository = repository;
            _movieRepository = movieRepository;
        }

        public IActionResult Index()
        {
            var categories = _repository.GetAll();
            return View(categories);
        }
        public IActionResult CategoryMovies(int id)
        {
            ViewBag.catName = _repository.GetById(id)?.Name;
            var movies = _movieRepository.GetAll().Where(e => e.CategoryId == id);
            return View(movies);
        }
      

    }
}
