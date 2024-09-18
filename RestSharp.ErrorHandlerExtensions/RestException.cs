using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace RestSharp
{
    public class RestException : Exception
    {
        public RestException(RestResponse RestResponse)
            :this(RestResponse, "Exception occured during REST call. See 'RestResponse.Content' for potential reasons.")
        {

        }


        public RestException(RestResponse RestResponse, string Message)
            :base(Message)
        {
            this.RestResponse = RestResponse;
            this.Status = RestResponse.StatusCode;
        }


        public RestResponse RestResponse { get; }

        public HttpStatusCode Status { get; }
    }



    public class RestException<ExceptionDataModel> : RestException
    {
        public RestException(RestResponse RestResponse)
            : this(RestResponse, "Exception occured during REST call. See 'ErrorData' and 'RestResponse.Content' for potential reasons.")
        {

        }

        public string ErrorJsonParseResult { get; set; }
        public Exception ErrorJsonParseException { get; }

        public RestException(RestResponse RestResponse, string Message) : base(RestResponse, Message)
        {
            try
            {
                this.ErrorData = JsonConvert.DeserializeObject<ExceptionDataModel>(RestResponse.Content);
                this.ErrorJsonParseResult = "Error JSON model is parsed.";
            }
            catch(Exception ex)
            {
                this.ErrorJsonParseResult = "Error JSON could not be parsed into. " + typeof(ExceptionDataModel).FullName + ". See 'ErrorJsonParseException' for details.";
                this.ErrorJsonParseException = ex;
            }
        }

        public ExceptionDataModel ErrorData { get; }
    }

}
