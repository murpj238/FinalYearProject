using SiliconStudio.Xenko.Engine;

namespace Drone_Traffic_Simulation
{
    internal static class DroneTrafficSimulationApp
    {
        //Main game loop
        private static void Main()
        {
            using (var game = new Game())
            {
                game.Run();
            }
        }
    }
}
