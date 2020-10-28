using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Application.ErrorHandler
{
    public class ExceptionHandler: Exception
    {
        public HttpStatusCode Code { get; }
        public object Error { get; }
        public ExceptionHandler(HttpStatusCode code, object error = null)
        {
            this.Code = code;
            this.Error = error;
        }
    }
}
