using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cibertec.UnitOfWork;
using System.Linq;

namespace Cibertec.Repositories.Tests
{
    
    public class CustomerRepositoryTests

    {
        private readonly IUnitOfWork _unit;

        public CustomerRepositoryTests()
        {
            _unit = new CibertecUnitOfWork(ConfigSettings.ConnectionString);
        }

        public void Test()
        {
          var result=  _unit.Customers.GetAll();
            Assert.IsNotNull(result);
            Assert.True(result.Count() > 0);
        }
    }
}
