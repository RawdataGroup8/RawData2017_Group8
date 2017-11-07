using System.Linq;
using DataAccesLayer;
using Microsoft.AspNetCore.Mvc;
using WebServiceLayer.DataTransferObjects;

namespace WebServiceLayer.Controllers
{
    [Produces("application/json")]
    [Route("api/User")]
    public class UserController : AbstractController
    {
        private readonly IDataService _ds;
        public UserController(IDataService iDataService) => _ds = iDataService;

        [HttpGet(Name = nameof(GetUsers))] 
        public IActionResult GetUsers(int page = 0, int pageSize = 10)
        {
            CheckPageSize(ref pageSize);

            var total = _ds.GetUsers().Count;
            var totalPages = GetTotalPages(pageSize, total);

            var data = _ds.GetUsers(page, pageSize)
                .Select(x => new ListingDTO
                    {
                        Url = Url.Link(nameof(GetUser), new {id = x.Userid}),
                        Name = x.UserName
                    });

            var result = new
            {
                Number_Of_Users = total,
                Number_Of_Pages = totalPages,
                PageSize = pageSize,
                Page = page,
                Prev = Link(nameof(GetUsers), page, pageSize, -1, () => page > 0),
                Next = Link(nameof(GetUsers), page, pageSize, +1, () => page < totalPages - 1),
                Url = Link(nameof(GetUsers), page, pageSize),
                Data = data
            };

            return Ok(result);
        }

        //like: "api/User/1"
        [HttpGet("{id}", Name = nameof(GetUser))]
        public IActionResult GetUser(int id)
        {
            var result = new
            {
                Data = new UserDTO
                {
                    Url = Url.Link(nameof(GetUser), new { id }),
                    Name = _ds.GetUser(id).UserName,
                    NumberOfPosts = _ds.GetUser(id).Posts.Count,
                    PostsByUser = Url.Link(nameof(GetUserPosts), new {id})
                }
            };
            return Ok(result);
        }

        [HttpGet("{id}/Posts", Name = nameof(GetUserPosts))]
        public IActionResult GetUserPosts(int id, int page = 0, int pageSize = 10)
        {
            CheckPageSize(ref pageSize);

            var total = _ds.GetUser(id).Posts.Count;
            var totalPages = GetTotalPages(pageSize, total);

            var data = _ds.GetUser(id).Posts
                .Select(x => new ListingDTO
                {
                    Url = Url.Link(nameof(PostController.GetPost), new { id = x.PostId }),
                    Name = x.Title
                });

            var result = new
            {
                Number_Of_Posts = total,
                Number_Of_Pages = totalPages,
                PageSize = pageSize,
                Page = page,
                Prev = Link(nameof(GetUserPosts), page, pageSize, -1, () => page > 0),
                Next = Link(nameof(GetUserPosts), page, pageSize, +1, () => page < totalPages - 1),
                Url = Link(nameof(GetUserPosts), page, pageSize),
                Data = data
            };
            return Ok(result);
        }
        
    }
}