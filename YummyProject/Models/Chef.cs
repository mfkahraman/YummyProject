using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YummyProject.Models
{
    public class Chef
    {
        public int ChefId { get; set; }

        [Required(ErrorMessage = "Ad gereklidir.")]
        [StringLength(100, ErrorMessage = "Ad en fazla 100 karakter olabilir.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Başlık gereklidir.")]
        [StringLength(100, ErrorMessage = "Başlık en fazla 100 karakter olabilir.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Açıklama gereklidir.")]
        [StringLength(500, ErrorMessage = "Açıklama en fazla 500 karakter olabilir.")]
        public string Description { get; set; }

        public string ImageUrl { get; set; }

        // Navigation property
        public virtual List<ChefSocial> ChefSocials { get; set; }
    }
}


