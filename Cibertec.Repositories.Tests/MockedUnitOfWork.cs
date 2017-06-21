using Cibertec.Models;
using Cibertec.UnitOfWork;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cibertec.Repositories.Tests
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
            mock.Setup(c => c.Customers.GetAll()).Returns(
                new List<Customer>
                {
                    new Customer
                    {
                        Id=1,
                        City="Lima",
                        Country="Peru",
                        FirstName="Juan",
                        LastName="Perez",
                       
                        Phone="555-55555"
                    },
                    new Customer
                    {
                        Id=2,
                        City="Ica",
                        Country="Peru",
                        FirstName="Raul",
                        LastName="Huaman",
                       
                        Phone="666-66666"
                    }
                });

            mock.Setup(c => c.Customers.GetEntityById(1)).Returns(
                 
                     new Customer
                    {
                        Id=1,
                        City="Lima",
                        Country="Peru",
                        FirstName="Juan",
                        LastName="Perez",

                        Phone="555-55555"
                    } );

            mock.Setup(c => c.Customers.Insert(null
          )).Returns(1);
            mock.Setup(c => c.Customers.Update(new Customer
            {
                Id = 1,
                City = "Lima",
                Country = "Peru",
                FirstName = "Juan",
                LastName = "Perez",

                Phone = "555-55555"
            })).Returns(true);
            mock.Setup(c => c.Customers.Delete(new Customer
            {
                Id = 1,
                City = "Lima",
                Country = "Peru",
                FirstName = "Juan",
                LastName = "Perez",

                Phone = "555-55555"
            })).Returns(true);


            return mock;
        }
    }
}
