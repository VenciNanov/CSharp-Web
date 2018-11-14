using System;
using SIS.MvcFramework;

namespace MISHMASH
{
    class Program
    {
        static void Main(string[] args)
        {
            WebHost.Start(new StartUp());
        }
    }
}
