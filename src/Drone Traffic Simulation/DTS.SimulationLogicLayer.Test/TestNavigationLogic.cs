using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DTS.DAL;
using DTS.SimulationLogicLayer.DTS.DataContracts;
using NUnit.Framework;
using Drone = DTS.SimulationLogicLayer.DTS.DataContracts.Drone;

namespace DTS.SimulationLogicLayer.Test
{
    [TestFixture]
    public class TestNavigationLogic
    {
        [Test]
        public void TestCheckNavigationPoints()
        {
            //Arrange
            var testPointOne = Guid.Parse("B19C9EF8-3CF5-4A85-A0F7-4D516E9FD53C");
            var testPointTwo = Guid.Parse("11111111-AAAA-0B0B-B605-F144357EAA00");

            //Act
            var testPointOneCheck = NavigationLogic.CheckNavigationPoint(testPointOne);
            var testPointTwoCheck = NavigationLogic.CheckNavigationPoint(testPointTwo);

            //Assert
            Assert.True(testPointOneCheck,"The Id was not found");
            Assert.False(testPointTwoCheck, "The Id was found when it should not have");
        }

        [Test]
        public void TestCheckStreets()
        {
            //Arrange
            var xCoordExist = new KeyValuePair<float,float>(-131.02f ,-116.02f);
            var xCoordNotExist = new KeyValuePair<float,float>(1,1);
            var zCoordExist = new KeyValuePair<float, float>(-471.16f, -456.16f);
            var zCoordNotExist = new KeyValuePair<float, float>(10, 10);

            //Act
            var resultOne = NavigationLogic.CheckStreet(xCoordExist);
            var resultTwo = NavigationLogic.CheckStreet(xCoordNotExist);
            var resultThree = NavigationLogic.CheckStreet(zCoordExist);
            var resultFour = NavigationLogic.CheckStreet(zCoordNotExist);

            //Assert
            Assert.True(resultOne,"The point does not exist in the database");
            Assert.False(resultTwo,"The point exists in the database");
            Assert.True(resultThree, "The point does not exist in the database");
            Assert.False(resultFour, "The point exists in the database");
        }

        [Test]
        public void TestFindStreets()
        {
            //Arrange
            const float xCoordExist = -131.02f;
            const float xCoordNotExist = 1.0f;
            const float zCoordExist = -471.16f;
            const float zCoordNotExist = 1.1f;

            //Act
            var resultOne = NavigationLogic.FindStreets(xCoordExist, zCoordExist).ToList();
            var resultTwo = NavigationLogic.FindStreets(xCoordNotExist, zCoordNotExist);

            //Assert
            Assert.IsNotEmpty(resultOne,"The results list is empty");
            Assert.AreEqual(resultOne.Count,2,"There are too many or too few results in the list");
            Assert.IsEmpty(resultTwo,"The results list is not empty");
        }

        [Test]
        public void TestGenerateRoute()
        {
            //Arrange
            var unitOfWork = new UnitOfWork(new DtsContext());
            var drone = new Drone
            {
                TargetPoint = new NavigationPoint(unitOfWork.NavigationPoints.GetSingle(Guid.Parse("B955D974-2D5C-45A3-AEBA-009AD23BAC3A"))),
                CurrentPoint = new NavigationPoint(unitOfWork.NavigationPoints.GetSingle(Guid.Parse("161E22AE-B152-4D7F-AC68-58201EF025DA")))
            };
            var route = new List<NavigationPoint>
            {
                drone.CurrentPoint,
                drone.TargetPoint
            };
            var droneRandom = new Drone
            {
                TargetPoint = new NavigationPoint(unitOfWork.NavigationPoints.GetRandom(Guid.Parse("B955D974-2D5C-45A3-AEBA-009AD23BAC3A"))),
                CurrentPoint = new NavigationPoint(unitOfWork.NavigationPoints.GetRandom(Guid.Parse("B955D974-2D5C-45A3-AEBA-009AD23BAC3A"))),
            };
            var routeRandom = new List<NavigationPoint>
            {
                droneRandom.CurrentPoint,
                droneRandom.TargetPoint
            };

            File.AppendAllText(
                @"C:\Users\Jack\Documents\Fourth Year\Fourth Year Project\2017-ca400-murpj238\docs\blog\RandomNavigationPoints.csv",
                "TestGenerateNavigationPoint," + droneRandom.TargetPoint.Id + "," + droneRandom.TargetPoint.XPosition + "," +
                droneRandom.TargetPoint.ZPosition + "," + droneRandom.TargetPoint.Id + "," +
                droneRandom.CurrentPoint.XPosition + "," + droneRandom.CurrentPoint.ZPosition+"\n");
            //Act
            var result = NavigationLogic.GenerateRoute(drone, route,
                unitOfWork.NavigationPoints.GetNaviationPoints().Where(x => x.Id != drone.TargetPoint.Id).ToList(), 0);
            var resultRandom = NavigationLogic.GenerateRoute(droneRandom, routeRandom,
                unitOfWork.NavigationPoints.GetNaviationPoints().Where(x => x.Id != droneRandom.TargetPoint.Id)
                    .ToList(), 0);

            //Assert
            Assert.IsNotEmpty(result,"The list does not contain any values");
            Assert.AreEqual(result.Last(),drone.TargetPoint,"The last value should be the target point");
            Assert.Greater(route.Count,2,"The route only contains the start and finish points");
            Assert.AreEqual(35, result.Count, "The list has too many or too few points");
            Assert.True(result.Any(x => x.Id == Guid.Parse("e378a340-b368-4cc1-b412-28ed418a3d24")),"This point does not exist in the list");
            Assert.IsNotEmpty(resultRandom,"The route returned empty");
            Assert.True(result.Any(x => x.Id == Guid.Parse("B955D974-2D5C-45A3-AEBA-009AD23BAC3A")));

            unitOfWork.Dispose();
        }

        [Test]
        public void TestNearestToTargetPoint()
        {
            //Arrange
            var unitOfWork = new UnitOfWork(new DtsContext());
            var target = new NavigationPoint(unitOfWork.NavigationPoints.GetSingle(Guid.Parse("B955D974-2D5C-45A3-AEBA-009AD23BAC3A")));
            var xPoint = new NavigationPoint(unitOfWork.NavigationPoints.GetSingle(Guid.Parse("FC284FE6-73E1-42AC-9409-6267F4BAB607")));
            var zPoint = new NavigationPoint(unitOfWork.NavigationPoints.GetSingle(Guid.Parse("1FEB85AD-CB1E-4501-8680-693348927648")));
            unitOfWork.Dispose();

            //Act
            var result = NavigationLogic.NearestToTargetPoint(target, xPoint, zPoint);

            //Assert
            Assert.IsNotNull(result,"The method returned null");
            Assert.AreEqual(result,xPoint,"The wrong point was chosen");
        }

        [Test]
        public void TestDetermineNextPoint()
        {
            //Arrange
            var unitOfWork = new UnitOfWork(new DtsContext());
            var droneOne = new Drone
            {
                CurrentPoint = new NavigationPoint(unitOfWork.NavigationPoints.GetSingle(Guid.Parse("B19C9EF8-3CF5-4A85-A0F7-4D516E9FD53C"))),
                TargetPoint = new NavigationPoint(unitOfWork.NavigationPoints.GetSingle(Guid.Parse("B955D974-2D5C-45A3-AEBA-009AD23BAC3A")))
            };
            var xPointOne = new NavigationPoint(unitOfWork.NavigationPoints.GetSingle(droneOne.CurrentPoint.XNeighbourId));
            var zPointOne = new NavigationPoint(unitOfWork.NavigationPoints.GetSingle(droneOne.CurrentPoint.ZNeighbourId));

            var droneTwo = new Drone
            {
                CurrentPoint = new NavigationPoint(unitOfWork.NavigationPoints.GetSingle(Guid.Parse("EAFB1408-C53F-449D-9856-4B18BF32B861"))),
                TargetPoint = new NavigationPoint(unitOfWork.NavigationPoints.GetSingle(Guid.Parse("B955D974-2D5C-45A3-AEBA-009AD23BAC3A")))
            };
            var xPointTwo = new NavigationPoint(unitOfWork.NavigationPoints.GetSingle(droneTwo.CurrentPoint.XNeighbourId));
            var zPointTwo = new NavigationPoint(unitOfWork.NavigationPoints.GetSingle(droneTwo.CurrentPoint.ZNeighbourId));
            unitOfWork.Dispose();

            //Act
            var resultOne = NavigationLogic.DetermineNextPoint(droneOne, xPointOne, zPointOne);
            var resultTwo = NavigationLogic.DetermineNextPoint(droneTwo, xPointTwo, zPointTwo);

            //Assert
            Assert.IsNotNull(resultOne,"A navigation point was not selected for result one");
            Assert.AreEqual(xPointOne,resultOne,"The x coordinate was selected");
            Assert.IsNotNull(resultTwo, "A navigation point was not selected for result two");
            Assert.AreEqual(zPointTwo,resultTwo,"The z coordinate was selected");
        }

        [Test]
        public void TestIsDistanceToGoal()
        {
            //Arrange
            var unitOfWork = new UnitOfWork(new DtsContext());
            var target = unitOfWork.NavigationPoints.GetSingle(Guid.Parse("FC284FE6-73E1-42AC-9409-6267F4BAB607"));
            var drone = new Drone
            {
                CurrentPoint = new NavigationPoint
                {
                    Id = target.Id,
                    XNeighbourId = target.XNeighbourId,
                    ZNeighbourId = target.ZNeighbourId
                },
                TargetPoint = new NavigationPoint
                {
                    Id = Guid.Parse("B955D974-2D5C-45A3-AEBA-009AD23BAC3A"),
                }
            };
            unitOfWork.Dispose();

            //Act
            var result = NavigationLogic.IsDistanceToGoal(drone);

            //Assert
            Assert.True(result,"The result came out false");

        }

        [Test]
        public void TestCalculateDistance()
        {
            //Act
            var navigationPointOne = new NavigationPoint
            {
                XPosition = 10,
                YPosition = 10,
                ZPosition = 10
            };
            var navigationPointTwo = new NavigationPoint
            {
                XPosition = 10,
                YPosition = 10,
                ZPosition = 10
            };

            //Arrange
            var distance = NavigationLogic.CalculateDistance(navigationPointOne, navigationPointTwo);

            //Assert
            Assert.AreEqual(distance,0,"The distance is incorrect");
        }
    }
}
