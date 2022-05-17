using SujalTraders.DataAccess.Data;
using SujalTraders.DataAccess.Repository.Implementation;
using SujalTraders.DataAccess.Repository.Interface;
using SujalTraders.Repository.Implementation;
using SujalTraders.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SujalTraders.Repository.UnitOfWork
{
    public class ImpUnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public ImpUnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            ProductCategoryRepsitory = new ImpProductCategory(_db);
            ProductSubCategoryRepsitory = new ImpProductSubCategory(_db);
            ProductRepository = new ImpProduct(_db);
            CompanyRepository = new ImpCompany(_db);
            ShoppingCartRepository = new ImpShoppingCart(_db);
        }
        public IProductCategory ProductCategoryRepsitory { get; private set; }
        public IProductSubCategory ProductSubCategoryRepsitory { get; private set; }
        public IProduct ProductRepository { get; private set; }
        public ICompany CompanyRepository { get; private set; }
        public IShoppingCart ShoppingCartRepository { get; private set; }



        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
