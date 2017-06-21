using Cibertec.Models;
using Cibertec.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using FluentAssertions;
namespace Cibertec.Repositories.Tests
{
    public class CustomerRepositoryTests
    {
        private readonly IUnitOfWork _unit;
        public CustomerRepositoryTests()
        {
            _unit = MockedUnitOfWork.GetUnitOfWork();
        }

        [Fact(DisplayName = "[CustomerRepositoryTests] Get All Customers")]
        public void Get_All_Customers()
        {
            var result = _unit.Customers.GetAll();

            result.Should().NotBeNull();
            result.Count().Should().BeGreaterThan(0);
            
        }

        [Fact(DisplayName = "[CustomerRepositoryTests] Insert Customers")]
        public void Insert_Customer()
        {
          //  var customer = new Customer();
            var result = _unit.Customers.Insert(null);
            result.Should().BeGreaterThan(0);
        }

        [Fact(DisplayName = "[CustomerRepositoryTests] Fail Insert Customers")]
        public void Insert_Customer_Wrong()
        {
            //  var customer = new Customer();
            var result = _unit.Customers.Insert(new Customer());
            result.Should().Be(0);
        }
    }
}
