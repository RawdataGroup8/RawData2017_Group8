using DataAccesLayer;
using Microsoft.AspNetCore.Mvc;
using WebServiceLayer.DataTransferObjects;

namespace WebServiceLayer.Controllers
{
    [Produces("application/json")]
    [Route("api/History")]
    public class HistoryController : AbstractController
    {
        private readonly IDataService _ds;
        public HistoryController(IDataService iDataService) => _ds = iDataService;

        //Working fine but not using TheDTO
        // GET: api/History/1
        [HttpGet("{id}", Name = nameof(UserHistory))]
        public IActionResult UserHistory(int id)
        {
            var data = _ds.UserHistory(id);
            return data != null ? (IActionResult)Ok(data) : NotFound(data);
        }

    }


}