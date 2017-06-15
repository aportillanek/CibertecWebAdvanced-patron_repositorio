using Cibertec.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Cibertec.Repositories.Northwind.Daper
{
    public class SupplierRepository : Repository<Supplier>,ISupplierRepository
    {
        public SupplierRepository(string connectionString) : base(connectionString)
        {
        }
        public Supplier SearchByContactName(string ContactName)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ContactName", ContactName);



                return connection.QueryFirst<Supplier>("dbo.SearchByContactName", parameters, commandType: System.Data.CommandType.StoredProcedure);

            }
        }

    }
}
