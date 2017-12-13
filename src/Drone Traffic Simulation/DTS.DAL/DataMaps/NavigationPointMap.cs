using System.Data.Entity.ModelConfiguration;
using DTS.DAL.Domain;

namespace DTS.DAL.DataMaps
{
    //Navigation Point Fluent Mapping
    public class NavigationPointMap : EntityTypeConfiguration<NavigationPoints>
    {
        public NavigationPointMap()
        {
            ToTable("DTS.NavigationPoints");
            HasKey(x => x.Id).Property(x => x.Id).HasColumnName("Id").IsRequired();
            Property(x => x.XPosition).HasColumnName("XPosition").IsRequired();
            Property(x => x.YPosition).HasColumnName("YPosition").IsRequired();
            Property(x => x.ZPosition).HasColumnName("ZPosition").IsRequired();
            Property(x => x.IsCollisionPoint).HasColumnName("IsCollisionPoint").IsRequired();
            Property(x => x.XNeighbourId).HasColumnName("XNeighbourId").IsOptional();
            Property(x => x.XNeighbourDistance).HasColumnName("XNeighbourDistance").IsOptional();
            Property(x => x.ZNeighbourId).HasColumnName("ZNeighbourId").IsOptional();
            Property(x => x.ZNeighbourDistance).HasColumnName("ZNeighbourDistance").IsOptional();

            HasMany(x => x.Street).WithMany(x => x.NavigationPoints).Map(x =>
            {
                x.MapLeftKey("NavigationPoint_Id");
                x.MapRightKey("Street_Id");
                x.ToTable("Street_NavigationPoints","DTS");
            });
        }
    }
}
