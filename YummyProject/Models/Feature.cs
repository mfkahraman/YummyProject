using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YummyProject.Models
{
    public class Feature
    {
        public int FeatureId { get; set; }
        [Required(ErrorMessage = "Resim Url Boş Bırakılamaz")]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "Başlık Boş Bırakılamaz")]
        [MinLength(5,ErrorMessage ="Başlık en az 5 karakter olmalıdır")]
        public string Title { get; set; }
        public string Description { get; set; }
        public string VideoUrl { get; set; }
    }
}