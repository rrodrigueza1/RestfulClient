using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
namespace RestfulClient
{
    public class Shipper
    {
        public int shipperID { get; set; }
        public string companyName { get; set; }
        public string phone { get; set; }
    }
    class Program
    {
        static HttpClient client = new HttpClient();
        List<Shipper> shippersList = new List<Shipper>();
        static void Main(string[] args)
        {
            RunAsync().Wait();
        }

        static async Task RunAsync()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://192.168.177.129:8080");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //GET Method
                Console.WriteLine("GET Mothod");
                Console.WriteLine("Enter Shipper ID to retrieve");
                HttpResponseMessage response = await client.GetAsync("/dmit2015-assignment02-renzellerodrigueza/rest/webapi/shippers/" + Console.ReadLine());
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    Shipper shipper = await response.Content.ReadAsAsync<Shipper>();
                    Console.WriteLine("Shipper ID: {0}\tCompany Name: {1}\tPhone: {2}", shipper.shipperID, shipper.companyName, shipper.phone);
                }
                else
                {
                    Console.WriteLine("Internal server Error");
                }

                //POST Method
                Console.WriteLine("POST Method");
                var shippertPost = new Shipper() { companyName = "Test Company", phone = "7806679502" };
                Console.WriteLine("Enter Company name");
                shippertPost.companyName = Console.ReadLine();
                Console.WriteLine("Enter Phone Number");
                shippertPost.phone = Console.ReadLine();
                HttpResponseMessage responsePost = await client.PostAsJsonAsync("/dmit2015-assignment02-renzellerodrigueza/rest/webapi/shippers", shippertPost);
                if (responsePost.IsSuccessStatusCode)
                {
                    // Get the URI of the created resource.
                    Uri returnUrl = responsePost.Headers.Location;
                    Console.WriteLine(returnUrl);
                    Console.WriteLine("Post Successful");
                }

                ////PUT Method
                Console.WriteLine("PUT Method");
                var shipperPut = new Shipper() { shipperID = 5, companyName = "Updated Company", phone = "6667777777" };
                Console.WriteLine("Enter Shipper ID");
                shipperPut.shipperID = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter Company name");
                shipperPut.companyName = Console.ReadLine();
                Console.WriteLine("Enter Phone Number");
                shipperPut.phone = Console.ReadLine();
                HttpResponseMessage responsePut = await client.PutAsJsonAsync("/dmit2015-assignment02-renzellerodrigueza/rest/webapi/shippers", shipperPut);
                if (responsePut.IsSuccessStatusCode)
                {
                    Console.WriteLine("Success");
                }

                //Delete Method
                Console.WriteLine("DELETE Method");
                int shipperID = 7;
                Console.WriteLine("Enter Shipper to Delete");
                shipperID = int.Parse(Console.ReadLine());
                HttpResponseMessage responseDelete = await client.DeleteAsync("/dmit2015-assignment02-renzellerodrigueza/rest/webapi/shippers/" + shipperID);
                if (responseDelete.IsSuccessStatusCode)
                {
                    Console.WriteLine("Success");
                }
            }
            Console.Read();
        }

    }

}
