using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using DTS.DAL.Domain;

namespace DTS.DAL.DataMaps
{
    //Scale Fluent Mapping
    public class ScaleMap : EntityTypeConfiguration<Scale>
    {
        public ScaleMap()
        {
            ToTable("DTS.Scales");
            HasKey(x => x.Id).Property(x => x.Id).HasColumnName("Id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.XSize).HasColumnName("XSize").IsRequired();
            Property(x => x.YSize).HasColumnName("YSize").IsRequired();
            Property(x => x.ZSize).HasColumnName("ZSize").IsRequired();
        }
    }
}
