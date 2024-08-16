using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nero.Models;
using Nero.Repository.IRepository;
using Nero.Repository.ModelsRepository.CinemaModel;
using Nero.Repository.ModelsRepository.MovieModel;
using Nero.ViewModel;

namespace Nero.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CinemaController : Controller
    {
        private readonly IUnitOfWork unitOfWork;


        public CinemaController(IUnitOfWork unitOfWork)
        {

            this.unitOfWork = unitOfWork;
        }
        [AllowAnonymous]
        public IActionResult Index(CinemaVM model)
        {
            model.Cinemas = unitOfWork.CinemaRepository.GetAll().AsQueryable()
                .Include(e=>e.Movies)
                .ToList();
            return View(model);
        }
        [AllowAnonymous]
        public IActionResult Movies(int id)
        {
            ViewBag.name = unitOfWork.CinemaRepository.Get(e => e.Id == id)?.SingleOrDefault();
            var movies = unitOfWork.MovieRepository.Get(e => e.CinemaId == id)?.ToList();//spacifc get
            return View(movies);
        }


        [HttpPost]
        public IActionResult Create(CinemaVM model)
        {
            if (ModelState.IsValid)
            {
                Cinema cinema = new()
                {
                    Name = model.Name,
                    Address = model.Address,
                    Description = model.Description,
                    CinemaLogo = model.CinemaLogo,


                };
                var result = CheckValidation.CheckValidation.Check(ModelState, Request, true);
                if (result != null) { return result; }
                unitOfWork.CinemaRepository.Add(cinema);
                unitOfWork.CinemaRepository.Save();
                return RedirectToAction("Index");
            }
            else
            {
                var result = CheckValidation.CheckValidation.Check(ModelState, Request, false);
                if (result != null) { return result; }
                return View("Index", model);

            }

        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var cinema=unitOfWork.CinemaRepository.Get(e=>e.Id == id)?.SingleOrDefault();
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_EditCinmaPartial",cinema);
            }
            return View("Index",cinema);
        }
        [HttpPost]
        public IActionResult Edit(CinemaVM model) 
        {
            
            if (ModelState.IsValid)
            {
                var cinema=unitOfWork.CinemaRepository.Get(e=>e.Id==model.Id)?.SingleOrDefault();
                if (cinema!=null)
                {
                    cinema.Address = model.Address;
                    cinema.CinemaLogo = model.CinemaLogo;
                    cinema.Name = model.Name;
                    cinema.Description = model.Description;
                    
                }
                var result=CheckValidation.CheckValidation.Check(ModelState,Request,true);
                if (result != null) { return result; };
                unitOfWork.CinemaRepository.Update(cinema);
                unitOfWork.CinemaRepository.Save();
                return RedirectToAction("Index");
            }
            else
            {
                var result = CheckValidation.CheckValidation.Check(ModelState, Request, false);
                if (result != null) { return result; };
                return View("Index",model);   
            }
        
        }
        public IActionResult Delete(int id)
        {
            unitOfWork.CinemaRepository.Delete(id);
            unitOfWork.CinemaRepository.Save();
            return RedirectToAction("Index");
        }
    }
}
