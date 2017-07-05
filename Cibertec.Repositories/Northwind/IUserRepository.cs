using System;
using System.Collections.Generic;
using System.Text;
using Cibertec.Models;

namespace Cibertec.Repositories.Northwind
{
     public interface IUserRepository
    {

        User ValidarUser(string email, string password);
    }
}
