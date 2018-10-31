using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SIS.Framework.ActionResults;
using SIS.Framework.ActionResults.Interfaces;
using SIS.Framework.Attributes.Method;
using Torshia.Controllers.Base;
using Torshia.Services.Contracts;
using Torshia.ViewModels;

namespace Torshia.Controllers
{
    public class TasksController : BaseController
    {
        private readonly ITasksService service;

        public TasksController(ITasksService service)
        {
            this.service = service;
        }

        public IActionResult Details()
        {
            if (!UserAuthorization())
            {
                if (!AdminAuthorization())
                {
                    return RedirectToAction("/users/login");
                }
            }


            var id = int.Parse(this.Request.QueryData["id"].ToString());

            var task = db.Tasks.FirstOrDefault(x => x.Id == id);

            var sectors = db.TaskSectors.Include(x => x.Sector).Where(x => x.TaskId == task.Id)
                .Select(x => x.Sector.Name).ToList();

            this.Model.Data["AffectedSectors"] = string.Join(", ", sectors);
            this.Model.Data["Description"] = UrlDecode(task.Description);
            this.Model.Data["DueDate"] = task.DueDate?.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
            this.Model.Data["Level"] = sectors.Count();
            this.Model.Data["Participants"] = UrlDecode(task.Participants);
            this.Model.Data["Title"] = UrlDecode(task.Title);

            return this.View();
        }

        public IActionResult Create()
        {
            if (!AdminAuthorization())
            {
                return RedirectToAction("/");
            }

            return this.View();
        }

        [HttpPost]
        public IActionResult Create(CreateTaskViewModel model)
        {
            if (!AdminAuthorization())
            {
                return RedirectToAction("/");
            }

            var affectedSectors = new List<string>
           {
               model.Customers,
               model.Marketing,
               model.Finances,
               model.Internal,
               model.Management
           };

            service.CreateTask(model.Title, model.DueDate, model.Description, model.Participants, affectedSectors);

            return RedirectToAction("/");
        }

    }
}
