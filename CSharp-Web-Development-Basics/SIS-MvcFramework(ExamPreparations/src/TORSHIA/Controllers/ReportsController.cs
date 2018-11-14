using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SIS.HTTP.Responses;
using SIS.MvcFramework;
using TORSHIA.Models;
using TORSHIA.Models.Enums;
using TORSHIA.ViewModels.Reports;

namespace TORSHIA.Controllers
{
   public class ReportsController:BaseController
    {
        [Authorize("Admin")]
        public IHttpResponse Create(int id)
        {
            var task = Db.Tasks.Include(x => x.Report).FirstOrDefault(x => x.Id == id);

            if (task ==null)
            {
                return BadRequestError("Invalid task");
            }

            int[] status = {0, 0, 0, 1};
            int statusIndex = new Random().Next(0,4);

            var user = Db.Users.FirstOrDefault(x => x.Username == this.User.Username);

            task.IsReported = true;

            task.Report = new Report
            {
                Reporter = user,
                ReportedOn = DateTime.UtcNow,
                Status = (Status) status[statusIndex]
            };

            Db.SaveChanges();

            return this.Redirect("/");

        }

        [Authorize("Admin")]
        public IHttpResponse All()
        {
            var reports = Db.Reports.Include(x => x.Task.AffectedSectors)
                .ToArray();

            return this.View(reports);
        }

        [Authorize("Admin")]
        public IHttpResponse Details(int id)
        {
            var report = Db.Reports.Include(x => x.Task)
                .Include(x => x.Reporter)
                .Include(x => x.Task.AffectedSectors)
                .FirstOrDefault(x => x.Id == id);

            var affectedSectors = Db.TaskSectors.Where(x => x.TaskId == report.TaskId)
                .Select(x => x.Sector.Name).ToList();

            var reportModed = new ReportDetailsViewModel
            {
                ReportId = report.Id,
                Reporter = report.Reporter.Username,
                Description = report.Task.Description,
                Title = report.Task.Title,
                Status = report.Status.ToString(),
                Participants = report.Task.Participants,
                ReportedOn = report.ReportedOn.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture),
                DueDate = report.Task.DueDate?.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture),
                AffectedSectors = string.Join(", ", affectedSectors),
                AffectedSectorsCount = affectedSectors.Count
            };

            return this.View(reportModed);
        }
    }
}
