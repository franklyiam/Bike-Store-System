using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HW6.Models;
using PagedList;
using HW6.ViewModels;

namespace HW6.Controllers
{
    public class HomeController : Controller
    {
        private BikeStoresEntities db = new BikeStoresEntities();

        // GET: products
        public ActionResult Index(int? page, string searchstring)
        {
            var products = db.products.Include(p => p.brand).Include(p => p.category).ToList();

            if (searchstring != null)
            {
                page = 1;
            }

            if (!String.IsNullOrEmpty(searchstring))
            {
                products = products.Where(x => x.product_name.ToUpper().Contains(searchstring.ToUpper())).ToList();
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(products.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
        
            product product = db.products.Find(id);
          
            ViewBag.brand_id = new SelectList(db.brands, "brand_id", "brand_name", product.brand_id);
            ViewBag.category_id = new SelectList(db.categories, "category_id", "category_name", product.category_id);
            return PartialView("Edit",product);
        }

        [HttpPost]
        public ActionResult Edit(product product)
        {
           
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
          
         
        }



        [HttpPost]
        public ActionResult Delete(int id)
        {
           
                product prod = db.products.Where(x => x.product_id == id).FirstOrDefault<product>();
                db.products.Remove(prod);
                db.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully", JsonRequestBehavior.AllowGet });
            
        }
        [HttpPost]
        public ActionResult Details(int idnty)
        {
           
            product product = db.products.Find(idnty);
            var stores = db.stores.Include(s => s.stocks).Select(x => new StoreVM { 
            
                store_id = x.store_id,
                store_name = x.store_name,
                count = db.stocks.Where(g => g.product_id == idnty && g.store_id == x.store_id).Sum(h => h.quantity)
            }).ToList();

            var det = new ProductDetailsVM { product = product, stores = stores };
         
            return PartialView("Details",det);
        }

        [HttpGet]
        public ActionResult Create() {

            ViewBag.brand_id = new SelectList(db.brands, "brand_id", "brand_name");
            ViewBag.category_id = new SelectList(db.categories, "category_id", "category_name");
            return PartialView("Create");
        }

        [HttpPost]
        public ActionResult Create(product product) {

            db.products.Add(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}