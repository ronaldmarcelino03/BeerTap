using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using BeerTapV2.Model;

namespace BeerTapV3UI
{
    class Program
    {
        static HttpClient client = new HttpClient();

        static void Main(string[] args)
        {
            Console.WriteLine("Loading offices..");
            RunAsync().Wait();
        }

        static void ShowOffice(OfficeModel office)
        {
            Console.WriteLine($"Office ID: {office.Id.ToString()}\tLocation: {office.Location}");
        }

        static async Task RunAsync()
        {
            client.BaseAddress = new Uri("http://localhost:61284");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var office = new OfficeModel() { Id = 1, Location = "Manila" };
            var url = await CreateOfficeAsync(office);
            office = await GetOfficeAsync(url.PathAndQuery);
            ShowOffice(office);

            Console.ReadLine();
        }

        static async Task<OfficeModel> GetOfficeAsync(string path)
        {
            OfficeModel office = null;

            try
            {
                HttpResponseMessage response = await client.GetAsync(path);

                if (response.IsSuccessStatusCode)
                {
                    office = await response.Content.ReadAsAsync<OfficeModel>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return office;
        }

        static async Task<Uri> CreateOfficeAsync(OfficeModel office)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync("Offices", office);
            response.EnsureSuccessStatusCode();

            // Return the URI of the created resource
            return response.Headers.Location;
        }
    }
}
