using Cibertec.Models;
using Cibertec.Repositories;
using Cibertec.Repositories.Northwind;
using Cibertec.Repositories.Northwind.Daper;

namespace Cibertec.UnitOfWork
{
    public class CibertecUnitOfWork : IUnitOfWork
    {
        public CibertecUnitOfWork(string connectionString)
        {
            Customers = new CustomerRepository(connectionString);
            Orders = new OrderRepository(connectionString);
            OrderItems = new OrderItemRepository(connectionString);
            Products = new ProductRepository(connectionString);
            Suppliers = new SupplierRepository(connectionString);
            Users= new UserRepository(connectionString);
        }

        public ICustomerRepository Customers { get; private set; }
        public IOrderRepository Orders { get; private set; }
        public IOrderItemRepository OrderItems { get; private set; }
        public IProductRepository Products { get; private set; }
        public ISupplierRepository Suppliers { get; private set; }
        public IUserRepository Users { get; private set; }
    }
}
