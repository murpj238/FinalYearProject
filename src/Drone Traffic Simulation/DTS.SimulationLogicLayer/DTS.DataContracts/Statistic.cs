using System;
using DTS.DAL.Domain;

namespace DTS.SimulationLogicLayer.DTS.DataContracts
{
    /// <summary>
    /// Statistic Data Contract
    /// For use in logic layer
    /// </summary>
    public sealed class Statistic
    {
        public int DroneCount { get; set; }

        public double AverageDroneSpeed { get; set; }

        public double AverageDistanceTravelled { get; set; }

        public DateTime CurrentTimeInRun { get; set; }

        public DateTime StartRunTime { get; set; }

        public int RunTimeSeconds { get; set; }

        public NavigationPoints CollisionLocation { get; set; }
    }
}
