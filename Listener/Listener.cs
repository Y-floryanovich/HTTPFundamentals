using System;
using System.Net;
using System.Net.Http;

namespace Listener
{
    public static class Listener
    {
        public static int StatusCode { get; set; }
        // This example requires the System and System.Net namespaces.
        public static void SimpleListenerExample(string[] prefixes)
        {
            if (!HttpListener.IsSupported)
            {
                Console.WriteLine("Windows XP SP2 or Server 2003 is required to use the HttpListener class.");
                return;
            }
            // URI prefixes are required,
            // for example "http://contoso.com:8080/index/".
            if (prefixes == null || prefixes.Length == 0)
                throw new ArgumentException("prefixes");

            // Create a listener.
            var listener = new HttpListener();
            // Add the prefixes.
            foreach (string s in prefixes)
            {
                listener.Prefixes.Add(s);
            }


            listener.Start();
            Console.WriteLine("Listening...");
            while (true)
            {
                // Note: The GetContext method blocks while waiting for a request.
                HttpListenerContext context = listener.GetContext();
                HttpListenerRequest request = context.Request;
                // Obtain a response object.
                HttpListenerResponse response = context.Response;
                // Construct a response.
                ParseResponse(request, response);

                
            }
            listener.Stop();
        }

        private static void ParseResponse(HttpListenerRequest request, HttpListenerResponse response)
        {
            switch (request.RawUrl)
            {
                case "/MyName/":
                    GetMyName(response);
                    return;
                case "/Redirection/":
                    GetRedirection(response);
                    return;
                case "/Success/":
                    GetSuccess(response);
                    return;
                case "/ClientError/":
                    GetClientError(response);
                    return;
                case "/ServerError/":
                    GetServerError(response);
                    return;
                case "/MyNameByHeader/":
                    GetMyNameByHeader(response);
                    return;
                case "/MyNameByCookies/":
                    GetMyNameByCookies(response);
                    return;

            }
        }

        private static void GetMyName(HttpListenerResponse response)
        {
            var name = "Yana";
            SendResponse(name, response);
        }

        private static void GetMyNameByCookies(HttpListenerResponse response)
        {
            var name = "Yana";
            Cookie cook = new Cookie("MyName", "Yana");
            response.AppendCookie(cook);
            SendResponse(name, response);
        }

        private static void GetRedirection(HttpListenerResponse response)
        {
            var name = "Redirection";
            response.StatusCode = (int)HttpStatusCode.PermanentRedirect;
            SendResponse(name, response);
        }

        private static void GetSuccess(HttpListenerResponse response)
        {
            var name = "Success";
            response.StatusCode = (int)HttpStatusCode.OK;
            SendResponse(name, response);
        }

        private static void GetClientError(HttpListenerResponse response)
        {
            var name = "ClientError";
            response.StatusCode = (int)HttpStatusCode.NotFound;
            SendResponse(name, response);
        }

        private static void GetServerError(HttpListenerResponse response)
        {
            var name = "ServerError";
            response.StatusCode = (int)HttpStatusCode.InternalServerError;
            SendResponse(name, response);
        }

        private static void GetMyNameByHeader(HttpListenerResponse response)
        {
            var name = "MyName is Yana";
            response.AddHeader("X-MyName", "Yana-MyName");
            SendResponse(name, response);
        }

        private static void SendResponse(string information, HttpListenerResponse response)
        {
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(information);
            response.ContentLength64 = buffer.Length;
            System.IO.Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            output.Close();
            response.Close();
        }
    }
}
