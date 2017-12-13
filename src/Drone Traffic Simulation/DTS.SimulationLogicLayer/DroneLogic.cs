using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DTS.SimulationLogicLayer.DTS.DataContracts;
using Humanizer;

namespace DTS.SimulationLogicLayer
{
    //Class encapsulating all drone logic
    public class DroneLogic
    {
        private DateTime StartTime { get; }

        private DateTime CurrentTime { get; set; }

        public List<DateTime> SpawnTimes { get; }

        private static int MaxDrones { get; set; }

        private double ArrivalsPerHour { get; }

        private double StandardDistribution { get; }

        //Intialising class parameters
        public DroneLogic(int numToGenerate)
        {
            MaxDrones = numToGenerate;
            ArrivalsPerHour = MaxDrones/8.0;
            StandardDistribution = 0.5;
            StartTime = DateTime.MinValue;
            CurrentTime = DateTime.MinValue;
            SpawnTimes = NormalDistributedSpawns();
        }

        //Generates an enumerable object of drones to be added or removed from the database
        public static IEnumerable<DAL.Domain.Drone> GenerateNewDrones(int numToGenerate,int droneCount)
        {
            var random = new Random();
            var droneList = new List<DAL.Domain.Drone>();
            while (droneCount < numToGenerate)
            {
                var num = random.Next(1, 7);
                var scale = NavigationLogic.UnitOfWork.Scales.GetSingle(num);
                droneList.Add(new DAL.Domain.Drone
                {
                    Name = "Drone" + (droneCount+1).ToWords().Underscore().Pascalize(),
                    IsLive = false,
                    Scale = scale
                });
                droneCount++;
            }
            return droneList;
        }

        //Update the drone as supplied by drone controller class
        public static void UpdateNumberOfDrones(int numToGenerate)
        {
            var droneCount = NavigationLogic.UnitOfWork.Drones.GetAll().Count();
            if (droneCount > numToGenerate) RemoveDrones(droneCount- numToGenerate);
            else
            {
                try
                {
                    NavigationLogic.UnitOfWork.Drones.AddRange(GenerateNewDrones(numToGenerate, droneCount));
                    var updateDrones = NavigationLogic.UnitOfWork.Drones.Get(x => x.IsLive).ToList();
                    foreach (var updateDrone in updateDrones)
                    {
                        updateDrone.IsLive = false;
                    }
                    NavigationLogic.UnitOfWork.Complete();
                }
                catch (Exception ex)
                {
                    File.AppendAllText("log.txt", "----UpdateDrones----");
                    File.AppendAllText("log.txt",ex.Message);
                    File.AppendAllText("log.txt",ex.Source);
                    File.AppendAllText("log.txt",ex.StackTrace);
                }
            }
        }

        //Removes drones from database if number to create less than current database size
        public static void RemoveDrones(int numToRemove)
        {
            try
            {
                var drones = NavigationLogic.UnitOfWork.Drones.Get(x => x.Id >= numToRemove).AsEnumerable();
                NavigationLogic.UnitOfWork.Drones.RemoveRange(drones);
                NavigationLogic.UnitOfWork.Complete();
                NavigationLogic.UnitOfWork.Drones.ReseedIdColumn();
            }
            catch (Exception ex)
            {
                File.AppendAllText("log.txt", "----Remove Drones----");
                File.AppendAllText("log.txt", ex.Message);
                File.AppendAllText("log.txt", ex.Source);
                File.AppendAllText("log.txt", ex.StackTrace);
            }
        }

        //Checks spawn time and new to the database
        public KeyValuePair<Drone, bool> AddDrone()
        {
            if (!CheckTimeToSpawn()) return new KeyValuePair<Drone, bool>(new Drone(), false);
            var newDrone = new KeyValuePair<Drone,bool>();
            var drone = NavigationLogic.UnitOfWork.Drones.GetRandom();
            try
            {
                drone.IsLive = true;
                drone.TargetPoint = NavigationLogic.UnitOfWork.NavigationPoints.GetRandom(null);
                NavigationLogic.UnitOfWork.Complete();
                newDrone = new KeyValuePair<Drone, bool>(CreateDrone(drone), true);
            }
            catch (Exception ex)
            {
                File.AppendAllText("log.txt", "----Add Drones----");
                File.AppendAllText("log.txt", ex.Message);
                File.AppendAllText("log.txt", ex.Source);
                File.AppendAllText("log.txt", ex.StackTrace);
            }
            return newDrone;
        }

        public static void SetDroneNotLive(int id)
        {
            try
            {
                var drone = NavigationLogic.UnitOfWork.Drones.GetSingle(id);
                drone.IsLive = false;
                drone.TargetPoint = null;
                NavigationLogic.UnitOfWork.Complete();
            }
            catch (Exception ex)
            {
                File.AppendAllText("log.txt", "----Set Drone Not Live----");
                File.AppendAllText("log.txt", ex.Message);
                File.AppendAllText("log.txt", ex.Source);
                File.AppendAllText("log.txt", ex.StackTrace);
            }
        }

        //Checks if the Normally Distributed spawn times contain the current time
        public bool CheckTimeToSpawn()
        {
            CurrentTime = CurrentTime.AddSeconds(1);
            return SpawnTimes.Contains(CurrentTime);
        }

        //Created a new instance of a drone object
        private static Drone CreateDrone(DAL.Domain.Drone drone)
        {
            var navigationPoints = NavigationLogic.UnitOfWork.NavigationPoints.GetNaviationPoints().ToList();
            var currentPoint = NavigationLogic.UnitOfWork.NavigationPoints.GetRandom(drone.TargetPoint.Id);
            var returnedDrone = new Drone
            {
                Id = drone.Id,
                Name = drone.Name,
                Scale = new Scale
                {
                    Id = drone.Scale.Id,
                    XPosition = drone.Scale.XSize,
                    YPosition = drone.Scale.YSize,
                    ZPosition = drone.Scale.ZSize
                },
                TargetPoint = new NavigationPoint(drone.TargetPoint),
                CurrentPoint = new NavigationPoint(currentPoint),
                Speed = 2f
            };
            File.AppendAllText(
                @"C:\Users\Jack\Documents\Fourth Year\Fourth Year Project\2017-ca400-murpj238\docs\blog\RandomNavigationPoints.csv",
                "Create Drone Method," + returnedDrone.TargetPoint.Id + "," + returnedDrone.TargetPoint.XPosition + "," +
                returnedDrone.TargetPoint.ZPosition + "," + returnedDrone.CurrentPoint.Id + "," +
                returnedDrone.CurrentPoint.XPosition + "," + returnedDrone.CurrentPoint.ZPosition + "\n");
            navigationPoints = navigationPoints.Where(x => x.Id != returnedDrone.TargetPoint.Id).ToList();
            returnedDrone.Route = NavigationLogic.GenerateRoute(returnedDrone,
                new List<NavigationPoint>
                {
                    returnedDrone.CurrentPoint,
                    returnedDrone.TargetPoint
                },
                navigationPoints, 0);
            return returnedDrone;
        }

        //Creates a normally distrubted list of Spawn Times
        private List<DateTime> NormalDistributedSpawns()
        {
            var random = new Random();
            var temp = new List<double>();
            for (var i = 0; i < MaxDrones; i++)
            {
                temp.Add(i + random.NextDouble() * StandardDistribution);
            }
            return temp
                .Select(d => StartTime.AddMinutes(d * (60 / ArrivalsPerHour)))
                .Select(date => date.AddTicks(-(date.Ticks % TimeSpan.TicksPerSecond))).ToList();
        }
    }
}
