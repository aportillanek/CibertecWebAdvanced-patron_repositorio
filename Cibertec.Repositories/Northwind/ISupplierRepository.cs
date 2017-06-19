using Cibertec.Models;
using System;
using System.Collections.Generic;
using System.Text;


namespace Cibertec.Repositories.Northwind
{
    public interface ISupplierRepository : IRepository<Supplier>
    {
        Supplier SearchByContactName(string ContactName);
    }
}
