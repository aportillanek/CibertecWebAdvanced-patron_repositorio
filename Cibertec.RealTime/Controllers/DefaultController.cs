using System.Collections.Generic;
using System.Web.Http;

namespace Cibertec.RealTime.Controllers
{
    public class DefaultController : ApiController
    {
        public IHttpActionResult Get()
        {
            var list = new List<string>();
            for(int i=0;i<3000;i++)
            {
                list.Add("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");

            }
            return Ok(list);
        }
    }
}
