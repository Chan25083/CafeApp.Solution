using CafeApp.Infrastructure;
using CafeApp.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CafeApp.Web.Controllers
{
    public class CafeAdminController : Controller
    {
        private UserRepository userRep = new UserRepository();

        // GET: CafeAdmin
        public ActionResult Login()
        {
            Session.Abandon();
            IdData.UserId = 0;
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                if (string.IsNullOrEmpty(username as string))
                {
                    ViewBag.UserName = "The UserName field is required.";
                }
                if (string.IsNullOrEmpty(password as string))
                {
                    ViewBag.Password = "The Password field is required.";
                }
            }
            else
            {
                var user = userRep.GetUser(username, password);
                if (user != null && user.UserRoles == Model.User.Roles.Admin)
                {
                    Session["Admin"] = "Admin";
                    return RedirectToAction("AdminMenu");
                }
                else
                {
                    ViewBag.LoginFail = "Invalid UserName / Password.";
                }
            }
            return View();
        }

        public ActionResult AdminMenu()
        {
            if (Session["Admin"] == null) return RedirectToAction("Login");
            else return View();
        }
    }
}