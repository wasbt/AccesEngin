using Mobile.Model.TableSql;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile.Data
{
    public class Database
    {


        readonly SQLiteAsyncConnection database;

        public Database(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<TableResultatExigenceModel>().Wait();
        }
      
        public List<TableResultatExigenceModel> GetItemsAsync()
        {
            var list = database.Table<TableResultatExigenceModel>().ToListAsync().Result;
            return list;
        }

        public Task<List<TableResultatExigenceModel>> GetItemsNotDoneAsync()
        {
            return database.QueryAsync<TableResultatExigenceModel>("SELECT * FROM [TableResultatExigenceModel]");
        }

        public Task<TableResultatExigenceModel> GetItemAsync(int id)
        {
            return database.Table<TableResultatExigenceModel>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(TableResultatExigenceModel item)
        {
            if (item.Id != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(TableResultatExigenceModel item)
        {
            return database.DeleteAsync(item);
        }
    }
}