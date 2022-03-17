using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public static class Client
    {
        static readonly HttpClient client;
        const string URLSIMPLE = "http://localhost:8888/";
        const string URLFORGETTINGNAME = "http://localhost:8888/MyName/";
        const string URLFORHEADER = "http://localhost:8888/MyNameByHeader/";
        const string URLFORCOOKIES = "http://localhost:8888/MyNameByCookies/";



        static Client()
        {
            client = new HttpClient();
        }

        public static async Task StartAsync()
        {
            try
            {
                string[] URLSTATUS = new string[4] { "http://localhost:8888/Success/", "http://localhost:8888/Redirection/", "http://localhost:8888/ClientError/", "http://localhost:8888/ServerError/" };
                foreach (var status in URLSTATUS)
                {
                    //using (HttpClient client = new HttpClient())
                    //{
                    //    var response = await client.GetAsync(status);
                    //    string responseBody = await response.Content.ReadAsStringAsync();
                    //    var responseStatus = response.StatusCode;
                    //    // Above three lines can be replaced with new helper method below
                    //    // string responseBody = await client.GetStringAsync(uri);

                    //    Console.WriteLine(responseStatus);
                    //    Console.WriteLine(responseBody);
                    //    response.Dispose();

                    //}

                }
                CookieContainer cookies = new CookieContainer();
                var response = await client.GetAsync(URLFORCOOKIES);
                string responseBody = await response.Content.ReadAsStringAsync();
                var responseStatus = response.StatusCode;
                var responseHeader = response.Headers;
                IEnumerable<Cookie> responseCookies = cookies.GetCookies(new Uri(URLFORCOOKIES)).Cast<Cookie>();
                foreach (Cookie cookie in responseCookies)
                    Console.WriteLine(cookie.Name + ": " + cookie.Value);
                // Above three lines can be replaced with new helper method below
                // string responseBody = await client.GetStringAsync(uri);

                Console.WriteLine("Status: " + responseStatus);
                Console.WriteLine("Header: " + responseHeader);
                Console.WriteLine("Body: " + responseBody);
                response.Dispose();

                client.Dispose();
                Console.ReadLine();
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }
    }
}
