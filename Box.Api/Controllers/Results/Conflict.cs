using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace Box.Api.Controllers.Results
{
    public class ConflictResult : StatusCodeResult
    {
        public ConflictResult() : base( (int) HttpStatusCode.Conflict )
        {
        }
    }

    public class ConflictObjectResult : ObjectResult
    {
        public ConflictObjectResult( object value ) : base( value )
        {
            StatusCode = (int?) HttpStatusCode.Conflict;
        }
    }
}