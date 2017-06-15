using Cibertec.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Cibertec.Repositories.Northwind.EntityFramework
{
    public class OrderItemRepository : RepositoryEF<OrderItem>, IOrderItemRepository
    {
        public OrderItemRepository(DbContext context) : base(context)
        {
        }

        public OrderItem SearchByUnitPrice(decimal UnitPrice)
        {
            return _context.Set<OrderItem>().FirstOrDefault(c => c.UnitPrice >= UnitPrice);
        }
    }
}
