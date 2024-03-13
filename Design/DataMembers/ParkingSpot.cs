using Design.Abstractions;

namespace Design.DataMembers
{
    public class ParkingSpot
    {

        public ParkingSpot(SpotTypes vechileTypeSupport, (int row, int column) location, int levelIndex)
        {
            VechileTypeSupport = vechileTypeSupport;
            Location = location;
            LevelIndex = levelIndex;
        }
        public SpotTypes VechileTypeSupport { get; set; }

        public bool IsOccupied { get; internal set; }

        public Vehicle? OccupiedBy { get; internal set; }

        public (int row, int column) Location { get; private set; }

        public int LevelIndex { get; private set; }
    }
}
