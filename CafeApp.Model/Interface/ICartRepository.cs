using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeApp.Model.Interface
{
    public interface ICartRepository
    {
        IEnumerable<Cart> GetCarts();
        Cart GetCart(int cartId);
        Cart GetCart(int userId, int menuId, int tableId);
        void AddCart(int userId, int menuId, int tableId);
        void AddMenuQuantity(int cartId);
        void MinusMenuQuantity(int cartId);
        void RemoveCart(int cartId);
        void RemoveAllCart(int tableId);
        void Save();
    }
}
