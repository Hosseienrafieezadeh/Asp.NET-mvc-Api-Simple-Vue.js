using System.ComponentModel.DataAnnotations;

namespace FreeBilling.Web.Data.Entitis;

public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public double BillingRate { get; set; }
    public string? ImageUrl { get; set; }
    public string Email { get; set; } = "";
}