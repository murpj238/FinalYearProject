using System;
using System.Collections.Generic;

namespace DTS.DAL.Domain
{
    //Navigation Points domain object
    public class NavigationPoints
    {
        public NavigationPoints()
        {
            Street = new HashSet<Street>();    
        }
        public virtual Guid Id { get; set; }

        public virtual float XPosition { get; set; }

        public virtual float YPosition { get; set; }

        public virtual float ZPosition { get; set; }

        public virtual bool IsCollisionPoint { get; set; }

        public virtual Guid XNeighbourId { get; set; }

        public virtual double? XNeighbourDistance { get; set; }

        public virtual Guid ZNeighbourId { get; set; }

        public virtual double? ZNeighbourDistance { get; set; }

        public virtual ICollection<Street> Street { get; }

        public virtual Drone Drone { get; set; }

        public virtual Statistic Statistic { get; set; }
    }
}
