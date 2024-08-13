using Microsoft.AspNetCore.Mvc;
using Nero.Models;
using Nero.Repository.IRepository;
using Nero.Repository.ModelsRepository.MovieModel;
using Nero.ViewModel;


namespace Nero.Controllers
{
    public class MovieController : Controller
    {
        
        private readonly IUnitOfWork unitOfWork;
        public MovieController(IUnitOfWork unitOfWork)
        {

            this.unitOfWork = unitOfWork;
        }

        public IActionResult Index(string status, MovieVM model)
        {
            model.Movies = unitOfWork.MovieRepository.GetAll();
            model.Categories = unitOfWork.CategoryRepository.GetAll().ToList();
            model.CinemaList = unitOfWork.CinemaRepository.GetAll().ToList();
            if (!string.IsNullOrEmpty(status))
            {
                if (Enum.TryParse(status, out MovieStatus movieStatus))
                {
                    model.Movies = model.Movies.Where(m => m.MovieStatus == movieStatus);
                }
            }

            return View(model);
        }
       
        public IActionResult Details(int id)
        {
            var item = unitOfWork.MovieRepository.GetMovieandCategoryById(id);
            if (item != null) { item.NumOfVisit++; unitOfWork.MovieRepository.Save(); }
            return View(item);
        }
        [HttpPost]
        public IActionResult Create(MovieVM model)
        {
            if (ModelState.IsValid) 
            {

                if (model.StartDate > DateTime.Now)
                {
                    model.MovieStatus = MovieStatus.Upcoming;
                }
                else if (model.StartDate < DateTime.Now)
                {
                    model.MovieStatus = MovieStatus.Available;
                }
                else
                {
                    model.MovieStatus = MovieStatus.Expired;
                }
                Movie movie = new Movie()
                {
                    Name = model.Name,
                    Price = model.Price,
                    Description = model.Description,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    MovieStatus = model.MovieStatus,
                    ImgUrl = model.ImgUrl,
                    CategoryId = model.CategoryId,
                    CinemaId = model.CinemaId,
                    TrailerUrl = model.TrailerUrl,

                };
                var result=CheckValidation.CheckValidation.Check(ModelState, Request, true);
                if (result != null) { return result; }
                unitOfWork.MovieRepository.Add(movie);
                unitOfWork.MovieRepository.Save();
                return RedirectToAction("Index");
            }
            else
            {
                var result = CheckValidation.CheckValidation.Check(ModelState, Request, false);
                if (result != null) { return result; }
                model.Categories = unitOfWork.CategoryRepository.GetAll().ToList();
                model.CinemaList = unitOfWork.CinemaRepository.GetAll().ToList();
                return View("Index", model);
            }
        }
        [HttpGet]
        public IActionResult Edit(int id,MovieVM model)
        {
            var movie = unitOfWork.MovieRepository.Get(e => e.Id == id).SingleOrDefault();
            if (movie == null)
            {
                return NotFound();
            }
            unitOfWork.MovieRepository.updateFromVm(movie, model, fromMtoVm: false);

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_EditMoviePartial", model);
            }

            return View("Edit", model);
        }
        [HttpPost]
        public IActionResult Edit(MovieVM model)
        {
            var movie = unitOfWork.MovieRepository.Get(e => e.Id == model.Id).SingleOrDefault();
            if (ModelState.IsValid)
            {
               unitOfWork.MovieRepository.updateFromVm(movie, model,fromMtoVm:true);
                var result = CheckValidation.CheckValidation.Check(ModelState, Request, true);
                if (result != null) { return result; }
                return RedirectToAction("Index");
            }
            else
            {
                var result = CheckValidation.CheckValidation.Check(ModelState, Request, false);
                if (result != null) { return result; }
                model.Categories = unitOfWork.CategoryRepository.GetAll().ToList();
                model.CinemaList = unitOfWork.CinemaRepository.GetAll().ToList();
                return View("Index", model);
            }

        }




        public IActionResult Search(string text)
        {

            if (string.IsNullOrEmpty(text))
            {
                return View("NotFound");
            }
            else
            {
                var movies = unitOfWork.MovieRepository.GetAll().Where(e => e.Name.Contains(text)).ToList();
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
        public IActionResult Delete(int id)
        {
            unitOfWork.MovieRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
