using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SecureVault.Models;

namespace SecureVault.Controllers
{
    public class VaultsController : Controller
    {
        private SecureVaultDataEntities db = new SecureVaultDataEntities();

        // GET: Vaults
        public ActionResult Index()
        {
            var vaults = db.Vaults.Include(v => v.Users);
            return View(vaults.ToList());
        }

        // GET: Vaults/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vaults vaults = db.Vaults.Find(id);
            if (vaults == null)
            {
                return HttpNotFound();
            }
            return View(vaults);
        }

        // GET: Vaults/Create
        public ActionResult Create()
        {
            ViewBag.U_ID = new SelectList(db.Users, "ID", "Email");
            return View();
        }

        // POST: Vaults/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "U_ID,ID,Name,DateCreated")] Vaults vaults)
        {
            if (ModelState.IsValid)
            {
                db.Vaults.Add(vaults);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.U_ID = new SelectList(db.Users, "ID", "Email", vaults.U_ID);
            return View(vaults);
        }

        // GET: Vaults/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vaults vaults = db.Vaults.Find(id);
            if (vaults == null)
            {
                return HttpNotFound();
            }
            ViewBag.U_ID = new SelectList(db.Users, "ID", "Email", vaults.U_ID);
            return View(vaults);
        }

        // POST: Vaults/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "U_ID,ID,Name,DateCreated")] Vaults vaults)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vaults).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.U_ID = new SelectList(db.Users, "ID", "Email", vaults.U_ID);
            return View(vaults);
        }

        // GET: Vaults/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vaults vaults = db.Vaults.Find(id);
            if (vaults == null)
            {
                return HttpNotFound();
            }
            return View(vaults);
        }

        // POST: Vaults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vaults vaults = db.Vaults.Find(id);
            db.Vaults.Remove(vaults);
            db.SaveChanges();
            return RedirectToAction("Index");
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
