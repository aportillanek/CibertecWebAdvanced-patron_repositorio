using Cibertec.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Cibertec.Repositories.Northwind.Daper
{
  public  class CustomerRepository:Repository<Customer>,ICustomerRepository
    {

        public CustomerRepository(string connectionString):base(connectionString)
        {

        }

        public Customer SearchByName(string firtsname, string lastName)
        {
            using (var connection =new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@firstName", firtsname);

                parameters.Add("@lastName", lastName);

                return connection.QueryFirst<Customer>("dbo.SearchByNames", parameters, commandType: System.Data.CommandType.StoredProcedure);

            }
        }

        public int RowNumber()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.ExecuteScalar<int>("SELECT Count(id) FROM dbo.Customer");

            }

        }

        public IEnumerable<Customer> SearchByPage(int startRow, int endRow)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@startRow", startRow);
                parameters.Add("@endRow", endRow);


                return connection.Query<Customer>("dbo.CustomerPagedList", parameters, commandType: System.Data.CommandType.StoredProcedure);

            }
        }
    }
}
