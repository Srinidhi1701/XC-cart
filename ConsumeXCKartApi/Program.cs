using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
namespace ConsumeXCKartApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            const string url = "https://localhost:7240";
            HttpClient client = new HttpClient();
            XCKartServiceClient xcKartService = new(url, client);
            var data = xcKartService.GetProductAsync();
        }

        /*static async void Main(string[] args)
        {
            using var httpClient = new HttpClient();
            var sevice = new XCKartServiceClient("https://localhost:7240/", httpClient);
            var data = sevice.GetProductAsync();
            
        }*/
        /*static void Main(string[] args)
        {
            var client = new XCKartServiceClient("https://localhost:7240", new System.Net.Http.HttpClient());
            var data =  client.GetProductAsync();
        }*/


    }
}