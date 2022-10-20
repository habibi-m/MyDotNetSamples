using System.ComponentModel.DataAnnotations;

namespace FileUpload.Models
{
    public class ProductImage
    {
        public long Id { get; set; }

        [Required]
        [StringLength(100)]
        public string? Name { get; set; }

        public long ProductId { get; set; }
    }
}
