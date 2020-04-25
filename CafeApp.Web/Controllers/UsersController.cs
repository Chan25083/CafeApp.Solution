using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CafeApp.Infrastructure;
using CafeApp.Model;

namespace CafeApp.Web.Controllers
{
    public class UsersController : Controller
    {
        private AppDbContext db = new AppDbContext();
        private UserRepository userRep = new UserRepository();

        // GET: Users
        public ActionResult ManageUser()
        {
            if (Session["Admin"] == null) return RedirectToAction("Login","CafeAdmin");

            return View(userRep.GetUsers());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["Admin"] == null) return RedirectToAction("Login", "CafeAdmin");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = userRep.GetUser(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            if (Session["Admin"] == null) return RedirectToAction("Login", "CafeAdmin");

            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,Username,Password,Balance,UserRoles")] User user)
        {
            if (ModelState.IsValid)
            {
                userRep.AddUser(user);
                return RedirectToAction("ManageUser");
            }

            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["Admin"] == null) return RedirectToAction("Login", "CafeAdmin");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = userRep.GetUser(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,Username,Password,Balance,UserRoles")] User user)
        {
            if (ModelState.IsValid)
            {
                userRep.UpdateUser(user);
                return RedirectToAction("ManageUser");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["Admin"] == null) return RedirectToAction("Login", "CafeAdmin");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = userRep.GetUser(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = userRep.GetUser(id);
            userRep.RemoveUser(user);
            return RedirectToAction("ManageUser");
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
