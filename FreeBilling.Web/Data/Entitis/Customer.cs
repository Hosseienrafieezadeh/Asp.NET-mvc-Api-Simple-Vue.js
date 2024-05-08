using System.ComponentModel.DataAnnotations;

namespace FreeBilling.Web.Data.Entitis;

public class Customer
{
    public int Id { get; set; }
    public string CompanyName { get; set; } = "";
    public Address? Address { get; set; }
    public string? Contact { get; set; }
    public string? PhoneNumber { get; set; }
}
