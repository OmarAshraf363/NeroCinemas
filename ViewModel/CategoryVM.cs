using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Nero.Models;

namespace Nero.ViewModel
{
    public class CategoryVM
    {
        [ValidateNever]
        public List<Category> Categories { get; set; }
        public int CategoryId {  get; set; }
        public string CategoryName { get; set; }=string.Empty;
    }
}
