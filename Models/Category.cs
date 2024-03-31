using ECommerce.Models;
using System.ComponentModel.DataAnnotations;
using ECommerce.Repositories.Generic_Repository;

namespace ECommerce.Models
{
    public class Category 
    {
        [Key]
        public int CategoryId { get; set; }

        public string? CategoryName { get; set; }
        public virtual ICollection<SubCategory> SubCategories { get; set; } =
            new HashSet<SubCategory>();
    }
}