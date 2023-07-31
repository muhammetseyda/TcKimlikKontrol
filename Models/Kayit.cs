using System;
using System.ComponentModel.DataAnnotations;

namespace TcKimlikKontrol.Models
{
    public class Kayit
    {

        public int Id { get; set; }
        public string? Ad { get; set; }

        [Required]
        [Display(Name = "Soyad")]
        public string Soyad { get; set; }

        [Required]
        [Display(Name = "TC Kimlik No")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "TC Kimlik Numarası 11 haneli olmalıdır.")]
        public string TcKimlikNo { get; set; }

        [Required]
        [Display(Name = "Doğum Tarihi")]
        [DataType(DataType.Date)]
        public DateTime DogumTarihi { get; set; }

        [Required]
        [Display(Name = "Doğum Tarihi")]
        public DateTime KayitTarihi { get; set; }
    }
}
