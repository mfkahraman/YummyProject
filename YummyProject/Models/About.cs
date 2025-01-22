using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YummyProject.Models
{
    public class About
    {
        public int AboutId { get; set; }

        [Required(ErrorMessage = "1. Madde gereklidir.")]
        [StringLength(100, ErrorMessage = "1. Madde en fazla 100 karakter olabilir.")]
        public string Item1 { get; set; }

        [Required(ErrorMessage = "2. Madde gereklidir.")]
        [StringLength(100, ErrorMessage = "2. Madde en fazla 100 karakter olabilir.")]
        public string Item2 { get; set; }

        [Required(ErrorMessage = "3. Madde gereklidir.")]
        [StringLength(100, ErrorMessage = "3. Madde en fazla 100 karakter olabilir.")]
        public string Item3 { get; set; }

        [Required(ErrorMessage = "3. Madde gereklidir.")]
        [StringLength(15, ErrorMessage = "Telefon en fazla 12 karakter olabilir.")]
        [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Açıklama gereklidir.")]
        [StringLength(700, ErrorMessage = "Açıklama en fazla 500 karakter olabilir.")]
        public string Description { get; set; }

        [Url(ErrorMessage = "Geçerli bir URL giriniz.")]
        public string VideoUrl { get; set; }
        public string ImageUrl { get; set; }
        public string ImageUrl2 { get; set; }
    }
}