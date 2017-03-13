using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SecureVault.Models;
using System.Web.Security;

namespace SecureVault.Controllers
{
    public class UsersController : Controller
    {
        private SecureVaultDataEntities db = new SecureVaultDataEntities();

        // GET: Users
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Login user)
        {
            if(ModelState.IsValid)
            {
                if(IsValid(user.Email, user.Password))
                {
                    FormsAuthentication.SetAuthCookie(user.Email, false);
                    return RedirectToAction("Index", "Home");
                }else
                {
                    ModelState.AddModelError("", "Login was incorrect.");
                }
            }
            return View(user);
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Register user)
        {
            if (ModelState.IsValid)
            {
                using (var db = new SecureVaultDataEntities())
                {
                    var sysUser = db.Users.Create();
                    sysUser.Email = user.Email;
                    sysUser.Password = user.Password;
                    sysUser.FirstName = user.FirstName;
                    sysUser.LastName = user.LastName;
                    sysUser.DateJoined = DateTime.Now;

                    db.Users.Add(sysUser);
                    db.SaveChanges();

                    return RedirectToAction("Index", "Home");
                }
            }else
            {
                ModelState.AddModelError("", "Register was incorrect");
            }
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        private bool IsValid(string email, string password)
        {
            //var crypto = new SimpleCrypto.PBKDF2();

            using (var db = new SecureVaultDataEntities())
            {
                return db.Users.Any(u => u.Email == email && u.Password == password);
            }
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Email,Password,FirstName,LastName,DateJoined,EmailVerified")] Users users)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(users);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(users);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Email,Password,FirstName,LastName,DateJoined,EmailVerified")] Users users)
        {
            if (ModelState.IsValid)
            {
                db.Entry(users).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(users);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Users users = db.Users.Find(id);
            db.Users.Remove(users);
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
