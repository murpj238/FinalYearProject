using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DTS.DAL;
using DTS.DAL.Domain;
using DTS.SimulationLogicLayer.DTS.DataContracts;
using Drone = DTS.SimulationLogicLayer.DTS.DataContracts.Drone;
using Street = DTS.DAL.Domain.Street;

namespace DTS.SimulationLogicLayer
{
    //Logic for all navigation
    public static class NavigationLogic
    {
        private static readonly Dictionary<NavigationPoints, bool> NavigationPoints = new Dictionary<NavigationPoints, bool>();

        private static readonly List<Street> Streets = new List<Street>();

        private static List<NavigationPoints> _originalList = new List<NavigationPoints>();

        //Initiating single unit of work
        public static readonly UnitOfWork UnitOfWork = new UnitOfWork(new DtsContext());

        //Update navigation attribubtes
        public static void UpdateNavigation(List<NavigationPoint> navigationPoints)
        {
            UpdateStreet(navigationPoints);
            UpdateNavigationPoints(navigationPoints);
        }

        //Update streets
        private static void UpdateStreet(IReadOnlyList<NavigationPoint> navigationPoints)
        {
            var direction = true;
            var xCoordinates = navigationPoints.Where(x => !x.IsCollisionPoint).Select(x => x.XPosition).Distinct().OrderBy(x => x).ToList();
            var zCoordinates = navigationPoints.Where(x => !x.IsCollisionPoint).Select(x => x.ZPosition).Distinct().OrderBy(x => x).ToList();
            for (var i = 0; i < xCoordinates.Count; i = i + 2)
            {
                var xCoords = new KeyValuePair<float,float>((float)Math.Round(xCoordinates[i],2), (float)Math.Round(xCoordinates[i + 1],2));
                var zCoords = new KeyValuePair<float,float>((float)Math.Round(zCoordinates[i],2), (float)Math.Round(zCoordinates[i + 1],2));
                if (!CheckStreet(xCoords))
                { 
                    Streets.Add(new Street
                    {
                        IsHorizontal = true,
                        XCoordinateOne = xCoords.Key,
                        XCoordinateTwo = xCoords.Value,
                        IsVertical = false,
                        Direction = direction ? "Left" : "Right" 
                    });
                }
                if (!CheckStreet(zCoords))
                {
                    Streets.Add(new Street
                    {
                        IsHorizontal = false,
                        IsVertical = true,
                        ZCoordinateOne = zCoords.Key,
                        ZCoordinateTwo = zCoords.Value,
                        Direction = direction ? "Up" : "Down"
                    });
                }
                direction = !direction;
            }
            try
            {
                UnitOfWork.Street.AddRange(Streets);
                UnitOfWork.Complete();
            }
            catch (Exception ex)
            {
                File.AppendAllText("log.txt", "----Update Streets----");
                File.AppendAllText("log.txt", ex.Message);
                File.AppendAllText("log.txt", ex.Source);
                File.AppendAllText("log.txt", ex.StackTrace);
            }
        }

        //Update navigation points
        private static void UpdateNavigationPoints(IEnumerable<NavigationPoint> navigationPoints)
        {
            foreach (var t in navigationPoints)
            {
                var navPoint = new NavigationPoints
                {
                    Id = t.Id,
                    XPosition = (float)Math.Round(t.XPosition, 2),
                    YPosition = (float)Math.Round(t.YPosition, 2),
                    ZPosition = (float)Math.Round(t.ZPosition, 2),
                    IsCollisionPoint = false
                };
                var streets = FindStreets(navPoint.XPosition, navPoint.ZPosition).ToList();
                navPoint.Street.Add(streets.First());
                navPoint.Street.Add(streets.Last());
                NavigationPoints.Add(navPoint, CheckNavigationPoint(navPoint.Id));
            }
            AddNeighbouringPoints();
            try
            {
                UnitOfWork.NavigationPoints.AddRange(NavigationPoints.Where(x => !x.Value).Select(x => x.Key));
                UnitOfWork.Complete();
            }
            catch (Exception ex)
            {
                File.AppendAllText("log.txt", "----Update Navigation Points----");
                File.AppendAllText("log.txt", ex.Message);
                File.AppendAllText("log.txt", ex.Source);
                File.AppendAllText("log.txt", ex.StackTrace);
            }
        }

        //Find streets for to add to navigation points
        public static IEnumerable<Street> FindStreets(float xPosition, float zPosition)
        {
            var result = UnitOfWork.Street.GetAll();
            var streets = result as IList<Street> ?? result.ToList();
            result =
                streets.Where(x => x.ZCoordinateOne <= zPosition && zPosition <= x.ZCoordinateTwo)
                    .Concat(streets.Where(x => x.XCoordinateOne <= xPosition && xPosition <= x.XCoordinateTwo));
            return result;
        }

        //Check if the street already exists
        public static bool CheckStreet(KeyValuePair<float,float> coordinates)
        {
            var streets = UnitOfWork.Street.Get(
                x =>
                    x.XCoordinateOne.Equals(coordinates.Key) || x.XCoordinateTwo.Equals(coordinates.Value) ||
                    x.XCoordinateOne.Equals(coordinates.Value) || x.XCoordinateTwo.Equals(coordinates.Key) ||
                    x.ZCoordinateOne.Equals(coordinates.Value) || x.ZCoordinateTwo.Equals(coordinates.Key) ||
                    x.ZCoordinateOne.Equals(coordinates.Key) || x.ZCoordinateTwo.Equals(coordinates.Value)).Any();
            return streets;
        }

        //Add neighbouring points to each navigation point
        private static void AddNeighbouringPoints()
        {
            foreach (var pointsKey in NavigationPoints.Keys)
            {
                var result =
                    NavigationPoints.Where(x => x.Key.XPosition.Equals(pointsKey.XPosition) && x.Key.Id != pointsKey.Id)
                        .OrderBy(x => Math.Abs(pointsKey.ZPosition - x.Key.ZPosition));
                var currentXDirection = pointsKey.Street.First(x => x.IsHorizontal).Direction;
                var currentZDirection = pointsKey.Street.Last(x => x.IsVertical).Direction;
                var point = currentXDirection.Equals("Left")
                    ? result.FirstOrDefault(x => x.Key.ZPosition < pointsKey.ZPosition)
                    : result.FirstOrDefault(x => x.Key.ZPosition > pointsKey.ZPosition);
                if (point.Equals(new KeyValuePair<NavigationPoints,bool>()))
                {
                    NavigationPoints.First(x => x.Key.Id == pointsKey.Id).Key.ZNeighbourId = Guid.Empty;
                    NavigationPoints.First(x => x.Key.Id == pointsKey.Id).Key.ZNeighbourDistance = null;
                }
                else
                {
                    NavigationPoints.First(x => x.Key.Id == pointsKey.Id).Key.ZNeighbourId = point.Key.Id;
                    NavigationPoints.First(x => x.Key.Id == pointsKey.Id).Key.ZNeighbourDistance = Math.Abs(point.Key.ZPosition - pointsKey.ZPosition);
                }

                result =
                    NavigationPoints.Where(x => x.Key.ZPosition.Equals(pointsKey.ZPosition) && x.Key.Id != pointsKey.Id)
                        .OrderBy(x => Math.Abs(pointsKey.XPosition - x.Key.XPosition));
                point = currentZDirection.Equals("Down")
                    ? result.FirstOrDefault(x => x.Key.XPosition < pointsKey.XPosition)
                    : result.FirstOrDefault(x => x.Key.XPosition > pointsKey.XPosition);
                if (point.Equals(new KeyValuePair<NavigationPoints, bool>()))
                {
                    NavigationPoints.First(x => x.Key.Id == pointsKey.Id).Key.XNeighbourId = Guid.Empty;
                    NavigationPoints.First(x => x.Key.Id == pointsKey.Id).Key.XNeighbourDistance = null;
                }
                else
                {
                    NavigationPoints.First(x => x.Key.Id == pointsKey.Id).Key.XNeighbourId = point.Key.Id;
                    NavigationPoints.First(x => x.Key.Id == pointsKey.Id).Key.XNeighbourDistance =
                        Math.Abs(point.Key.XPosition - pointsKey.XPosition);
                }
            }
        }

        //Check if the navigation point already exists in the database
        public static bool CheckNavigationPoint(Guid guid)
        {
            var navigationPoint = UnitOfWork.NavigationPoints.Get(x => x.Id.Equals(guid)).Any();
            return navigationPoint;
        }

        //Generate a list of navigation points for the drones
        public static List<NavigationPoint> GenerateRoute(Drone drone,List<NavigationPoint> result,List<NavigationPoints>  navigationPoints,int targetReached)
        {
            if (result.Count == 2) _originalList = navigationPoints;
            var isDistanceToGoal = IsDistanceToGoal(drone);
            targetReached = isDistanceToGoal ? Math.Abs(targetReached - 1) : targetReached + 0;
            if (!isDistanceToGoal)
            { 
                var xPoint = navigationPoints.Any(x => x.Id == drone.CurrentPoint.XNeighbourId)
                    ? new NavigationPoint(navigationPoints.FirstOrDefault(x => x.Id == drone.CurrentPoint.XNeighbourId))
                    : null;
                var zPoint = navigationPoints.Any(x => x.Id == drone.CurrentPoint.ZNeighbourId)
                    ? new NavigationPoint(navigationPoints.FirstOrDefault(x => x.Id == drone.CurrentPoint.ZNeighbourId))
                    : null;
                navigationPoints = navigationPoints.Where(x => x.Id != drone.CurrentPoint.Id).ToList();
                if (xPoint == null && zPoint == null)
                {
                    xPoint = drone.CurrentPoint.XNeighbourId.Equals(Guid.Empty)
                        ? null
                        : new NavigationPoint(_originalList.Find(x => x.Id == drone.CurrentPoint.XNeighbourId));
                    zPoint = drone.CurrentPoint.ZNeighbourId.Equals(Guid.Empty)
                        ? null
                        : new NavigationPoint(_originalList.Find(x => x.Id == drone.CurrentPoint.ZNeighbourId));
                }
                drone.CurrentPoint = DetermineNextPoint(drone, xPoint, zPoint);
                result.Insert(result.Count - 1, drone.CurrentPoint);
            }
            if (targetReached == 1)
            {
                drone.CurrentPoint = drone.TargetPoint;
                drone.TargetPoint = result.First();
                targetReached *= -1;
                navigationPoints = _originalList;
                result.Add(drone.TargetPoint);
            }
            return targetReached == 2 ? result : GenerateRoute(drone,result,navigationPoints,targetReached);
        }

        //Determine the next point to choose
        public static NavigationPoint DetermineNextPoint(Drone drone, NavigationPoint xPoint, NavigationPoint zPoint)
        {
            if (xPoint == null) return zPoint;
            if (zPoint == null) return xPoint;
            var currentStreets = drone.CurrentPoint.Streets;
            var targetStreets = drone.TargetPoint.Streets;
            if (drone.TargetPoint.XPosition < drone.CurrentPoint.XPosition &&
                drone.TargetPoint.ZPosition < drone.CurrentPoint.ZPosition)
            {
                if (currentStreets.Any(x => x.Direction.Equals("Right")) &&
                    currentStreets.Any(x => x.Direction.Equals("Up")))
                {
                    return NearestToTargetPoint(drone.TargetPoint, xPoint, zPoint);
                }
                if (currentStreets.Any(x => x.Direction.Equals("Left")) &&
                    currentStreets.Any(x => x.Direction.Equals("Up")))
                {
                    return zPoint;
                }
                if (currentStreets.Any(x => x.Direction.Equals("Right")) &&
                    currentStreets.Any(x => x.Direction.Equals("Down")))
                {
                    return xPoint;
                }
                return targetStreets.Any(x => x.Direction.Equals("Down") && x.Direction.Equals("Right"))
                    ? zPoint
                    : xPoint;
            }
            if (drone.TargetPoint.XPosition > drone.CurrentPoint.XPosition &&
                drone.TargetPoint.ZPosition < drone.CurrentPoint.ZPosition)
            {
                if (currentStreets.Any(x => x.Direction.Equals("Right")) &&
                    currentStreets.Any(x => x.Direction.Equals("Down")))
                {
                    return NearestToTargetPoint(drone.TargetPoint, xPoint, zPoint);
                }
                if (currentStreets.Any(x => x.Direction.Equals("Left")) &&
                    currentStreets.Any(x => x.Direction.Equals("Down")))
                {
                    return zPoint;
                }
                if (currentStreets.Any(x => x.Direction.Equals("Right")) &&
                    currentStreets.Any(x => x.Direction.Equals("Up")))
                {
                    return xPoint;
                }
                return targetStreets.Any(x => x.Direction.Equals("Left")) ? xPoint : zPoint;
            }
            if (drone.TargetPoint.XPosition < drone.CurrentPoint.XPosition &&
                drone.TargetPoint.ZPosition > drone.CurrentPoint.ZPosition)
            {
                if (currentStreets.Any(x => x.Direction.Equals("Left")) &&
                    currentStreets.Any(x => x.Direction.Equals("Up")))
                {
                    return NearestToTargetPoint(drone.TargetPoint, xPoint, zPoint);
                }
                if (currentStreets.Any(x => x.Direction.Equals("Right")) &&
                    currentStreets.Any(x => x.Direction.Equals("Up")))
                {
                    return zPoint;
                }
                if (currentStreets.Any(x => x.Direction.Equals("Left")) &&
                    currentStreets.Any(x => x.Direction.Equals("Down")))
                {
                    return xPoint;
                }
                return targetStreets.Any(x => x.Direction.Equals("Right") && x.Direction.Equals("Down"))
                    ? xPoint
                    : zPoint;
            }
            if (drone.TargetPoint.XPosition > drone.CurrentPoint.XPosition &&
                drone.TargetPoint.ZPosition > drone.CurrentPoint.ZPosition)
            {
                if (currentStreets.Any(x => x.Direction.Equals("Left")) &&
                    currentStreets.Any(x => x.Direction.Equals("Down")))
                {
                    return NearestToTargetPoint(drone.TargetPoint, xPoint, zPoint);
                }
                if (currentStreets.Any(x => x.Direction.Equals("Right")) &&
                    currentStreets.Any(x => x.Direction.Equals("Down")))
                {
                    return zPoint;
                }
                if (currentStreets.Any(x => x.Direction.Equals("Left")) &&
                    currentStreets.Any(x => x.Direction.Equals("Up")))
                {
                    return xPoint;
                }
                return targetStreets.Any(x => x.Direction.Equals("Left") && x.Direction.Equals("Up")) ? zPoint : xPoint;
            }
            return NearestToTargetPoint(drone.TargetPoint, xPoint, zPoint);
        }

        //Check has the goal been reached yet
        public static bool IsDistanceToGoal(Drone drone)
        {
            return drone.CurrentPoint.XNeighbourId.Equals(drone.TargetPoint.Id) ||
                    drone.CurrentPoint.ZNeighbourId.Equals(drone.TargetPoint.Id);
        }

        //Return the point nearest to the target out of the X and the Z Coordinate
        public static NavigationPoint NearestToTargetPoint(NavigationPoint target, NavigationPoint xPoint, NavigationPoint zPoint)
        {
            return CalculateDistance(target, xPoint) < CalculateDistance(target, zPoint)
            ? xPoint
            : zPoint;
        }

        //Calculate the Euclydian Distance between two points
        public static double CalculateDistance(NavigationPoint first, NavigationPoint second)
        {
            return
                Math.Round(Math.Sqrt(Math.Pow(first.XPosition - second.XPosition, 2) +
                          Math.Pow(first.YPosition - second.YPosition, 2) +
                          Math.Pow(first.ZPosition - second.ZPosition, 2)),2);
        }
    }
}