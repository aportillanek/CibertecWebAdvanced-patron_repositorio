using Cibertec.Models;
using Cibertec.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using FluentAssertions;
using Cibertec.MockData;

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

        
        [Fact(DisplayName = "Customer Update Test")]
        public void Customer_update()
        {
            //  var customer = new Customer();
            var result = _unit.Customers.Update(new Customer());
            result.Should().BeTrue();
        }

        [Fact(DisplayName = "Customer Delete Test")]
        public void Customer_delete()
        {
            //  var customer = new Customer();
            var result = _unit.Customers.Delete(new Customer());
            result.Should().BeTrue();
        }
        [Theory(DisplayName ="Customer Search By Names Test")]
        [InlineData("Joan", "Perez")]
        [InlineData("Julio","Velarde")]
        [InlineData("Alan", "Garcia")]
        public void Customer_SearchByName(string firstName,string lastName)
        {

            var customer = _unit.Customers.SearchByName(firstName, lastName);
            customer.Should().NotBeNull();
        }

    }
}
