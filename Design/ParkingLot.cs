using Design.Abstractions;
using Design.DataMembers;

namespace Design
{
    public class ParkingLot
    {
        private readonly List<Level> _levelsList;
        
        private readonly Dictionary<int, Level> _levels;

        public ParkingLot(Dictionary<int, Level> levels, string name)
        {
            _levels = levels;
            _levelsList = levels.Values.ToList();
            Name = name;
        }
        public string Name { get; set; }

        public bool TryAssignParkingSpot(Vehicle vehicle)
        {
            foreach (var level in _levelsList)
            {
                if (level.TryAssignParkingSpot(vehicle))
                {
                    return true;
                }
            }

            return false;
        }

        public bool TryBackout(Vehicle vehicle)
        {
            if (vehicle.OccupiedSpot == null) return false;

            if (_levels.TryGetValue(vehicle.OccupiedSpot[0].LevelIndex, out var level))
            {
                return level.TryBackout(vehicle);
            }

            return false;
        }

    }
}
