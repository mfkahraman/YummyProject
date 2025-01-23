using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YummyProject.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Kategori adı gereklidir.")]
        [StringLength(100, ErrorMessage = "Kategori adı en fazla 100 karakter olabilir.")]
        public string CategoryName { get; set; }

        // Navigation property
        public virtual List<Product> Products { get; set; }
    }
}

