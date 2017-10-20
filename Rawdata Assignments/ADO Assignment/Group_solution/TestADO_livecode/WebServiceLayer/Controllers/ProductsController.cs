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
    [Route("api/Products")]
    public class ProductsController : Controller
    {
        private readonly IDataService _ds = new DataService();

        // GET: api/Products
        [HttpGet(Name = "GetProducts")]
        public string GetProducts()
        {
            return JsonConvert.SerializeObject(_ds.GetProducts());
        }

        // GET: api/Products/5
        [HttpGet("{id}", Name = "GetProduct")]
        public string GetProduct(int id)
        {
            return JsonConvert.SerializeObject(_ds.GetProduct(id)); ;
        }

        // GET: api/products/category/5
        //[HttpGet("{id}", Name = "GetProductByCatId")]
        [Route("category/{id}")]
        public string GetBy(int id)
        {
            return JsonConvert.SerializeObject(_ds.GetProductByCategory(id)); ;
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
