using System.ComponentModel.DataAnnotations;

namespace PetShopClient.Models
{
    public class RequestAccount
    {
        [Required(ErrorMessage ="this field is required")]
        public string? Email {  get; set; }
        [Required(ErrorMessage = "this field is required")]
        public string? Password { get; set; }
    }
}
