using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Box.Api.Extensions;

namespace Box.Api.Validation
{
    public class ValidGuidAttribute : ValidationAttribute
    {
        private const string GuidRegex = @"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$";

        public ValidGuidAttribute( string message )
        {
            Message = message;
        }

        public ValidGuidAttribute()
        {
            Message = "Provide a valid GUID";
        }

        private string Message { get; }

        /// <summary>Validates the specified value with respect to the current validation attribute.</summary>
        /// <param name="value">The value to validate.</param>
        /// <param name="validationContext">The context information about the validation operation.</param>
        /// <returns>An instance of the <see cref="T:System.ComponentModel.DataAnnotations.ValidationResult"></see> class.</returns>
        protected override ValidationResult IsValid( object value, ValidationContext validationContext )
        {
            if ( value != null && !Regex.IsMatch( value.ToString(), GuidRegex ) )
            {
                return new ValidationResult( Message, new[] { validationContext.MemberName.FirstToLower() } );
            }
            return ValidationResult.Success;
        }
    }
}