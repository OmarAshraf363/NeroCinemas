using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nero.Models;
using Nero.Repository.IRepository;

namespace Nero.Controllers
{
    public class OrderController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<AppUser>userManager;

        public OrderController(IUnitOfWork unitOfWork, UserManager<AppUser> userManager)
        {
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
        }

        public IActionResult Index(string status,string paystatus)
        {
            var allOrrders=unitOfWork.OrderRepository.GetAll()
                .Include(e=>e.AppUser).Include(e=>e.OrderItems).ToList();
            
            if (status != null)
            {
                if(status=="1"&&paystatus==CheckValidation.CheckValidation.StaticDataSuccessPayment)
                {
                allOrrders=allOrrders.Where(e=>e.Status == int.Parse(status)&&e.PaymentStatus==paystatus).ToList();

                }
                else if (status == "1" && paystatus != CheckValidation.CheckValidation.StaticDataSuccessPayment)
                {
                    allOrrders = allOrrders.Where(e => e.Status == int.Parse(status) && e.PaymentStatus == paystatus).ToList();
                }
                else
                {

                    allOrrders = allOrrders.Where(e => e.Status == int.Parse(status)).ToList();
                }
            }
            if (User.IsInRole("Customar"))
            {
                allOrrders = allOrrders.Where(e => e.UserId == userManager.GetUserId(User)).ToList();
            }
            return View(allOrrders);
        }
        public IActionResult Delete(int id ) 
        { 
            unitOfWork.OrderRepository.Delete(id);
            unitOfWork.OrderRepository.Save();
            return RedirectToAction("Index");
        }
    }
}
