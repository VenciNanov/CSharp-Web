using CHUSHKA.Data;
using CHUSHKA.Models;
using CHUSHKA.Models.Enums;
using SIS.Framework;

namespace CHUSHKA
{
    class Program
    {
        static void Main(string[] args)
        {
            //var context = new ChuskaDbContext();
            //for (int i = 0; i < 5; i++)
            //{
            //    context.Products.Add(new Product
            //    {
            //        Name = $"Generated_{i}",
            //        Description = $"Generated-{i + 500}",
            //        Price = 20M,
            //        Type = Type.Other
            //    });
            //}

            //context.SaveChanges();
            WebHost.Start(new StartUp());
        }
    }
}
