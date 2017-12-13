using System;
using System.Collections.Generic;
using System.Linq;
using DTS.DAL.Domain;

namespace DTS.SimulationLogicLayer.DTS.DataContracts
{
    /// <summary>
    /// Navigation Point Data Contract
    /// For use in logic layer
    /// </summary>
    public class NavigationPoint
    {
        public NavigationPoint() { }

        //To create data contract from domain object
        public NavigationPoint(NavigationPoints navigationPoint)
        {
            Id = navigationPoint.Id;
            XPosition = navigationPoint.XPosition;
            YPosition = navigationPoint.YPosition;
            ZPosition = navigationPoint.ZPosition;
            XNeighbourId = navigationPoint.XNeighbourId;
            XNeighbourDistance = navigationPoint.XNeighbourDistance;
            ZNeighbourId = navigationPoint.ZNeighbourId;
            ZNeighbourDistance = navigationPoint.ZNeighbourDistance;
            Streets = new List<Street>
            {
                new Street(navigationPoint.Street.First()),
                new Street(navigationPoint.Street.Last())
            };
        }

        public Guid Id { get; set; }

        public float XPosition { get; set; }

        public float YPosition { get; set; }

        public float ZPosition { get; set; }

        public Guid XNeighbourId { get; set; }

        public double? XNeighbourDistance { get; set; }

        public Guid ZNeighbourId { get; set; }

        public double? ZNeighbourDistance { get; set; }

        public bool IsCollisionPoint { get; set; }

        public List<Street> Streets { get; set; }
    }
}
