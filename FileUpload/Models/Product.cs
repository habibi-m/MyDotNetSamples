using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;

namespace FileUpload.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public int Stock { get; set; }

        public DateTime RegisterDate { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }

        [StringLength(100)]
        public string? Thumb { get; set; }

        public ICollection<ProductImage>? Images { get; set; }
    }
}
