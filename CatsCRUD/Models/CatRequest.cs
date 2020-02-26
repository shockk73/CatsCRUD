using System.ComponentModel.DataAnnotations;

namespace CatsCRUD.Models
{
    public class CatRequest
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Age { get; set; }

        public bool IsAlive { get; set; }
    }
}
