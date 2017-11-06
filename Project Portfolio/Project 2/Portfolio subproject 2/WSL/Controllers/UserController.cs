using System;
using System.Linq;
using AutoMapper;
using DataAccesLayer;
using DataAccesLayer.DBObjects;
using Microsoft.AspNetCore.Mvc;
using WebServiceLayer.Models;

namespace WebServiceLayer.Controllers
{
    [Produces("application/json")]
    [Route("api/User")]
    public class UserController : Controller
    {
        private readonly IDataService _ds;
        private readonly IMapper _mapper;
        public UserController(IDataService iDataService)
        {
            _ds = iDataService;
            _mapper = CreateMapper();
        }

        [HttpGet(Name = nameof(GetUsers))] 
        public IActionResult GetUsers(int page = 0, int pageSize = 10)
        {
            CheckPageSize(ref pageSize);

            var total = _ds.GetUsers().Count;
            var totalPages = GetTotalPages(pageSize, total);

            var data = _ds.GetUsers(page, pageSize)
                .Select(x => new SimpleReturnModel
                {
                    Url = Url.Link(nameof(GetUser), new {id = x.Userid}),
                    Name = x.UserName
                });

            var result = new
            {
                Total = total,
                Pages = totalPages,
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
//            var user = _ds.GetUser(id);
//            if (user == null) return NotFound();
//
//            var model = mapper.Map<UserReturnModel>(User);
//            model.Url = Url.Link(nameof(GetUser), new {id = user.Userid});
//
//            return Ok(model);
            return Ok(_ds.GetUser(id));
        }

        public IMapper CreateMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserReturnModel>()
                    .ReverseMap();
            });

            return config.CreateMapper();
        }

        private string Link(string route, int page, int pageSize, int pageInc = 0, Func<bool> f = null)
        {
            if (f == null) return Url.Link(route, new { page, pageSize });

            return f()
                ? Url.Link(route, new { page = page + pageInc, pageSize })
                : null;
        }

        private static int GetTotalPages(int pageSize, int total)
        {
            return (int)Math.Ceiling(total / (double)pageSize);
        }

        private static void CheckPageSize(ref int pageSize)
        {
            pageSize = pageSize > 50 ? 50 : pageSize;
        }
    }
}