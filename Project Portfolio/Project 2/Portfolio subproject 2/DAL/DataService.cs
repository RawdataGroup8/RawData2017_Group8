using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.DBObjects;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class DataService : IDataService
    {
        private readonly StackoverflowContext _db;

        public DataService()
        {
            _db = new StackoverflowContext();
        }

        public List<User> GetUsers() => _db.User.ToList();

 
        public IQueryable GetQuestion(int id) //NOT WORKING/DONE
        {
            /*var question = from post in _db.Post.ToList()
                           join q in _db.Question.ToList() on post.PostId == q.PostId;*/
            var q = _db.Post.Join(_db.Answers,
                    post => post.PostId,
                    ans => ans.Postid1,
                    (post, ans) => new {Post = post, Meta = ans})
                .Where(postAndAns => postAndAns.Post.PostId == id);
            return q;
        }

        //return a full post, including all comments.
        public Post GetPost(int id) => _db.Post.Include(c => c.Comments).FirstOrDefault(p => p.PostId == id);

        //return a full user, including all posts.
        public User GetUser(int id) => _db.User.Include(c => c.Posts).FirstOrDefault(p => p.Userid == id);
    }

}