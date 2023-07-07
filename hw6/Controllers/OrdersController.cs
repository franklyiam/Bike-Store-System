using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HW6.Models;
using HW6.ViewModels;
using PagedList;

namespace HW6.Controllers
{
    public class OrdersController : Controller
    {
        private BikeStoresEntities db = new BikeStoresEntities();

        // GET: Orders
        public ActionResult Index(int? page, string searchstring)
        {
            var orders = db.orders.AsEnumerable().Select(x => new OrderVM
            {
                order_id = x.order_id,
                order_date = x.order_date.ToString("yyyy-MM-dd"),
                products = db.order_items.Include(o => o.product).AsEnumerable().Select(y => new ProductVM
                {

                    product_id = y.product_id,
                    product_name = y.product.product_name,
                    list_price = y.list_price,
                    quantity = y.quantity,
                    order_id = y.order_id

                }).Where(w => w.order_id == x.order_id).ToList(),

                total=0


            }).ToList();

            if (searchstring != null)
            {
                page = 1;
            }

            if (!String.IsNullOrEmpty(searchstring))
            {
                orders = orders.Where(x => x.order_date.Contains(searchstring)).ToList();
            }
     
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(orders.ToPagedList(pageNumber, pageSize));
        }

        //report
        public ActionResult Report() {


            List<ReportVM> repo = new List<ReportVM> { 
                new ReportVM(db.order_items.Include(o => o.order).Where(k => k.order.order_date.Month == 1 && k.product.category_id == 6).Sum(y => y.quantity),"January"),
                new ReportVM(db.order_items.Include(o => o.order).Where(k => k.order.order_date.Month == 2 && k.product.category_id == 6).Sum(y => y.quantity),"February"),
                new ReportVM(db.order_items.Include(o => o.order).Where(k => k.order.order_date.Month == 3 && k.product.category_id == 6).Sum(y => y.quantity),"March"),
                new ReportVM(db.order_items.Include(o => o.order).Where(k => k.order.order_date.Month == 4 && k.product.category_id == 6).Sum(y => y.quantity),"April"),
                new ReportVM(db.order_items.Include(o => o.order).Where(k => k.order.order_date.Month == 5 && k.product.category_id == 6).Sum(y => y.quantity),"May"),
                new ReportVM(db.order_items.Include(o => o.order).Where(k => k.order.order_date.Month == 6 && k.product.category_id == 6).Sum(y => y.quantity),"June"),
                new ReportVM(db.order_items.Include(o => o.order).Where(k => k.order.order_date.Month == 7 && k.product.category_id == 6).Sum(y => y.quantity),"July"),
                new ReportVM(db.order_items.Include(o => o.order).Where(k => k.order.order_date.Month == 8 && k.product.category_id == 6).Sum(y => y.quantity),"August"),
                new ReportVM(db.order_items.Include(o => o.order).Where(k => k.order.order_date.Month == 9 && k.product.category_id == 6).Sum(y => y.quantity),"Sepetember"),
                new ReportVM(db.order_items.Include(o => o.order).Where(k => k.order.order_date.Month == 10 && k.product.category_id == 6).Sum(y => y.quantity),"Octomer"),
                new ReportVM(db.order_items.Include(o => o.order).Where(k => k.order.order_date.Month == 11 && k.product.category_id == 6).Sum(y => y.quantity),"November"),
                new ReportVM(db.order_items.Include(o => o.order).Where(k => k.order.order_date.Month == 12 && k.product.category_id == 6).Sum(y => y.quantity),"December")
            };

            var m = db.order_items.Include(o => o.order).Where(k => k.order.order_date.Month == 1 && k.product.category_id == 6).Sum(y => y.quantity);
            //int m = db.orders.Where(x => x.order_date.Month == 1 && x.).Sum(v => v.order_items.Sum(oi => oi.quantity));

            return View(repo);
        
        }
 

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            order_items order_items = db.order_items.Find(id);
            if (order_items == null)
            {
                return HttpNotFound();
            }
            return View(order_items);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.product_id = new SelectList(db.products, "product_id", "product_name");
            ViewBag.order_id = new SelectList(db.orders, "order_id", "order_id");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "order_id,item_id,product_id,quantity,list_price,discount")] order_items order_items)
        {
            if (ModelState.IsValid)
            {
                db.order_items.Add(order_items);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.product_id = new SelectList(db.products, "product_id", "product_name", order_items.product_id);
            ViewBag.order_id = new SelectList(db.orders, "order_id", "order_id", order_items.order_id);
            return View(order_items);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            order_items order_items = db.order_items.Find(id);
            if (order_items == null)
            {
                return HttpNotFound();
            }
            ViewBag.product_id = new SelectList(db.products, "product_id", "product_name", order_items.product_id);
            ViewBag.order_id = new SelectList(db.orders, "order_id", "order_id", order_items.order_id);
            return View(order_items);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "order_id,item_id,product_id,quantity,list_price,discount")] order_items order_items)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order_items).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.product_id = new SelectList(db.products, "product_id", "product_name", order_items.product_id);
            ViewBag.order_id = new SelectList(db.orders, "order_id", "order_id", order_items.order_id);
            return View(order_items);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            order_items order_items = db.order_items.Find(id);
            if (order_items == null)
            {
                return HttpNotFound();
            }
            return View(order_items);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            order_items order_items = db.order_items.Find(id);
            db.order_items.Remove(order_items);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
