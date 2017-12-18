using System;
using System.Collections.Generic;
using System.Linq;
using DataAccesLayer.DBObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.ExpressionTranslators.Internal;

namespace DataAccesLayer
{
    public class DataService : IDataService
    {
        //private readonly StackoverflowContext _db;

        //public DataService() => _db = new StackoverflowContext();

        // ------------------------ USERS  ------------------------  
        //public List<User> GetUsers() => _db.User.ToList();
        public List<User> GetUsers(int page, int pageSize)
        {
            using (var db = new StackoverflowContext())
            {
                return db.User.OrderBy(x => x.Userid).Skip(page * pageSize).Take(pageSize).ToList();
            }
        }

        //return count of numbers
        public int GetUserCount()
        {
            using (var db = new StackoverflowContext())
            {
                return db.User.Count();
            }
        }

        //return a full user, including all posts.
        public User GetUser(int id)
        {
            using (var db = new StackoverflowContext())
            {
                return db.User.Include(c => c.Posts).FirstOrDefault(p => p.Userid == id);
            }
        }


        // get all users owning the post-id
        public List<Post> GetPostsUser(int id)
        {
            using (var db = new StackoverflowContext())
            {
                return db.Post.Where(p => p.OwnerUserId == id).ToList();
            }
        }
        // ------------------------ POSTS (QUESTIONS / ANSWERS) ------------------------   

        public List<Post> GetPosts(int page, int pageSize)
        {
            using (var db = new StackoverflowContext())
            {
                return db.Post
                    .Skip(page * pageSize)
                    .Take(pageSize)
                    .ToList();
            }
        }

        public List<Answers> GetAnswers(int id)
        {
            using (var db = new StackoverflowContext())
            {
                var answers = db.Answer.Where(x => x.Parentid == id).ToList();
                foreach (var answer in answers)
                {
                    answer.SetPost(db.Post.FirstOrDefault(p => p.PostId == answer.PostId));
                }
                return answers;
            }
        }

        public int NumberOfQuestions()
        {
            using (var db = new StackoverflowContext())
            {
                return db.Post.Count(x => x.TypeId == 1);
            }
        }

        //return a full post, including all comments and tags. 
        public Post GetPost(int id)
        {
            using (var db = new StackoverflowContext())
            {
                return db.Post.Include(p1 => p1.Comments).Include(p1 => p1.PostTags)
                    .FirstOrDefault(p => p.PostId == id);
            }
        }

        //return a Post, including tags related to the post.
        public Post GetPosts_Tags(int id)
        {
            using (var db = new StackoverflowContext())
            {
                return db.Post.Include(c => c.PostTags).FirstOrDefault(p => p.PostId == id);
            }
        }

        //return a full question
        public Question GetQuestion(int id)
        {
            using (var db = new StackoverflowContext())
            {
                var question = db.Question.FirstOrDefault(q => q.PostId1 == id);
                question?.SetPost(GetPost(id));
                return question;
            }
        }

        public List<NewestQuestions> GetNewestQuestions(int page, int pageSize)
        {
            using (var db = new StackoverflowContext())
            {
                //var posts = db.Post.Where(p => p.TypeId == 1 /*&& p.CreationDate>DateTime.Now.AddDays(-1027)*/).OrderByDescending(q => q.CreationDate)
                //    .Skip(page * pageSize).Take(pageSize).ToList();
                var posts = db.NewestQuestions.Skip(page * pageSize).Take(pageSize).ToList();
                return posts;//.Select(post => GetQuestion(post.PostId)).ToList();
            }
        }

        //return a full answer
        public Answers GetAnswer(int id)
        {
            using (var db = new StackoverflowContext())
            {
                var answer = db.Answer.FirstOrDefault(q => q.PostId == id);
                answer?.SetPost(GetPost(id));
                return answer;
            }
        }
        // ------------------------ LINKS ------------------------         

        public List<LinkedPosts> LinkedFromThisPost(int id)
        {
            using (var db = new StackoverflowContext())
            {
                return db.LinkedPosts.Where(lp => lp.PostId == id).ToList();
            }
        }

        public List<LinkedPosts> LinkingToThisPost(int id)
        {
            using (var db = new StackoverflowContext())
            {
                return db.LinkedPosts.Where(lp => lp.PostId == id).ToList();
            }
        }


        // ------------------------ HISTORY & BOOKMARKS ------------------------         
        public List<History> UserHistory(int id)
        {
            using (var db = new StackoverflowContext())
            {
                return db.History.Where(h => h.Userid == id).ToList();
            }
        }

        public List<Marking> UserBookmarks(int id)
        {
            using (var db = new StackoverflowContext())
            {
                return db.Marking.Where(m => m.Postid == id).ToList();
            }
        }

        public int AddMarking(int uid, int pid, string mark)
        {
            using (var db = new StackoverflowContext())
            {
                return db.Database.ExecuteSqlCommand("call add_marking({0},{1},{2})", uid, pid, mark);
            }
        }

        public int AddQuestion(int uid, string body, string title)
        {
            using (var db = new StackoverflowContext())
            {
                return db.Database.ExecuteSqlCommand("call add_question({0},{1},{2})", uid, body, title);
            }
        }

        public void DeleteMarking(int uid, int pid)
        {
            using (var db = new StackoverflowContext())
            {
                db.Database.ExecuteSqlCommand("delete from marking where user_id = {0} and post_id = {1}", uid, pid);
            }
        }

        public List<History> GetHistory()
        {
            using (var db = new StackoverflowContext())
            {
                return db.History.ToList();
            }
        }

        public History GetHistoryItem(int userId, int linkPostId)
        {
            using (var db = new StackoverflowContext())
            {
                return db.History.FirstOrDefault(h => h.Userid == userId && h.LinkPostId == linkPostId);
            }
        }

        public void AddQuestionToHistory(int userId, int postId)
        {
            using (var db = new StackoverflowContext())
            {
                db.History.Add(new History(userId, postId));
                db.SaveChanges();
            }
        }

        public void RemoveQuestionFromHistory(int userId, int linkPostId)
        {
            using (var db = new StackoverflowContext())
            {
                db.History.Remove(GetHistoryItem(userId, linkPostId));
                db.SaveChanges();
            }
        }
        // ------------------------ PROCEDURES ------------------------       


        public List<RankedQuestions> RankedPostSearch(string terms, int page, int pageSize)
        {
            using (var db = new StackoverflowContext())
            {
                if (string.IsNullOrEmpty(terms)) terms = "Python Dictionary"; //only for testing, parameter shpuld be used
                terms = terms.Replace(" ", ", ");
                var posts = db.RankedQuestions.FromSql("call ranked_post_search({0})", terms).OrderByDescending(q => q.Rank)
                    .Skip(page * pageSize).Take(pageSize).ToList();
                /*foreach (var p in posts)
                {
                    p.Question = GetQuestion(p.Id);
                }*/
                return posts;//.Select(post => GetQuestion(post.Id)).ToList();

            }
        }

        public List<RankedWords> RankedWordsSearch(string terms, int page, int pageSize)
        {
            using (var db = new StackoverflowContext())
            {
                if (string.IsNullOrEmpty(terms)) terms = "Python Dictionary"; //only for testing, parameter shpuld be used
                terms = terms.Replace(" ", ", ");
                var posts = db.RankedWords.FromSql("call ranked_words({0})", terms).OrderByDescending(q => q.Rank).ToList();
                    //.Skip(page * pageSize).Take(pageSize).ToList();
                /*foreach (var p in posts)
                {
                    p.Question = GetQuestion(p.Id);
                }*/
                return posts;//.Select(post => GetQuestion(post.Id)).ToList();

            }
        }

        public List<Post> BestMatch(string text)
        {
            using (var db = new StackoverflowContext())
            {
                return
                    db.Post.FromSql("call bestmatch({0})")
                        .ToList(); // the words in the text needs to be coma seperated and the string needs to be max 5000 in length for this  to work
            }
        }

        public List<Question> SearchQuestionsByTag(string tag, int limit)
        {
            using (var db = new StackoverflowContext())
            {
                return db.Question.FromSql("call search_questions_by_tag({0},{1})", tag, limit).ToList();
            }
        }

        //----- For the wordcloud -----//
        public List<WordIndex> GetTfOfWordsInAPost(int id)
        {
            using (var db = new StackoverflowContext())
            {
                return db.WordIndex.Where(i => i.Id == id)
                    .GroupBy(test => test.Word)
                    .Select(grp => grp.First())
                    .ToList();
            }
        }
    }
}