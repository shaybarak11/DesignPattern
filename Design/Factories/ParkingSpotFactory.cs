using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Design.Abstractions;
using Design.DataMembers;

namespace Design.Factories
{
    public static class ParkingSpotFactory
    {
        public static ParkingSpot Create((int row, int column) location, int levelIndex, SpotTypes type)
        {
            return new ParkingSpot(type, location, levelIndex);
        }
        public static List<ParkingSpot> CreateRow(int rowIndex, int numberOfSpots, int levelIndex, SpotTypes type)
        {
            var parkingSpots = new List<ParkingSpot>();
            for (int col = 0; col < numberOfSpots; col++)
            {
                parkingSpots.Add(new ParkingSpot(type, (rowIndex, col), levelIndex));
            }

            return parkingSpots;
        }
    }
}
