using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebServiceLayer.Controllers
{
    public abstract class AbstractController : Controller
    {
        protected string Link(string route, int page, int pageSize, int pageInc = 0, Func<bool> f = null)
        {
            if (f == null) return Url.Link(route, new { page, pageSize });

            return f()
                ? Url.Link(route, new { page = page + pageInc, pageSize })
                : null;
        }

        protected int GetTotalPages(int pageSize, int total) => (int)Math.Ceiling(total / (double)pageSize);

        protected static void CheckPageSize(ref int pageSize)
        {
            pageSize = pageSize > 50 ? 50 : pageSize; //Q: What is 50 all about? :P A: it's just the upperlimet of the pagesize 
        }
    }
}