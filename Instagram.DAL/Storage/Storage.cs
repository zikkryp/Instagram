﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Windows.Storage;
using Instagram.DAL.Storage.Model;
using Windows.UI.Popups;

namespace Instagram.DAL.Storage
{
    public class Storage
    {
        public async Task CreateAsync<T>(T item)
        {
            StorageFile file = await ApplicationData.Current.LocalFolder.CreateFileAsync("Instagram.db", CreationCollisionOption.OpenIfExists);

            await Task.Delay(1);

            using (SQLiteConnection connection = new SQLiteConnection(file.Path))
            {
                connection.CreateTable<T>();
                connection.InsertOrReplace(item);
            }
        }

        public async Task<Account> GetActiveAccountAsync()
        {
            StorageFile file = await ApplicationData.Current.LocalFolder.CreateFileAsync("Instagram.db", CreationCollisionOption.OpenIfExists);

            await Task.Delay(1);

            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(file.Path))
                {
                    return connection.Find<Account>(e => e.IsActive == true);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Account>> GetAllUsers()
        {
            StorageFile file = await ApplicationData.Current.LocalFolder.CreateFileAsync("Instagram.db", CreationCollisionOption.OpenIfExists);

            await Task.Delay(1);

            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(file.Path))
                {
                    return connection.Table<Account>().ToArray<Account>();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task UpdateItem<T>(T item)
        {
            StorageFile file = await ApplicationData.Current.LocalFolder.CreateFileAsync("Instagram.db", CreationCollisionOption.OpenIfExists);

            await Task.Delay(1);

            using (SQLiteConnection connection = new SQLiteConnection(file.Path))
            {
                connection.InsertOrReplace(item);
            }
        }

        public async Task<bool> DeleteItem<T>(int id)
        {
            StorageFile file = await ApplicationData.Current.LocalFolder.CreateFileAsync("Instagram.db", CreationCollisionOption.OpenIfExists);

            await Task.Delay(1);

            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(file.Path))
                {
                    connection.Delete<T>(id);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
