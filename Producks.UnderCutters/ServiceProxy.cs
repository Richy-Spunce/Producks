using Producks.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Producks.UnderCutters
{

    public class ServiceProxy
    {
        private HttpClient client;

        public ServiceProxy()
        {
            client = new HttpClient();
            client.BaseAddress = new System.Uri("http://http://undercutters.azurewebsites.net/");
            client.DefaultRequestHeaders.Accept.ParseAdd("application/json");
        }

        public IEnumerable<Category> GetAllCategories()
        {
            HttpResponseMessage response = client.GetAsync("api/Category").Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<IEnumerable<Category>>().Result;
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<Brand> GetAllBrands()
        {
            HttpResponseMessage response = client.GetAsync("api/Brand").Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<IEnumerable<Brand>>().Result;
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<Product> GetAllProducts()
        {
            HttpResponseMessage response = client.GetAsync("api/Product").Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<IEnumerable<Product>>().Result;
            }
            else
            {
                return null;
            }
        }
    }
}
