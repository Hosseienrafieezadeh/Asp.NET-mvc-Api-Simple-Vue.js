using FreeBilling.Web.Data;
using FreeBilling.Web.Data.Entitis;
using Microsoft.AspNetCore.Mvc;

namespace FreeBilling.Web.Controllers
{
    [Route("/api/customers/{id:int}/timebills")]
    public class CustomerTimeBillsController : ControllerBase
    {
        private readonly ILogger<CustomerTimeBillsController> _logger;
        private readonly IBillingRepozitory _repozitory;


        public CustomerTimeBillsController(ILogger<CustomerTimeBillsController> logger,IBillingRepozitory repozitory)
        {
            _logger = logger;
            _repozitory = repozitory;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<TimeBill>>> GetCustomerTimeBills(int id)
        {
            var result = await _repozitory.GetTimebillsForCustomer(id);
            if (result is not null) return Ok(result);
            return BadRequest();
        }
        [HttpGet("{billId:int}")]
        public async Task<ActionResult<IEnumerable<TimeBill>>> GetCustomerTimeBills(int id,int billId)
        {
            var result = await _repozitory.GetTimebillForCustomer(id,billId);
            if (result is not null) return Ok(result);
            return BadRequest();
        }
    }
}
