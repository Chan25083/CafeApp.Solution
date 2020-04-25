using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeApp.Model
{
    public class Table
    {
        public int TableId { get; set; }
        public string TableName { get; set; }
        public TableSet TablePlace { get; set; }
        public ICollection<Cart> Cart { get; set; }
        public enum TableSet
        {
            Empty, Taked
        }
    }
}
