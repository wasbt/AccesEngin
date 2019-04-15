using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Shared.ENUMS;

namespace DemandeExpireBach
{
    class Program
    {
        static void Main(string[] args)
        {
            DemandeExpire();
        }
        static public async void DemandeExpire()
        {
            try
            {
            EnginDbContext context = new EnginDbContext();
            var demandes = context
            .DemandeAccesEngin
            .Where(x =>
            x.Autorise &&
            x.DemandeResultatEntete.Any(y => y.ResultatExigence.Any(d => d.Date.HasValue && DbFunctions.DiffDays(DateTime.Now, d.Date.Value) <= 15)));

            foreach (var item in demandes)
            {
                item.StatutDemandeId = (int)DemandeStatus.Expirer;
                context.Entry(item).State = EntityState.Modified;
            }
                context.SaveChanges();

            }
            catch (Exception e)
            {

                throw;
            }

        }
    }
}
