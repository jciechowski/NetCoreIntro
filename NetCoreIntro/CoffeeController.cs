using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace NetCoreIntro
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoffeeController : ControllerBase
    {
        private readonly CoffeeDBContext _dbContext;
        private readonly ILogger<CoffeeController> _logger;

        public CoffeeController(CoffeeDBContext dbContext, ILogger<CoffeeController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<CoffeeBean>> Get()
        {
            return _dbContext.CoffeeBeans.ToList();
        }

        [HttpPost]
        public void Post([FromBody] CoffeeBean coffeeBean)
        {
            _dbContext.Add(coffeeBean);
            _dbContext.SaveChanges();
        }
    }
}