using Microsoft.EntityFrameworkCore;
using SujalTraders.DataAccess.Data;
using SujalTraders.DataAccess.Repository.Interface;
using SujalTraders.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SujalTraders.DataAccess.Repository.Implementation
{
    class ImpProduct:IProduct
    {
        private readonly ApplicationDbContext _db;
        public ImpProduct(ApplicationDbContext db)
        {
            _db = db;
        }
        public IEnumerable<Product> GetAll()
        {
            IEnumerable<Product> productCategories = _db.md_products.Include(c=>c.ProductSubCategory).ToList();
            return productCategories;
        }

        public Product GetByID(int productId)
        {
            Product productCategory = _db.md_products.SingleOrDefault(c => c.Id == productId);
            return productCategory;
        }
        public void Insert(Product product)
        {
            _db.md_products.Add(product);
        }

        public void Delete(int? id)
        {
            Product productFromDb = _db.md_products.SingleOrDefault(c => c.Id == id);
            _db.md_products.Remove(productFromDb);
        }

        public void Update(Product product)
        {
            _db.md_products.Update(product);

        }

        public Product GetFirstOrDefault(int id)
        {
            Product productFromDb = _db.md_products.SingleOrDefault(c => c.Id == id);
            return productFromDb;
        }
    }
}
