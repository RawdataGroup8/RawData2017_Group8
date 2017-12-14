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


            //var total = _ds.GetUsers().Count;
            var total = _ds.GetUserCount();
            var totalPages = (int)Math.Ceiling(total / (double)total);

            var data = _ds.GetUsers(page, pageSize)
                .Select(x => new
                {
                    Url = Url.Link(nameof(GetUser), new { id = x.Userid }),
                    Name = x.UserName
                });

            //var total = _ds.NumberOfQuestions();
            var pages = Math.Ceiling(total / (double)pageSize);
            var prev = page > 0 ? Url.Link(nameof(GetUsers), new { page = page - 1, pageSize }) : null;
            var next = page < pages - 1 ? Url.Link(nameof(GetUsers), new { page = page + 1, pageSize }) : null;


            var result = new
            {
                //total,
                pages,
                prev,
                next,
                items = data
            };


            return Ok(result);
        }





        //like: "api/User/1"
        [HttpGet("{id}", Name = nameof(GetUser))]
        public IActionResult GetUser(int id)
        {
            var data = _ds.GetUser(id);

            var result = new
            {
                Link1 = Url.Link(nameof(GetUser), new { id }),
                Names = data.UserName != null ? data.UserName : null,
                Link2 = Url.Link(nameof(GetUserPosts), new { data.Userid }),
                data = data
            };

            return Ok(result);

        }

        [HttpGet("{id}/Posts", Name = nameof(GetUserPosts))]
        public IActionResult GetUserPosts(int id, int page = 0, int pageSize = 10)
        {


            try
            {

                var total = _ds.GetUser(id).Posts.Count;
                var totalPages = Math.Ceiling(total / (double)total);
                var pages = Math.Ceiling(total / (double)pageSize); // check it

                var data = _ds.GetUser(id).Posts
                    .Select(x => new
                    {
                        Url = Url.Link((nameof(PostsController.GetPosts)), new { id = x.PostId }),
                        Name = x.Title
                    });

                var result = new
                {
                    Number_Of_Posts = total,
                    Number_Of_Pages = totalPages,
                    PageSize = pages,
                    Page = page,
                    prev = page > 0 ? Url.Link(nameof(GetUserPosts), new { page = page - 1, pageSize }) : null,
                    next = page < pages - 1 ? Url.Link(nameof(GetUserPosts), new { page = page + 1, pageSize }) : null,
                    Url=  Url.Link(nameof(GetUserPosts), new { page, pageSize })
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

