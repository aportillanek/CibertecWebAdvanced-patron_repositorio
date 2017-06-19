using Cibertec.Models;
using Cibertec.Repositories;
using Cibertec.Repositories.Northwind;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cibertec.UnitOfWork
{
    public interface IUnitOfWork
    {
        ICustomerRepository Customers { get; }
        IOrderRepository Orders { get; }
        IOrderItemRepository OrderItems { get; }
        IProductRepository Products { get; }
        ISupplierRepository Suppliers { get; }
    }
}
