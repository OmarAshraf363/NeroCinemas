using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using Nero.ViewModel;
using Nero.Models;
using System.Net.Mail;

namespace Nero.CheckValidation
{
    public static class CheckValidation
    {
        public static bool SendConfirmationEmail(string userEmail, Order order)
        {
           
            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(userEmail);
                mail.From = new MailAddress("oa38150@gmail.com");
                mail.Subject = "Booking Confirmation";
                mail.Body = $"Dear {order.AppUser.UserName},\n\nThank you for your booking! Your tickets have been successfully booked.\n\nOrder Details:\nOrder ID: {order.Id}\nOrder Date: {order.CreatedAt}\n\nThank you for choosing us!";
                mail.IsBodyHtml = false;

                SmtpClient smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com ",
                    Port = 587,
                    UseDefaultCredentials = false,
                    Credentials = new System.Net.NetworkCredential("oa38150@gmail.com", "deeo bpmm pmty pzcf"),
                    EnableSsl = true,

                };


                smtp.Send(mail);

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public static JsonResult Check(ModelStateDictionary modelState, HttpRequest request, bool state)
        {

            if (request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                if (state != true)
                {

                    var nameErrors = modelState
                        .Where(e => e.Value.Errors.Count > 0)
                        .ToDictionary(k => k.Key, v => v.Value.Errors.Select(e => e.ErrorMessage).ToList());

                    return new JsonResult(new { isvalid = false, nameErrors });
                }
                else
                {
                    return new JsonResult(new { isvalid = true });

                }
            }
            return null!;




        }
        public static void CheckStatus(MovieVM model)
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
        }
       public static Movie Test(MovieVM model)
        {
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
            return movie;
        } 
    }
}
