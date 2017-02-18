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
    public class KeysController : Controller
    {
        private SecureVaultDataEntities db = new SecureVaultDataEntities();

        // GET: Keys
        public ActionResult Index()
        {
            var keys = db.Keys.Include(k => k.Vaults);
            return View(keys.ToList());
        }

        // GET: Keys/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Keys keys = db.Keys.Find(id);
            if (keys == null)
            {
                return HttpNotFound();
            }
            return View(keys);
        }

        // GET: Keys/Create
        public ActionResult Create()
        {
            ViewBag.V_ID = new SelectList(db.Vaults, "ID", "Name");
            return View();
        }

        // POST: Keys/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "V_ID,ID,DateCreated,Key,Value,Description,Hidden")] Keys keys)
        {
            if (ModelState.IsValid)
            {
                db.Keys.Add(keys);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.V_ID = new SelectList(db.Vaults, "ID", "Name", keys.V_ID);
            return View(keys);
        }

        // GET: Keys/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Keys keys = db.Keys.Find(id);
            if (keys == null)
            {
                return HttpNotFound();
            }
            ViewBag.V_ID = new SelectList(db.Vaults, "ID", "Name", keys.V_ID);
            return View(keys);
        }

        // POST: Keys/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "V_ID,ID,DateCreated,Key,Value,Description,Hidden")] Keys keys)
        {
            if (ModelState.IsValid)
            {
                db.Entry(keys).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.V_ID = new SelectList(db.Vaults, "ID", "Name", keys.V_ID);
            return View(keys);
        }

        // GET: Keys/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Keys keys = db.Keys.Find(id);
            if (keys == null)
            {
                return HttpNotFound();
            }
            return View(keys);
        }

        // POST: Keys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Keys keys = db.Keys.Find(id);
            db.Keys.Remove(keys);
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
