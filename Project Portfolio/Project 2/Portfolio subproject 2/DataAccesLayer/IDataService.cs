using System.Collections.Generic;
using DataAccesLayer.DBObjects;
using DAL.DBObjects;

namespace DataAccesLayer
{
    public interface IDataService
    {
        //List<User> GetUsers();
        Question GetQuestion(int id);
        List<User> GetUsers(int page = 0, int pageSiez = 10);
        Post GetPost(int id);
        User GetUser(int id);
    }
}
