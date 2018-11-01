using System;
using System.Collections.Generic;
using System.Text;

namespace CHUSHKA.ViewModels
{
    public class IndexProductsRowViewModel
    {
        public IndexProductsRowViewModel()
        {
            Products= new List<IndexProductViewModel>();
        }

        public List<IndexProductViewModel> Products { get; set; }

       public string[] Empty =>new string[5-this.Products.Count];
    }
}
