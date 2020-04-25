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
    public class UserRepository : IUserRepository
    {
        private AppDbContext db = new AppDbContext();
        public IEnumerable<User> GetUsers()
        {
            return db.Users.ToList();
        }
        public User GetUser(int? id)
        {
            return db.Users.Find(id);
        }
        public User GetUser(string username,string password)
        {
            return db.Users.SingleOrDefault(u => u.Username == username && u.Password == password);
        }
        public void AddUser(User user)
        {
            db.Users.Add(user);
            Save();
        }
        public void UpdateUser(User user)
        {
            db.Entry(user).State = EntityState.Modified;
            Save();
        }
        public void RemoveUser(User user)
        {
            db.Users.Remove(user);
            Save();
        }
        public void PayBalance(decimal subTotal)
        {
            var user = this.GetUser(IdData.UserId);
            user.Balance -= subTotal;
            Save();
        }
        public void Save()
        {
            db.SaveChanges();
        }
    }
}
