using System.Collections.Generic;

namespace Torshia.ViewModels.Task
{
    public class TaskViewModelWrapper
    {
        public TaskViewModelWrapper()
        {
            this.TaskViewModels=new List<TaskViewModel>();
        }

       public ICollection<TaskViewModel> TaskViewModels { get; set; }
    }
}
