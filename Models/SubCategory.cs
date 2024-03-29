using ECommerce.Models;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Models;

public class SubCategory
{
    [Key]
    public int SubCategoryId { get; set; }

    public string? SubCategoryName { get; set; }
    public virtual Category Category { get; set; }
    public virtual ICollection<Product> Products { get; set; } =
        new HashSet<Product>();
}