using CafeApp.Infrastructure;
using CafeApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CafeApp_Customer.Web.Controllers
{
    public class CafeCustomerController : Controller
    {
        private UserRepository userRep = new UserRepository();

        // GET: CafeCustomer
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

                if (user != null && user.UserRoles == CafeApp.Model.User.Roles.Customer)
                {
                    Session["Customer"] = "Customer";
                    IdData.UserId = user.UserId;
                    return RedirectToAction("CustomerMenu");
                }
                else
                {
                    ViewBag.LoginFail = "Invalid UserName / Password.";
                }
            }
            return View();
        }

        public ActionResult CustomerMenu()
        {
            if (Session["Customer"] == null) return RedirectToAction("Login");
            return View();
        }

        public ActionResult MyProfile()
        {
            if (Session["Customer"] == null) return RedirectToAction("Login");
            return View(userRep.GetUser(IdData.UserId));
        }
    }
}
