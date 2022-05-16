using SujalTraders.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SujalTraders.Repository.Interface
{
    public interface IProductCategory
    {
        IEnumerable<ProductCategory> GetAll();
        ProductCategory GetByID(int productCategoryId);
        void Insert(ProductCategory productCategory);
        void Delete(int? productCategoryId);
        void Update(ProductCategory productCategory);

        ProductCategory GetFirstOrDefault(int id);
    }
}
