using FreeBilling.Web.Data;

namespace FreeBilling.Web.Apis
{
    public static class EmployeesApi
    {
        public static void Register( WebApplication app)
        {
            app.MapGet("/api/employees", GetEmployees)
                .RequireAuthorization("ApiPolicy");
        }

        public static async Task<IResult> GetEmployees(IBillingRepozitory repozitory )
        {
            return Results.Ok(await repozitory.GetEmployess());
        }
    }
}
