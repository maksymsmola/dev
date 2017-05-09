using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MoneyKeeper.Mobile.Api.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        [CustomAuthorize]
        public IEnumerable<string> Get()
        {
            return new string[] { "вася", "петя-876876" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
