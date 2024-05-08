using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FreeBilling.Web.Pages
{
    public class ContactModel : PageModel
    {
        public string Title { get; set; } = "Contact me";
        public string Message { get; set; } = "";
        [BindProperty]
        public ContactViewModel Contact { get; set; } = new ContactViewModel()
        {
            Name = "write name",
        };
        public void OnGet()
        {
        }
        public void OnPost()
        {
         if (ModelState.IsValid) 
        {
                Message = "will be send";
        }
       else { Message = "pelese fix the error before senfing."; }
        }
    }
}