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
    public class adminController : Controller
    {
        // GET: employee
        [adminAccess]
        public ActionResult Index()
        {
            return View();
        }
        [adminAccess]
        public ActionResult employeelist()
        {
            var db = new zero_hungerEntities();
            return View(db.employees.ToList());
        }
        [adminAccess]
        [HttpGet]
        public ActionResult restaurantDetails(int id)
        {
            var db = new zero_hungerEntities();
            return View(db.restaurants.Find(id));
        }
        [adminAccess]
        public ActionResult requestlist()
        {
            var db = new zero_hungerEntities();
            return View(db.requests.ToList());
        }
        [adminAccess]
        [HttpGet]
        public ActionResult requestdetails(int id)
        {
            var db = new zero_hungerEntities();
            ViewBag.empList=db.employees.ToList();
            return View(db.requests.Find(id));
        }
        [adminAccess]
        [HttpPost]
        public ActionResult requestdetails(int id, assignEmpDTO obj)
        {
            var db = new zero_hungerEntities();
            if (ModelState.IsValid)
            {
                employee emp = db.employees.Find(obj.emp_id);
                if (emp == null)
                {
                    TempData["msg"] = "Employee id does not exist";
                }
                else
                {
                    request rq = db.requests.Find(id);
                    rq.employee_id = obj.emp_id;
                    rq.admin_id = (int)Session["id"];
                    db.requests.AddOrUpdate(rq);
                    db.SaveChanges();
                    TempData["msg"] = emp.username+"has been assigned for request id "+id;
                    return RedirectToAction("requestlist");
                }
            }
            return View(db.requests.Find(id));
        }
        [adminAccess]
        public ActionResult deleteEmployee(int id)
        {
            var db = new zero_hungerEntities();
            db.employees.Remove(db.employees.Find(id));
            db.SaveChanges(); 
            TempData["msg"] = "Employee of ID " + id + " has been deleted";
            return RedirectToAction("employeelist");
        }
        [HttpPost]
        public ActionResult login(loginDTO obj)
        {
            if(ModelState.IsValid)
            {
                var db = new zero_hungerEntities();
                var user = (from u in db.admins
                            where 
                                u.username.Equals(obj.username) &&
                                u.password.Equals(obj.password)
                            select u).SingleOrDefault();
                if (user != null)
                {
                    Session["user"] = user.username;
                    Session["id"] = user.id;
                    Session["type"] = "admin";
                    TempData["msg"] = "Successfully login";
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
        public ActionResult login()
        {
            return View();
        }
        public ActionResult logout()
        {
            Session.Clear();
            return RedirectToAction("login");
        }
        [HttpGet]
        [adminAccess]
        public ActionResult addemployee() 
        {
            return View();
        }
        [HttpPost]
        [adminAccess]
        public ActionResult addemployee(addemployeeDTO empModel) 
        {
            if(ModelState.IsValid)
            {
                var db = new zero_hungerEntities();
                db.employees.Add(convert(empModel));
                db.SaveChanges();
                TempData["msg"] = "New employee is added successfully";
                return RedirectToAction("employeelist");
            }
            return View(empModel);
        }
        [HttpGet]
        [adminAccess]
        public ActionResult editEmployee(int id) 
        {
            var db = new zero_hungerEntities();
            employee emp = db.employees.Find(id);
            editEmployeeDTO empDTO = new editEmployeeDTO()
            {
                id = emp.id,
                username = emp.username,
                name = emp.name,
                email = emp.email,
                phone = emp.phone,
                address = emp.address,
                dob = emp.dob
            };
            return View(empDTO);
        }
        [HttpPost]
        [adminAccess]
        public ActionResult editEmployee(editEmployeeDTO empModel) 
        {
            if(ModelState.IsValid)
            {
                var db = new zero_hungerEntities();
                employee emp = db.employees.Find(empModel.id);

                emp.username = empModel.username;
                emp.name = empModel.name;
                emp.email = empModel.email;
                emp.phone = empModel.phone;
                emp.address = empModel.address;
                emp.dob = empModel.dob;

                db.employees.AddOrUpdate(emp);
                db.SaveChanges();
                TempData["msg"] = "Information of employee of ID " + empModel.id.ToString() + " is edited successfully";
                return RedirectToAction("employeelist");
            }
            return View(empModel);
        }
        employee convert(addemployeeDTO empDTO)
        {
            employee emp = new employee()
            {
                name = empDTO.name,
                email = empDTO.email,
                phone = empDTO.phone,
                address = empDTO.address,
                dob = empDTO.dob,
                username = empDTO.username,
                password = empDTO.password,
                admin_id = (int?)Session["id"]
            };
            return emp;
        }
    }
}