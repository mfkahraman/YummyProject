using System.ComponentModel.DataAnnotations;

namespace YummyProject.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Ürün adı gereklidir.")]
        [StringLength(100, ErrorMessage = "Ürün adı en fazla 100 karakter olabilir.")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "İçerikler gereklidir.")]
        [StringLength(500, ErrorMessage = "İçerikler en fazla 500 karakter olabilir.")]
        public string Ingrdients { get; set; }

        [Required(ErrorMessage = "Fiyat gereklidir.")]
        [Range(0.01, 10000, ErrorMessage = "Fiyat 0.01 ile 10000 arasında olmalıdır.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Kategori gereklidir.")]
        public int CategoryId { get; set; }

        public string ImageUrl { get; set; }

        // Navigation property
        public virtual Category Category { get; set; }
    }
}

