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
    public class TablesController : Controller
    {
        private AppDbContext db = new AppDbContext();
        private TableRepository tableRep = new TableRepository();

        // GET: Tables
        public ActionResult ManageTable()
        {
            if (Session["Admin"] == null) return RedirectToAction("Login", "CafeAdmin");

            var tableList = tableRep.GetTables();
            if (tableList == null || tableList.ToList().Count < 1)
            {
                ViewBag.NoTable = "No Table Create.";
                return View();
            }
            else
            {
                return View(tableList);
            }
        }

        // GET: Tables/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["Admin"] == null) return RedirectToAction("Login", "CafeAdmin");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Table table = tableRep.GetTable(id);
            if (table == null)
            {
                return HttpNotFound();
            }
            return View(table);
        }

        // GET: Tables/Create
        public ActionResult Create()
        {
            if (Session["Admin"] == null) return RedirectToAction("Login", "CafeAdmin");

            return View();
        }

        // POST: Tables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TableId,TableName,TablePlace")] Table table)
        {
            if (ModelState.IsValid)
            {
                tableRep.AddTable(table);
                return RedirectToAction("ManageTable");
            }

            return View(table);
        }

        // GET: Tables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["Admin"] == null) return RedirectToAction("Login", "CafeAdmin");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Table table = tableRep.GetTable(id);
            if (table == null)
            {
                return HttpNotFound();
            }
            return View(table);
        }

        // POST: Tables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TableId,TableName,TablePlace")] Table table)
        {
            if (ModelState.IsValid)
            {
                tableRep.UpdateTable(table);
                return RedirectToAction("ManageTable");
            }
            return View(table);
        }

        // GET: Tables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["Admin"] == null) return RedirectToAction("Login", "CafeAdmin");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Table table = tableRep.GetTable(id);
            if (table == null)
            {
                return HttpNotFound();
            }
            return View(table);
        }

        // POST: Tables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Table table = tableRep.GetTable(id);
            tableRep.RemoveTable(table);
            return RedirectToAction("ManageTable");
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
