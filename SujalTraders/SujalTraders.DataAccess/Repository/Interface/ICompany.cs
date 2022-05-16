using SujalTraders.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SujalTraders.DataAccess.Repository.Interface
{
    public interface ICompany
    {
        IEnumerable<Company> GetAll();
        Company GetByID(int companyId);
        void Insert(Company company);
        void Delete(int? companyId);
        void Update(Company company);

        Company GetFirstOrDefault(int id);
    }
}
