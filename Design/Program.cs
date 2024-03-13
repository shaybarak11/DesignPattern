using Design;
using Design.Abstractions;
using Design.DataMembers;
using Design.Factories;
using Design.Vehicles;

var levelIndex = 0;
var rowA = ParkingSpotFactory.CreateRow(0, 10, levelIndex, SpotTypes.Motorcycle);
var rowB = ParkingSpotFactory.CreateRow(1, 20, levelIndex, SpotTypes.Compact);
var rowC = ParkingSpotFactory.CreateRow(2, 7, levelIndex, SpotTypes.Large);

var layout = new List<List<ParkingSpot>>() { rowA, rowB, rowC};
var groundLevel = new Level(levelIndex, layout);
var carApprovesSpots = new Dictionary<SpotTypes, int>() 
{
    {SpotTypes.Compact, 1}, {SpotTypes.Large, 1}
};
var busApprovesSpots = new Dictionary<SpotTypes, int>()
{
    {SpotTypes.Large, 5}
};

var levels = new Dictionary<int, Level> { {groundLevel.Index, groundLevel } };
var parkingLot = new ParkingLot(levels, "Ahuzat-Hof");

var car1 = new Car(1, "Mazda", carApprovesSpots);
var car2 = new Car(2, "Toyota", carApprovesSpots);
var car3 = new Car(3, "Tesla", carApprovesSpots);
var bus1 = new Bus(4, "Eged", busApprovesSpots);


var res1 = parkingLot.TryAssignParkingSpot(car1);
var res2 = parkingLot.TryAssignParkingSpot(car2);
var res3 = parkingLot.TryAssignParkingSpot(car3);
var res4 = parkingLot.TryAssignParkingSpot(bus1);
var res5 = parkingLot.TryBackout(bus1);
Console.WriteLine();