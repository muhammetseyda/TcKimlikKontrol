using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TcKimlikKontrol.Models;

namespace TcKimlikKontrol.Controllers
{
    public class HomeController : Controller
    {
        private readonly TcKimlikDbContext _dbContext;

        public HomeController(TcKimlikDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Kayit item)
        {
            if (ModelState.IsValid)
            {
                var kayit = new Kayit
                {
                    TcKimlikNo = item.TcKimlikNo,
                    Ad = item.Ad,
                    Soyad = item.Soyad,
                    DogumTarihi = item.DogumTarihi,
                    KayitTarihi = DateTime.Now
                };

                
                _dbContext.TcKimlik.Add(kayit);
                await _dbContext.SaveChangesAsync();

                return RedirectToAction("Succes");
            }

            ViewBag.Error = "Tc Kimlik Nosu 11 haneden küçük veya büyük olamaz.";
            return View(item);
        }

        public IActionResult Succes()
        {
            return View();
        }
    }
}
