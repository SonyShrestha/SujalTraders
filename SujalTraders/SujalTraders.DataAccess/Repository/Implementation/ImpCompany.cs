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
    class ImpCompany:ICompany
    {
        private readonly ApplicationDbContext _db;
        public ImpCompany(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Company> GetAll()
        {
            IEnumerable<Company> companyUsers = _db.Company.ToList();
            return companyUsers;
        }

        public Company GetByID(int companyId)
        {
            Company companyUser = _db.Company.SingleOrDefault(c => c.Id == companyId);
            return companyUser;
        }
        public void Insert(Company companyUser)
        {
            _db.Company.Add(companyUser);
        }

        public void Delete(int? id)
        {
            Company companyUserFromDb = _db.Company.SingleOrDefault(c => c.Id == id);
            _db.Company.Remove(companyUserFromDb);
        }

        public void Update(Company companyUser)
        {
            _db.Company.Update(companyUser);

        }

        public Company GetFirstOrDefault(int id)
        {
            Company companyFromDb = _db.Company.SingleOrDefault(c => c.Id == id);
            return companyFromDb;
        }
    }
}
