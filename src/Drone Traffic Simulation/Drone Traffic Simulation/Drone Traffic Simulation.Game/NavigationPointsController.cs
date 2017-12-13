using System.Collections.Generic;
using System.Linq;
using DTS.SimulationLogicLayer;
using DTS.SimulationLogicLayer.DTS.DataContracts;
using SiliconStudio.Xenko.Engine;

namespace Drone_Traffic_Simulation
{
    public class NavigationPointsController : StartupScript
    {
        //Called at the beginning to update the navigation points
        public override void Start()
        {
            var streets = Entity.GetChildren().ToList();
            var navigationPoints = new List<NavigationPoint>();
            foreach (var street in streets)
            {
                var temp = street.GetChildren().ToList();
                navigationPoints.AddRange(temp.Select(t => new NavigationPoint
                {
                    Id = t.Id,
                    XPosition = t.Transform.Position.X,
                    YPosition = t.Transform.Position.Y,
                    ZPosition = t.Transform.Position.Z
                }));
            }
            NavigationLogic.UpdateNavigation(navigationPoints);

        }
    }
}
