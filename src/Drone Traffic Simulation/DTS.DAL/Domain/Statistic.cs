using System;

namespace DTS.DAL.Domain
{
    //Statistics domain object
    public class Statistic
    {
        public virtual int Id { get; set; }

        public virtual int DroneCount { get; set; }

        public virtual double AverageDroneSpeed { get; set; }

        public virtual double AverageDistanceTravelled { get; set; }

        public virtual DateTime CurrentTimeInRun { get; set; }

        public virtual DateTime StartRunTime { get; set; }

        public virtual int RunTimeSeconds { get; set; }

        public virtual NavigationPoints CollisionLocation { get; set; }
    }
}
