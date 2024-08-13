using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Nero.Models;

namespace Nero.ViewModel
{
    public class CinemaVM
    {
        [ValidateNever]
        public List<Cinema> Cinemas { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }=string.Empty;
        public string Description { get; set; } = string.Empty;
        public string CinemaLogo { get; set; } = string.Empty;
        public string Address { get; set; }= string.Empty;
    }
}
