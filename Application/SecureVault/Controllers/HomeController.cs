using SecureVault.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace SecureVault.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        /*[HttpPost]
        public ActionResult Login(string username, string password)
        {
            if (new UserManager().IsValid(username, password))
            {
                var ident = new ClaimsIdentity(
                  new[] { 
              // adding following 2 claim just for supporting default antiforgery provider
              new Claim(ClaimTypes.NameIdentifier, username),
              new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "ASP.NET Identity", "http://www.w3.org/2001/XMLSchema#string"),

              new Claim(ClaimTypes.Name,username),

              // optionally you could add roles if any
              new Claim(ClaimTypes.Role, "RoleName"),
              new Claim(ClaimTypes.Role, "AnotherRole"),

                  },
                  DefaultAuthenticationTypes.ApplicationCookie);

                HttpContext.GetOwinContext().Authentication.SignIn(
                   new AuthenticationProperties { IsPersistent = false }, ident);
                return RedirectToAction("MyAction"); // auth succeed 
            }
            // invalid username or password
            ModelState.AddModelError("", "invalid username or password");
            return View();
        }
        
        [Authorize]
        public ActionResult MySecretAction()
        {
            // all authorized users can use this method
            // we have accessed current user principal by calling also
            // HttpContext.User
            return View();
        }*/
    }
}