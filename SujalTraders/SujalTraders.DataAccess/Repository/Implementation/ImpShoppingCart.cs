using Microsoft.EntityFrameworkCore;
using SujalTraders.DataAccess.Data;
using SujalTraders.DataAccess.Repository.Interface;
using SujalTraders.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SujalTraders.DataAccess.Repository.Implementation
{
    class ImpShoppingCart: IShoppingCart
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<ShoppingCart> dbSet;
        public ImpShoppingCart(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<ShoppingCart>();
        }

        public IEnumerable<ShoppingCart> GetAll(Expression<Func<ShoppingCart, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<ShoppingCart> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.ToList();
        }

        public ShoppingCart GetByID(int companyId)
        {
            ShoppingCart companyUser = _db.ShoppingCarts.SingleOrDefault(c => c.Id == companyId);
            return companyUser;
        }
        public void Insert(ShoppingCart ShoppingCart)
        {
            _db.ShoppingCarts.Add(ShoppingCart);
        }

        public void Delete(int? id)
        {
            ShoppingCart shoppingCartFromDb = _db.ShoppingCarts.SingleOrDefault(c => c.Id == id);
            _db.ShoppingCarts.Remove(shoppingCartFromDb);
        }

        public void Update(ShoppingCart shoppingCart)
        {
            _db.ShoppingCarts.Update(shoppingCart);

        }

        public ShoppingCart GetFirstOrDefault(Expression<Func<ShoppingCart, bool>> filter)
        {
            IQueryable<ShoppingCart> query = dbSet;
            query = query.Where(filter);
            ShoppingCart companyFromDb = query.FirstOrDefault();
            return companyFromDb;
        }

        public int IncrementCount(ShoppingCart shoppingCart, int count)
        {
            shoppingCart.Count = shoppingCart.Count + count;
            
            return shoppingCart.Count;
        }

        public int DecrementCount(ShoppingCart shoppingCart, int count)
        {
            shoppingCart.Count = shoppingCart.Count - count;
            return shoppingCart.Count;
        }
    }
}
