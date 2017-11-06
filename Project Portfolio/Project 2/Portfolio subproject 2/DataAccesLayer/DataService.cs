﻿using System.Collections.Generic;
using System.Linq;
using DataAccesLayer.DBObjects;
using Microsoft.EntityFrameworkCore;

namespace DataAccesLayer
{
    public class DataService : IDataService
    {
        private readonly StackoverflowContext _db;

        public DataService() => _db = new StackoverflowContext();

        //public List<User> GetUsers() => _db.User.ToList();
        public List<User> GetUsers(int page, int pageSize) => _db.User.OrderBy(x => x.Userid).Skip(page*pageSize).Take(pageSize).ToList();

        public Question GetQuestion(int id)
        {
            var question = _db.Question.FirstOrDefault(q => q.PostId1 == id);
            question.SetPost(GetPost(id));
            return question;
        }
        public Answers GetAnswer(int id)
        {
            var question = _db.Answer.FirstOrDefault(q => q.PostId == id);
            question.SetPost(GetPost(id));
            return question;
        }

        //return a full post, including all comments and tags.
        public Post GetPost(int id) => _db.Post.Include(p1 => p1.Comments).Include(p1 => p1.PostTags).FirstOrDefault(p => p.PostId == id);

        //return a full user, including all posts.
        public User GetUser(int id) => _db.User.Include(c => c.Posts).FirstOrDefault(p => p.Userid == id);

        //return a Post, including tags related to the post.
        public Post GetPosts_Tags(int id) => _db.Post.Include(c => c.PostTags).FirstOrDefault(p => p.PostId == id);

        // A procedure that searches
        public bool Searching_usingtype_String()
        {          
            // you can also use the string interpolation syntax
            //var str = "What used for java";
            //var id1 = 1;
            var result = _db.Post.FromSql("call fulltext_search({0},{1})", "What used for java", 1);

            foreach (var text in result)
            {
                if (text.PostId == 22944075) return true;
                //Console.WriteLine($"{category.CategoryId}, {category.CategoryName}");
            }
            return false;
        }
        public List<Answers> Retrieve_Answers_Procedure(string query)
        {
            // you can also use the string interpolation syntax
            var type_id = 1;
            return _db.Answer.FromSql($"call fulltext_search({query},{type_id})").ToList();
        }
    }

}   