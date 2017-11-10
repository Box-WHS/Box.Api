using System.Net;

namespace Box.Api.Controllers.Results
{
    public static class Result
    {
        /// <summary>
        ///     Returns a <see cref="InternalServerErrorObjectResult" />, with a value, which represents a
        ///     <see cref="HttpStatusCode.InternalServerError" />(500)
        /// </summary>
        /// <param name="value">Value which will be provided in the body</param>
        /// <returns>The created <see cref="InternalServerErrorObjectResult" /> for the response</returns>
        public static InternalServerErrorObjectResult InternalServerError( object value )
        {
            return new InternalServerErrorObjectResult( value );
        }

        /// <summary>
        ///     Returns a <see cref="InternalServerErrorResult" />, with a value, which represents a
        ///     <see cref="HttpStatusCode.InternalServerError" />(500)
        /// </summary>
        /// <returns>The created <see cref="InternalServerErrorResult" /> for the response</returns>
        public static InternalServerErrorResult InternalServerError()
        {
            return new InternalServerErrorResult();
        }

        /// <summary>
        ///     Returns a <see cref="ConflictResult" />, with a value, which represents a <see cref="HttpStatusCode.Conflict" />
        ///     (409)
        /// </summary>
        /// <param name="value">Value which will be provided in the body</param>
        /// <returns>The created <see cref="ConflictObjectResult" /> for the response</returns>
        public static ConflictObjectResult Conflict( object value )
        {
            return new ConflictObjectResult( value );
        }

        /// <summary>
        ///     Returns a <see cref="ConflictResult" /> which represents a <see cref="HttpStatusCode.Conflict" />(409)
        /// </summary>
        /// <returns>The created <see cref="ConflictResult" /> for the response</returns>
        public static ConflictResult Conflict()
        {
            return new ConflictResult();
        }
    }
}