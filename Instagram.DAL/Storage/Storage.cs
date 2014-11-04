using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Windows.Storage;
using Instagram.DAL.Storage.Model;

namespace Instagram.DAL.Storage
{
    public class Storage
    {
        public async Task CreateAsync<T>(T item)
        {
            StorageFile file = await ApplicationData.Current.LocalFolder.CreateFileAsync("Instagram.db", CreationCollisionOption.OpenIfExists);

            using (SQLiteConnection connection = new SQLiteConnection(file.Path))
            {
                connection.CreateTable<T>();
                connection.InsertOrReplace(item);
            }
        }

        public async Task<Account> GetActiveAccountAsync()
        {
            StorageFile file = await ApplicationData.Current.LocalFolder.CreateFileAsync("Instagram.db", CreationCollisionOption.OpenIfExists);

            using (SQLiteConnection connection = new SQLiteConnection(file.Path))
            {
                return connection.Find<Account>(e => e.IsActive == true);
            }
        }
    }
}
