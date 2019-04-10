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

        [HttpPost]
        public ActionResult cart (string pid, string pqty) {

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

        public ActionResult checkout () {
            return View();
        }
    }
}