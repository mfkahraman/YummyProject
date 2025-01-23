using System.ComponentModel.DataAnnotations;

namespace YummyProject.Models
{
    public class Feature
    {
        public int FeatureId { get; set; }

        [Required(ErrorMessage = "Başlık gereklidir.")]
        [StringLength(100, ErrorMessage = "Başlık en fazla 100 karakter olabilir.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Açıklama gereklidir.")]
        [StringLength(500, ErrorMessage = "Açıklama en fazla 500 karakter olabilir.")]
        public string Description { get; set; }

        [Url(ErrorMessage = "Geçerli bir URL giriniz.")]
        public string VideoUrl { get; set; }

        public string ImageUrl { get; set; }
    }
}

