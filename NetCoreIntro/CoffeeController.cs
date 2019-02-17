using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace NetCoreIntro
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
            return _dbContext.CoffeeBeans.ToList();
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
    }
}