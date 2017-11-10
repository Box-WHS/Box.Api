using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace Box.Api.Controllers.Results
{
    public class InternalServerErrorResult : StatusCodeResult
    {
        public InternalServerErrorResult(  ) : base( (int) HttpStatusCode.InternalServerError )
        {
        }
    }

    public class InternalServerErrorObjectResult : ObjectResult
    {
        public InternalServerErrorObjectResult( object value ) : base( value )
        {
            StatusCode = (int?) HttpStatusCode.InternalServerError;
        }
    }
}