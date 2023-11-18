using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zero_Hunger.Auth;
using Zero_Hunger.DB;
using Zero_Hunger.Models;

namespace Zero_Hunger.Controllers
{
    public class restaurantController : Controller
    {
        // GET: restaurant
        [restAccess]
        [HttpGet]
        public ActionResult Index()
        {
            var db = new zero_hungerEntities();
            return View(db.restaurants.Find((int)Session["id"]).requests.ToList());
        }
        [restAccess]
        [HttpGet]
        public ActionResult add()
        {
            return View();
        }
        [restAccess]
        [HttpGet]
        public ActionResult clear()
        {
            Session["foodlist"] = null;
            return RedirectToAction("cart");
        }
        [restAccess]
        [HttpGet]
        public ActionResult checkout()
        {
            if (Session["foodlist"]==null)
            {
                TempData["msg"] = "The food list is empty!";
                return RedirectToAction("cart");
            }
            return View();
        }
        [restAccess]
        [HttpPost]
        public ActionResult checkout(processRequestDTO pr)
        {
            if (Session["foodlist"] == null)
            {
                TempData["msg"] = "The food list is empty!";
                return RedirectToAction("cart");
            }
            if(ModelState.IsValid)
            {
                var db = new zero_hungerEntities();
                request rq = new request()
                {
                    status = "Processing",
                    order_datetime = DateTime.Now,
                    expire_datetime = pr.expire_datetime,
                    restaurant_id = (int)Session["id"]
                };
                db.requests.Add(rq);
                foreach(var item in (List<addFoodDTO>)Session["foodlist"])
                {
                    food fd = new food()
                    {
                        type = item.type,
                        quantity = item.quantity,
                        request_id = rq.id
                    };
                    db.foods.Add(fd);
                    rq.total_quantity += item.quantity;
                }
                db.SaveChanges();
                Session["foodlist"] = null;
                TempData["msg"] = "Successfully added an reqest";
                return RedirectToAction("cart");
            }
            return View(pr);
        }
        [restAccess]
        [HttpGet]
        public ActionResult requestdetails(int id)
        {
            var db = new zero_hungerEntities();
            return View(db.requests.Find(id));
        }
        [restAccess]
        [HttpPost]
        public ActionResult add(addFoodDTO afDTO)
        {
            if(ModelState.IsValid)
            {
                List<addFoodDTO> foodList = null;
                if(Session["foodlist"]==null)
                {
                    foodList = new List<addFoodDTO>();
                }
                else
                {
                    foodList = (List<addFoodDTO>)Session["foodlist"];
                }
                foodList.Add(afDTO);
                Session["foodlist"]=foodList;
                TempData["msg"] = "New food is added to request(total food "+foodList.Count+")";
                return RedirectToAction("cart");
            }
            return View(afDTO);
        }
        [restAccess]
        [HttpGet]
        public ActionResult cart()
        {
            return View((List<addFoodDTO>)Session["foodlist"]);
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
                var user = (from u in db.restaurants
                            where
                                u.username.Equals(obj.username) &&
                                u.password.Equals(obj.password)
                            select u).SingleOrDefault();
                if (user != null)
                {
                    Session["user"] = user.username;
                    Session["id"] = user.id;
                    Session["type"] = "restaurant";
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
        [HttpGet]
        public ActionResult signup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult signup(restSignupDTO obj)
        {
            if (ModelState.IsValid)
            {
                var db = new zero_hungerEntities();
                db.restaurants.Add(convert(obj));
                db.SaveChanges();
                TempData["msg"] = "Successfully signed up";
                return RedirectToAction("login");
            }
            return View(obj);
        }
        restaurant convert(restSignupDTO obj)
        {
            return new restaurant()
            {
                name = obj.name,
                email = obj.email,
                phone = obj.phone,
                address = obj.address,
                username = obj.username,
                password = obj.password
            };
        }
    }
}