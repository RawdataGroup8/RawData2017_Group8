using DataAccesLayer;
using Microsoft.AspNetCore.Mvc;

namespace WebServiceLayer.Controllers
{
    [Produces("application/json")]
    [Route("api/Comment")]
    public class CommentController : AbstractController
    {
        private readonly IDataService _ds;
        public CommentController(IDataService iDataService) => _ds = iDataService;
    }

}