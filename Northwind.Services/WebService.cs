using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Northwind.Entities;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Northwind.Services
{
    public class WebService
    {
        // Api Endpoint URL
        protected readonly string url = "https://openexchangerates.org/api/latest.json?app_id=65acab209bf94627b27717b6b5d13e62";
        // Variable for storing the endpoint response
        protected Task<HttpResponseMessage> httpResponse;

        /// <summary>
        /// Initializes a new instance of the <see cref="WebService"/> class.
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        public WebService()
        {
            // Check if the Api endpoint is reachable
            try
            {
                HttpClient client = new HttpClient();

                // Get response from the url host
                httpResponse = client.GetAsync(url);

                client.Dispose();
            }
            // Catch any exception and throw new argument exception
            catch(Exception e)
            {
                throw new ArgumentException(e.Message);
            }
        }

        /// <summary>
        /// Returns a list of the current exchange rates
        /// </summary>
        /// <returns></returns>
        public virtual async Task<List<ExchangeRate>> GetRates()
        {
            // Get the JSON data from the endpoint
            string json = await CallWebApi(url);

            // Get the rates from the JSON data by parsing the JSON rates array into a string
            string parsedRates = JObject.Parse(json)["rates"].ToString();

            // Deserialize the parsed string into a dictionary
            Dictionary<string, double> dict = JsonConvert.DeserializeObject<Dictionary<string, double>>(parsedRates);

            // Create a List for storing the exchange rates
            List<ExchangeRate> rates = new List<ExchangeRate>();

            // Add all the rates from the Dictionary to the List
            foreach(KeyValuePair<string, double> dictItem in dict)
            {
                ExchangeRate exchangeRate = new ExchangeRate(dictItem.Key.ToString(), Convert.ToDouble(dictItem.Value.ToString()));
                rates.Add(exchangeRate);
            }

            // Return the List
            return rates;
        }

        /// <summary>
        /// Calls the Api endpoint, and returns a string with the retrieved data
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        protected virtual async Task<string> CallWebApi(string url)
        {
            // String for storing the data retrieved from the endpoint
            string result;

            // Retrieve the JSON data from the endpoint
            using(WebClient client = new WebClient())
            {
                result = await client.DownloadStringTaskAsync(url);
            }

            // Return the retrieved JSON data.
            return result;
        }
    }
}