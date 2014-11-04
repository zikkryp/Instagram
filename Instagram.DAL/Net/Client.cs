using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace Instagram.DAL.Net
{
    public class Client
    {
        public async Task<string> JsonObjectAsync(string uri)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(uri);

            string jsonText = await response.Content.ReadAsStringAsync();

            return jsonText;
        }
    }
}
