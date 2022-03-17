using System;

namespace Listener
{
    public class Program
    {
        static void Main(string[] args)
        {
            Listener.SimpleListenerExample(new string[] { "http://localhost:8888/", "http://localhost:8888/MyName/", "http://localhost:8888/Information/", "http://localhost:8888/Success/", "http://localhost:8888/Redirection/", "http://localhost:8888/ClientError/", "http://localhost:8888/ServerError/" });
            Console.ReadLine();
        }
    }
}
