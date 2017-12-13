using System.Collections.Generic;

namespace DTS.DAL.Domain
{
    //Street domain object
    public class Street
    {
        public Street()
        {
            NavigationPoints = new HashSet<NavigationPoints>();
        }

        public virtual int Id { get; set; }

        public virtual bool IsHorizontal { get; set; }

        public virtual float XCoordinateOne { get; set; }

        public virtual float XCoordinateTwo { get; set; }

        public virtual bool IsVertical { get; set; }

        public virtual float ZCoordinateOne { get; set; }

        public virtual float ZCoordinateTwo { get; set; }

        public virtual string Direction { get; set; }

        public virtual ICollection<NavigationPoints> NavigationPoints { get; }
    }
}
