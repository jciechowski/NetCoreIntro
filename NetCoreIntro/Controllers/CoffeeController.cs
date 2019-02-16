using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NetCoreIntro.Models;

namespace NetCoreIntro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoffeeController : ControllerBase
    {
        private readonly CoffeeDBContext _dbContext;

        public CoffeeController(CoffeeDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<CoffeeBean>> Get()
        {
            var coffeeBeans = _dbContext.CoffeeBeans.ToList();

            return coffeeBeans;
        }

        [HttpGet("{id}")]
        public ActionResult<CoffeeBean> Get(long id)
        {
            return _dbContext.CoffeeBeans.Find(id);
        }

        [HttpPost]
        public void Post([FromBody] CoffeeBean coffeeBean)
        {
            _dbContext.Add(coffeeBean);
            _dbContext.SaveChanges();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}