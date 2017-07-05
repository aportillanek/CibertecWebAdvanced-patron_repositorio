using Cibertec.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cibertec.WebApi.Controllers
{
    [Produces("application/json")]
    [Authorize]
    public class BaseController:Controller
    {
        protected readonly IUnitOfWork _unit;

        public BaseController(IUnitOfWork unit)
        {

            _unit = unit;
        }


    }
}
