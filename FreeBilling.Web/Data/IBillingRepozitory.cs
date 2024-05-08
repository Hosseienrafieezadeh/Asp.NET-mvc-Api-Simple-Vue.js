using FreeBilling.Web.Data.Entitis;

namespace FreeBilling.Web.Data
{
    public interface IBillingRepozitory
    {
        Task <IEnumerable<Customer>> GetCustomers( );
        Task <IEnumerable<Customer>> GetCustomersWithAddresses();
        Task < Customer?> GetCustomer(int id);
        Task <TimeBill?> GetTimebill(int id);
        Task<List<Employee>> GetEmployess();
        Task<TimeBill?> GetTimebillForCustomer(int id, int billId);
        void AddEntity<T>(T entiry) where T : notnull;
        Task<bool> saveChanges();


        Task <IEnumerable<TimeBill>> GetTimebillsForCustomer(int id);
        Task<Employee?> GetEmployee(string Name);
    }
       
}