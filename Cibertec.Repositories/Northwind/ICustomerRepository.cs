using Cibertec.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cibertec.Repositories.Northwind
{
    public  interface ICustomerRepository:IRepository<Customer>
    {

        Customer SearchByName(string firtsname, string lastName);


    }
}
