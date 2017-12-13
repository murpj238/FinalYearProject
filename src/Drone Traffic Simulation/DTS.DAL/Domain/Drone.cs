
namespace DTS.DAL.Domain
{
    //Drone domain object
    public class Drone
    {
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual bool IsLive { get; set; }

        public virtual NavigationPoints TargetPoint { get; set; }

        public virtual Scale Scale { get; set; }
    }
}
