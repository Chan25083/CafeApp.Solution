using CafeApp.Model;
using CafeApp.Model.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeApp.Infrastructure
{
    public class CartRepository : ICartRepository
    {
        private AppDbContext db = new AppDbContext();
        private MenuRepository menuRep = new MenuRepository();
        private TableRepository tableRep = new TableRepository();
        public IEnumerable<Cart> GetCarts()
        {
            return db.Carts.ToList();
        }
        public Cart GetCart(int cartId)
        {
            return db.Carts.Find(cartId);
        }
        public Cart GetCart(int userId, int menuId, int tableId)
        {
            return db.Carts.SingleOrDefault(c => c.UserId == userId && c.MenuId == menuId && c.TableId == tableId);
        }
        public void AddCart(int userId, int menuId, int tableId)
        {
            Cart cart = new Cart();
            Menu menu = menuRep.GetMenu(menuId);
            Table table = tableRep.GetTable(tableId);

            cart.UserId = userId;
            cart.MenuId = menuId;
            cart.TableId = tableId;
            cart.Quantity = 1;
            cart.TotalPrice = menu.Price * cart.Quantity;
            table.TablePlace = Table.TableSet.Taked;

            db.Carts.Add(cart);
            Save();
        }
        public void AddMenuQuantity(int cartId)
        {
            var cart = this.GetCart(cartId);
            cart.Quantity += 1;
            Save();
        }
        public void MinusMenuQuantity(int cartId)
        {
            var cart = this.GetCart(cartId);
            cart.Quantity -= 1;
            if (cart.Quantity < 1) cart.Quantity = 1;
            Save();
        }
        public void RemoveCart(int cartId)
        {
            var cart = this.GetCart(cartId);
            db.Carts.Remove(cart);
            Save();
        }
        public void RemoveAllCart(int tableId)
        {
            var cart = db.Carts.Where(c => c.TableId == tableId).ToList();
            foreach (var item in cart)
            {
                db.Carts.Remove(item);
            }

            tableRep.EmptyTable(tableId);
        }
        public void Save()
        {
            db.SaveChanges();
        }
    }
}
