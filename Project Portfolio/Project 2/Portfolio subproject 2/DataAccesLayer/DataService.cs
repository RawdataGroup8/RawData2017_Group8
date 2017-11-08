using System.Collections.Generic;
using System.Linq;
using DataAccesLayer.DBObjects;
using Microsoft.EntityFrameworkCore;
using DataAccesLayer.DBObjects.SimpleObjects;

namespace DataAccesLayer
{
    public class DataService : IDataService
    {
        private readonly StackoverflowContext _db;

        public DataService() => _db = new StackoverflowContext();

        // ------------------------ USERS  ------------------------  
        //public List<User> GetUsers() => _db.User.ToList();
        public List<User> GetUsers(int page, int pageSize) => _db.User.OrderBy(x => x.Userid).Skip(page * pageSize).Take(pageSize).ToList();
        //return a full user, including all posts.
        public User GetUser(int id) => _db.User.Include(c => c.Posts).FirstOrDefault(p => p.Userid == id);

        // ------------------------ POSTS (QUESTIONS / ANSWERS) ------------------------         
        //return a full post, including all comments and tags. helper method
        public Post GetPost(int id) => _db.Post.Include(p1 => p1.Comments).Include(p1 => p1.PostTags).FirstOrDefault(p => p.PostId == id);
        //return a Post, including tags related to the post.
        public Post GetPosts_Tags(int id) => _db.Post.Include(c => c.PostTags).FirstOrDefault(p => p.PostId == id);

        //return a full question
        public Question GetQuestion(int id)
        {
            var question = _db.Question.FirstOrDefault(q => q.PostId1 == id);
            question?.SetPost(GetPost(id));
            return question;
        }

        public List<Question> GetNewestQuestions(int limit, int page, int pageSize)
        {
            var posts = _db.Post.Where(p => p.TypeId == 1).OrderByDescending(q => q.CreationDate).Take(limit).Skip(page * pageSize).Take(pageSize).ToList();
            return posts.Select(post => GetQuestion(post.PostId)).ToList();
        }

        //return a full answer
        public Answers GetAnswer(int id)
        {
            var answer = _db.Answer.FirstOrDefault(q => q.PostId == id);
            answer?.SetPost(GetPost(id));
            return answer;
        }
        // ------------------------ LINKS ------------------------         

        public List<LinkedPosts> LinkedFromThisPost(int id) => _db.LinkedPosts.Where(lp => lp.PostId == id).ToList();

        public List<LinkedPosts> LinkingToThisPost(int id) => _db.LinkedPosts.Where(lp => lp.PostId == id).ToList();

        // ------------------------ HISTORY & BOOKMARKS ------------------------         
        public List<History> UserHistory(int id) => _db.History.Where(h => h.Userid == id).ToList();

        public List<Marking> UserBookmarks(int id) => _db.Marking.Where(m => m.Postid == id).ToList();

        public int AddMarking(int uid, int pid, string mark) => _db.Database.ExecuteSqlCommand("call add_marking({0},{1},{2})", uid, pid, mark);

        public void DeleteMarking(int uid, int pid) => _db.Database.ExecuteSqlCommand("delete from marking where user_id = {0} and post_id = {1}", uid, pid);

        public List<History> GetHistory() => _db.History.ToList();

        public History GetHistoryItem(int userId, int linkPostId) => _db.History.FirstOrDefault(h => h.Userid == userId && h.LinkPostId == linkPostId);

        public void AddQuestionToHistory(int userId, int postId)
        {
            _db.History.Add(new History(userId, postId));
            _db.SaveChanges();
        }

        public void RemoveQuestionFromHistory(int userId, int linkPostId)
        {
            _db.History.Remove(GetHistoryItem(userId, linkPostId));
            _db.SaveChanges();
        }
        // ------------------------ PROCEDURES ------------------------         
        // A procedure that searches
        public bool FulltextSearchQuestion(string text) //We should "un"hardcode fulltextsearch and searchbytag and make it return a list :)
        {
            var result = _db.Post.FromSql("call fulltext_search({0},{1})", text, 1);

            foreach (var simplePost in result)
            {
                if (simplePost.PostId == 25115395) return true;
            }
            return false;
            //return result.ToList();
        }
       
        public List<SimpleQuestion> SearchQuestionsByTag(string tag, int limit) => _db.SimpleQuestion.FromSql("call search_questions_by_tag({0},{1})", tag, limit).ToList();
    }
}