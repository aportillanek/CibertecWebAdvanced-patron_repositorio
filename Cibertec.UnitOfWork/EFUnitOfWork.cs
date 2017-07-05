using System;
using System.Collections.Generic;
using System.Text;
using Cibertec.Models;
using Cibertec.Repositories;
using Microsoft.EntityFrameworkCore;
using Cibertec.Repositories.Northwind;
using Cibertec.Repositories.Northwind.EntityFramework;
namespace Cibertec.UnitOfWork
{
    public class EFUnitOfWork 
    {
        //public EFUnitOfWork(DbContext context)
        //{
        //    Customers = new CustomerRepository(context);
        //    Orders = new OrderRepository(context);
        //    OrderItems = new OrderItemRepository(context);
        //    Products = new ProductRepository(context);
        //    Suppliers = new SupplierRepository(context);
        //    Users = new UserRepository(context);
        //}

        //public ICustomerRepository Customers { get; private set; }
        //public IOrderRepository Orders { get; private set; }
        //public IOrderItemRepository OrderItems { get; private set; }
        //public IProductRepository Products { get; private set; }
        //public ISupplierRepository Suppliers { get; private set; }
    }
}
