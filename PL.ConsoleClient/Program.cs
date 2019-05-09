using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using BLL.CoreEntities.Entities;

namespace PL.ConsoleClient
{
    class Program
    {
        static HttpClient client = new HttpClient();
        private const string itemIdFromDb = "60";
        static void ShowProduct(Product product)
        {
            Console.WriteLine($"Name: {product.ProductName}\tPrice: " +
                              $"{product.UnitPrice}\tCategory: {product.Category}");
        }
        static async Task<Product> GetProductAsync(string path)
        {
            Product product = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                product = await response.Content.ReadAsAsync<Product>();
            }
            return product;
        }

        static async Task<IEnumerable<Product>> GetAllProducts(string path)
        {
            IEnumerable<Product> products= new List<Product>();
            var response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                products = await response.Content.ReadAsAsync<IEnumerable<Product>>();
            }

            return products;
        }
        static void Main()
        {
            RunAsync().GetAwaiter().GetResult();

        }
        static async Task RunAsync()
        {
            // Update port # in the following line.
            client.BaseAddress = new Uri("https://localhost:44380/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                // Get the product;
                ShowProduct(await GetProductAsync($"https://localhost:44380/api/products/{itemIdFromDb}"));
                Console.WriteLine("All products:\n");
                foreach (var product in await GetAllProducts("https://localhost:44380/api/products"))
                {
                    ShowProduct(product);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}
