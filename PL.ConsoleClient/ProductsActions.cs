using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using BLL.CoreEntities.Entities;

namespace PL.ConsoleClient
{
    public static class ProductsActions
    {
        public static async Task<Product> GetProductAsync(string path, HttpClient client)
        {
            Product product = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                product = await response.Content.ReadAsAsync<Product>();
            }
            return product;
        }

        public static async Task<IEnumerable<Product>> GetAllProducts(string path, HttpClient client)
        {
            IEnumerable<Product> products = new List<Product>();
            var response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                products = await response.Content.ReadAsAsync<IEnumerable<Product>>();
            }

            return products;
        }
    }
}