using System;
using System.Linq;
using DTS.DAL.Domain;
using DTS.DAL.IRepositories;

namespace DTS.DAL.Repositories
{
    //Extends generic repository
    public class NavigationRepository: Repository<NavigationPoints>, INavigationRepository
    {
        public NavigationRepository(DtsContext context) : base(context)
        {
        }

        private DtsContext DtsContext => Context as DtsContext;

        //Gets random navigation points
        public NavigationPoints GetRandom(Guid? id)
        {
            var random = new Random();
            var points = GetNaviationPoints().ToList();
            var next = random.Next(points.Count);
            if (id == null) return points.ElementAt(next);
            points = points.Where(x => x.Id != id).ToList();
            next = random.Next(points.Count);
            return points.ElementAt(next);
        }

        //Gets only navigation points
        public IQueryable<NavigationPoints> GetNaviationPoints()
        {
            return DtsContext.Set<NavigationPoints>().Where(x => !x.IsCollisionPoint);
        }

        //Gets only collision points
        public IQueryable<NavigationPoints> GetCollisionPoints()
        {
            return DtsContext.Set<NavigationPoints>().Where(x => x.IsCollisionPoint);
        }
    }
}