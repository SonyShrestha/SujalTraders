using Microsoft.AspNetCore.Mvc.Rendering;
using SujalTraders.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SujalTraders.DataAccess.ViewModels
{
    public class ProductSubCategoryVM
    {
        public ProductSubCategory ProductSubCategory { get; set; }
        public IEnumerable<SelectListItem> ProductCategoryList { get; set; }
    }
}
