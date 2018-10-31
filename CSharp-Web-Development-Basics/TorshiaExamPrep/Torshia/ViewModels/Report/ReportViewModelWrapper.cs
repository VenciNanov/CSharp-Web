using System;
using System.Collections.Generic;
using System.Text;

namespace Torshia.ViewModels
{
   public class ReportViewModelWrapper
    {
        public ReportViewModelWrapper()
        {
            ReportViewModels=new List<ReportViewModel>();
        }

        public ICollection<ReportViewModel> ReportViewModels { get; set; }
    }
}
