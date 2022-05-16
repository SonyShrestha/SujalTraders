using SujalTraders.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SujalTraders.Repository.Interface
{
    public interface IProductSubCategory
    {
        IEnumerable<ProductSubCategory> GetAll();
        ProductSubCategory GetByID(int productSubCategoryId);
        void Insert(ProductSubCategory productSubCategory);
        void Delete(int? productSubCategoryId);
        void Update(ProductSubCategory productSubCategory);

        ProductSubCategory GetFirstOrDefault(int id);
    }
}
