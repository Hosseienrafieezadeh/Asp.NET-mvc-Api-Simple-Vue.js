using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FreeBilling.Web.Pages
{
    [Authorize]
    public class CustomerModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
