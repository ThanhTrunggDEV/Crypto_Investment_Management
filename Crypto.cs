using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Windows;
using System.Windows.Forms;


namespace CryptoManagement
{
    public class Crypto
    {
         public static async Task<JToken> GetPrice(string coinName)
        {
            string apiKey = Environment.GetEnvironmentVariable("CRYPTO_API");
            string url = $"https://pro-api.coinmarketcap.com/v1/cryptocurrency/quotes/latest?symbol={coinName}&convert=VND";

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", apiKey);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync(url);
                var content = await response.Content.ReadAsStringAsync();

                var data = JObject.Parse(content)["data"][$"{coinName}"]["quote"]["VND"]["price"];
                return data;
                
            }
        }
    }
    
}
