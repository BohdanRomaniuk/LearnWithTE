using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : ControllerBase
    {
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id:int}")]
        public string Get(int id, string query)
        {
            return $"value {id}  query={query}";
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]Value value)
        {
            if(!ModelState.IsValid)
            {
                throw new InvalidOperationException("Invalid");
            }
        }

        // PUT api/<controller>/5
        [HttpPut("{id=123}")]
        public void Put(int id, [FromBody]string value)
        {

        }

        // DELETE api/<controller>/5
        [HttpDelete("{id:int}")]
        public void Delete(int id)
        {
        }
    }

    public class Value
    {
        public int Id { get; set; }

        [MinLength(3)]
        public string Text { get; set; }
    }
}
