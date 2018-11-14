using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TORSHIA.Models
{
    public class Task
    {
        public Task()
        {
            AffectedSectors = new List<TaskSector>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime? DueDate { get; set; }

        public bool IsReported { get; set; } = false;
        public string Description { get; set; }

        public string Participants { get; set; }

        public List<TaskSector> AffectedSectors { get; set; }

        [NotMapped]
        public int ReportId { get; set; }

        public Report Report { get; set; }
    }
}
