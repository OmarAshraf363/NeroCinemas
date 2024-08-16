using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Nero.Models;
using Nero.Repository.IRepository;
using System.Net.Mail;

namespace Nero.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly SignInManager<AppUser> signInManager;
        private readonly UserManager<AppUser> userManager;
        private readonly IUnitOfWork unitOfWork;

        public ShoppingCartController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IUnitOfWork unitOfWork)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var userId = userManager.GetUserId(signInManager.Context.User);
            if (userId == null)
            {
                return RedirectToAction("Login", "Account"); 
            }

            var userOrder = unitOfWork.OrderRepository
                .Get(e => e.UserId == userId && e.Status == 0)?
                                .FirstOrDefault();

            if (userOrder == null)
            {
                return View(new List<OrderItem>()); 
            }

            var orderItems = unitOfWork.OrderItemRepository
                .Get(e => e.OrderId == userOrder.Id,e=>e.Movie)?
                                .ToList();

            return View(orderItems);
        }


        public IActionResult AddToCart(int id, int quantity)
        {
            var userId = userManager.GetUserId(signInManager.Context.User);
            if (userId == null)
            {
                return RedirectToAction("Login", "Account"); 
            }

            
            var userOrder = unitOfWork.OrderRepository.Get(e => e.UserId == userId && e.Status == 0)?.SingleOrDefault();
            if (userOrder == null)
            {
                userOrder = new Order
                {
                    UserId = userId,
                    Status = 0, 
                };
                unitOfWork.OrderRepository.Add(userOrder);
                unitOfWork.OrderRepository.Save();
            }

            
            var orderItem = unitOfWork.OrderItemRepository.Get(e => e.OrderId == userOrder.Id && e.MovieId == id)?.SingleOrDefault();
            if (orderItem == null)
            {
                var movie = unitOfWork.MovieRepository.Get(e => e.Id == id)?.SingleOrDefault();
                if (movie == null)
                {
                    return View("NotFound"); 
                }

                orderItem = new OrderItem
                {
                    OrderId = userOrder.Id,
                    MovieId = id,
                    Quantity = quantity,
                    Price = (decimal)movie.Price,
                    TotalPrice = (decimal)(quantity * movie.Price),
                };
                unitOfWork.OrderItemRepository.Add(orderItem);
            }
            else
            {
                
                orderItem.Quantity += quantity;
                orderItem.TotalPrice += (quantity * orderItem.Price);
            }

            unitOfWork.OrderItemRepository.Save();

            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult UpdateQuantity(int id, int change)
        {
            var orderItem = unitOfWork.OrderItemRepository.Get(e => e.Id == id)?.SingleOrDefault();

            if (orderItem != null)
            {
                orderItem.Quantity += change;
                if (orderItem.Quantity < 1)
                {
                    orderItem.Quantity = 1;
                }

                orderItem.TotalPrice = orderItem.Quantity * orderItem.Price;
                unitOfWork.OrderItemRepository.Save();

                var grandTotal = unitOfWork.OrderItemRepository.Get(e => e.OrderId == orderItem.OrderId)?.Sum(e => e.TotalPrice);

                return Json(new
                {
                    newQuantity = orderItem.Quantity,
                    newTotalPrice = orderItem.TotalPrice.ToString("C"),
                    grandTotal = grandTotal.ToString()
                });
            }

            return BadRequest();
        }
        public IActionResult Delete(int id)
        {
            var userId = userManager.GetUserId(signInManager.Context.User);
            if (userId == null)
            {
                return RedirectToAction("Login", "Account"); 
            }

            var orderItem = unitOfWork.OrderItemRepository.Get(oi => oi.Id == id && oi.Order.UserId == userId)?
                                .FirstOrDefault();

            if (orderItem != null)
            {
                unitOfWork.OrderItemRepository.Delete(orderItem.Id);
                unitOfWork.OrderItemRepository.Save();
            }

            return RedirectToAction("Index");
        }
        public IActionResult Checkout()
        {
            var userId = userManager.GetUserId(User);
            var userOrder = unitOfWork.OrderRepository.Get(e => e.UserId == userId && e.Status == 0)?.SingleOrDefault();

            if (userOrder != null)
            {
               
                userOrder.Status = 1; 
                userOrder.UpdatedAt = DateTime.Now;
                unitOfWork.OrderRepository.Save();

                
                SendConfirmationEmail("Oa38150@gmail.com", userOrder);

                
                TempData["SuccessMessage"] = "Payment completed successfully. A confirmation email has been sent to your email address.";
            }
            else
            {
                TempData["ErrorMessage"] = "No items in the cart to checkout.";
            }

            return RedirectToAction("Index");
        }

        private void SendConfirmationEmail(string userEmail, Order order)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(userEmail);
                mail.From = new MailAddress("your-email@example.com");
                mail.Subject = "Booking Confirmation";
                mail.Body = $"Dear {order.AppUser.UserName},\n\nThank you for your booking! Your tickets have been successfully booked.\n\nOrder Details:\nOrder ID: {order.Id}\nOrder Date: {order.CreatedAt}\n\nThank you for choosing us!";
                mail.IsBodyHtml = false;

                SmtpClient smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com ",
                    Port = 587,
                    UseDefaultCredentials = false,
                    Credentials = new System.Net.NetworkCredential("xxx", "xxx"),
                    EnableSsl = true
                };
                smtp.Send(mail);
            }
            catch (Exception )
            {
                Console.WriteLine("er");
            }
        }

    }
}
    

