using FreeBilling.Web.Data.Entitis;
using Microsoft.EntityFrameworkCore;

namespace FreeBilling.Web.Data
{
    public class BillingRepozitory : IBillingRepozitory
    {
        private readonly BillingContext _context;
        private readonly ILogger<BillingRepozitory> _logger;

        public BillingRepozitory(BillingContext context,ILogger<BillingRepozitory> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<TimeBill?> GetTimebill(int id)
        {
          var  bill=await _context.TimeBills
              .Include(_=>_.Employee)
              .Include(_=>_.Customer)
              .ThenInclude(_=>_!.Address)
              .Where(_=>_.Id==id)
              .FirstOrDefaultAsync();
          return bill;
        }

        public async Task<List<Employee>> GetEmployess()
        {
            try
            {
            return await _context.Employees.OrderBy(_ => _.Name).ToListAsync();

            }
            catch (Exception ex)
            {

                _logger.LogError($"could not get employees:{ex.Message}");
                throw;
            }
        }
        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            try
            {
            return await _context.Customers.OrderBy(_ => _.CompanyName).ToListAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError($"could not get Customers:{ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<Customer>> GetCustomersWithAddresses()
        {
            try
            {
                return await _context.Customers
                    .Include(_=>_.Address)
                    .OrderBy(_ => _.CompanyName)
                    .ToListAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError($"could not get Customers:{ex.Message}");
                throw;
            }
        }

        public async Task<Customer?> GetCustomer(int id)
        {
            try
            {
                return await _context.Customers.
                    Where(_=>_.Id==id)
                   .FirstOrDefaultAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError($"could not get Customers:{ex.Message}");
                throw;
            }
        }
        public async Task<bool> saveChanges()
        {
            try
            {
            return await _context.SaveChangesAsync() > 0;

            }
            catch (Exception ex)
            {
                _logger.LogError($"could not save to the data base :{ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<TimeBill>> GetTimebillsForCustomer(int id)
        {
            return await _context.TimeBills
                .Where(_ =>_.CustomerId!=null&& _.CustomerId == id)
                .Include(_ => _.Customer)
                .Include(_ => _.Employee).ToListAsync();

                  ;
        }

        public  async Task<Employee?> GetEmployee(string Name)
        {
            return await _context.Employees.Where(_ => _.Email == Name).FirstOrDefaultAsync();
        }

        public async Task<TimeBill?> GetTimebillForCustomer(int id, int billId)
        {
            return await _context.TimeBills
                .Where(_ => _.CustomerId != null && _.CustomerId == id&&_.Id==billId)
                .Include(_ => _.Customer)
                .Include(_ => _.Employee).FirstOrDefaultAsync();
        }

        public void AddEntity<T>(T entiry) where T:notnull
        {
            _context.Add(entiry);
        }
    }
}
