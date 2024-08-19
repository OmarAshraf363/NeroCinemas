using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Nero.Models;
using Nero.Repository.IRepository;
using Stripe;
using Stripe.Checkout;


namespace Nero.Controllers
{
    [Authorize]
    public class checkoutController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IUnitOfWork unitOfWork;

        public checkoutController(UserManager<AppUser> userManager, IUnitOfWork unitOfWork)
        {
            this.userManager = userManager;
            this.unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult success()
        {

            var userId = userManager.GetUserId(User);
            var userOrder = unitOfWork.OrderRepository.Get(e => e.UserId == userId && e.Status == 0)?.FirstOrDefault();
            if (userOrder != null)
            {

                userOrder.Status = 1;
                userOrder.UpdatedAt = DateTime.Now;
                userOrder.PaymentStatus = CheckValidation.CheckValidation.StaticDataInProcessPayment;
                unitOfWork.OrderRepository.Save();
            }
            else
            {
                return View("NotFound");
            }

            ViewBag.Message = "Your payment was successful! Thank you for your purchase.";
            return View();
        }
        public IActionResult cancel()
        {


            ViewBag.Message = "Your payment was canceled. Please try again.";
            return View();
        }
        public IActionResult RejectOrder(int id)//refund
        {
            //get the order
            var order = unitOfWork.OrderRepository.Get(e => e.Id == id)?.FirstOrDefault();
            if (order != null)
            {
                if (order.Status == 1&&order.PaymentStatus!=CheckValidation.CheckValidation.StaticDataSuccessPayment)
                {
                    var service = new SessionService();
                    var session = service.Get(order.StripeChargeId);

                    var refundOptions = new RefundCreateOptions
                    {
                        PaymentIntent = session.PaymentIntentId,
                        Amount = session.AmountTotal,
                        Reason = RefundReasons.RequestedByCustomer
                    };
                    var refundService = new RefundService();
                    var refund = refundService.Create(refundOptions);
                    order.Status = 0;
                    order.PaymentStatus =CheckValidation.CheckValidation.StaticDataRefundedPayment;
                    unitOfWork.OrderRepository.Save();
                    TempData["success?"] = "The Order is Rejected Successfully";
                    return RedirectToAction("Index", "Order");
                }
                else
                {
                    TempData["success?"] = "Sorry,Can not Cancle this Order!!";
                    return RedirectToAction("Index", "Order");
                }
            }
            else
            {
                TempData["success?"] = "Faild To Reject";
                return RedirectToAction("Index", "Order");
            }

        }
        public IActionResult ConfirmOrder(int id)
        {
            var order = unitOfWork.OrderRepository.Get(e => e.Id == id, e => e.AppUser)?.FirstOrDefault();
            if (order != null) 
            {
                var user = order.AppUser.Email;
                var result=CheckValidation.CheckValidation.SendConfirmationEmail(user, order);
                if (result) {
                    TempData["success?"] = "Payment completed successfully. A confirmation email has been sent to your email address.";
                    order.PaymentStatus =CheckValidation.CheckValidation.StaticDataSuccessPayment;
                    unitOfWork.OrderRepository.Save();
                }
                else { TempData["success?"] = "Faild To Send Email"; }
                return RedirectToAction("Index", "Order");
            }
            else
            {
                return View("NotFound");
            }
        }
        
    }
}
