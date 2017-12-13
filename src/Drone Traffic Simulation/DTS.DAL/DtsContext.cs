using DTS.DAL.DataMaps;

namespace DTS.DAL
{
    using Domain;
    using System.Data.Entity;

    //Initialises Database Context
    public class DtsContext : DbContext
    {
        public DtsContext()
            : base("name=DtsContext")
        {
        }

        public DbSet<Drone> Drones { get; set; }

        public DbSet<NavigationPoints> NavigationPoints { get; set; }

        public DbSet<Scale> Scales { get; set; }

        public DbSet<Statistic> Statistics { get; set; }

        public DbSet<Street> Streets { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new DroneMap());
            modelBuilder.Configurations.Add(new NavigationPointMap());
            modelBuilder.Configurations.Add(new ScaleMap());
            modelBuilder.Configurations.Add(new StatisticMap());
            modelBuilder.Configurations.Add(new StreetMap());
        }
    }
}
