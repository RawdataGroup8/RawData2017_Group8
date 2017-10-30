using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.DBObjects;

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
            
    }
}