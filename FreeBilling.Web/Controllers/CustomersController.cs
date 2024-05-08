using FreeBilling.Web.Data;
using FreeBilling.Web.Data.Entitis;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreeBilling.Web.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("/api/[controller]")]
    [ApiController]
    public class CustomersController:ControllerBase
    {
        private readonly IBillingRepozitory _repozitory;
        private readonly ILogger _logger;
        public CustomersController(ILogger<CustomersController> logger,IBillingRepozitory repozitory)
        {
            _repozitory = repozitory;
            _logger = logger;
        }
        
        [HttpGet("")]
        public async Task<ActionResult< IEnumerable<Customer>>> Get( bool withAddresses=false)
        {
            try
            {
                IEnumerable<Customer> results;
                if (withAddresses)
                {
                    results = await _repozitory.GetCustomersWithAddresses();
                }
                else
                {
                    results = await _repozitory.GetCustomers();
                }
                return Ok( results);
            }
            catch (Exception )
            {
                _logger.LogError("fell to get customer from database.");
                return Problem("fell to get customer from database.");
            }
           
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult< Customer>> GetOne(int id)
        {
            try
            {
                var result = await _repozitory.GetCustomer(id);
                if (result is null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception thrown while reading customer");
                return Problem($"Exception throw:{ex.Message}"); 
               
            }

          
        }
    
    }
}
