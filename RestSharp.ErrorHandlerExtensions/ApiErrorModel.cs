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
            Title = Message;
            this.Status = Status;
        }




        public Dictionary<string, List<string>> Errors { get; set; }

        public string TraceId { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }

        public HttpStatusCode Status { get; set; }
    }
}
