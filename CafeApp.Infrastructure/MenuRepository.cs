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
    public class MenuRepository : IMenuRepository
    {
        private AppDbContext db = new AppDbContext();
        public IEnumerable<Menu> GetMenus()
        {
            return db.Menus.ToList();
        }
        public Menu GetMenu(int? id)
        {
            return db.Menus.Find(id);
        }
        public void AddMenu(Menu menu, string postedFile, byte[] bytes)
        {
            menu.Photo = postedFile;
            menu.Data = bytes;
            db.Menus.Add(menu);
            Save();
        }
        public void UpdateMenu(Menu menu)
        {
            db.Entry(menu).State = EntityState.Modified;
            Save();
        }
        public void RemoveMenu(Menu menu)
        {
            db.Menus.Remove(menu);
            Save();
        }
        public void Save()
        {
            db.SaveChanges();
        }
    }
}
