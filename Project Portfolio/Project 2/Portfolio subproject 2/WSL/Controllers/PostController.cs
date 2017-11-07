using DataAccesLayer;
using Microsoft.AspNetCore.Mvc;

namespace WebServiceLayer.Controllers
{
    [Produces("application/json")]
    [Route("api/Post")]
    public class PostController : AbstractController
    {
        private readonly IDataService _ds;
        public PostController(IDataService iDataService) => _ds = iDataService;

        [HttpGet("id", Name = nameof(GetPost))]
        public IActionResult GetPost(int id)
        {
            var data = new
            {

            };

            var resultat = new
            {
                Warning= "not implementet yet"
            };

            return Ok(resultat);
        }
    }
}