using Cibertec.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Cibertec.Repositories.Northwind.EntityFramework
{
    public class ProductRepository : RepositoryEF<Product>, IProductRepository
    {
        public ProductRepository(DbContext context) : base(context)
        {
        }

        public int RowNumber()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> SearchByPage(int startRow, int endRow)
        {
            throw new NotImplementedException();
        }

        public Product SearchByProductName(string ProducName)
        {
            return _context.Set<Product>().FirstOrDefault(c => c.ProductName == ProducName);
        }
    }
}
