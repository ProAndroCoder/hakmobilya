using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Areas.admin.Controllers
{
    public class KategoriController : Controller
    {
        // GET: admin/Kategori
        public ActionResult Index()
        {
            mobilyaEntities db = new mobilyaEntities();
            return View(db.kategori.ToList());
        }

        public ActionResult Yeni()
        {
            var model = new kategori();
            return View("KatForm", model);
        }

        public ActionResult Guncelle(int id)
        {
            using (mobilyaEntities db = new mobilyaEntities())
            {
                var model = db.kategori.Find(id);
                if (model == null)
                {
                    return HttpNotFound();
                }
                return View("KatForm", model);
            }
        }

        public ActionResult Kaydet(kategori gelenKategori)
        {

            if (!ModelState.IsValid)
            {
                return View("KatForm", gelenKategori);
            }

            using (mobilyaEntities db = new mobilyaEntities())
            {
                if (gelenKategori.id == 0) //yeni ürün kayıt
                {
                    db.kategori.Add(gelenKategori);
                    TempData["kategori"] = "Kategori başarılı bir şekilde eklendi.";
                }
                else  //güncelleme
                {
                    var GuncellenecekVeri = db.kategori.Find(gelenKategori.id);
                    db.Entry(GuncellenecekVeri).CurrentValues.SetValues(gelenKategori);
                    TempData["kategori"] = "Kategori başarılı bir şekilde güncellendi.";
                }
                db.SaveChanges();
                return RedirectToAction("index");
            }

        }

        public ActionResult Sil(int id)
        {
            using (mobilyaEntities db = new mobilyaEntities())
            {
                var silinecekKategori = db.kategori.Find(id);
                if (silinecekKategori == null)
                {
                    return HttpNotFound();
                }
                var katurun = db.urunler.Where(u => u.katId == silinecekKategori.id).ToList();
                if (katurun == null || katurun.Count > 0)
                {
                    TempData["kategoriHata"] = "İçinde ürün olan bir kategoriyi silemezmsiniz.";
                    return RedirectToAction("index");
                }
                else
                {
                    db.kategori.Remove(silinecekKategori);
                    db.SaveChanges();
                    TempData["kategori"] = "Kategori başarılı bir şekilde silindi.";
                    return RedirectToAction("index");
                }
            }
        }
    }
}