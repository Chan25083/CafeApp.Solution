using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeApp.Model.Interface
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();
        User GetUser(int? id);
        User GetUser(string username, string password);
        void AddUser(User user);
        void UpdateUser(User user);
        void RemoveUser(User user);
        void PayBalance(decimal subTotal);
        void Save();
    }
}
