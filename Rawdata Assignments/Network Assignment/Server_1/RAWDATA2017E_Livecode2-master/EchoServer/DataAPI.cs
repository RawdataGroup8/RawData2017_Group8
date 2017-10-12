using System.Linq;
using Newtonsoft.Json;

namespace Server1
{
    internal static class DataAPI
    {
        private static DataModel _model;

        public static void InitDataModel() => _model = new DataModel();

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
            //------------------ Constraints ----------------
            //Check if path is present
            if (string.IsNullOrEmpty(requestObj.Path)) //If there is nothing in the path, add 'missing resource'
            {
                response.Status += "missing resource, ";
                return;
            }

            //------------------ API tests ------------------
            //First check if it is a readAll call
            if (requestObj.Path == "/api/categories")
            {
                response.Status = "1 Ok";
                response.Body = JsonConvert.SerializeObject(_model.ReadAll());
                return;
            }

            //Else, check if path is valid
            if (!requestObj.Path.Contains("/api/categories") || !requestObj.Path.Any(char.IsDigit)) //Must be valid eg. contain "/api/categories", and must contain a number (the id)
            {
                response.Status = "4 Bad Request";
                return;
            }

            //If not, check if exists
            if (Exists(requestObj, response))
            {
                response.Status = "1 Ok";
                response.Body = JsonConvert.SerializeObject(_model.Retrieve(requestObj.Path));
            }

            //If it does, retrieve and return it

            /*if (requestObj.Path == "/api/categories") //read all (slightly hacky, i know) ;)
            {
                response.Status = "1 Ok";
                response.Body = JsonConvert.SerializeObject(_model.ReadAll());
            }
            else
            {
                var passed = CheckPath(requestObj, response);
                    passed = CheckPathForId(requestObj, response);
                if (!Exists(requestObj, response)) passed = false;
                if (!passed) return;

                response.Status = "1 Ok";
                response.Body = JsonConvert.SerializeObject(_model.Retrieve(requestObj.Path));
            }*/
        }

        public static void Update(Server.RequestObj requestObj, ref Server.Response response)
        {
            var passed = CheckPath(requestObj, response);
            if (!Exists(requestObj, response)) passed = false;
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
            //if (!Exists(requestObj, response)) passed = false;
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
            CheckBody(requestObj, response);
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

        private static bool Exists(Server.RequestObj requestObj, Server.Response response)
        {
            if (_model.Retrieve(requestObj.Path) != null) return true;
            //if (!response.Status.Contains("Bad Request"))
                response.Status += "5 not found, ";
            return false;
        }

        private static bool CheckPath(Server.RequestObj requestObj, Server.Response response)
        {
            if (string.IsNullOrEmpty(requestObj.Path)) //CONSTRAINT: if there is nothing in the path, add 'missing resource'
            {
                response.Status += "missing resource, ";
                return false;
            }

            if (_model.Retrieve(requestObj.Path) == null) //API: 
            {
                if (requestObj.Path.Contains("/api/categories")) response.Status += "5 Not Found"; //if its a valid path but the id doesnt exist
                else response.Status = "4 Bad Request"; //invalid path
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
