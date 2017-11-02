using System;
using System.ComponentModel.DataAnnotations;
using Box.Api.Extensions;

namespace Box.Api.Validation
{
    public class StringLength : ValidationAttribute
    {
        public StringLength( int minLength, int maxLength, string message ) : this( minLength, maxLength )
        {
            Message = message;
        }

        public StringLength( int minLength, int maxLength )
        {
            if ( minLength > maxLength )
            {
                throw new ArgumentException( $"{nameof(minLength)} cannot be larger than {nameof(maxLength)}" );
            }
            if ( minLength < 0 )
            {
                throw new ArgumentException( $"{nameof(minLength)} cannot be less than 0" );
            }
            if ( maxLength < 0 )
            {
                throw new ArgumentException( $"{nameof(maxLength)} cannot be less than 0" );
            }
            MinLength = minLength;
            MaxLength = maxLength;
            Message = $"Length has to be between {minLength} and {maxLength} Characters";
        }

        private string Message { get; }

        private int MinLength { get; }

        private int MaxLength { get; }

        /// <summary>Validates the specified value with respect to the current validation attribute.</summary>
        /// <param name="value">The value to validate.</param>
        /// <param name="validationContext">The context information about the validation operation.</param>
        /// <returns>An instance of the <see cref="T:System.ComponentModel.DataAnnotations.ValidationResult"></see> class.</returns>
        protected override ValidationResult IsValid( object value, ValidationContext validationContext )
        {
            if ( value is string str && str.Length >= MinLength && str.Length <= MaxLength )
            {
                return ValidationResult.Success;
            }

            return new ValidationResult( Message, new[] { validationContext.MemberName.FirstToLower() } );
        }
    }
}