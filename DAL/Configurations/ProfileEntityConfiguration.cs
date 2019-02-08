using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Configurations
{
    public class ProfileEntityConfiguration : EntityTypeConfiguration<Profiles>
    {
        public ProfileEntityConfiguration() : base()
        {
            this
           .HasOptional(e => e.AspNetUsers)
           .WithRequired(e => e.Profiles);
        }

    }
}
