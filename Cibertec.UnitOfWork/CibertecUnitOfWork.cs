using Cibertec.Models;
using Cibertec.Repositories;

namespace Cibertec.UnitOfWork
{
    public class CibertecUnitOfWork : IUnitOfWork
    {
        public CibertecUnitOfWork(string connectionString)
        {
            Customers = new Repository<Customer>(connectionString);
            Orders = new Repository<Order>(connectionString);
            OrderItems = new Repository<OrderItem>(connectionString);
            Products = new Repository<Product>(connectionString);
            Suppliers = new Repository<Supplier>(connectionString);
        }

        public IRepository<Customer> Customers { get; private set; }
        public IRepository<Order> Orders { get; private set; }
        public IRepository<OrderItem> OrderItems { get; private set; }
        public IRepository<Product> Products { get; private set; }
        public IRepository<Supplier> Suppliers { get; private set; }
    }
}
