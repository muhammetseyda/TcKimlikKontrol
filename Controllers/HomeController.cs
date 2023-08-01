using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TcKimlikKontrol.Models;
using TcKimlikKontrol.Services;

namespace TcKimlikKontrol.Controllers
{
    public class HomeController : Controller
    {
        private readonly TcKimlikDbContext _dbContext;
        private readonly NviService _nviService;

        public HomeController(TcKimlikDbContext dbContext, NviService nviService)
        {
            _dbContext = dbContext;
            _nviService = nviService;
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
                var kayitlikullanici = _dbContext.TcKimlik.FirstOrDefault(x => x.TcKimlikNo == item.TcKimlikNo);

                if (kayitlikullanici != null) 
                {
                    ViewBag.Error = "Kullanıcı zaten kayıtlı!";
                    return View(item); 
                }

                var kayit = new Kayit
                {
                    TcKimlikNo = item.TcKimlikNo,
                    Ad = item.Ad,
                    Soyad = item.Soyad,
                    DogumTarihi = item.DogumTarihi,
                    KayitTarihi = DateTime.Now
                };

                bool isValid = await _nviService.DogrulaAsync(item.TcKimlikNo, item.Ad, item.Soyad, item.DogumTarihi);

                if (isValid)
                {
                    _dbContext.TcKimlik.Add(kayit);
                    await _dbContext.SaveChangesAsync();

                    return RedirectToAction("Succes");
                }
                else
                {
                    ViewBag.Error = "Tc Kimlik Nosunu Kontrol Ediniz!";
                    return View(item);
                }
            }
            ViewBag.Error = "Bilgileri tekrardan kontrol ediniz. Tc Kimlik Nosu 11 haneden oluşmaktadır.";
            return View(item);
        }

        public IActionResult Succes()
        {
            return View();
        }
    }
}
