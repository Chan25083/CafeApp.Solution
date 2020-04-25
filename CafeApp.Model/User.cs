using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeApp.Model
{
    public class User
    {
        public int UserId { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public decimal Balance { get; set; }
        public Roles UserRoles { get; set; }
        public ICollection<Cart> Cart { get; set; }
        public enum Roles
        {
            Customer, Admin, Cashier
        }
    }
}
