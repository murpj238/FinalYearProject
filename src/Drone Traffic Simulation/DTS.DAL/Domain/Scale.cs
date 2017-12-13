using System.Collections.Generic;

namespace DTS.DAL.Domain
{
    //Scale domain object
    public class Scale
    {
        public Scale()
        {
            Drones = new List<Drone>();
        }
        public virtual int Id { get; set; }

        public virtual float XSize { get; set; }

        public virtual float YSize { get; set; }

        public virtual float ZSize { get; set; }

        public ICollection<Drone> Drones { get; set; }

    }
}
