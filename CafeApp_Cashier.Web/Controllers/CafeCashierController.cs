using CafeApp.Infrastructure;
using CafeApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CafeApp_Cashier.Web.Controllers
{
    public class CafeCashierController : Controller
    {
        private UserRepository userRep = new UserRepository();
        private MenuRepository menuRep = new MenuRepository();
        private TableRepository tableRep = new TableRepository();
        private CartRepository cartRep = new CartRepository();
        private List<TableCartList> tableCartList = new List<TableCartList>();
        private decimal subTotal { get; set; }

        // GET: CafeCashier
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
                if (user != null && user.UserRoles == CafeApp.Model.User.Roles.Cashier)
                {
                    Session["Cashier"] = "Cashier";
                    return RedirectToAction("CashierMenu");
                }
                else
                {
                    ViewBag.LoginFail = "Invalid UserName / Password.";
                }
            }
            return View();
        }

        public ActionResult CashierMenu()
        {
            if (Session["Cashier"] == null) return RedirectToAction("Login");

            foreach (var item in tableCartList)
            {
                tableCartList.Remove(item);
            }

            IdData.TableId = 0;
            var tableList = tableRep.GetTables();
            if (tableList == null || tableList.ToList().Count() < 1)
            {
                ViewBag.NoTable = "No Table";
                return View();
            }
            return View(tableList);
        }

        public ActionResult TableCart(int? id)
        {
            if (Session["Cashier"] == null) return RedirectToAction("Login");

            var checkTable = tableRep.GetTable(id);
            IdData.TableId = checkTable.TableId;

            if (checkTable.TablePlace == CafeApp.Model.Table.TableSet.Empty)
            {
                ViewBag.TableEmpty = "No Order Food";
                return View();
            }
            else
            {
                var tableCart = (from c in cartRep.GetCarts()
                                 join m in menuRep.GetMenus() on c.MenuId equals m.MenuId
                                 where c.TableId == checkTable.TableId
                                 select new { m.FoodName, m.Price, c.CartId, c.UserId, c.Quantity, c.TotalPrice }).ToList();

                this.subTotal = 0;
                foreach (var item in tableCart.ToList())
                {
                    tableCartList.Add(new TableCartList()
                    {
                        CartId = item.CartId,
                        FoodName = item.FoodName,
                        UnitPrice = item.Price,
                        Quantity = item.Quantity,
                        TotalPrice = item.TotalPrice
                    });

                    subTotal += item.TotalPrice;
                }
                ViewBag.subTotal = subTotal;
                return View(tableCartList);
            }
        }

        [HttpPost]
        public ActionResult TableCart(decimal payment)
        {
            if (payment < this.subTotal)
            {
                ViewBag.NoEnoughtPayment = "No Enought to Pay";
                return View();
            }
            else
            {
                decimal giveBackBalance = payment - this.subTotal;
                userRep.PayBalance(this.subTotal);
                cartRep.RemoveAllCart(IdData.TableId);

                Session["FindBackPayment"] = giveBackBalance;
                return RedirectToAction("FindBackPayment");
            }
        }

        public ActionResult FindBackPayment()
        {
            if (Session["Cashier"] == null) return RedirectToAction("Login");

            ViewBag.BackBalance =$"Find Back RM {Session["FindBackPayment"]}";
            return View();
        }
    }
}