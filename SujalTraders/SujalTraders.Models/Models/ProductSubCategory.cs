using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SujalTraders.Models.Models
{
    public class ProductSubCategory
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public ProductCategory ProductCategory { get; set; }
        [Required]
        public int ProductCategoryId { get; set; }
    }
}
