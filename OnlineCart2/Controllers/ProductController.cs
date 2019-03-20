using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineCart2;

namespace OnlineCart2.Controllers
{
    public class ProductController : Controller
    {
        private OnlineCartEntitiesDB db = new OnlineCartEntitiesDB();

        // GET: Product
        public ActionResult Index()
        {
            var tbl_product = db.tbl_product.Include(t => t.tbl_category);
            return View(tbl_product.ToList());
        }

        // GET: Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_product tbl_product = db.tbl_product.Find(id);
            if (tbl_product == null)
            {
                return HttpNotFound();
            }
            return View(tbl_product);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            ViewBag.cid = new SelectList(db.tbl_category, "cid", "cname");
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "pid,pname,pprice,pdetails,cid")] tbl_product tbl_product)
        {
            if (ModelState.IsValid)
            {
                db.tbl_product.Add(tbl_product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.cid = new SelectList(db.tbl_category, "cid", "cname", tbl_product.cid);
            return View(tbl_product);
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_product tbl_product = db.tbl_product.Find(id);
            if (tbl_product == null)
            {
                return HttpNotFound();
            }
            ViewBag.cid = new SelectList(db.tbl_category, "cid", "cname", tbl_product.cid);
            return View(tbl_product);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "pid,pname,pprice,pdetails,cid")] tbl_product tbl_product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cid = new SelectList(db.tbl_category, "cid", "cname", tbl_product.cid);
            return View(tbl_product);
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_product tbl_product = db.tbl_product.Find(id);
            if (tbl_product == null)
            {
                return HttpNotFound();
            }
            return View(tbl_product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_product tbl_product = db.tbl_product.Find(id);
            db.tbl_product.Remove(tbl_product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Images(int id) {
            // tbl_product tbl_product = db.tbl_product.Find(id);
            var pro = db.tbl_product.Where(l => l.pid == id).ToList();
            if (pro == null)
            {
                return HttpNotFound();
            }
            // ViewBag.cid = new SelectList(db.tbl_category, "cid", "cname", tbl_product.cid);
            ViewBag.pro = pro;

            var imgs = db.tbl_images.Where(l => l.pid == id).ToList();
            ViewBag.imgs = imgs;

            return View();
        }

        [HttpPost]
        public ActionResult Images(int id, HttpPostedFileBase file) {
            string path = System.IO.Path.Combine("~/Content/Images/" + file.FileName);
            file.SaveAs(Server.MapPath(path));

            tbl_images obj = new tbl_images();
            obj.iname = file.FileName.ToString();
            obj.pid = id;
            
            db.tbl_images.Add(obj);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
