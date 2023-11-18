using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zero_Hunger.Auth;
using Zero_Hunger.DB;
using Zero_Hunger.Models;

namespace Zero_Hunger.Controllers
{
    public class employeeController : Controller
    {
        // GET: employee
        [HttpGet]
        [empAccess]
        public ActionResult Index()
        {
            return View();
        }
        [empAccess]
        [HttpGet]
        public ActionResult collectRequest(int id)
        {
            var db = new zero_hungerEntities();
            request rq = db.requests.Find(id);
            if(rq==null)
            {
                TempData["msg"]= "Three is no request with id " + id.ToString();
            }
            else
            {
                rq.status = "collected";
                db.requests.AddOrUpdate(rq);
                TempData["msg"] = "Request of id " + id.ToString() + " is set as collected";
                db.SaveChanges();
            }
            return RedirectToAction("requestlist");
        }
        [empAccess]
        [HttpGet]
        public ActionResult requestlist()
        {
            var db = new zero_hungerEntities();
            return View(db.employees.Find((int)Session["id"]).requests.ToList());
        }
        [empAccess]
        [HttpGet]
        public ActionResult requestdetails(int id)
        {
            var db = new zero_hungerEntities();
            return View(db.requests.Find(id));
        }
        [empAccess]
        [HttpGet]
        public ActionResult restaurantDetails(int id)
        {
            var db = new zero_hungerEntities();
            return View(db.restaurants.Find(id));
        }
        [HttpGet]
        public ActionResult login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult login(loginDTO obj)
        {
            if (ModelState.IsValid)
            {
                var db = new zero_hungerEntities();
                var user = (from u in db.employees
                            where
                                u.username.Equals(obj.username) &&
                                u.password.Equals(obj.password)
                            select u).SingleOrDefault();
                if (user != null)
                {
                    Session["user"] = user.username;
                    Session["id"] = user.id;
                    Session["type"] = "employee";
                    TempData["msg"] = "Successfully logged in";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["msg"] = "Invalid credential";
                }
            }
            return View(obj);
        }
        [HttpGet]
        public ActionResult logout()
        {
            Session.Clear();
            TempData["msg"] = "Successfully logged out";
            return RedirectToAction("login");
        }
    }
}