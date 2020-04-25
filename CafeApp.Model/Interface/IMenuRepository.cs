using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeApp.Model.Interface
{
    public interface IMenuRepository
    {
        IEnumerable<Menu> GetMenus();
        Menu GetMenu(int? id);
        void AddMenu(Menu menu, string postedFile, byte[] bytes);
        void UpdateMenu(Menu menu);
        void RemoveMenu(Menu menu);
        void Save();
    }
}
