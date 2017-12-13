using System;
using DTS.DAL.Domain;
using NUnit.Framework;
using Statistic = DTS.SimulationLogicLayer.DTS.DataContracts.Statistic;

namespace DTS.SimulationLogicLayer.Test
{
    /// <summary>
    /// - Testing the logic for creating a statistics entry
    /// </summary>

    [TestFixture]
    public class TestStatisticsLogic
    {
        [Test]
        public void TestCreateStatistic()
        {
            //Arrange
            
            var statistic = new Statistic
            {
                AverageDistanceTravelled = 1,
                AverageDroneSpeed = 1,
                CurrentTimeInRun = DateTime.Now,
                StartRunTime = DateTime.Now,
                DroneCount = 1,
                RunTimeSeconds = 10,
                CollisionLocation = new NavigationPoints
                {
                    Id = Guid.NewGuid(),
                    IsCollisionPoint = true,
                    XPosition = 10,
                    YPosition = 10,
                    ZPosition = 10,
                    XNeighbourId = Guid.Empty,
                    ZNeighbourId = Guid.Empty,
                    Street = { new Street
                    {
                        Id = 1,
                        Direction = "Left",
                        IsHorizontal = true,
                        XCoordinateOne = -131.02f,
                        XCoordinateTwo = -116.02f,
                        IsVertical = false,
                        ZCoordinateOne = 0,
                        ZCoordinateTwo = 0
                    }}
                }
            };

            //Act
            var result = StatisticsLogic.CreateStatistic(statistic);

            //Assert
            Assert.IsNotNull(result,"The domain object was not created");
        }
    }
}
