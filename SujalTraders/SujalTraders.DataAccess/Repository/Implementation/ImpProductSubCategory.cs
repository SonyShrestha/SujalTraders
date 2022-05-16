using SujalTraders.DataAccess.Data;
using SujalTraders.Models.Models;
using SujalTraders.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SujalTraders.Repository.Implementation
{
    public class ImpProductSubCategory:IProductSubCategory
    {
        private readonly ApplicationDbContext _db;
        public ImpProductSubCategory(ApplicationDbContext db)
        {
            _db = db;
        }
        public IEnumerable<ProductSubCategory> GetAll()
        {
            IEnumerable<ProductSubCategory> productSubCategories = _db.md_product_sub_categories.ToList();
            return productSubCategories;
        }

        public ProductSubCategory GetByID(int productSubCategoryId)
        {
            ProductSubCategory productSubCategory = _db.md_product_sub_categories.SingleOrDefault(c => c.Id == productSubCategoryId);
            return productSubCategory;
        }
        public void Insert(ProductSubCategory productSubCategory)
        {
            _db.md_product_sub_categories.Add(productSubCategory);
        }

        public void Delete(int? id)
        {
            ProductSubCategory productSubCategoryFromDb = _db.md_product_sub_categories.SingleOrDefault(c => c.Id == id);
            _db.md_product_sub_categories.Remove(productSubCategoryFromDb);
        }

        public void Update(ProductSubCategory productSubCategory)
        {
            _db.md_product_sub_categories.Update(productSubCategory);

        }

        public ProductSubCategory GetFirstOrDefault(int id)
        {
            ProductSubCategory productCategoryFromDb = _db.md_product_sub_categories.SingleOrDefault(c => c.Id == id);
            return productCategoryFromDb;
        }
    }
}
