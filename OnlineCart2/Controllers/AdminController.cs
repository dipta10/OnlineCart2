using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineCart2.Controllers;

namespace OnlineCart2.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            ViewBag.msg = "";
            return View();
        }


        [HttpPost]
        public ActionResult Index(tbl_users usr)
        {
            OnlineCartEntitiesDB obj = new OnlineCartEntitiesDB();
            var a = obj.tbl_users.Where(l => l.uname.Equals(usr.uname) && l.upass.Equals(usr.upass)).ToList();

            if (a != null && obj.tbl_users.ToArray().Length != 0 && a.Count == 1)
            {
                Session["uname"] = usr.uname.ToString();
                return RedirectToAction("Dashboard");
            } else
            {
                ViewBag.msg = "Invalid Username or password!";
            }

            return View();
        }

        public ActionResult Dashboard()
        {
            if (Session["uname"] == null) {
                ViewBag.msg = "You're not logged in...!";
                return RedirectToAction("Index");
            }

            return View();
        }



    }
}