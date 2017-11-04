using System;
using System.Collections.Generic;
using System.Text;
using DAL.DBObjects;


namespace DAL
{
    public interface IDataService
    {
        List<User> GetUsers();
        Post GetPost(int id);
        User GetUser_Posts(int id);
    }
}
