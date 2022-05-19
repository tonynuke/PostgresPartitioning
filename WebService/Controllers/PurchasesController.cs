using Microsoft.AspNetCore.Mvc;
using Domain;
using Persistence;
using WebService.Dto;

namespace WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchasesController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public PurchasesController(ApplicationContext context)
        {
            _context = context;
        }

        // POST: api/Purchases
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Purchase>> PostPurchase(PurchaseDto dto)
        {
            var purchase = new Purchase(dto.PersonId, dto.DateTime);

            _context.Purchases.Add(purchase);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPurchase", new { id = purchase.Id }, purchase);
        }
    }
}
