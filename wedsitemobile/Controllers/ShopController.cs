using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using wedsitemobile.Models;

namespace wedsitemobile.Controllers
{
    public class ShopController : Controller
    {
        ShopmobileEntities db = new ShopmobileEntities();
        // GET: Shop
        public ActionResult Index()
        {
            Class1 dbc = new Class1();
            dbc.ListSanPham = db.SanPhams.ToList();
            return View(dbc);
        }

        // GET: Shop/Details/5
        public ActionResult Details(String id)
        {
            var objdetals = db.SanPhams.Where(n => n.MaSanPham == id).FirstOrDefault();
            return View(objdetals);
        }

        // GET: Shop/Create
        public ActionResult Create()
        {
            return View();
        }
            
        // POST: Shop/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Shop/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Shop/Edit/5
        [HttpGet]
        public ActionResult Register()
        {
            {
                return View();
            }
        }

        //POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User _user)
        {
            if (ModelState.IsValid)
            {
                var check = db.Users.FirstOrDefault(s => s.Email == _user.Email);
                if (check == null)
                {
                    _user.Password = _user.Password;
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.Users.Add(_user);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Email already exists";
                    return View();
                }


            }
            return View();


        }

        //create a string MD5

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            if (ModelState.IsValid)
            {


                var f_password = password;
                var data = db.Users.Where(s => s.Email.Equals(email) && s.Password.Equals(f_password)).ToList();
                if (data.Count() > 0)
                {
                    //add session
                    Session["FullName"] = data.FirstOrDefault().FirstName + " " + data.FirstOrDefault().LastName;
                    Session["Email"] = data.FirstOrDefault().Email;
                    Session["idUser"] = data.FirstOrDefault().Id;
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Login failed";
                    return RedirectToAction("Login");
                }
            }
            return View();
        }


        //Logout
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Index");
        }
    }
}
