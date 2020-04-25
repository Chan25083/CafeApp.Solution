using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeApp.Model
{
    public class Menu
    {
        public int MenuId { get; set; }
        [Required]
        public string FoodName { get; set; }
        [Required]
        public decimal Price { get; set; }
        public Type FoodType { get; set; }
        public string Photo { get; set; }
        public byte[] Data { get; set; }
        public ICollection<Cart> Cart { get; set; }
        public enum Type
        {
            Rice, Drink, Noodle, WesternFood, Dessert
        }
    }
}
