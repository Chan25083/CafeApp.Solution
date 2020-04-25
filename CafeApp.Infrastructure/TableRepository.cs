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
    public class TableRepository : ITableRepository
    {
        private AppDbContext db = new AppDbContext();
        public IEnumerable<Table> GetTables()
        {
            return db.Tables.ToList();
        }
        public Table GetTable(int? id)
        {
            return db.Tables.Find(id);
        }
        public void AddTable(Table table)
        {
            db.Tables.Add(table);
            Save();
        }
        public void UpdateTable(Table table)
        {
            db.Entry(table).State = EntityState.Modified;
            Save();
        }
        public void RemoveTable(Table table)
        {
            db.Tables.Remove(table);
            Save();
        }
        public void EmptyTable(int id)
        {
            Table table = GetTable(id);
            table.TablePlace = Table.TableSet.Empty;
            Save();
        }
        public void Save()
        {
            db.SaveChanges();
        }
    }
}
