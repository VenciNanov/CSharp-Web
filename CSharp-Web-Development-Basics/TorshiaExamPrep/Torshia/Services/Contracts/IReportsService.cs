using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Torshia.Models;

namespace Torshia.Services.Contracts
{
   public interface IReportsService
    {
        IQueryable<Report> All { get; }
    }
}
