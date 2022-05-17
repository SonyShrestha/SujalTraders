using SujalTraders.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SujalTraders.DataAccess.Repository.Interface
{
    public interface IShoppingCart
    {
        IEnumerable<ShoppingCart> GetAll(Expression<Func<ShoppingCart, bool>>? filter = null, string? includeProperties = null);
        ShoppingCart GetByID(int shoppingCartId);
        void Insert(ShoppingCart shoppingCart);
        void Delete(int? shoppingCartId);
        void Update(ShoppingCart shoppingCart);

        ShoppingCart GetFirstOrDefault(Expression<Func<ShoppingCart, bool>> filter);
        int IncrementCount(ShoppingCart shoppingCart, int count);
        int DecrementCount(ShoppingCart shoppingCart, int count);
    }
}
