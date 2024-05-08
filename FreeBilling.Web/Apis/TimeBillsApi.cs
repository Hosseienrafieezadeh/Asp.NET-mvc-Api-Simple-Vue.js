using System.Security.Claims;
using FluentValidation;
using FreeBilling.Web.Data;
using FreeBilling.Web.Data.Entitis;
using FreeBilling.Web.Models;
using FreeBilling.Web.Validators;
using Mapster;

namespace FreeBilling.Web.Apis
{
    public static class TimeBillsApi
    {
        public static void Register(WebApplication app)
        {
            app.MapGet("/api/timebills/{id:int}", GetTimeBill)
                .WithName("GetTimeBill")
                .RequireAuthorization("ApiPolicy");
            app.MapPost("api/timebills", PostTimeBill)
                .RequireAuthorization("ApiPolicy");
        }

        public static async Task<IResult> GetTimeBill(IBillingRepozitory repozitory, int id)
        {
           
                var bill = await repozitory.GetTimebill(id);
                if (bill is null) Results.NotFound();
                {
                    return Results.Ok(bill);
                }
            
        }

        public static async Task<IResult> PostTimeBill(IValidator<TimeBillModel> validator
            ,IBillingRepozitory repozitory
            , TimeBillModel model,ClaimsPrincipal user)
        {

            var validation = validator.Validate(model);
            if (!validation.IsValid)
            {
                return Results.ValidationProblem(validation.ToDictionary());
            }

            //var newEnttry = new TimeBill()
            //{
            //    CustomerId = model.CustomerId,
            //    EmployeeId = model.EmployeeId,
            //    Hours = model.Hours,
            //    BillingRate = model.BillingRate,
            //    Date = model.Date,
            //    WorkPerformed = model.WorkPerformed,

            //};
            var newEnttry = model.Adapt<TimeBill>();
            var employee = await repozitory.GetEmployee(user.Identity?.Name!);
            if (employee is null)
                Results.BadRequest("no employee with user's email");
            newEnttry.EmployeeId = employee?.Id;
                 repozitory.AddEntity(newEnttry);
                if (await repozitory.saveChanges())
                {
                    var newBill = await repozitory.GetTimebill(newEnttry.Id);
                    return Results.CreatedAtRoute("GetTimeBill", new { id = newEnttry.Id },newBill);
                }
                else
                {
                    return Results.BadRequest();
                }
            
        }
    }
}
