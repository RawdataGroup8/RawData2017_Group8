using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.DBObjects;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class DataService: IDataService

    {

    private readonly StackoverflowContext _db;

    public DataService()
    {
        _db = new StackoverflowContext();
    }

        public List<User> GetUsers()
        {
            return _db.User.ToList();
        }

        public Post GetPost(int id)
        {
            return _db.Post.Include(c => c.Comments).FirstOrDefault(p => p.PostId == id);
        }
    }
}