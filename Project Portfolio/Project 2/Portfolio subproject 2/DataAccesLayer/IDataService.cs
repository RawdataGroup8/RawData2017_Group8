using System.Collections.Generic;
using DataAccesLayer.DBObjects;
using DAL.DBObjects;

namespace DataAccesLayer
{
    public interface IDataService
    {
        List<User> GetUsers(int page = 0, int pageSize = 10);
        Post GetPost(int id);
        Post GetPosts_Tags(int id);
        Question GetQuestion(int id);
        Answers GetAnswer(int id);
        User GetUser(int id);
        LinkedPosts LinkingToThisPost(int id);


        List<Post> LinkedFromThisPost(int id);
        //List<Post> LinkingToThisPost(int id);
        List<History> UserHistory(int id);
        List<Marking> UserBookmarks(int id);
        int AddMarking(int uid, int pid, string mark);
        void AddQuestionToHistory(int PostID, int UserID);
    }
}
