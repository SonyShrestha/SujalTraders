using SujalTraders.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SujalTraders.DataAccess.Repository.Interface
{
    public interface IProduct
    {
        IEnumerable<Product> GetAll();
        Product GetByID(int productId);
        void Insert(Product product);
        void Delete(int? productId);
        void Update(Product product);

        Product GetFirstOrDefault(int id);
    }
}
