using System;
using System.Collections.Generic;

namespace _03.RequestParser
{
    class StartUp
    {
        private static readonly string responseTemplate = "HTTP/1.1 {0}" + Environment.NewLine +
                                                         "Content-Length: {1}" + Environment.NewLine +
                                                         "Content-Type: text/plain" + Environment.NewLine +
                                                         Environment.NewLine +
                                                         "{2}";

        static void Main(string[] args)
        {
            var validPaths = new Dictionary<string, HashSet<string>>();

            while (true)
            {
                var input = Console.ReadLine();
                if (input == "END")
                {
                    break;
                }

                var inputTokens = input.Split("/", StringSplitOptions.RemoveEmptyEntries);
                var path = "/" + inputTokens[0];
                var method = inputTokens[1];

                if (!validPaths.ContainsKey(path))
                {
                    validPaths.Add(path, new HashSet<string>());
                }
                validPaths[path].Add(method);
            }
            var requestedInput = Console.ReadLine();
            var requestedTokens = requestedInput.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            var requestedMethod = requestedTokens[0];
            var requestedPath = requestedTokens[1];
            var requestedProtocol = requestedTokens[2];

            var statusCode = 0;
            var statusText = string.Empty;

            if (validPaths.ContainsKey(requestedPath) && validPaths[requestedPath].Contains(requestedMethod.ToLower()))
            {
                statusCode = 200;
                statusText = "OK";
            }
            else
            {
                statusCode = 404;
                statusText = "NotFound";
            }

            Console.WriteLine(string.Format(responseTemplate, statusCode + " " + statusText, statusText.Length, statusText));



        }
    }
}
