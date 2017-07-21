using Cibertec.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;


namespace Cibertec.Repositories.Northwind.Daper
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(string connectionString) : base(connectionString)
        {
        }
        public Product SearchByProductName(string producName)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@producName", producName);



                return connection.QueryFirst<Product>("dbo.SearchByProductName", parameters, commandType: System.Data.CommandType.StoredProcedure);

            }
        }
        public int RowNumber()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.ExecuteScalar<int>("SELECT Count(id) FROM dbo.Product");

            }

        }

        public IEnumerable<Product> SearchByPage(int startRow, int endRow)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@startRow", startRow);
                parameters.Add("@endRow", endRow);


                return connection.Query<Product>("dbo.ProductPagedList", parameters, commandType: System.Data.CommandType.StoredProcedure);

            }
        }
    }
}
