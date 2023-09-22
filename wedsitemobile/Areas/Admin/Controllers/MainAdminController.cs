using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Protocols;
using wedsitemobile.Models;

namespace wedsitemobile.Areas.Admin.Controllers
{
    public class MainAdminController : Controller
    {
        ShopmobileEntities db = new ShopmobileEntities();
        // GET: Admin/MainAdmin
        public ActionResult Index()
        {
            return View();
        }


        // sanpham
        public ActionResult product()
        {
            var lstProduct = db.SanPhams.ToList();
            return View(lstProduct);
        }

            // GET: Admin/MainAdmin/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(SanPham product)
        {
            try 
            {
                if (product.ImageUpload != null) 
                {
                    string fileName = Path.GetFileNameWithoutExtension(product.ImageUpload.FileName);
                    string extension = Path.GetExtension(product.ImageUpload.FileName);
                    fileName = fileName + extension;
                    product.HinhChinh = fileName;
                    product.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Asset/images/"), fileName));
                }
                db.SanPhams.Add(product);
                db.SaveChanges();

                return RedirectToAction("product");
            }
            catch (Exception) 
            {
                return RedirectToAction("Create");
            }
        }

            // GET: Admin/MainAdmin/Edit/5
        [HttpGet]
         public ActionResult Edit(string id)
         {
             var product = db.SanPhams.Where(n => n.MaSanPham == id).FirstOrDefault();
             return View(product);
         }
            
        [HttpPost]
        public ActionResult Edit(SanPham product)
        {
            if (product.ImageUpload != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(product.ImageUpload.FileName);
                string extension = Path.GetExtension(product.ImageUpload.FileName);
                fileName = fileName + extension;
                product.HinhChinh = fileName;
                product.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Asset/images/"), fileName));
            }
            db.Entry(product).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("product");
        }
            
            // GET: Admin/MainAdmin/Delete
        public ActionResult Delete(String id)
        {
            var product = db.SanPhams.Find(id);
            db.SanPhams.Remove(product);    
            db.SaveChanges();
            return RedirectToAction("product");
        }


        // user
        public ActionResult nguoidung()
        {
            var lstUser = db.Users.ToList();
            return View(lstUser);
        }

            // GET: Admin/MainAdmin/CreateUser
        [HttpGet]
        public ActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateUser(User objuser)
        {
            db.Users.Add(objuser);
            db.SaveChanges();
            return RedirectToAction("nguoidung");
        }

            // GET: Admin/MainAdmin/DeleteUser
        public ActionResult DeleteUser(int id)
        {
            var objuser = db.Users.Find(id);
            db.Users.Remove(objuser);
            db.SaveChanges();
            return RedirectToAction("nguoidung");
        }

        // GET: Admin/MainAdmin/EditUser
        [HttpGet]
        public ActionResult EditUser(int id)
        {
            var objuser = db.Users.Where(n => n.Id == id).FirstOrDefault();
            return View(objuser);
        }

        [HttpPost]
        public ActionResult EditUser(User objuser)
        {
            db.Entry(objuser).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("nguoidung");
        }
    }

}
