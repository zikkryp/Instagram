using Instagram.DAL.Storage;
using Instagram.DAL.Storage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Authentication.Web;

namespace Instagram.UI
{
    public class Authentication
    {
        public async Task<Account> AuthenticateAsync()
        {
            var result = await WebAuthenticationBroker.AuthenticateAsync(WebAuthenticationOptions.None,
                new Uri("https://api.instagram.com/oauth/authorize/?client_id=039d35ec6aef455592c2e073e70b2e70&scope=likes&redirect_uri=http://krypapp.com/&response_type=token"),
                new Uri("http://krypapp.com/"));

            if (result.ResponseStatus == WebAuthenticationStatus.Success)
            {
                string tokenUri = result.ResponseData;

                string token = tokenUri.Split('=')[1].Split('&')[0];
                int id = int.Parse(token.Split('.')[0]);

                Account account = new Account(id, token);

                await new Storage().CreateAsync<Account>(account);

                return account;
            }

            return null;
        }
    }
}
