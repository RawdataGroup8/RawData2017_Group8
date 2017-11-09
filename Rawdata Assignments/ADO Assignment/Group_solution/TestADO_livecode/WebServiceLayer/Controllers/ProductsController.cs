using System;
using System.Collections.Generic;
using System.Linq;
using DBMapper;
using DBMapper.DTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WebServiceLayer.Controllers
{
    [Produces("application/json")]
    [Route("api/products")]
    public class ProductsController : Controller
    {
        public ProductsController(IDataService iDataService) => _ds = iDataService;
        private readonly IDataService _ds;

        [HttpGet("{id}", Name = "GetProduct")] // GET: api/products/5
        public IActionResult GetProduct(int id) => _ds.GetProduct(id) != null ? (IActionResult) Ok(_ds.GetProduct(id)) : NotFound(_ds.GetProduct(id));

        [HttpGet(Name = "GetProducts")] // GET: api/products
        public IActionResult GetProducts() => Ok(_ds.GetProducts());
       
        [Route("category/{id}")] // GET: api/products/category/5
        public IActionResult GetByCategory(int id) => ListResult(id, method => _ds.GetProductByCategory(id)); 
        //alternative: public IActionResult GetByCat(int id) => _ds.GetProductByCategory(id).Any() ? (IActionResult) Ok(_ds.GetProductByCategory(id)) : NotFound(_ds.GetProductByCategory(id));
        
        [Route("name/{str}")] // GET: api/products/name/<str>
        public IActionResult GetByNameMatch(string str) => ListResult(str, method => _ds.GetProductByName(str));
        //alternative: public IActionResult GetByNameMatch(string str) => _ds.GetProductByName(str).Any() ? (IActionResult)Ok(_ds.GetProductByName(str)) : NotFound(_ds.GetProductByName(str));

        //Used for all queries that take one input and returns a list of DTO's. Would be more useful with more queries, and a "dto
        //layer" between the datalayer and the webservice allowing use of the same ListResult funtion for all entities)
        public IActionResult ListResult(object input, Func<object, List<ProductDTO>> genericFunc)
        {
            var products = genericFunc(input);
            return products.Any() ? (IActionResult) Ok(products) : NotFound(products);         
        }





        [Route("coffee")]// GET: api/coffee
        public IActionResult GetCoffee() => StatusCode(418, JsonConvert.SerializeObject("{Not a french press!}"));

    }
}
