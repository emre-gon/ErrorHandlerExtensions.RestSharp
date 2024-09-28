using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace RestSharp
{
    public class ApiException : RestException<ApiErrorModel>
    {
        public ApiException(RestResponse ResponseContent)
            : base(ResponseContent)
        {

        }
        public ApiException(RestResponse ResponseContent, string Message)
            : base(ResponseContent, Message)
        {

        }

        public Dictionary<string, List<string>> Errors
        {
            get
            {                
                return ErrorData.Errors;
            }
        }

        public string RequestID
        {
            get
            {
                return ErrorData.RequestID;
            }
        }
        public string ErrorMessage
        {
            get
            {
                return ErrorData.ErrorMessage;
            }
        }
        //public string Type
        //{
        //    get
        //    {
        //        return ErrorData.Type;
        //    }
        //}

    }
}
