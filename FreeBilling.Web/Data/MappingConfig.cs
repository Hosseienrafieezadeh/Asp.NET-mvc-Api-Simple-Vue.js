using System.Security.Cryptography;
using FreeBilling.Web.Data.Entitis;
using FreeBilling.Web.Models;
using Mapster;

namespace FreeBilling.Web.Data
{
    public class MappingConfig:IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<TimeBill, TimeBillModel>()
                .TwoWays()
                .Map(_ => _.BillingRate, s => s.BillingRate)
                .Map(_ => _.WorkPerformed, s => s.WorkPerformed)
                .Map(d => d.Hours, _ => _.Hours);


        }
    }
}
