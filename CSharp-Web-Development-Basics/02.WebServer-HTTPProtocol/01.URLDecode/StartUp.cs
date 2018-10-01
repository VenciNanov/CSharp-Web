using System;
using System.Web;

namespace _01.URLDecode
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var encodedURL = Console.ReadLine();

            var decodedUrl = HttpUtility.UrlDecode(encodedURL);

            Console.WriteLine(decodedUrl);
        }
    }
}
