using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nero.Repository.IRepository;

namespace Nero.Controllers
{
    public class OrderController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public OrderController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var allOrrders=unitOfWork.OrderRepository.GetAll().Where(e=>e.Status==1).AsQueryable().Include(e=>e.AppUser).Include(e=>e.OrderItems);
            return View(allOrrders);
        }
    }
}
