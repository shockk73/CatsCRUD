using System.ComponentModel.DataAnnotations;

namespace CatsCRUD.Models
{
    public class TokenRequest
    {
        [Required]
        public string Password { get; set; }

        [Required]
        public string Username { get; set; }
    }
}
