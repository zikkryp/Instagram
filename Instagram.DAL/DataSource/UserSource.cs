using Instagram.DAL.Model;
using Instagram.DAL.Net;
using Instagram.DAL.Storage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.UI.Popups;

namespace Instagram.DAL.DataSource
{
    public class UserSource : DataParser
    {
        private static UserSource _userSource = new UserSource();

        private User User;

        public static async Task<User> GetUserAsync()
        {
            _userSource = new UserSource();
            await _userSource.GetDataAsync();

            return _userSource.User;
        }

        private async Task GetDataAsync()
        {
            Account account = await new Storage.Storage().GetActiveAccountAsync();
            string jsonText = await new Client().JsonObjectAsync("https://api.instagram.com/v1/users/5946413?access_token=" + account.Token);

            var jsonObject = JsonObject.Parse(jsonText);

            var userObject = jsonObject["data"].GetObject();

            var counts = GetCounts(userObject["counts"].GetObject());

            User = new User(
                userObject["username"].GetString(),
                userObject["profile_picture"].GetString(),
                userObject["full_name"].GetString(),
                userObject["id"].GetString(),
                userObject["website"].GetString(),
                userObject["bio"].GetString(),
                counts);
        }
    }
}
