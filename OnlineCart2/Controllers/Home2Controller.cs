using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineCart2;

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

        public ActionResult About (int id) {

            return View();
        }
    }
}