using Design.Abstractions;

namespace Design.Vehicles
{
    public class Bus : Vehicle
    {
        public Bus(int id, string? brandName, Dictionary<SpotTypes, int> spotTypesToSize)
            : base(id, brandName, spotTypesToSize)
        {

        }
    }
}
