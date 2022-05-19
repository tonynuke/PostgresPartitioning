namespace WebService.Dto
{
    public record PersonDto
    {
        public DateTime BirthDate { get; set; }

        public PassportDto Passport { get; set; }
    }
}
