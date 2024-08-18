using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Nero.Models;
using Nero.Repository.IRepository;
using Stripe.Checkout;
using Stripe;
using System.Net.Mail;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

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

            //var orderItems = unitOfWork.OrderItemRepository
            //    .Get(e => e.OrderId == userOrder.Id,e=>e.Movie)?
            //                    .ToList();
            var orderItems=unitOfWork.OrderItemRepository.GetAll().Where(e=>e.OrderId==userOrder.Id).Include(e=>e.Movie).ToList();
            var spacifcOrderItems = orderItems.Select(e => new { e.Movie, e.Quantity,e.OrderId }).ToList();
            TempData["shoppingCart"] = JsonConvert.SerializeObject(spacifcOrderItems);

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

 
        public IActionResult Pay()
        {
            var items = JsonConvert.DeserializeObject<IEnumerable<OrderItem>>((string)TempData["shoppingCart"]);
            var order=unitOfWork.OrderRepository.Get(e=>e.Id==items.FirstOrDefault().OrderId)?.FirstOrDefault();
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>(),
            
                Mode = "payment",
                SuccessUrl = $"{Request.Scheme}://{Request.Host}/checkout/success",
                CancelUrl = $"{Request.Scheme}://{Request.Host}/checkout/cancel",
            };
            foreach (var model in items)
            {
                var result = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = model.Movie.Name,
                        },
                        UnitAmount = (long)model.Movie.Price * 100,
                    },
                    Quantity = model.Quantity,
                };
                options.LineItems.Add(result);
            }
            var service = new SessionService();
            var session = service.Create(options);
          
            if (order != null)
            {
                order.StripeChargeId = session.Id; // Save the session ID or charge ID as needed
                unitOfWork.OrderRepository.Save();
            }
            return Redirect(session.Url);
        }
      
       
       
    }
}
    

