using Design.Abstractions;

namespace Design.DataMembers
{
    public class Level
    {
        public Level(int index, List<List<ParkingSpot>> parkingSpots)
        {
            Index = index;
            ParkingSpots = parkingSpots;
            TotalSpots = parkingSpots.Sum(x => x.Count);
            Rows = parkingSpots.Count();
            _freeSpots = new Dictionary<SpotTypes, List<(int row, int column)>>();
            MapFreeSpotsByTypes();
        }

        public int Index { get; private set; }
        public int Rows { get; private set; }
        public long TotalSpots { get; private set; }

        internal Dictionary<SpotTypes, List<(int row, int column)>> _freeSpots;
        internal List<List<ParkingSpot>> ParkingSpots { get; private set; }

        public void AddRow(List<ParkingSpot> newParkingSpots)
        {
            ParkingSpots.Add(newParkingSpots);
            Rows++;
            TotalSpots += newParkingSpots.Count;
        }

        public void AddRows(List<List<ParkingSpot>> newParkingSpots)
        {
            ParkingSpots.AddRange(newParkingSpots);
            Rows += newParkingSpots.Count;
            TotalSpots += newParkingSpots.Sum(x => x.Count);
        }

        public void SetLevelIndex(int index)
        {
            if (index == Index)
            {
                throw new ArgumentException($"Can not set new index - level index is already {index}");
            }
        }

        public bool TryAssignParkingSpot(Vehicle vehicle)
        {
            var takenParkingSpot = new List<ParkingSpot>();
            var spotTypes = vehicle.GetApprovedSpotTypes();
            foreach (var spotType in spotTypes)
            {
                var minimumSpotSize = vehicle.ApprovedSpotsTypes[spotType];
                if (_freeSpots.TryGetValue(spotType, out var spots))
                {
                    if (spots.Count == 0) continue;

                    if (minimumSpotSize > spots.Count) continue;

                    if (minimumSpotSize == 1)
                    {
                        var spot = spots[spots.Count - 1];
                        spots.RemoveAt(spots.Count - 1);
                        ParkingSpots[spot.row][spot.column].IsOccupied = true;
                        ParkingSpots[spot.row][spot.column].OccupiedBy = vehicle;
                        takenParkingSpot.Add(ParkingSpots[spot.row][spot.column]);
                        vehicle.OccupiedSpot = takenParkingSpot;
                        return true;
                    }
                    else
                    {
                        if (TrySeacrhConsecutiveParkingSpots(minimumSpotSize, spotType, out var consecutiveParkingSpots))
                        {
                            SetParkingSpotsToOccupied(consecutiveParkingSpots, vehicle);
                            takenParkingSpot.AddRange(consecutiveParkingSpots);
                            vehicle.OccupiedSpot = consecutiveParkingSpots;
                            return true;
                        }
                    }

                }
            }

            return false;


        }

        public bool TryBackout(Vehicle vehicle)
        {
            var occupiedSpots = vehicle.OccupiedSpot;

            if (occupiedSpots == null) return false;

            vehicle.OccupiedSpot = null;
            foreach (var occupiedSpot in occupiedSpots)
            {
                occupiedSpot.IsOccupied = false;
                occupiedSpot.OccupiedBy = null;
                _freeSpots[occupiedSpot.VechileTypeSupport].Add(occupiedSpot.Location);
            }

            return true;
        }

        private bool TrySeacrhConsecutiveParkingSpots(int number, SpotTypes type, out List<ParkingSpot> consecutiveParkingSpots)
        {
            consecutiveParkingSpots = new List<ParkingSpot>();
            var sum = 0;
            for (int row = 0; row < ParkingSpots.Count; row++)
            {
                for (int col = 0; col < ParkingSpots[row].Count; col++)
                {
                    if (ParkingSpots[row][col].VechileTypeSupport == type)
                    {
                        sum++;
                        consecutiveParkingSpots.Add(ParkingSpots[row][col]);
                        if (sum == number) return true;
                    }
                    else
                    {
                        consecutiveParkingSpots.Clear();
                        sum = 0;
                    }
                }

                consecutiveParkingSpots.Clear();
                sum = 0;
            }

            return false;
        }

        private void SetParkingSpotsToOccupied(List<ParkingSpot> parkingSpots, Vehicle vehicle)
        {
            foreach (var spot in parkingSpots)
            {
                spot.IsOccupied = true;
                spot.OccupiedBy = vehicle;
            }

            MapFreeSpotsByTypes();
        }

        private void MapFreeSpotsByTypes()
        {
            _freeSpots.Clear();
            for (int row = 0; row < ParkingSpots.Count; row++)
            {
                for (int col = 0; col < ParkingSpots[row].Count; col++)
                {
                    var spot = ParkingSpots[row][col];
                    if (!spot.IsOccupied)
                    {
                        if (!_freeSpots.ContainsKey(spot.VechileTypeSupport))
                        {
                            _freeSpots.Add(spot.VechileTypeSupport, new List<(int row, int column)>());
                        }

                        _freeSpots[spot.VechileTypeSupport].Add((row, col));

                    }
                }
            }
        }

    }
}
