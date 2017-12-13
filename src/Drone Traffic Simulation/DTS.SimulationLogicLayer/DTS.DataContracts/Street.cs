namespace DTS.SimulationLogicLayer.DTS.DataContracts
{
    /// <summary>
    /// Street Data Contract
    /// For use in logic layer
    /// </summary>
    public sealed class Street
    {
        //To create data contract from domain object
        public Street(DAL.Domain.Street street)
        {
            Id = street.Id;
            IsHorizontal = street.IsHorizontal;
            XCoordinateOne = street.XCoordinateOne;
            XCoordinateTwo = street.XCoordinateTwo;
            IsVertical = street.IsVertical;
            ZCoordinateOne = street.ZCoordinateOne;
            ZCoordinateTwo = street.ZCoordinateTwo;
            Direction = street.Direction;
        }

        public int Id { get; set; }

        public bool IsHorizontal { get; set; }

        public float XCoordinateOne { get; set; }

        public float XCoordinateTwo { get; set; }

        public bool IsVertical { get; set; }

        public float ZCoordinateOne { get; set; }

        public float ZCoordinateTwo { get; set; }

        public string Direction { get; set; }
    }
}
