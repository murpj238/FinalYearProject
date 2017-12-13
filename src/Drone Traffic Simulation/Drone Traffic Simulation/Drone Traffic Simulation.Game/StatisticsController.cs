using System;
using System.Linq;
using System.Threading.Tasks;
using DTS.SimulationLogicLayer;
using DTS.SimulationLogicLayer.DTS.DataContracts;
using SiliconStudio.Core.Extensions;
using SiliconStudio.Core.Mathematics;
using SiliconStudio.Xenko.Engine;

namespace Drone_Traffic_Simulation
{
    public class StatisticsController: SyncScript
    {

        private DateTime CurrentTime { get; set; }
        

        private static Statistic Statistic { get; set; }

        //Adds statistics at the beginning of the run
        public override void Start()
        {
            base.Start();
            CurrentTime = DateTime.Now.AddTicks(-(DateTime.Now.Ticks % TimeSpan.TicksPerSecond));
            Statistic = new Statistic
            {
                CurrentTimeInRun = DateTime.Now.AddTicks(-(DateTime.Now.Ticks % TimeSpan.TicksPerSecond)),
                StartRunTime = DateTime.Now.AddTicks(-(DateTime.Now.Ticks % TimeSpan.TicksPerSecond)),
                DroneCount = 0,
                RunTimeSeconds = 0,
                AverageDistanceTravelled = 0,
                AverageDroneSpeed = 0,
            };
            RecordStatistics(false);
        }

        //Updates statistics
        public override async void Update()
        {
            CurrentTime = CurrentTime.AddSeconds(1);
            var distancesTravelled = DroneController.DistanceTravelled.Values.ToList();
            var speeds = DroneController.Speeds.Values.ToList();
            await Task.Run(() =>
            { 
                Statistic.RunTimeSeconds = (int)CurrentTime.Subtract(Statistic.StartRunTime).TotalSeconds;
                Statistic.DroneCount = !DroneController.Drones.IsNullOrEmpty() ? DroneController.Drones.Count : 0;
                Statistic.AverageDistanceTravelled = distancesTravelled.IsNullOrEmpty() ? 0 : distancesTravelled.Average();
                Statistic.AverageDroneSpeed = speeds.IsNullOrEmpty() ? 0 : speeds.Average();
            });
            if (!CurrentTime.Equals(Statistic.CurrentTimeInRun.AddMinutes(5))) return;
            Statistic.CurrentTimeInRun = CurrentTime;
            RecordStatistics(false);

        }

        //Adds updated statistics to the database
        public static void RecordStatistics(bool collisionDetected,Vector3? collisionPoint = null)
        {
            var navigationPoint = new NavigationPoint();
            if (collisionDetected && collisionPoint.HasValue)
            {
                navigationPoint.XPosition = collisionPoint.Value.X;
                navigationPoint.YPosition = collisionPoint.Value.Y;
                navigationPoint.ZPosition = collisionPoint.Value.Z;
                StatisticsLogic.UpdateStatistics(Statistic, navigationPoint);
            }
            else
            {
                StatisticsLogic.UpdateStatistics(Statistic);
            }
        }
    }
}
