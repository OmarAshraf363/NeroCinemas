using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Nero.Models;

namespace Nero.ViewModel
{
    public class MovieVM
    {
     
        public int Id { get; set; } 

        public IEnumerable<Movie> Movies { get; set; }=new List<Movie>();
        public string Name { get; set; }=string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Price { get; set; }
        public string ImgUrl { get; set; } = string.Empty;
        public string TrailerUrl { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public MovieStatus MovieStatus { get; set; }
        public int CinemaId { get; set; }
        public int CategoryId {  get; set; }
        [ValidateNever]
        public List<Category> Categories { get; set; }= new List<Category>();
        [ValidateNever]
        public List<Cinema> CinemaList { get; set;}=new List<Cinema>();
    }
}
