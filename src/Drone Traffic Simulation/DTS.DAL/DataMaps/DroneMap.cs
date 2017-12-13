using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using DTS.DAL.Domain;

namespace DTS.DAL.DataMaps
{
    //Drone Fluent Mapping
    public class DroneMap : EntityTypeConfiguration<Drone>
    {
        public DroneMap()
        {
            ToTable("DTS.Drones");
            HasKey(x => x.Id).Property(x => x.Id).HasColumnName("Id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Name).HasColumnName("Name").IsRequired();
            Property(x => x.IsLive).HasColumnName("IsLive").IsRequired();

            HasOptional(x => x.TargetPoint).WithOptionalPrincipal(x => x.Drone);
            HasRequired(x => x.Scale).WithMany(x => x.Drones);
        }
    }
}
