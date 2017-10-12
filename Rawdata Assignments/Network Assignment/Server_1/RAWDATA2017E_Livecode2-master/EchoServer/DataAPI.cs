using System.Linq;
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
            var passed = CheckPath(requestObj, response);
            if (_model.Retrieve(requestObj.Path) != null)
            {
                //The path already exists
                response.Status = "4 Bad Request";
                passed = false;
            }
            else
            {
               passed = CheckBody(requestObj, response);
            }
            if (!passed) return;

            var b =_model.Create(requestObj.Path, JsonConvert.DeserializeObject<Category>(requestObj.Body).Name);
            response.Body = JsonConvert.SerializeObject(b);
        }


        public static void Read(Server.RequestObj requestObj, ref Server.Response response)
        {
            if (requestObj.Path == "/api/categories") //read all
            {
                response.Status = "1 Ok";
                response.Body = JsonConvert.SerializeObject(_model.ReadAll());
            }
            else
            {
                var passed = CheckPath(requestObj, response) || !CheckPathForId(requestObj, response);
                if (!CheckExists(requestObj, response)) passed = false;
                if (!passed) return;

                response.Status = "1 Ok";
                response.Body = JsonConvert.SerializeObject(_model.Retrieve(requestObj.Path));
            }
        }

        public static void Update(Server.RequestObj requestObj, ref Server.Response response)
        {
            var passed = CheckPath(requestObj, response);
            if (!CheckExists(requestObj, response)) passed = false;
            if (!CheckBody(requestObj, response)) passed = false;
            if (!CheckPathForId(requestObj, response)) passed = false;
            if (!passed) return;

            var b = _model.UpdateName(requestObj.Path, JsonConvert.DeserializeObject<Category>(requestObj.Body).Name);
            response.Status += "3 updated";
            response.Body = JsonConvert.SerializeObject(b);
        }

        public static void Delete(Server.RequestObj requestObj, ref Server.Response response)
        {
            var passed = CheckPath(requestObj, response);
            if (!CheckExists(requestObj, response)) passed = false;
            if (!CheckPathForId(requestObj, response)) return;

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

        private static bool CheckPathForId(Server.RequestObj requestObj, Server.Response response)
        {
            if (response.Status != null && response.Status.ToLower().Contains("5 not found")
                && requestObj.Path != null && !requestObj.Path.Any(char.IsDigit))
            {
                response.Status = "4 Bad Request";
                return false;
            }
            return true;
        }

        private static bool CheckExists(Server.RequestObj requestObj, Server.Response response)
        {
            if (_model.Retrieve(requestObj.Path) != null) return true;
            if (!response.Status.Contains("Bad Request"))
                response.Status += "5 not found, ";
            return false;
        }

        private static bool CheckPath(Server.RequestObj requestObj, Server.Response response)
        {
            if (string.IsNullOrEmpty(requestObj.Path))
            {
                response.Status += "missing resource, ";
                return false;
            }
            if (_model.Retrieve(requestObj.Path) == null)
            {
                response.Status = requestObj.Path.Contains("/api/categories") ? "5 Not Found" : "4 Bad Request";
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
