using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nero.CheckValidation;
using Nero.Models;
using Nero.Repository.IRepository;
using Nero.Repository.ModelsRepository.CategoryModel;
using Nero.Repository.ModelsRepository.MovieModel;
using Nero.ViewModel;

namespace Nero.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork unitOfWork;


        public CategoryController(IUnitOfWork unitOfWork)
        {

            this.unitOfWork = unitOfWork;
        }

        public IActionResult Index(CategoryVM model)
        {
            model.Categories = unitOfWork.CategoryRepository.GetAll().ToList();
            return View(model);
        }
        public IActionResult CategoryMovies(int id)
        {
            ViewBag.catName = unitOfWork.CategoryRepository.Get(e => e.Id == id)?.SingleOrDefault().Name;
            var movies = unitOfWork.MovieRepository.Get(e => e.CategoryId == id);
            return View(movies);
        }
        [HttpPost]
        public IActionResult Create([Bind("CategoryName")] CategoryVM model)
        {
            if (ModelState.IsValid)
            {
                Category category = new()
                {
                    Name = model.CategoryName
                };
                var result = CheckValidation.CheckValidation.Check(ModelState, Request, true);
                if (result != null) { return result; }
                unitOfWork.CategoryRepository.Add(category);
                unitOfWork.CategoryRepository.Save();
                return RedirectToAction("Index");
            }
            else
            {
                var result = CheckValidation.CheckValidation.Check(ModelState, Request, false);
                if (result != null) { return result; }
                return View("Index", model);

            }

        }
        public IActionResult Edit(CategoryVM model)
        {
            var category = unitOfWork.CategoryRepository.Get(e => e.Id == model.CategoryId)?.SingleOrDefault();
            if (category != null)
            {
                if (ModelState.IsValid)
                {
                    category.Name = model.CategoryName;
                    unitOfWork.CategoryRepository.Update(category);
                    unitOfWork.CategoryRepository.Save();
                    var result=CheckValidation.CheckValidation.Check(ModelState,Request,true);
                    if (result != null) { return result; }
                    return RedirectToAction("Index");

                }
                else
                {
                    var result = CheckValidation.CheckValidation.Check(ModelState, Request, false);
                    if (result != null) { return result; }
                    return View("Index",model);
                }
            }
            return View("NotFound");
        }
        public IActionResult Delete(int id)
        {
            unitOfWork.CategoryRepository.Delete(id);
            unitOfWork.CategoryRepository.Save();
            return RedirectToAction("Index");
        }


    }
}
