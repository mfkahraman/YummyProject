using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YummyProject.Models
{
    public class Booking
    {
        public int BookingId { get; set; }

        [Required(ErrorMessage = "Ad alanı zorunludur.")]
        [StringLength(50, ErrorMessage = "Ad en fazla 50 karakter olabilir.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "E-posta alanı zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Telefon numarası zorunludur.")]
        [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Rezervasyon tarihi zorunludur.")]
        public DateTime BookingDate { get; set; }

        [Required(ErrorMessage = "Kişi sayısı zorunludur.")]
        [Range(1, 20, ErrorMessage = "Kişi sayısı 1 ile 20 arasında olmalıdır.")]
        public int PersonCount { get; set; }

        [StringLength(250, ErrorMessage = "Mesaj en fazla 250 karakter olabilir.")]
        public string MessageContent { get; set; }

        public bool IsApproved { get; set; }
    }
}