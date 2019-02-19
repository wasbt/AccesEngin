using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Configurations
{
    public class AspNetUsersEntityConfiguration : EntityTypeConfiguration<AspNetUsers>
    {
        public AspNetUsersEntityConfiguration() : base()
        {
            this
           .HasOptional(s => s.Profile) 
                .WithRequired(ad => ad.AspNetUsers); 
        }
    }

}
