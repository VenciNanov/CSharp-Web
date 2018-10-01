using System;
using System.Net;
using System.Web;

namespace _02.ValidateURL
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var url = Console.ReadLine();

            url = WebUtility.UrlDecode(url);

            Uri uri = new Uri(url);

            if (string.IsNullOrEmpty(uri.Scheme) ||
                string.IsNullOrEmpty(uri.Host) ||
                string.IsNullOrEmpty(uri.AbsolutePath) ||
                uri.Port < 0 ||
                (uri.Scheme == "http" && uri.Port != 80) ||
                (uri.Scheme == "https" && uri.Port != 443))
            {
                Console.WriteLine("Invalid URL");
            }
            else
            {
                Console.WriteLine($"Protocol: {uri.Scheme}");
                Console.WriteLine($"Host: {uri.Host}");
                Console.WriteLine($"Port: {uri.Port}");
                Console.WriteLine($"Path: {uri.AbsolutePath}");


                if (!string.IsNullOrEmpty(uri.Query))
                {
                    Console.WriteLine($"Query: {uri.Query}");
                }
                if (!string.IsNullOrEmpty(uri.Fragment))
                {
                    Console.WriteLine($"Fragment: {uri.Fragment}");
                }

            }
        }
    }
}
