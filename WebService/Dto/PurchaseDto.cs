namespace WebService.Dto
{
    public record PurchaseDto
    {
        public long PersonId { get; set; }

        public DateTime DateTime { get; set; }
    }
}
