using SujalTraders.DataAccess.Repository.Interface;
using SujalTraders.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SujalTraders.Repository.UnitOfWork
{
    public interface IUnitOfWork
    {
        IProductCategory ProductCategoryRepsitory { get; }
        IProductSubCategory ProductSubCategoryRepsitory { get; }
        IProduct ProductRepository { get; }

        ICompany CompanyRepository { get; }
        void Save();
    }
}
