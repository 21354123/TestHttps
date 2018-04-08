using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace TestHttps.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return Program.Values;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            if (id >= Program.Values.Count || id < 0) return "木有找到"; else return Program.Values[id];            
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
            Program.Values.Add(value);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void  Put(int id, [FromBody]string value)
        {
            if (id < Program.Values.Count && id >= 0) Program.Values[id] = value;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            if (id < Program.Values.Count && id >= 0) Program.Values.RemoveAt(id);
        }
    }
}
