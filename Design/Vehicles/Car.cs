using Design.Abstractions;

namespace Design.Vehicles
{
    public class Car : Vehicle
    {
        public Car(int id, string? brandName, Dictionary<SpotTypes, int> spotTypesToSize)
            : base(id, brandName, spotTypesToSize)
        {

        }
    }
}
