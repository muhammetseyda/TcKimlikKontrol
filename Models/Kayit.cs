using System;
using System.ComponentModel.DataAnnotations;

namespace TcKimlikKontrol.Models
{
    public class Kayit
    {

        public int Id { get; set; }
        public string Ad { get; set; }

        public string Soyad { get; set; }

        public string TcKimlikNo { get; set; }

        public DateTime DogumTarihi { get; set; }

        public DateTime KayitTarihi { get; set; }
    }
}
