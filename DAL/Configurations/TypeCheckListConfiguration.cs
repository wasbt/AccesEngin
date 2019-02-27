using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Configurations
{
    public class TypeCheckListConfiguration : EntityTypeConfiguration<TypeCheckList>
    {
        public TypeCheckListConfiguration() : base()
        {
            this.HasMany(e => e.TypeEngin)
                .WithRequired(e => e.TypeCheckList)
                .WillCascadeOnDelete(false);
        }
    }

}
