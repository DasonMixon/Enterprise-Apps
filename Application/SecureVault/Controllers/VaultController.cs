using SecureVault.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SecureVault.Controllers
{
    public class VaultController : Controller
    {
        private SecureVaultDataEntities db = new SecureVaultDataEntities();

        // GET: Vault
        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {
            var user = db.Users.FirstOrDefault(u => u.Email.Equals(User.Identity.Name));
            var vaults = db.Vaults.Where(u => u.U_ID == user.ID).ToList();  //.Include(v => v.Users);
            return View(vaults);
        }
    }
}