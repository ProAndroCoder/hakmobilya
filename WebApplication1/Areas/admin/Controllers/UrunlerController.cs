using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Controllers;
using WebApplication1.Models;


namespace WebApplication1.Areas.admin.Controllers
{
    [Authorize]
    public class UrunlerController : Controller
    {
        // GET: admin/Urunler
        public ActionResult Index()
        {
            using (mobilyaEntities db = new mobilyaEntities())
            {
                var model = db.urunler.ToList();
                return View(model);
            }

        }

        public ActionResult Yeni()
        {
            var model = new urunler();// Create urun mıodel
            //kategoileri getir
            mobilyaEntities db = new mobilyaEntities();
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (kategori kategori in db.kategori.ToList())
            {
                items.Add(new SelectListItem { Text = kategori.ad, Value = kategori.id.ToString() });
            }
            ViewBag.Kategoriler = items;
            return View("UrunForm", model);
        }

        public ActionResult Guncelle(int id)
        {
            using (mobilyaEntities db = new mobilyaEntities())
            {
                var model = db.urunler.Find(id);
                List<SelectListItem> items = new List<SelectListItem>();
                foreach (kategori kategori in db.kategori.ToList())
                {
                    items.Add(new SelectListItem { Text= kategori.ad, Value=kategori.id.ToString()});
                }
                if (model == null)
                {
                    return HttpNotFound();
                }
                ViewBag.Kategoriler = items;
                return View("UrunForm", model);
            }
        }

        public ActionResult Kaydet(urunler gelenUrun)
        {

            if (!ModelState.IsValid)
            {
                return View("UrunForm", gelenUrun);
            }

            using (mobilyaEntities db = new mobilyaEntities())
            {
                if (gelenUrun.id == 0) //yeni ürün kayıt
                {
                    if (gelenUrun.fotoFile == null)
                    {
                        ViewBag.HataFoto = "Lütfen Resim Yükleyiniz..";
                        return View("UrunForm", gelenUrun);
                    }


                    string fotoAdi = Seo.DosyaAdiDuzenle(gelenUrun.fotoFile.FileName);
                    if (fotoAdi.Length < 10)
                    {
                        gelenUrun.foto = fotoAdi;
                        db.urunler.Add(gelenUrun);
                        gelenUrun.fotoFile.SaveAs(Path.Combine(Server.MapPath("~/Content/img"), Path.GetFileName(fotoAdi)));
                        TempData["urun"] = "Ürün başarılı bir şekilde eklendi.";
                    }
                    else
                    {
                        ViewBag.HataFoto = "Resim dosyasının adı 10 karakterden uzun olamaz";
                        return View("UrunForm", gelenUrun);
                    }
                }
                else  //güncelleme
                {
                    var GuncellenecekVeri = db.urunler.Find(gelenUrun.id);
                    if (gelenUrun.fotoFile != null)
                    {
                        string fotoAdi = Seo.DosyaAdiDuzenle(gelenUrun.fotoFile.FileName);
                        gelenUrun.foto = fotoAdi;
                        gelenUrun.fotoFile.SaveAs(Path.Combine(Server.MapPath("~/Content/img"), Path.GetFileName(fotoAdi)));
                    }

                    db.Entry(GuncellenecekVeri).CurrentValues.SetValues(gelenUrun);
                    TempData["urun"] = "Ürün başarılı bir şekilde güncellendi.";
                }
                db.SaveChanges();
                return RedirectToAction("index");
            }

        }

        public ActionResult Sil(int id)
        {
            using (mobilyaEntities db = new mobilyaEntities())
            {
                var silinecekUrun = db.urunler.Find(id);
                if (silinecekUrun == null)
                {
                    return HttpNotFound();
                }

                db.urunler.Remove(silinecekUrun);
                db.SaveChanges();
                TempData["urun"] = "Ürün başarılı bir şekilde silindi.";
                return RedirectToAction("index");
            }
        }
    }
}