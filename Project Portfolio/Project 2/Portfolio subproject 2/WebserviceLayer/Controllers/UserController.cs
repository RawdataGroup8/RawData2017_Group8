using System.Linq;
using DataAccesLayer;
using DataAccesLayer.DBObjects;
using Microsoft.AspNetCore.Mvc;
using System;

namespace WebLayer.Controllers
{
    [Produces("application/json")]
    [Route("api/user")]
    public class UserController : Controller
    {

        private readonly IDataService _ds;
        public UserController(IDataService iDataService) => _ds = iDataService;

        [HttpGet(Name = nameof(GetUsers))]
        public IActionResult GetUsers(int page = 0, int pageSize = 10)
        {
            //CheckPageSize(ref pageSize);

            //var total = _ds.GetUsers().Count;
            var total = _ds.GetUserCount();
            var pages = Math.Ceiling(total / (double)pageSize);
            var totalPages= (int)Math.Ceiling(total / (double)pageSize);
            //var totalPages = GetTotalPages(pageSize, total);

            var data = _ds.GetUsers(page, pageSize)
                .Select(x => new
                {
                    Url = Url.Link(nameof(GetUser), new { postId=x.Userid }),
                    Name = x.UserName,
                    Age =x.Userage,
                    Adress = x.UserLocation,
                    
                });

            var result = new
            {
                Number_Of_Users = total,
                Number_Of_Pages = totalPages,
                PageSize = pageSize,
                Page = page,
              
                prev = page > 0 ? Url.Link(nameof(GetUsers), new { page = page - 1, pageSize }) : null,
                next = page < pages - 1 ? Url.Link(nameof(GetUsers), new { page = page + 1, pageSize }) : null,
                //Url = Url.Link(nameof(GetUsers), new { page, pageSize })
                //Data = data
            };

            return Ok(new { result, data });
        }

        //like: "api/User/1"
        [HttpGet("{postId}", Name = nameof(GetUser))]
        public IActionResult GetUser(int postId)
        {

            var Data = _ds.GetUser(postId);
            var post= _ds.GetUser(postId).Posts;
            var Linku = Url.Link(nameof(GetUser), new { postId });
            var Linkp = Url.Link(nameof(GetUserPosts), new { postId });

            var data = new
            {
                Data.Userid,
                Data.UserName,
                Data.Userage,
                Data.UserLocation,
                Linku,
                Linkp
         
            };
            return Data != null ? (IActionResult)Ok(data) : NotFound();
        }

        [HttpGet("{postId}/Posts", Name = nameof(GetUserPosts))]
        public IActionResult GetUserPosts(int postId, int page = 0, int pageSize = 10)
        {
            try
            {
                //CheckPageSize(ref pageSize);

                var total = _ds.GetUser(postId).Posts.Count;
                //var totalPages = (int)Math.Ceiling(total / (double)pageSize);
                var pages = (int)Math.Ceiling(total / (double)pageSize);

                var data = _ds.GetUser(postId).Posts
                    .Select(x => new
                    {
                        Url = Url.Link(nameof(PostsController.GetPost), new { x.PostId }),
                        Name = x.Title
                    });

                var result = new
                {
                    Number_Of_Posts = total,
                    Number_Of_Pages = pages,
                    //PageSize = pageSize,
                    //Page = page,



                    prev = page > 0 ? Url.Link(nameof(GetUserPosts), new { page = page - 1, pageSize }) : null,
                    next = page < pages - 1 ? Url.Link(nameof(GetUserPosts), new { page = page + 1, pageSize }) : null,
                    Link = Url.Link(nameof(GetUserPosts), new { page, pageSize })
                    //Data = data
                };
                return Ok(new { result, data });
            }
            catch
            {
                return NotFound();
            }
        }

       
    }

}



   