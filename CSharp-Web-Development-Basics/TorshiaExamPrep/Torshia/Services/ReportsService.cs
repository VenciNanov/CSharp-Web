using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Torshia.Data;
using Torshia.Models;
using Torshia.Services.Contracts;

namespace Torshia.Services
{
   public class ReportsService:IReportsService
   {
       private readonly TorshiaDbContext context;

       public ReportsService(TorshiaDbContext context)
       {
           this.context = context;
       }


       public IQueryable<Report> All => context.Reports;


   }
}
