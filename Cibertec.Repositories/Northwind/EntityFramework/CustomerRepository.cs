using Cibertec.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Cibertec.Repositories.Northwind.EntityFramework
{
    public class CustomerRepository : RepositoryEF<Customer>, ICustomerRepository
    {

        public CustomerRepository(DbContext context):base(context)
        {

        }

        public int RowNumber()
        {
            throw new NotImplementedException();
        }

        public Customer SearchByName(string firtsname, string lastName)
        {
            return _context.Set<Customer>().FirstOrDefault(c => c.FirstName == firtsname && c.LastName == lastName);
        }

        public IEnumerable<Customer> SearchByPage(int startRow, int endRow)
        {
            throw new NotImplementedException();
        }
    }
}
