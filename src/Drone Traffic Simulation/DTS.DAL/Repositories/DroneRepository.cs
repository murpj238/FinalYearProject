using System;
using System.Linq;
using DTS.DAL.Domain;
using DTS.DAL.IRepositories;

namespace DTS.DAL.Repositories
{
    //Extends generic repository
    public class DroneRepository : Repository<Drone>, IDroneRepository
    {
        public DroneRepository(DtsContext context) : base(context)
        {
        }

        private DtsContext DtsContext => Context as DtsContext;

        //Gets random drone
        public Drone GetRandom()
        {
            var random = new Random();
            var drones = DtsContext.Drones.Where(x => !x.IsLive).ToList();
            var next = random.Next(1, drones.Count);
            return drones.ElementAt(next);
        }

        //Reseeds identity column
        public void ReseedIdColumn()
        {
            var newId = Context.Set<Drone>().Count();
            Context.Database.ExecuteSqlCommand("DBCC CHECKIDENT([DTS.Drones], RESEED," + newId + ");");
        }
    }
}
