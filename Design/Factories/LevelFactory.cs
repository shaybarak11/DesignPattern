using Design.DataMembers;

namespace Design.Factories
{
    public class LevelFactory
    {
        public Level Create(int index, List<List<ParkingSpot>> parkingSpots)
        {
            return new Level(index, parkingSpots);
        }
    }
}
