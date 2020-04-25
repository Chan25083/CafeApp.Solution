using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeApp.Model
{
    public class Cart
    {
        public int CartId { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public Table Table { get; set; }
        public int TableId { get; set; }
        public Menu Menu { get; set; }
        public int MenuId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
