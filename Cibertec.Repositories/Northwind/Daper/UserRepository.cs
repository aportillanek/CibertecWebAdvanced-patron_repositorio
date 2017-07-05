using System;
using System.Collections.Generic;
using System.Text;
using Cibertec.Models;
using Dapper.Contrib.Extensions;
using System.Data.SqlClient;
using Dapper;

namespace Cibertec.Repositories.Northwind.Daper
{
    public class UserRepository : IUserRepository
    {

        protected readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            SqlMapperExtensions.TableNameMapper = (type) => { return $"[{type.Name}]"; };
            _connectionString = connectionString;
        }
        public User ValidarUser(string email, string password)
        {
            using (var connection =new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@email",email);
                parameters.Add("@password", password);

                return connection.QueryFirst<User>("dbo.ValidateUser",
                    parameters,
                    commandType: System.Data.CommandType.StoredProcedure


                    );
            }
        }
    }
}
