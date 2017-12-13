using System;
using System.Linq;
using DTS.DAL.Domain;
using DTS.SimulationLogicLayer.DTS.DataContracts;
using Statistic = DTS.SimulationLogicLayer.DTS.DataContracts.Statistic;

namespace DTS.SimulationLogicLayer
{
    //Class for adding statistics to the database
    public static class StatisticsLogic
    {
        public static void UpdateStatistics(Statistic statistic, NavigationPoint navPoint = null)
        {
            statistic.CollisionLocation = null;
            try
            {
                if (navPoint != null)
                {
                    var point = NavigationLogic.UnitOfWork.NavigationPoints.GetCollisionPoints()
                        .FirstOrDefault(x => Math.Round(x.XPosition,2).Equals(Math.Round(navPoint.XPosition,2)) &&
                                             Math.Round(x.ZPosition,2).Equals(Math.Round(navPoint.ZPosition,2)));
                    if (point == null)
                    {
                        var streets = NavigationLogic.FindStreets(navPoint.XPosition, navPoint.ZPosition).ToList();
                        var collisionPoint = new NavigationPoints
                        {
                            Id = Guid.NewGuid(),
                            IsCollisionPoint = true,
                            XPosition = navPoint.XPosition,
                            YPosition = navPoint.YPosition,
                            ZPosition = navPoint.ZPosition,
                            XNeighbourId = Guid.Empty,
                            ZNeighbourId = Guid.Empty
                        };
                        collisionPoint.Street.Add(streets.First());
                        collisionPoint.Street.Add(streets.Last());
                        statistic.CollisionLocation = collisionPoint;
                    }
                    else
                    {
                        statistic.CollisionLocation = point;
                    }
                }
                NavigationLogic.UnitOfWork.Statistics.Add(CreateStatistic(statistic));
                NavigationLogic.UnitOfWork.Complete();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        //Creates a domain object from the data contract object
        public static DAL.Domain.Statistic CreateStatistic(Statistic statistic)
        {
            return new DAL.Domain.Statistic
            {
                DroneCount = statistic.DroneCount,
                AverageDroneSpeed = statistic.AverageDroneSpeed,
                AverageDistanceTravelled = statistic.AverageDistanceTravelled,
                CurrentTimeInRun = statistic.CurrentTimeInRun,
                StartRunTime = statistic.StartRunTime,
                RunTimeSeconds = statistic.RunTimeSeconds,
                CollisionLocation = statistic.CollisionLocation
            };
        }
    }
}
