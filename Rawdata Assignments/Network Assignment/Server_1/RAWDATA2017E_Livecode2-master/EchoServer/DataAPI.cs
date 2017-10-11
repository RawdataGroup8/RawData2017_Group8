using System;
using System.Collections.Generic;
using System.Text;

namespace Server1
{
    static class DataAPI
    {
        private static DataModel _model;
        private static bool _badRequest;

        public static void InitDataModel()
        {
            _model = new DataModel();
        }

        public static void Create(Server.RequestObj requestObj, ref Server.Response response)
        {
            CheckMissingResource(requestObj, response);
            CheckMissingBody(requestObj, response);
            if (_model.Retrieve(requestObj.Path) != null) BadRequest(response);
            
        }


        public static void Read(Server.RequestObj requestObj, ref Server.Response response)
        {
            CheckMissingResource(requestObj, response);
        }

        public static void Update(Server.RequestObj requestObj, ref Server.Response response)
        {
            CheckMissingResource(requestObj, response);
            CheckMissingBody(requestObj, response);
        }

        public static void Delete(Server.RequestObj requestObj, ref Server.Response response)
        {
            CheckMissingResource(requestObj, response);
        }

        public static void Echo(Server.RequestObj requestObj, ref Server.Response response)
        {
            _badRequest = false;
            //CheckMissingResource(requestObj, response); if (_badRequest) return;
            CheckMissingBody(requestObj, response); if (_badRequest) return;
            response.Body = requestObj.Body;
        }

        ////////// Private methods //////////
        private static void CheckMissingResource(Server.RequestObj requestObj, Server.Response response)
        {
            if (string.IsNullOrEmpty(requestObj.Path)) response.Status += "missing resource, ";
            else if (_model.Retrieve(requestObj.Path) == null) BadRequest(response);
        }

        private static void CheckMissingBody(Server.RequestObj requestObj, Server.Response response)
        {
            if (string.IsNullOrEmpty(requestObj.Body)) response.Status += "missing body, ";
            else if (requestObj.Body[0] != '{') response.Status += "illegal body, ";

        }

        private static void BadRequest(Server.Response response)
        {
            response.Status = "4 Bad Request";
            _badRequest = true;
        }

    }


}
