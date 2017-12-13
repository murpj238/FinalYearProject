using DTS.DAL.IRepositories;
using DTS.DAL.Repositories;

namespace DTS.DAL
{
    //Set up of the unit of work design pattern with repositories
    public class UnitOfWork : IUnitOfWork
    {

        private readonly DtsContext _context;

        public UnitOfWork(DtsContext context)
        {
            _context = context;
            Drones = new DroneRepository(_context);
            NavigationPoints = new NavigationRepository(_context);
            Scales = new ScaleRepository(_context);
            Street = new StreetRepository(_context);
            Statistics = new StatisticsRepository(_context);
        }

        public IDroneRepository Drones { get; set; }

        public INavigationRepository NavigationPoints { get; set; }

        public IScaleRepository Scales { get; set; }

        public IStreetRepository Street { get; set; }

        public IStatisticsRepository Statistics { get; set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}