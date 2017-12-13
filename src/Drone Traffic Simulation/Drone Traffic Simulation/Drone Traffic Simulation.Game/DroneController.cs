using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DTS.SimulationLogicLayer;
using SiliconStudio.Core.Extensions;
using SiliconStudio.Core.Mathematics;
using SiliconStudio.Core.Serialization.Contents;
using SiliconStudio.Xenko.Engine;
using SiliconStudio.Xenko.Games;
using SiliconStudio.Xenko.Rendering;

namespace Drone_Traffic_Simulation
{
    /// <summary>
    /// A script to generate and control drones in the simulation
    /// </summary>
    public class DroneController : SyncScript
    {

        private readonly DroneLogic _droneLogic = new DroneLogic(NumberOfDronesToGenerate);

        private const int NumberOfDronesToGenerate = 1000;

        public static Dictionary<int,Entity> Drones { get; private set; }

        private readonly Dictionary<int,List<Vector3>> _routes = new Dictionary<int,List<Vector3>>();

        private readonly Dictionary<int, int> _waypointIndexes = new Dictionary<int, int>();

        public static readonly Dictionary<int, float> Speeds = new Dictionary<int, float>();

        public static readonly Dictionary<int,float> DistanceTravelled = new Dictionary<int, float>();

        private static int TotalDroneCount { get; set; }

        //Generates new drones
        public override void Start()
        {
            base.Start();
            DroneLogic.UpdateNumberOfDrones(NumberOfDronesToGenerate);
        }

        //Update drone positions and initialises them
        public override async void Update()
        {
            CreateDrone();
            if (!Drones.IsNullOrEmpty())
            {
               await NavigateDrones();
            }
            CheckTimeToExit();
        }

        //Checks if it is time to exit the simulation
        private void CheckTimeToExit()
        {
            if (TotalDroneCount == NumberOfDronesToGenerate)
            {
                ((GameBase)Game).Exit();
            }
        } 

        //Loops through each drone to update their position and remove them from the world if necessary
        private async Task NavigateDrones()
        {
            var toRemove = new List<int>();
            foreach (var drone in Drones.ToList())
            {
                toRemove.Add(await NavigateToNexPoint(drone));
            }
            foreach (var key in toRemove.Where(x => x != 0))
            {
                var drone = Drones.ContainsKey(key) ? Drones[key] : null;
                SceneSystem.SceneInstance.RootScene.Entities.Remove(drone);
                Drones.Remove(key);
                DistanceTravelled.Remove(key);
                _routes.Remove(key);
                _waypointIndexes.Remove(key);
                Speeds.Remove(key);
                DroneLogic.SetDroneNotLive(key);
            }
        }

        //Checks if a collision has occurd
        private static bool CheckCollision(Vector3 position, int id)
        {
            return Drones.Any(x => Vector3.NearEqual(x.Value.Transform.Position, position, x.Value.Transform.Scale) &&
                                   x.Key != id);
        }

        //Updates drone position and records statistics if collision occurs
        private async Task<int> NavigateToNexPoint(KeyValuePair<int,Entity> drone)
        {
            if (!_waypointIndexes.ContainsKey(drone.Key)) return 0;
            var currentWaypointIndex = _waypointIndexes[drone.Key];
            var nextPoint = _routes[drone.Key].ElementAt(currentWaypointIndex);
            if (CheckCollision(drone.Value.Transform.Position, drone.Key))
            {
                StatisticsController.RecordStatistics(true, drone.Value.Transform.Position);
                return drone.Key;
            }
            drone.Value.Transform.Position = await MoveTowards(drone.Value.Transform.Position, nextPoint, Speeds[drone.Key],drone.Key);
            if (drone.Value.Transform.Position != nextPoint)
                return 0;
            currentWaypointIndex++;
            _waypointIndexes[drone.Key] = currentWaypointIndex;
            return currentWaypointIndex == _routes[drone.Key].Count - 1 ? drone.Key : 0;
        }

        //returns the new position to move to
        private static async Task<Vector3> MoveTowards(Vector3 origin, Vector3 target, float speed, int droneId)
        {
            var difference = target - origin;
            var distance = difference.Length();

            var moveBy = await Task.Run(() => origin + difference * (speed / distance));

            // Avoid divide by zero issues
            if (distance < speed || distance.Equals(0.0f)) return target;

            if(DistanceTravelled.ContainsKey(droneId))
                DistanceTravelled[droneId] += (moveBy - origin).Length();

            // Same as normalizing and multiplying speed, but reusing our length since it's expensive to calculate
            return moveBy;
        }

        //Initialises a new drone object in the world
        private void CreateDrone()
        {
            var update =  _droneLogic.AddDrone();
            if (!update.Value) return;
            var model = Content.Load<Model>("Models/Drones/drone", ContentManagerLoaderSettings.Default);
            var drone = new Entity(new Vector3(update.Key.Route.First().XPosition, update.Key.Route.First().YPosition,
                update.Key.Route.First().ZPosition),update.Key.Name);
            drone.Components.Add(new ModelComponent(model));
            drone.Transform.Scale = new Vector3(update.Key.Scale.XPosition, update.Key.Scale.YPosition,
                update.Key.Scale.ZPosition);
            drone.Transform.Rotation = Quaternion.Identity;
            update.Key.Route.RemoveAt(0);
            Speeds.Add(update.Key.Id, update.Key.Speed);
            _waypointIndexes.Add(update.Key.Id,0);
            if (Drones.IsNullOrEmpty())
            {
                Drones = new Dictionary<int, Entity>();
            }
            Drones.Add(update.Key.Id, drone);
            DistanceTravelled.Add(update.Key.Id,0);
            TotalDroneCount++;
            _routes.Add(update.Key.Id,update.Key.Route.Select(x => new Vector3(x.XPosition,x.YPosition,x.ZPosition)).ToList());
            SceneSystem.SceneInstance.RootScene.Entities.Add(drone);
        }
    }
}
