using Cibertec.Repositories.Tests;
using Cibertec.UnitOfWork;
using System;
using System.Linq;
using Xunit;

namespace XUnitTestProject1
{
    public class CustomerRepositoryTests
    {
        private readonly IUnitOfWork _unit;

        public CustomerRepositoryTests()
        {
            _unit = new CibertecUnitOfWork(ConfigSettings.ConnectionString);
        }
        [Fact(DisplayName = "[CustomerRepositoryTests] Get All Customers")]
        public void Get_All_Customers()
        {
            var result = _unit.Customers.GetAll();
            Assert.NotNull(result);
            Assert.True(result.Count() > 0);
        }
    }
}
