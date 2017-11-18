using System;
using System.Security.Claims;

namespace Box.Core.Extensions
{
    public static class ClaimTypes
    {
        public const string Sub = "sub";
    }
    
    public static class UserExtensions
    {
        
        
        public static Guid GetId(this ClaimsPrincipal user)
        {
            var sub = user.FindFirst(ClaimTypes.Sub);
            return new Guid(sub.Value.ToString());
        }
    }
}