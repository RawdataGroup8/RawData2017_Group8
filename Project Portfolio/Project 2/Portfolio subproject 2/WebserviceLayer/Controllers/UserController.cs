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
            var total = _ds.GetUserCount(); //
            var totalPages = (int)Math.Ceiling(total / (double)total);

            var data = _ds.GetUsers(page, pageSize)
                .Select(x => new
                {
                    Url = Url.Link(nameof(GetPostUser), new { id = x.Userid }),
                    Name = x.UserName,
                    age = x.Userage,
                    adress = x.UserLocation,
                    total = _ds.GetPostsUser(x.Userid).Count


        });

            //var total = _ds.NumberOfQuestions();
            var pages = Math.Ceiling(total / (double)pageSize);
            var prev = page > 0 ? Url.Link(nameof(GetUsers), new { page = page - 1, pageSize }) : null;
            var next = page < pages - 1 ? Url.Link(nameof(GetUsers), new { page = page + 1, pageSize }) : null;


            var result = new
            {
                total,
                pages,
                prev,
                next,
                items = data
            };


            return Ok(result);
        }


        [HttpGet("{id}", Name = nameof(GetPostUser))]
        public IActionResult GetPostUser(int id)
        {


            try
            {

                var total = _ds.GetPostsUser(id).Count;
            
                var Data = _ds.GetPostsUser(id)
                    .Select(x => new
                    {
                        Url = Url.Link((nameof(PostsController.GetPost)), new { id = x.PostId }),
                        Name = x.Title,
                        Type = x.TypeId,
                        x.Body
                    });

                var result = new
                {
                    Number_Of_Posts = total,
                    
                    data = Data


                };
                return Ok(result);

            }

            catch
            {
                return NotFound();
            }

            



        }
   }


}

//-----------------------------------------------------------------------------------------------------

// The old Controller just incase

/*

[HttpGet("{id}/Posts", Name = nameof(GetUserPosts))]
like: "api/User/1"
[HttpGet("{id}", Name = nameof(GetUser))]
public IActionResult GetUser(int id, int page = 0, int pageSize = 10)
{


try
{

    var total = _ds.GetUser1(id).Count;
    // var totalPages = Math.Ceiling(total / (double)total);
    // var pages = Math.Ceiling(total / (double)pageSize); // check it

    var Data = _ds.GetUser1(id)
        .Select(x => new
        {
            Url = Url.Link((nameof(PostsController.GetPost)), new { id = x.PostId }),
            Name = x.Title
        });

    var result = new
    {
        Number_Of_Posts = total,
        Number_Of_Pages = totalPages,
        PageSize = pages,
        Page = page,
        prev = page > 0 ? Url.Link(nameof(GetUser), new { page = page - 1, pageSize }) : null,
        next = page < pages - 1 ? Url.Link(nameof(GetUser), new { page = page + 1, pageSize }) : null,
        Url=  Url.Link(nameof(GetUser), new { page, pageSize }),
        data = Data


    };
    return Ok(result);

}

catch
{
    return NotFound();
}

*/
