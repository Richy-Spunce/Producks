using Producks.UnderCutters.Dtos;
using System.Collections.Generic;
using System.Net.Http;

namespace Producks.UnderCutters
{

    public class ServiceProxy
    {
        private HttpClient client;

        public ServiceProxy()
        {
            client = new HttpClient();
            client.BaseAddress = new System.Uri("http://undercutters.azurewebsites.net/");
            client.DefaultRequestHeaders.Accept.ParseAdd("application/json");
        }

        public IEnumerable<UnderCutterCategory> GetAllCategories()
        {
            HttpResponseMessage response = client.GetAsync("api/Category").Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<IEnumerable<UnderCutterCategory>>().Result;
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<UnderCutterBrand> GetAllBrands()
        {
            HttpResponseMessage response = client.GetAsync("api/Brand").Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<IEnumerable<UnderCutterBrand>>().Result;
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<UnderCutterProduct> GetAllProducts()
        {
            HttpResponseMessage response = client.GetAsync("api/Product").Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<IEnumerable<UnderCutterProduct>>().Result;
            }
            else
            {
                return null;
            }
        }

        public UnderCutterProduct GetProductByName(string name)
        {
            HttpResponseMessage response = client.GetAsync("api/Product?category_name=").Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<UnderCutterProduct>().Result;
            }
            else
            {
                return null;
            }
        }
    }
}
