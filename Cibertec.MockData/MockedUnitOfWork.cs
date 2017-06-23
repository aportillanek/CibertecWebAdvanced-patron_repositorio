using Cibertec.Models;
using Cibertec.Repositories.Northwind;
using Cibertec.UnitOfWork;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Cibertec.MockData
{
    public static class MockedUnitOfWork
    {
        public static IUnitOfWork GetUnitOfWork()
        {
            Mock<IUnitOfWork> unit = new Mock<IUnitOfWork>();
            unit.ConfigureCustomer();
            return unit.Object;
        }
    }

    public static class MockedUnitOfWorkExtensions
    {
        public static Mock<IUnitOfWork> ConfigureCustomer(this Mock<IUnitOfWork> mock)
        {
            var customerList = new List<Customer> {
                    new Customer
                    {
                        Id = 1,
                        City = "Lima",
                        Country = "Peru",
                        FirstName = "Julio",
                        LastName = "Velarde",
                        Phone = "555-5555"
                    },
                    new Customer
                    {
                        Id = 2,
                        FirstName = "Juan",
                        LastName = "Perez",
                        City = "Lima",
                        Country = "Peru",
                        Phone = "666-6666"
                    }
                };

            mock.Setup(c => c.Customers.GetEntityById(It.IsAny<int>()))
                .Returns((int id) => { return customerList.FirstOrDefault(x => x.Id == id); }
                );

            mock.Setup(c => c.Customers.GetAll()).Returns(customerList);
            mock.Setup(c => c.Customers.Insert(It.IsAny<Customer>())).Returns(1);
            mock.Setup(c => c.Customers.Update(It.IsAny<Customer>())).Returns(true);
            mock.Setup(c => c.Customers.Delete(It.IsAny<Customer>())).Returns(true);
            mock.Setup(c => c.Customers.SearchByName(It.IsAny<string>(), It.IsAny<string>()))
                .Returns((string firstName, string lastName) =>
                {
                    return customerList
                    .FirstOrDefault(x => x.FirstName == firstName && x.LastName == lastName);
                });
            return mock;
        }
    }
}
