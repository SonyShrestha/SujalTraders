using SujalTraders.DataAccess.Data;
using SujalTraders.Models.Models;
using SujalTraders.Repository.Interface;
using System.Collections.Generic;
using System.Linq;

namespace SujalTraders.Repository.Implementation
{
    public class ImpProductCategory:IProductCategory
    {
        private readonly ApplicationDbContext _db;
        public ImpProductCategory(ApplicationDbContext db)
        {
            _db = db;
        }
        public IEnumerable<ProductCategory> GetAll()
        {
            IEnumerable<ProductCategory> productCategories = _db.md_product_categories.ToList();
            return productCategories;
        }

        public ProductCategory GetByID(int productCategoryId)
        {
            ProductCategory productCategory = _db.md_product_categories.SingleOrDefault(c => c.Id == productCategoryId);
            return productCategory;
        }
        public void Insert(ProductCategory productCategory)
        {
            _db.md_product_categories.Add(productCategory);
        }

        public void Delete(int? id)
        {
            ProductCategory productCategoryFromDb = _db.md_product_categories.SingleOrDefault(c => c.Id == id);
            _db.md_product_categories.Remove(productCategoryFromDb);            
        }

        public void Update(ProductCategory productCategory)
        {
             _db.md_product_categories.Update(productCategory);
                
        }

        public ProductCategory GetFirstOrDefault(int id)
        {
            ProductCategory productCategoryFromDb = _db.md_product_categories.SingleOrDefault(c => c.Id == id);
            return productCategoryFromDb;
        }
    }
}
