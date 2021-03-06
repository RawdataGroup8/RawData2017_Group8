﻿using DBMapper;
using DBMapper.DBObjects;
using Microsoft.AspNetCore.Mvc;

namespace WebServiceLayer.Controllers
{
    [Produces("application/json")]
    [Route("/api/categories")]
    public class CategoriesController : Controller
    {
        public CategoriesController(IDataService iDataService) => _ds = iDataService;
        private readonly IDataService _ds;

        // GET: api/Categories
        [HttpGet(Name = "GetCategories")]
        public IActionResult Get() => Ok(_ds.GetCategories());

        // GET: api/Categories/5
        [HttpGet("{id}", Name = "GetCategory")]
        public IActionResult Get(int id) => _ds.GetCategory(id) != null ? (IActionResult)Ok(_ds.GetCategory(id)) : NotFound(_ds.GetCategory(id));

        // POST: api/Categories
        [HttpPost(Name = "AddCategory")]
        public IActionResult CreateCategory([FromBody](string, string) content) => Created("", _ds.CreateCategory(content.Item1, content.Item2));

        // PUT: api/Categories/5
        [HttpPut("{id}", Name = "UpdateOrAddCategory")]
        public IActionResult Put(int id, [FromBody]Category category) => _ds.UpdateCategory(category.Id, category.Name, category.Description) ? (IActionResult)Ok() : NotFound();

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}", Name = "DeleteCategory")]
        public IActionResult Delete(int id) => _ds.DeleteCategory(id) ? (IActionResult)Ok(_ds.DeleteCategory(id)) : NotFound(_ds.DeleteCategory(id));
    }
}