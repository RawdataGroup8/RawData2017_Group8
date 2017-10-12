using System.Linq;
using Newtonsoft.Json;

namespace Server1
{
    internal static class DataAPI
    {
        private static DataModel _model;

        public static void InitDataModel() => _model = new DataModel();

        public static void Create(Server.RequestObj requestObj, ref Server.Response response, bool violated)
        {            
            //Initialize violatedConstraint and check date 
            var violatedConstraint = violated;

            //Missing body
            violatedConstraint = MissingBody(requestObj, response, violatedConstraint);

            //Missing path/resource
            violatedConstraint = MissingResource(requestObj, response, violatedConstraint);
            if (violatedConstraint) return;

            //If id is already present
            if (AlreadyExists(requestObj, response)) return;

            var b = _model.Create(requestObj.Path, JsonConvert.DeserializeObject<Category>(requestObj.Body).Name);
            response.Body = JsonConvert.SerializeObject(b);
        }

        public static void Read(Server.RequestObj requestObj, ref Server.Response response, bool violated)
        {
            //Check if path is present
            if (MissingResource(requestObj, response)) return;

            //Check if it is a readAll call
            if (requestObj.Path == "/api/categories") //(slightly hacky i suppose?)
            {
                response.Status = "1 Ok";
                response.Body = JsonConvert.SerializeObject(_model.ReadAll());
                return;
            }

            //Check if path is valid
            if (!PathIsValid(requestObj, response)) return;

            //If exists, retrieve and return it
            var category = _model.Retrieve(requestObj.Path);
            if (category != null)
            {
                response.Status = "1 Ok";
                response.Body = JsonConvert.SerializeObject(category);
            }
            else
            {
                response.Status += "5 not found, ";
            }
        }

        public static void Update(Server.RequestObj requestObj, ref Server.Response response, bool violated)
        {
            var violatedConstraint = violated;

            //Check if path is present
            violatedConstraint = MissingResource(requestObj, response, violatedConstraint);

            //Check for missing body
            violatedConstraint = MissingBody(requestObj, response, violatedConstraint);

            //Check for illegal body
            violatedConstraint = IllegalBody(requestObj, response, violatedConstraint);
            if (violatedConstraint) return;

            //------------------ API tests ------------------
            //Check if path is valid
            if (!PathIsValid(requestObj, response)) return; //actually a constraint but returns only "4 Bad Request"

            //Check if exists in "database" 
            if (!Exists(requestObj, response)) return;

            //Update category
            var b = _model.UpdateName(requestObj.Path, JsonConvert.DeserializeObject<Category>(requestObj.Body).Name);
            response.Status += "3 updated";
            response.Body = JsonConvert.SerializeObject(b);
        }

        public static void Delete(Server.RequestObj requestObj, ref Server.Response response, bool violated)
        {
            if (MissingResource(requestObj, response)) return;

            if (!PathIsValid(requestObj, response)) return;

            if(!Exists(requestObj, response)) return;

             _model.Delete(requestObj.Path);
             response.Status += "1 ok";
            
            _model.PrintModel();           
        }

        public static void Echo(Server.RequestObj requestObj, ref Server.Response response, bool violated)
        {
            MissingBody(requestObj, response);
            response.Body = requestObj.Body;
        }

        ////////// Private methods //////////
        /// 

        private static bool Exists(Server.RequestObj requestObj, Server.Response response)
        {
            if (_model.Retrieve(requestObj.Path) != null) return true;
            response.Status += "5 not found, ";
            return false;
        }

        private static bool AlreadyExists(Server.RequestObj requestObj, Server.Response response)
        {
            if (_model.Retrieve(requestObj.Path) == null) return false;
            response.Status = response.Status = "4 Bad Request";
            return true;
        }

        private static bool PathIsValid(Server.RequestObj requestObj, Server.Response response)
        {
            if (requestObj.Path == null ||
                (requestObj.Path.Contains("/api/categories") && requestObj.Path.Any(char.IsDigit))) return true;
            response.Status = "4 Bad Request";
            return false;
        }

        private static bool MissingBody(Server.RequestObj requestObj, Server.Response response, bool violatedConstraint)
        {
            if (requestObj.Body == null)
            {
                response.Status += "missing body, ";
                violatedConstraint = true;
            }
            return violatedConstraint;
        }

        private static bool MissingBody(Server.RequestObj requestObj, Server.Response response)
        {
            if (requestObj.Body != null) return true;
            response.Status += "missing body, ";
            return true;
        }

        private static bool IllegalBody(Server.RequestObj requestObj, Server.Response response, bool violatedConstraint)
        {
            if (!violatedConstraint && !requestObj.Body.StartsWith("{"))
            {
                response.Status += "illegal body, ";
                violatedConstraint = true;
            }
            return violatedConstraint;
        }

        private static bool MissingResource(Server.RequestObj requestObj, Server.Response response, bool violatedConstraint)
        {
            if (requestObj.Path == null)
            {
                response.Status += "missing resource, ";
                violatedConstraint = true;
            }
            return violatedConstraint;
        }
        private static bool MissingResource(Server.RequestObj requestObj, Server.Response response)
        {
            if (requestObj.Path != null) return false;
            response.Status += "missing resource, ";
            return true;
        }
    }
}
