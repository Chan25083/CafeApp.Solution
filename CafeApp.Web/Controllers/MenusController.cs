using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CafeApp.Infrastructure;
using CafeApp.Model;

namespace CafeApp.Web.Controllers
{
    public class MenusController : Controller
    {
        private AppDbContext db = new AppDbContext();
        private MenuRepository menuRep = new MenuRepository();

        // GET: Menus
        public ActionResult ManageMenu()
        {
            if (Session["Admin"] == null) return RedirectToAction("Login", "CafeAdmin");

            var foodMenu = menuRep.GetMenus();
            if (foodMenu == null || foodMenu.ToList().Count < 1)
            {
                ViewBag.NoMenu = "No Food Menu.";
                return View();
            }
            else
            {
                return View(foodMenu);
            }
        }

        // GET: Menus/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["Admin"] == null) return RedirectToAction("Login", "CafeAdmin");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = menuRep.GetMenu(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        public ActionResult RenderImage(int id)
        {
            Menu menu = menuRep.GetMenu(id);
            byte[] bytedata = menu.Data;
            return File(bytedata, "Image/jpg");
        }

        // GET: Menus/Create
        public ActionResult Create()
        {
            if (Session["Admin"] == null) return RedirectToAction("Login", "CafeAdmin");

            return View();
        }

        // POST: Menus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MenuId,FoodName,Price,FoodType")] Menu menu, HttpPostedFileBase postedFile)
        {
            if (ModelState.IsValid)
            {
                var check = true;
                byte[] bytes;
                string fileExtension = Path.GetExtension(postedFile.FileName);
                if ((fileExtension != ".png" || fileExtension != ".jpg") && postedFile.ContentLength > 10000000)
                {
                    ViewBag.PhotoError = "The Photo must be ('.png' / '.jpg') file & maximum size: 10MB";
                    check = false;
                }

                if (check == true)
                {
                    using (BinaryReader br = new BinaryReader(postedFile.InputStream))
                    {
                        bytes = br.ReadBytes(postedFile.ContentLength);
                    }

                    menuRep.AddMenu(menu, postedFile.FileName, bytes);
                    return RedirectToAction("ManageMenu");
                }
            }

            return View(menu);
        }

        // GET: Menus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["Admin"] == null) return RedirectToAction("Login", "CafeAdmin");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = menuRep.GetMenu(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // POST: Menus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MenuId,FoodName,Price,FoodType")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                menuRep.UpdateMenu(menu);
                return RedirectToAction("ManageMenu");
            }
            return View(menu);
        }

        // GET: Menus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["Admin"] == null) return RedirectToAction("Login", "CafeAdmin");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menus.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // POST: Menus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Menu menu = menuRep.GetMenu(id);
            menuRep.RemoveMenu(menu);
            return RedirectToAction("ManageMenu");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
