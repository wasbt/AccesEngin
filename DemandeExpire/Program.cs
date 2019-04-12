using System;

namespace DemandeExpire
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var demandes = context
              .DemandeAccesEngin
              .Where(x =>
              x.DemandeResultatEntete.Any(y => y.ResultatExigence.Any(d => d.Date.HasValue && DbFunctions.DiffDays(DateTime.Now, d.Date.Value) <= interval)));

        }
    }
}
