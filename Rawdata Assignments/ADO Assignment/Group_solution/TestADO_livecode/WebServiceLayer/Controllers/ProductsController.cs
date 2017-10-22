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
    [Route("api/products")]
    public class ProductsController : Controller
    {
        private readonly IDataService _ds = new DataService();

        // GET: api/Products
        [HttpGet(Name = "GetProducts")]
        public string GetProducts()
        {
            return JsonConvert.SerializeObject(_ds.GetProducts());
        }

        // GET: api/products/5
        [HttpGet("{id}", Name = "GetProduct")]
        public IActionResult GetProduct(int id) => _ds.GetProduct(id) != null ? (IActionResult)Ok(_ds.GetProduct(id)) : NotFound(_ds.GetProduct(id));

        // GET: api/products/category/5
        [Route("category/{id}")]
        public IActionResult GetByCat(int id)
        {
            var product = _ds.GetProductByCategory(id);
            return product.Any() ? (IActionResult)Ok(product) : NotFound(product);
        }

        // GET: api/products/name/<str>
        [Route("name/{str}")]
        public IActionResult GetBy(string str)
        {
            var products = _ds.GetProductByName(str);
            return products.Any() ? (IActionResult) Ok(products) : NotFound(products);
        }

        // POST: api/Products
        [HttpPost(Name = "AddProduct")]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Products/5
        [HttpPut("{id}", Name = "UpdateOrAddProduct")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/Products/5
        [HttpDelete("{id}", Name = "DeleteProduct")]
        public void Delete(int id)
        {
        }
    }
}
