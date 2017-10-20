using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebServiceLayer.Controllers
{
    [Produces("application/json")]
    [Route("api/Products")]
    public class ProductsController : Controller
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "Products" };
        }
    }
}

//[Route("/api/persons")]
//public class PersonsController : Controller
//{
//    private IDataService _dataService;

//    public PersonsController(IDataService dataService)
//    {
//        _dataService = dataService;
//    }
//    [HttpGet]
//    public IActionResult GetPersons()
//    {
//        return Ok(_dataService.GetPersons());
//    }

//    [HttpGet("{name}")]
//    public IActionResult GetPerson(string name)
//    {
//        var person = _dataService.GetPersons()
//            .FirstOrDefault(x => x.Name == name);
//        if (person == null) return NotFound();
//        return Ok(person);
//    }
//}

//// GET api/values
//[HttpGet]
//public IEnumerable<string> Get()
//{
//return new string[] { "value1", "value2" };
//}

//// GET api/values/5
//[HttpGet("{id}")]
//public string Get(int id)
//{
//return "value " + id;
//}

//// POST api/values
//[HttpPost]
//public void Post([FromBody]string value)
//{
//}

//// PUT api/values/5
//[HttpPut("{id}")]
//public void Put(int id, [FromBody]string value)
//{
//}

//// DELETE api/values/5
//[HttpDelete("{id}")]
//public void Delete(int id)
//{
//}