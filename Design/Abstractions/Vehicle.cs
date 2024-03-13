using Design.DataMembers;

namespace Design.Abstractions
{
    public abstract class Vehicle
    {       
        protected Vehicle(
            int id, 
            string? brandName,
            Dictionary<SpotTypes, int> approvedSpotsTypes
            )
        {
            Id = id;
            BrandName = brandName;
            ApprovedSpotsTypes = approvedSpotsTypes;
        }
        protected int Id { get; private set; }
        protected string? BrandName { get; private set; }
        public Dictionary<SpotTypes, int> ApprovedSpotsTypes { get; set; }

        public List<ParkingSpot>? OccupiedSpot { get; set; }

        public List<SpotTypes> GetApprovedSpotTypes()
        {
            return ApprovedSpotsTypes.Keys.ToList();
        }

        public void AddSpotType(SpotTypes newType, int size)
        {
            ApprovedSpotsTypes[newType] = size;
        }

    }
}
