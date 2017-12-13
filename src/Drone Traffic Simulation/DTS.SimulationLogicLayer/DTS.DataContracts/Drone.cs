using System.Collections.Generic;

namespace DTS.SimulationLogicLayer.DTS.DataContracts
{
    /// <summary>
    /// Drone Data Contract
    /// For use in logic layer
    /// </summary>
    public class Drone
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public float Speed { get; set; }

        public Scale Scale { get; set; }

        public NavigationPoint CurrentPoint { get; set; }

        public NavigationPoint TargetPoint { get; set; }

        public List<NavigationPoint> Route { get; set; }
    }
}
