using System;
using System.Collections.Generic;
using System.Text;
using Cibertec.Models;
using Cibertec.Repositories;
using Microsoft.EntityFrameworkCore;
using Cibertec.Repositories.Northwind.EF;
using Cibertec.Repositories.Northwind;

namespace Cibertec.UnitOfWork
{
    public class EFUnitOfWork : IUnitOfWork
    {
        public EFUnitOfWork(DbContext context)
        {
            Customers = new CustomerRepository(context);
            Orders = new RepositoryEF<Order>(context);
            OrderItems = new RepositoryEF<OrderItem>(context);
            Products = new RepositoryEF<Product>(context);
            Suppliers = new RepositoryEF<Supplier>(context);
        }

        public ICustomerRepository Customers { get; private set; }
        public IRepository<Order> Orders { get; private set; }
        public IRepository<OrderItem> OrderItems { get; private set; }
        public IRepository<Product> Products { get; private set; }
        public IRepository<Supplier> Suppliers { get; private set; }
    }
}
