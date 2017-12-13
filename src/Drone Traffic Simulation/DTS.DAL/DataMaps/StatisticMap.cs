using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using DTS.DAL.Domain;

namespace DTS.DAL.DataMaps
{
    //Statistics Fluent Mapping
    public class StatisticMap : EntityTypeConfiguration<Statistic>
    {
        public StatisticMap()
        {
            ToTable("DTS.Statistics");
            HasKey(x => x.Id)
                .Property(x => x.Id)
                .HasColumnName("Id")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.DroneCount).HasColumnName("DroneCount").IsRequired();
            Property(x => x.CurrentTimeInRun).HasColumnName("CurrentTimeInRun").IsRequired();
            Property(x => x.StartRunTime).HasColumnName("StartRunTime").IsRequired();
            Property(x => x.RunTimeSeconds).HasColumnName("RunTime").IsRequired();
            Property(x => x.AverageDistanceTravelled).HasColumnName("AverageDistanceTravelled").IsRequired();
            Property(x => x.AverageDroneSpeed).HasColumnName("AverageDroneSpeed").IsRequired();

            HasOptional(x => x.CollisionLocation).WithOptionalDependent(x => x.Statistic);
        }
    }
}
