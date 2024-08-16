using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using Nero.ViewModel;
using Nero.Models;

namespace Nero.CheckValidation
{
    public static class CheckValidation
    {
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
