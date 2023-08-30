using WebService.Validation;

namespace WebService.Dto
{
    [Passport]
    public record PassportDto(string Series, string Number);
}
