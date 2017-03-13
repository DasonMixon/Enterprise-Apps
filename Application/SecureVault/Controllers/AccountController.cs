using SecureVault.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SecureVault.Controllers
{
    public class AccountController : Controller
    {

        private SecureVaultDataEntities db = new SecureVaultDataEntities();

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Login user)
        {
            if (ModelState.IsValid)
            {
                if (IsValid(user.Email, user.Password))
                {
                    FormsAuthentication.SetAuthCookie(user.Email, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("Login", "Incorrect login credentials.");
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
                    if (db.Users.Any(u => u.Email == user.Email))
                    {
                        ModelState.AddModelError("Register", "A user with these credentials already exists.");
                        return View();
                    }

                    var sysUser = db.Users.Create();
                    sysUser.Email = user.Email;
                    sysUser.Password = Hash(user.Password);
                    sysUser.FirstName = user.FirstName;
                    sysUser.LastName = user.LastName;
                    sysUser.DateJoined = DateTime.Now;

                    db.Users.Add(sysUser);
                    db.SaveChanges();

                    Models.Login login = new Models.Login();
                    login.Email = sysUser.Email;
                    login.Password = sysUser.Password;

                    Login(login);

                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ModelState.AddModelError("Register", "Please make sure to fill out all of the fields.");
            }
            return View();
        }

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        private string Hash(string value)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            Byte[] originalBytes = ASCIIEncoding.Default.GetBytes(value);
            Byte[] encodedBytes = md5.ComputeHash(originalBytes);

            return BitConverter.ToString(encodedBytes);
        }

        private bool IsValid(string email, string password)
        {
            //var crypto = new SimpleCrypto.PBKDF2();

            password = Hash(password);

            using (var db = new SecureVaultDataEntities())
            {
                return db.Users.Any(u => u.Email == email && u.Password == password);
            }
        }

        [HttpGet]
        [Authorize]
        public ActionResult Edit()
        {
            string email = User.Identity.Name;

            // Fetch the userprofile
            var user = db.Users.FirstOrDefault(u => u.Email.Equals(email));

            // Construct the viewmodel
            Users model = new Users();
            model.FirstName = user.FirstName;
            model.LastName = user.LastName;
            model.Email = user.Email;
            model.Password = user.Password;

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Edit(Users user)
        {
            if (ModelState.IsValid)
            {
                string email = User.Identity.Name;

                // Fetch the userprofile
                var dbuser = db.Users.FirstOrDefault(u => u.Email.Equals(email));

                dbuser.FirstName = user.FirstName;
                dbuser.LastName = user.LastName;
                dbuser.Email = user.Email;
                dbuser.Password = user.Password;

                db.Entry(dbuser).State = EntityState.Modified;

                db.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Please make sure to fill out all of the fields.");
            }
            return View(user);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Delete()
        {
            FormsAuthentication.SignOut();

            if (ModelState.IsValid)
            {
                string email = User.Identity.Name;

                // Fetch the userprofile
                var dbuser = db.Users.FirstOrDefault(u => u.Email.Equals(email));

                db.Entry(dbuser).State = EntityState.Deleted;

                db.SaveChanges();
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize]
        public ActionResult Vaults()
        {
            var user = db.Users.FirstOrDefault(u => u.Email.Equals(User.Identity.Name));
            var vaults = db.Vaults.Where(u => u.U_ID == user.ID).ToList();  //.Include(v => v.Users);
            return View(vaults);
        }

        [HttpGet]
        [Authorize]
        public ActionResult VaultDetails(int? id)
        {
            var user = db.Users.FirstOrDefault(u => u.Email.Equals(User.Identity.Name));
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vaults vaults = db.Vaults.Find(id);
            if (vaults == null || vaults.U_ID != user.ID)
            {
                return HttpNotFound();
            }
            return View(vaults);
        }

        [HttpPost]
        [Authorize]
        public ActionResult DeleteVault(int id)
        {
            if (ModelState.IsValid)
            {
                var dbvault = db.Vaults.FirstOrDefault(v => v.ID == id);

                db.Entry(dbvault).State = EntityState.Deleted;

                db.SaveChanges();
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [Authorize]
        public ActionResult Vaults(Vaults vault)
        {
            if (ModelState.IsValid)
            {
                string email = User.Identity.Name;

                // Fetch the userprofile
                var dbuser = db.Users.FirstOrDefault(u => u.Email.Equals(email));

                vault.U_ID = dbuser.ID;

                vault.DateCreated = DateTime.Now;

                vault.Users = dbuser;

                db.Vaults.Add(vault);

                db.SaveChanges();
            }
            else
            {
                ModelState.AddModelError("", "Please make sure to fill out all of the fields.");
            }
            return RedirectToAction("Vaults", "Account");
        }

        [HttpPost]
        [Authorize]
        public ActionResult EditVaults(Vaults vault)
        {
            if (ModelState.IsValid)
            {
                string email = User.Identity.Name;

                // Fetch the userprofile
                var dbvault = db.Vaults.FirstOrDefault(v => v.ID == vault.ID);

                dbvault.Name = vault.Name;

                db.Entry(dbvault).State = EntityState.Modified;

                db.SaveChanges();
            }
            else
            {
                ModelState.AddModelError("", "Please make sure to fill out all of the fields.");
            }
            return RedirectToAction("Vaults", "Account");
        }

        [HttpGet]
        [Authorize]
        public ActionResult Settings()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Settings(int temp)
        {

            return View();
        }
    }
}