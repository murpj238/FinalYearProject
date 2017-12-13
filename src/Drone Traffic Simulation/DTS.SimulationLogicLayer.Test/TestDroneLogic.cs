using System;
using System.Linq;
using DTS.DAL;
using NUnit.Framework;

namespace DTS.SimulationLogicLayer.Test
{
    [TestFixture]
    public class TestDroneLogic
    {

        [Test]
        public void TestNormalDistributedSpawns()
        {
            //Arrange

            //Act
            var droneLogic = new DroneLogic(50);

            //Assert
            Assert.IsNotEmpty(droneLogic.SpawnTimes,"Drone objects were not generated");
            Assert.AreEqual(50,droneLogic.SpawnTimes.Count,"The correct number of times was not generated");
        }

        [Test]
        public void TestCheckSpawnTimes()
        {
            //Arrange
            var droneLogic = new DroneLogic(50);
            droneLogic.SpawnTimes.Clear();
            droneLogic.SpawnTimes.Add(DateTime.MinValue.AddSeconds(1));

            //Act
            var correctTime = droneLogic.CheckTimeToSpawn();

            //Assert
            Assert.True(correctTime,"It is not the current time yet");
            Assert.IsNotEmpty(droneLogic.SpawnTimes,"There are values in the spawn times");
            Assert.AreEqual(droneLogic.SpawnTimes.Count,1,"The spawn times were not cleared or they were not added");
        }

        [Test]
        public void TestRemoveDrones()
        {
            //Arrange
            const int numToKeep = 9;
            var unitOfWork = new UnitOfWork(new DtsContext());

            //Act
            DroneLogic.RemoveDrones(numToKeep);

            //Assert
            Assert.AreEqual(numToKeep-1,unitOfWork.Drones.GetAll().Count(),"The number of drones should be equal to 40");
        }

        [Test]
        public void TestGenerateNewDrones()
        {
            //Arrange
            const int numDrones = 10;
            const int droneCount = 0;

            //Act
            var droneList = DroneLogic.GenerateNewDrones(numDrones,droneCount).ToList();

            //Assert
            Assert.IsNotEmpty(droneList,"The list of drones is empty and was not generated");
            Assert.AreEqual(droneList.Count,numDrones,"The number of drones generated was less than 10");
            Assert.True(droneList.All(x => x.Scale.Id >= 1),"One of the scales generated an id less than 1");
            Assert.True(droneList.All(x => x.Scale.Id <= 7),"One of the scales has an incorrect id greater than 7");
        }

        [Test]
        public void TestAddDrone()
        {
            //Arrange
            var unitOfWork = new UnitOfWork(new DtsContext());
            var comparisionList = unitOfWork.Drones.GetAll().ToList();
            unitOfWork.Dispose();
            var droneLogic = new DroneLogic(50);
            droneLogic.SpawnTimes.Clear();
            droneLogic.SpawnTimes.Add(DateTime.MinValue.AddSeconds(1));
            
            //Act
            var result = droneLogic.AddDrone();

            //Assert
            Assert.IsNotEmpty(comparisionList.Where(x => x.Id == result.Key.Id), "The Id generated does not exist");
            Assert.IsNotNull(result.Key.CurrentPoint,"The current point was not generated");
            Assert.True(result.Value,"The spawn time is incorrect");

            ResetValues(result.Key.Id);
        }

        private static void ResetValues(int id)
        {
            var unitOfWork = new UnitOfWork(new DtsContext());
            try
            {
                var drone = unitOfWork.Drones.GetSingle(id);
                drone.IsLive = false;
                drone.TargetPoint = null;
                unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            finally
            {
                unitOfWork.Dispose();
            }
        }
    }
}
