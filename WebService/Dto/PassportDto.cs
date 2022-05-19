using WebService.Validation;

namespace WebService.Dto
{
    [Passport]
    public record PassportDto
    {
        public string Series { get; set; }

        public string Number { get; set; }
    }
}
