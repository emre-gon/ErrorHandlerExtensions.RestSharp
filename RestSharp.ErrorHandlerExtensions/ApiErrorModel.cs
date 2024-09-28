using System.Collections.Generic;
using System.Net;

namespace RestSharp
{
    public class ApiErrorModel
    {
        public ApiErrorModel()
        {

        }


        public ApiErrorModel(HttpStatusCode Status, string Message)
        {
            ErrorMessage = Message;
            this.Status = Status;
        }




        public Dictionary<string, List<string>> Errors { get; set; }

        public string RequestID { get; set; }
        public string ErrorMessage { get; set; }
        //public string Type { get; set; }

        public HttpStatusCode Status { get; set; }
    }
}
