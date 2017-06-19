using Cibertec.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Cibertec.Repositories.Northwind.EntityFramework
{
    public class SupplierRepository : RepositoryEF<Supplier>, ISupplierRepository
    {
        public SupplierRepository(DbContext context) : base(context)
        {
        }

        public Supplier SearchByContactName(string ContactName)
        {
            return _context.Set<Supplier>().FirstOrDefault(c => c.ContactName == ContactName);
        }
    }
}
