using System;
using SIS.MvcFramework;

namespace Chushka
{
    class Program
    {
        static void Main(string[] args)
        {
            WebHost.Start(new StartUp());
        }
    }
}
