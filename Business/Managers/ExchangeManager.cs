using Business.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Business.Managers
{
    public class ExchangeManager
    {
        private string _serviceEndpoint;
        public ExchangeInfo exchangeInfo; 
        public ExchangeManager (string serviceEndPoint)
        {
            _serviceEndpoint = serviceEndPoint;
        }
        public ExchangeManager(string serviceEndPoint, ExchangeInfo exchangeInfo)
        {
            _serviceEndpoint = serviceEndPoint;
            this.exchangeInfo = exchangeInfo;
        }

        public static async Task<ExchangeInfo> GetRatesAsync(string serviceEndpoint) {
            string json;
            string url = serviceEndpoint;
            //@"http://data.fixer.io/api/latest?access_key=d0def2e2f997f931537d524b82434b2d&symbols=EUR,USD"

            HttpResponseMessage result;

            /*var proxy = new WebProxy()
            {
                Address = new Uri("http://10.150.150.20:8080"),
                UseDefaultCredentials = true,
            };

            var httpClientHandler = new HttpClientHandler()
            {
                Proxy = proxy,
            };

            using (HttpClient client = new HttpClient(httpClientHandler, true))*/
            using (HttpClient client = new HttpClient())
            {
                result = await client.GetAsync(url);
                result.EnsureSuccessStatusCode();

                json = await result.Content.ReadAsStringAsync();
            }

            return JsonConvert.DeserializeObject<ExchangeInfo>(json);
        }

        public async Task<decimal> ExchangeCurrencies(decimal value, string fromCurrency, string targetCurrency) {
            decimal result = 0;
            decimal baseRate = 0;

            if (value <= 0)
            {
                throw new Exception("The value must be greather than zero");
            }

            if (fromCurrency.ToUpper() == targetCurrency.ToUpper())
            {
                result = value;
            }
            else
            {
                if (exchangeInfo == null)
                {
                    exchangeInfo = await GetRatesAsync(_serviceEndpoint);
                }

                if (!exchangeInfo.Rates.ContainsKey(fromCurrency))
                {
                    throw new Exception("The from currency doesn't exists in the available currency list");
                }

                if (!exchangeInfo.Rates.ContainsKey(targetCurrency))
                {
                    throw new Exception("The target currency doesn't exists in the available currency list");
                }


                if (targetCurrency.ToUpper() == exchangeInfo.Base)
                {
                    result = exchangeInfo.Rates[fromCurrency] * value;
                }
                else if(fromCurrency.ToUpper() == exchangeInfo.Base)
                {
                    result = exchangeInfo.Rates[targetCurrency] * value;
                }
                else
                {
                    baseRate = (1 / exchangeInfo.Rates[fromCurrency]) * value; //pass to base currency

                    result = baseRate * exchangeInfo.Rates[targetCurrency]; //convert to target currency
                }
            }

            return result;
        }
    }
}
