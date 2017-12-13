using System;
using DTS.DAL.IRepositories;

namespace DTS.DAL
{
    public interface IUnitOfWork : IDisposable
    {
        IDroneRepository Drones { get; } 

        INavigationRepository NavigationPoints { get; } 

        IScaleRepository Scales { get; }

        IStreetRepository Street { get; }

        IStatisticsRepository Statistics {get;}

        int Complete();
    }
}