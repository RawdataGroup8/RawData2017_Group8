using System;
using System.Collections.Generic;
using System.Text;
using DAL.DBObjects;


namespace DAL
{
    public interface IDataService
    {
        //List<User> GetUsers();
        List<User> GetUsers(int page = 0, int pageSiez = 10);
        Post GetPost(int id);
        User GetUser_Posts(int id);
    }
}
