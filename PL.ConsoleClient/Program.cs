using System;
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
                ShowProduct(await ProductsActions.GetProductAsync($"https://localhost:44380/api/products/{itemIdFromDb}", client));
                Console.WriteLine("All products:\n");
                foreach (var product in await ProductsActions.GetAllProducts("https://localhost:44380/api/products", client))
                {
                    ShowProduct(product);
                }
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.Message);
            }

            Console.ReadLine();
        }
    }
}
