using DapperDemo.Data;
using DapperDemo.Models;

namespace DapperDemo.Repository
{
    public class CompanyRepositoryEF : ICompanyRepository
    {
        private readonly ApplicationDbContext _db;

        public CompanyRepositoryEF(ApplicationDbContext db)
        {
            _db = db;
        }

        public Company Add(Company company)
        {
            _db.Companies.Add(company);
            _db.SaveChanges();
            return company;
        }

        public void Remove(int id)
        {
            var company = _db.Companies.FirstOrDefault(x => x.CompanyId == id);
            if (company != null)
            {
                _db.Remove(company);
                _db.SaveChanges();
            }
        }

        public Company Find(int id)
        {
            return _db.Companies.FirstOrDefault(x => x.CompanyId == id);
        }

        public List<Company> GetAll()
        {
            return _db.Companies.ToList();
        }

        public Company Update(Company company)
        {
            _db.Companies.Update(company);
            _db.SaveChanges();
            return company;
        }
    }
}
