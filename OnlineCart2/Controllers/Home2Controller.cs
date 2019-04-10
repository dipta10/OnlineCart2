using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineCart2;
using OnlineCart2.Models;

namespace OnlineCart2.Controllers
{
    public class Home2Controller : Controller
    {
        OnlineCartEntitiesDB db = new OnlineCartEntitiesDB();
        // GET: Home2
        public ActionResult Index()
        {

            OnlineCartEntitiesDB db =new OnlineCartEntitiesDB();
            var p = db.tbl_product.ToList();

            ViewBag.p = p;
            var imgs = db.tbl_images.ToList();
            ViewBag.imgs = imgs;


            return View();
        }

        public ActionResult cat (int id) {
            var p = db.tbl_product.Where(l => l.cid == id).ToList();
            ViewBag.p = p;
            var imgs = db.tbl_images.ToList();
            ViewBag.imgs = imgs;

            return View();
        }


        public ActionResult pro(int id) {
            var p = db.tbl_product.Where(l => l.pid == id).ToList();
            ViewBag.p = p;
            var imgs = db.tbl_images.Where(l => l.pid == id).ToList();
            ViewBag.imgs = imgs;
            Console.WriteLine("this is from PRO!");

            return View();
        }

        public ActionResult About (int id) {

            return View();
        }

        public ActionResult cart (string pid, string pqty) {
            if (pid == null || pqty == null) {
                ViewBag.c = ok.c;
                return View();
            }
            if (pid == "0" && pqty == "0") {
                return View();
            }

            foreach (var item in ok.c) {
                if (item.iid == int.Parse(pid)) {
                    item.iqty += int.Parse(pqty);
                ViewBag.c = ok.c;
                return View();
                }
            }

            cartitem i = new cartitem() { iid = int.Parse(pid), iqty = int.Parse(pqty) };
            ok.c.Add(i);
            ViewBag.c = ok.c;
            return View();
        }

        [HttpPost]
        public ActionResult checkout (string total) {
            ViewBag.g = total;
            return View();
        }

        [HttpPost]
        public ActionResult remove (string pid) {
            int id = int.Parse(pid);

            var item = ok.c.SingleOrDefault(x => x.iid == id);
            if (item != null)
                ok.c.Remove(item);

            return RedirectToAction("cart");
        }


        [HttpPost]
        public ActionResult doneorder(tbl_orders tb, string total) {

            tbl_orders obj = new tbl_orders();
            obj.opname = tb.opname;
            obj.opphone = tb.opphone;
            obj.opaddress = tb.opaddress;
            obj.opsaddress = tb.opsaddress;
            obj.ostatus = 0;
            // obj.oamount = int.Parse(total);
            obj.oamount = total;
            db.tbl_orders.Add(obj);
            db.SaveChanges();

            var moid = db.tbl_orders.Select(a=>a.oid).DefaultIfEmpty(0).Max();


            var pro = from prod in ok.c
                      join od in db.tbl_product
                      on prod.iid equals od.pid
                      select new {PID = od.pid, PPRICE = od.pprice, PQTY = prod.iqty };


            foreach (var item in pro) {
                tbl_order_details orderdetails = new tbl_order_details();

                orderdetails.oid = moid;
                orderdetails.pid = item.PID;
                orderdetails.pprice = item.PPRICE;
                orderdetails.pqty = item.PQTY;
                orderdetails.pamount = item.PQTY * item.PPRICE;

                db.tbl_order_details.Add(orderdetails);
                db.SaveChanges();
            }

            ok.amt = 0;
            ok.c.Clear();

            return RedirectToAction("Index");

        }
    }
}