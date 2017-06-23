using Cibertec.Controllers;
using Cibertec.MockData;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;
using FluentAssertions;
using Cibertec.Models;
using System.Collections.Generic;

namespace Cibertec.Web.Tests
{
    public class CustomerControllerTests
    {
        private readonly CustomerController _customercontroller ;
       
        public CustomerControllerTests()
        {
            _customercontroller = new CustomerController(MockedUnitOfWork.GetUnitOfWork());
        }
        [Fact(DisplayName = "Index Customer")]
        public void Index_Customer()
        {
            var result = _customercontroller.Index() as ViewResult;
            result.Should().NotBeNull();
            var model = result.Model as List<Customer>;
            model.Count.Should().Be(2);
            model[0].Id.Should().Be(1);
        }
        [Fact(DisplayName = "Detail Customer")]
        public void Detail_Customer()
        {
            var result = _customercontroller.Detail() as ViewResult;
            result.Should().NotBeNull();
            var model = result.Model as List<Customer>;

        }

    }
}
