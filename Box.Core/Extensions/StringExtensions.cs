using System;

namespace Box.Core.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }
        
        public static string FirstCharToLower( this string @string )
        {
            return char.ToLower( @string[0] ) + @string.Substring( 1, @string.Length - 1 );
        }

        public static Guid ToGuid(this string s)
        {
            return new Guid(s);
        }
    }
}