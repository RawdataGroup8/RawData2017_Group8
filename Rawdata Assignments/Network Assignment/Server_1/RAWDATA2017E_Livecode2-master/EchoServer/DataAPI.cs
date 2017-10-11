using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;


namespace Server1
{
    static class DataAPI
    {
        private static DataModel _model;

        public static void InitDataModel()
        {
            _model = new DataModel();
        }

        public static void Create(Server.RequestObj requestObj, ref Server.Response response)
        {
            var passed = false;
            passed = CheckPath(requestObj, response);
            passed = CheckBody(requestObj, response);
            if (_model.Retrieve(requestObj.Path) != null)
            {
                response.Status = "4 Bad Request";
                passed = false;
            }
            if (passed)
            {
                var b =_model.Create(requestObj.Path, JsonConvert.DeserializeObject<Category>(requestObj.Body).Name);
                response.Body = JsonConvert.SerializeObject(b);
            }
        }


        public static void Read(Server.RequestObj requestObj, ref Server.Response response)
        {
            CheckPath(requestObj, response);

        }

        public static void Update(Server.RequestObj requestObj, ref Server.Response response)
        {
            CheckPath(requestObj, response);
            CheckBody(requestObj, response);
        }

        public static void Delete(Server.RequestObj requestObj, ref Server.Response response)
        {
            bool passed;
            passed = CheckPath(requestObj, response);
            if (passed && _model.Retrieve(requestObj.Path) == null)
            {
                response.Status += "5 not found, ";
                passed = false;
            }

            if (passed)
            {
                _model.Delete(requestObj.Path);
                response.Status += "1 ok";
            }
            _model.PrintModel();
            
        }

        public static void Echo(Server.RequestObj requestObj, ref Server.Response response)
        {
            //_badRequest = false;
            CheckBody(requestObj, response); //if (_badRequest) return;
            response.Body = requestObj.Body;
        }

        ////////// Private methods //////////
        private static bool CheckPath(Server.RequestObj requestObj, Server.Response response)
        {
            if (string.IsNullOrEmpty(requestObj.Path))
            {
                response.Status += "missing resource, ";
                return false;
            }
            if (_model.Retrieve(requestObj.Path) == null)
            {
                response.Status = "4 Bad Request";
                return false;
            }
            return true;
        }

        private static bool CheckBody(Server.RequestObj requestObj, Server.Response response)
        {
            if (string.IsNullOrEmpty(requestObj.Body))
            {
                response.Status += "missing body, ";
                return false;
            }
            if (requestObj.Body[0] != '{')
            {
                response.Status += "illegal body, ";
                return false;
            }
            return true;

        }


    }


}
