using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SIS.HTTP.Responses;
using SIS.MvcFramework;
using TORSHIA.Models;
using TORSHIA.ViewModels.Tasks;

namespace TORSHIA.Controllers
{
    public class TasksController : BaseController
    {
        [Authorize]
        public IHttpResponse Details(int id)
        {
            var task = Db.Tasks.Include(x => x.AffectedSectors).FirstOrDefault(x => x.Id == id);

            var sectors = Db.TaskSectors.Include(x => x.Sector).Where(x => x.TaskId == task.Id)
                .Select(x => x.Sector.Name).ToList();

            var model = new DetailsTaskViewModel
            {
                Title = task.Title,
                Level = sectors.Count.ToString(),
                AffectedSectors = string.Join(", ", sectors),
                Description = task.Description,
                DueDate = task.DueDate?.ToString(@"dd-MM-yyyy", CultureInfo.InvariantCulture),
                Participants = task.Participants
            };

            return this.View(model);
        }

        [Authorize("Admin")]
        public IHttpResponse Create()
        {
            return this.View();
        }

        [Authorize("Admin")]
        [HttpPost]
        public IHttpResponse Create(CreateTaskViewModel model)
        {
            var task = model.To<Task>();

            if (DateTime.TryParse(model.DueDate, out DateTime dueDate))
            {
                task.DueDate = dueDate;
            }

            var properties = model.GetType().GetProperties();

            var taskSectors = new List<TaskSector>();
            foreach (var prop in properties)
            {
                var value = prop.GetValue(model);
                if (value != null)
                {
                    bool isValidSector = Enum.TryParse(typeof(Models.Enums.Sector), value.ToString(),
                        out object sectorValue);

                    if (isValidSector)
                    {
                        var sector = new Models.Sector
                        {
                            Name = (Models.Enums.Sector)sectorValue
                        };
                        var taskSector = new TaskSector
                        {
                            Task = task,
                            Sector = sector
                        };

                        taskSectors.Add(taskSector);
                    }
                }
            }

            task.AffectedSectors.AddRange(taskSectors);
            Db.Tasks.Add(task);
            Db.SaveChanges();

            return this.Redirect("/");
        }
    }
}
