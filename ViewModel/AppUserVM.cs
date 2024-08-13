using System.ComponentModel.DataAnnotations;

namespace Nero.ViewModel
{
    public class AppUserVM
    {
        [Required]       
        
        public string Name { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword {  get; set; }
        [Required]
        public string Address {  get; set; }
    }
}
