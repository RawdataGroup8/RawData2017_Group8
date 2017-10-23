using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DBMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WebServiceLayer.Controllers
{
    [Produces("application/json")]
    [Route("/api/categories")]
    public class CategoriesController : Controller
    {
        public CategoriesController(IDataService iDataService)
        {
            _ds = iDataService;
        }
        private readonly IDataService _ds;

        // GET: api/Categories
        [HttpGet(Name = "GetCategories")]
        public string Get()
        {
            return JsonConvert.SerializeObject(_ds.GetCategories());
        }

        // GET: api/Categories/5
        [HttpGet("{id}", Name = "GetCategory")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Categories
        [HttpPost(Name = "AddCategory")]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Categories/5
        [HttpPut("{id}", Name = "UpdateOrAddCategory")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}", Name = "DeleteCategory")]
        public IActionResult Delete(int id)
        {            
            var del = _ds.DeleteCategory(id);
            if (del)
            {
                return Ok(_ds.DeleteCategory(id));
            }
            return NotFound(_ds.DeleteCategory(id));
        }
    }
}
