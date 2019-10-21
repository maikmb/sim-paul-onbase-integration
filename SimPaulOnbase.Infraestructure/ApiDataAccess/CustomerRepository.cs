using Newtonsoft.Json;
using SimPaulOnbase.Core.Customers;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace SimPaulOnbase.Infraestructure.ApiDataAccess
{
    /// <summary>
    /// CustomerRepository class
    /// </summary>
    public class CustomerRepository
    {
        private HttpClient _client;
        private CustomerApiSettings _customerApiSettings;

        /// <summary>
        /// CustomerRepository constructor
        /// </summary>
        /// <param name="client"></param>
        /// <param name="customerApiSettings"></param>
        public CustomerRepository(HttpClient client, CustomerApiSettings customerApiSettings)
        {
            _client = client;
            _customerApiSettings = customerApiSettings;

            _client = client;
            _client.BaseAddress = new Uri(customerApiSettings.BaseUrl);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        }

        /// <summary>
        /// Get diverged registrations from SimPaul Customer API
        /// </summary>
        /// <returns></returns>
        public IList<Customer> DivergedRegistrations()
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, _customerApiSettings.DivergedResource);

            var responseMessage = _client.SendAsync(requestMessage)
                    .GetAwaiter()
                    .GetResult();          

            responseMessage.EnsureSuccessStatusCode();

            string responseContent = responseMessage.Content
                  .ReadAsStringAsync()
                  .GetAwaiter()
                  .GetResult();
            
            var divergedRegistrations = JsonConvert.DeserializeObject<List<Customer>>(responseContent);
            return divergedRegistrations;
        }
    }
}
