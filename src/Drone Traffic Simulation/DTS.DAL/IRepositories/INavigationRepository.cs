using System;
using System.Linq;
using DTS.DAL.Domain;

namespace DTS.DAL.IRepositories
{
    public interface INavigationRepository : IRepository<NavigationPoints>
    {
        NavigationPoints GetRandom(Guid? id);

        IQueryable<NavigationPoints> GetNaviationPoints();

        IQueryable<NavigationPoints> GetCollisionPoints();
    }
}