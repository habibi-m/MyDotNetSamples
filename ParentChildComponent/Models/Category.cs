using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;

namespace ParentChildComponent.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }

        public int? ParentId { get; set; }
        public virtual Category Parent { get; set; }

        public List<Category> SubCategories { get; set; }
    }
}
