using Cibertec.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Cibertec.Repositories.Northwind.Daper
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(string connectionString) : base(connectionString)
        {
        }
        public Order SearchByOrderNumber(string orderNumber)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@orderNumber", orderNumber);



                return connection.QueryFirst<Order>("dbo.SearchByOrderNumber", parameters, commandType: System.Data.CommandType.StoredProcedure);

            }
        }
    }
}
