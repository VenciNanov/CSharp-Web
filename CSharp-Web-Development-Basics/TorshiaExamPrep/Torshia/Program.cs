using System;
using System.Linq;
using SIS.Framework;
using Torshia.Data;
using Torshia.Models;
using Torshia.Services;

namespace Torshia
{
    class Program
    {
        static void Main(string[] args)
        {
            //var context = new TorshiaDbContext();
            //if (!context.Tasks.Any())
            //{
            //    for (int i = 0; i < 6; i++)
            //    {
            //        var task = new Task
            //        {
            //            Description = "Generated",
            //            Title = $"Generated_ID_{i}"
            //        };
            //        context.Tasks.Add(task);
            //    }

            //    context.SaveChanges();
            //}
            
            WebHost.Start(new StartUp());
        }
    }
}
