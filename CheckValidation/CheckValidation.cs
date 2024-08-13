using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;

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
    }
}
