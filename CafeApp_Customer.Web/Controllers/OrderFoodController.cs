using CafeApp.Infrastructure;
using CafeApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CafeApp_Customer.Web.Controllers
{
    public class OrderFoodController : Controller
    {
        private UserRepository userRep = new UserRepository();
        private MenuRepository menuRep = new MenuRepository();
        private TableRepository tableRep = new TableRepository();
        private CartRepository cartRep = new CartRepository();
        private List<TableCartList> tableCartList = new List<TableCartList>();

        public ActionResult TableMenu()
        {
            if (Session["Customer"] == null) return RedirectToAction("Login", "CafeCustomer");

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

        public ActionResult FoodCart(int? id)
        {
            if (Session["Customer"] == null) return RedirectToAction("Login", "CafeAdmin", "CafeApp.Web");

            var checkTable = id != 0 ? tableRep.GetTable(id) : tableRep.GetTable(IdData.TableId);

            var tableCart = from c in cartRep.GetCarts()
                            join m in menuRep.GetMenus() on c.MenuId equals m.MenuId
                            where c.TableId == checkTable.TableId
                            select new { m.FoodName, m.Price, c.CartId, c.UserId, c.Quantity, c.TotalPrice };

            if (checkTable.TablePlace == CafeApp.Model.Table.TableSet.Empty || tableCart.Where(c => c.UserId == IdData.UserId) != null)
            {
                IdData.TableId = checkTable.TableId;
                if (tableCart == null || tableCart.ToList().Count() < 1)
                {
                    tableRep.EmptyTable(checkTable.TableId);
                    ViewBag.NoOrder = "No Order Food.";
                    return View();
                }
                else
                {
                    decimal subTotal = 0;
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
            else
            {
                ViewBag.TableTaked = "The Table has Taked";
                return RedirectToAction("ChooseTable", "CafeCustomer");
            }
        }

        [HttpPost]
        public ActionResult AddUnite(int cartId)
        {
            cartRep.AddMenuQuantity(cartId);
            return RedirectToAction("FoodCart");
        }

        [HttpPost]
        public ActionResult MinusUnite(int cartId)
        {
            cartRep.MinusMenuQuantity(cartId);
            return RedirectToAction("FoodCart");
        }

        [HttpPost]
        public ActionResult DeleteOrder(int cartId)
        {
            cartRep.RemoveCart(cartId);
            return RedirectToAction("FoodCart");
        }

        public ActionResult FoodMenu()
        {
            if (Session["Customer"] == null) return RedirectToAction("Login", "CafeCustomer");

            if (Session["OrderFood"] == null)
                ViewBag.AddToCart = Session["OrderFood"];
            else
                Session["OrderFood"] = null;

            var menuList = menuRep.GetMenus();

            if (menuList == null || menuList.ToList().Count() < 1)
                ViewBag.NoMenu = "No Menu";

            return View(menuList);
        }

        [HttpPost]
        public ActionResult OrderFood(int menuId)
        {
            cartRep.AddCart(IdData.UserId, menuId, IdData.TableId);
            Session["OrderFood"] = "Added to Cart";
            return RedirectToAction("FoodMenu");
        }

        public ActionResult RenderImage(int id)
        {
            Menu menu = menuRep.GetMenu(id);
            byte[] bytedata = menu.Data;
            return File(bytedata, "Image/jpg");
        }
    }
}
