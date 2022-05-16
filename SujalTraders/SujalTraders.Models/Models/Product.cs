using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SujalTraders.Models.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name="Product Code")]
        public string ProductCode { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        [Range(0,int.MaxValue)]
        [Display(Name="Available Quantity")]
        public int AvailableQuantity { get; set; }

        [Range(0,double.MaxValue)]
        [Display(Name="Unit Price")]
        public double UnitPrice { get; set; }
        [Display(Name = "Image URL")]
        public string ImageURL { get; set; }
        [ValidateNever]
        public ProductSubCategory ProductSubCategory { get; set; }
        [Required]
        public int ProductSubCategoryId { get; set; }
        
}
}
