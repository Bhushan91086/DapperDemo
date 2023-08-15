using Dapper;
using DapperDemo.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DapperDemo.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly IDbConnection _db;

        public CompanyRepository(IConfiguration configuration)
        {
            _db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public Company Add(Company company)
        {
            var sql = "Insert into companies (Name, Address, City, State, PostalCode) Values (@Name, @Address, @City, @State, @PostalCode);"
                      + "Select CAST(SCOPE_IDENTITY() as int)";

            var id =_db.Query<int>(sql, company).Single();

            company.CompanyId = id;
            return company;
        }

        public Company Find(int id)
        {
            var sql = "select * from companies where CompanyId = @CompanyId";
            return _db.Query<Company>(sql, new { @CompanyId = id }).Single();
        }

        public List<Company> GetAll()
        {
            var sql = "select * from companies";
            return _db.Query<Company>(sql).ToList();
        }

        public void Remove(int id)
        {
            var sql = "Delete from Companies where CompanyId = @id";
            _db.Execute(sql, new { id });
        }

        public Company Update(Company company)
        {
            var sql = "Update Companies SET Name = @Name, Address = @Address, City = @City," +
                "State = @State, PostalCode = @PostalCode where CompanyId = @CompanyId";

            _db.Execute(sql, company);
            return company;
        }
    }
}
