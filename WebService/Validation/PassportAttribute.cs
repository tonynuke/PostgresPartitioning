using CSharpFunctionalExtensions;
using Domain;
using System.ComponentModel.DataAnnotations;
using WebService.Dto;

namespace WebService.Validation
{
    public sealed class PassportAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            var dto = value as PassportDto;
            return Passport
                .Create(dto!.Series, dto!.Number)
                .Map(_ => ValidationResult.Success)
                .OnFailureCompensate(error => new ValidationResult(error))
                .Value!;
        }
    }
}
