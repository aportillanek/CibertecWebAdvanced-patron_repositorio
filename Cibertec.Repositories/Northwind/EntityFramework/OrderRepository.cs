using Cibertec.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Cibertec.Repositories.Northwind.EntityFramework
{
    public class OrderRepository : RepositoryEF<Order>, IOrderRepository
    {
        public OrderRepository(DbContext context) : base(context)
        {
        }

        public Order SearchByOrderNumber(string orderNumber)
        {
            return _context.Set<Order>().FirstOrDefault(c => c.OrderNumber == orderNumber);
        }
    }
}
