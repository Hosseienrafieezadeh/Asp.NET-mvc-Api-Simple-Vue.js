using FreeBilling.Web.Data;
using FreeBilling.Web.Data.Entitis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FreeBilling.Web.Pages
{
    public class IndexModel : PageModel
    {
    
        private readonly IBillingRepozitory _repozitory;

        public IndexModel(IBillingRepozitory repozitory)
        {
       
           _repozitory = repozitory;
        }
        public IEnumerable<Customer?>Customers { get; set; }
        public async Task  OnGetAsync()
        {
          Customers=await  _repozitory.GetCustomers();

        }
    }
}
