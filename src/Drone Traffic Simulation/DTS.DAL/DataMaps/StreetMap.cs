using System.Data.Entity.ModelConfiguration;
using DTS.DAL.Domain;

namespace DTS.DAL.DataMaps
{
    //Street Fluent Mapping
    public class StreetMap : EntityTypeConfiguration<Street>
    {
        public StreetMap()
        {
            ToTable("DTS.Streets");
            HasKey(x => x.Id).Property(x => x.Id).HasColumnName("Id").IsRequired();
            Property(x => x.IsHorizontal).HasColumnName("IsHorizontal").IsRequired();
            Property(x => x.XCoordinateOne).HasColumnName("XCoordinateOne").IsOptional();
            Property(x => x.XCoordinateTwo).HasColumnName("XCoordinateTwo").IsOptional();
            Property(x => x.IsVertical).HasColumnName("IsVertical").IsRequired();
            Property(x => x.ZCoordinateOne).HasColumnName("ZCoordinateOne").IsOptional();
            Property(x => x.ZCoordinateTwo).HasColumnName("ZCoordinateTwo").IsOptional();
            Property(x => x.Direction).HasColumnName("Direction").IsRequired();

        }
    }
}
