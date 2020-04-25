using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeApp.Model.Interface
{
    public interface ITableRepository
    {
        IEnumerable<Table> GetTables();
        Table GetTable(int? id);
        void AddTable(Table table);
        void UpdateTable(Table table);
        void RemoveTable(Table table);
        void EmptyTable(int id);
        void Save();
    }
}
