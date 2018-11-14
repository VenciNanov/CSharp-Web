using System;
using SIS.MvcFramework;

namespace TORSHIA
{
    class Program
    {
        static void Main(string[] args)
        {
           WebHost.Start(new Startup());
        }
    }
}
