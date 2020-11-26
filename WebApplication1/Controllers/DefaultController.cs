using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class DefaultController : Controller
    {
        private indexViewModel getIndexData()
        {
            using (mobilyaEntities db = new mobilyaEntities())
            {
                var indexData = new indexViewModel();
                indexData.products= db.urunler.OrderBy(x => x.sira).ToList();
                indexData.categories= db.kategori.OrderBy(x => x.id).ToList();

                return indexData;
            }
        }

        // GET: Default
        public ActionResult Index()
        {
            return View(getIndexData());
        }

        public ActionResult Hakkimizda()
        {
            /*
            using (mobilyaEntities db = new mobilyaEntities())
            {
                var model = db.hakkimizda.Find(1);
                return View(model);
            }
            */
            return View(getIndexData());
        }

        public ActionResult Iletisim()
        {
            return View(getIndexData());
        }

        public ActionResult Urunler()
        {
            return View(getIndexData());
        }

        [Route("urun/{id}")]
        public ActionResult UrunDetay(int id)
        {
            var model = getIndexData();

            var products = new List<urunler>();

            foreach(var product in model.products)
            {
                if (product.katId == id)
                {
                    products.Add(product);
                }
            }
            model.categoryProducts = products;

            foreach(var category in model.categories)
            {
                if (category.id == id)
                {
                    ViewBag.CategoryText = category.ad;
                }
            }

            return View(model);

            using (mobilyaEntities db = new mobilyaEntities())
            {
                /*
                var model = db.urunler.Where(x => x.id == id).FirstOrDefault();
                if (model == null)
                {
                    return HttpNotFound();
                }
                */
            }
        }

        [Route("kategori")]
        public ActionResult Kategori()
        {
            using (mobilyaEntities db = new mobilyaEntities())
            {
                var model = db.kategori.OrderBy(k => k.ad).ToList();
                for (int i = 0; i < model.Count; i++)
                {
                    kategori item = model[i];
                    var katurun = db.urunler.Where(u => u.katId == item.id).ToList();
                    if (katurun == null || katurun.Count <= 0)
                    {
                        model.Remove(item);
                    }
                }
                return View(model);
            }
        }

        [Route("kategori/{id}")]
        public ActionResult KategoriUrunler(int id)
        {
            using (mobilyaEntities db = new mobilyaEntities())
            {
                var model = db.urunler.Where(x => x.aktifmi == 1 && x.katId == id).ToList();
                if (model == null || !(model.Count > 0))
                {
                    return HttpNotFound();
                }
                ViewBag.katn = "deneme";
                return View("KategoriUrunler", model);
            }
        }
        [Route("urun/partial/{id}")]
        public ActionResult partial(int id)
        {
            using (mobilyaEntities db = new mobilyaEntities())
            {
                var list = db.urunler.Where(x => x.id == id).ToList();
                if (list == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    return PartialView("partial", list.Single());
                }
            }
        }
    }
}