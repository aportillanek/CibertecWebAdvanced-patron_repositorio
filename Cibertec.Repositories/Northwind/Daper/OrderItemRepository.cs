using Cibertec.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Cibertec.Repositories.Northwind.Daper
{
    public class OrderItemRepository : Repository<OrderItem>, IOrderItemRepository
    {
        public OrderItemRepository(string connectionString) : base(connectionString)
        {
        }
        public OrderItem SearchByUnitPrice(decimal UnitPrice)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@UnitPrice", UnitPrice);



                return connection.QueryFirst<OrderItem>("dbo.SearchByUnitPrice", parameters, commandType: System.Data.CommandType.StoredProcedure);

            }
        }

    }
}
