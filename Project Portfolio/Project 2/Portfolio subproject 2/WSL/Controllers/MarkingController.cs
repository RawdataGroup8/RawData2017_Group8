using DataAccesLayer;
using Microsoft.AspNetCore.Mvc;

namespace WebServiceLayer.Controllers
{
    [Produces("application/json")]
    [Route("api/Marking")]
    public class MarkingController : AbstractController
    {
        private readonly IDataService _ds;
        public MarkingController(IDataService iDataService) => _ds = iDataService;
    }
}