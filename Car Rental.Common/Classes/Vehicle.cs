using Car_Rental.Common.Enums;
using Car_Rental.Common.Interfaces;
using System.Transactions;

namespace Car_Rental.Common.Classes;

public class Vehicle : IVehicle
{
    public string RegNo { get; set; }
    public string Make { get; set; }
    public int Odometer { get; set; }
    public int PricePerKm { get; set; }
    public VehicleTypes VehicleType { get; set; }
    public int PricePerDay { get; set; }
    public bool Status { get; set; } = true;

}
