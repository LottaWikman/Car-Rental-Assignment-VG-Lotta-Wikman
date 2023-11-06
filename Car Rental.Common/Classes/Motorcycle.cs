using Car_Rental.Common.Enums;
using Car_Rental.Common.Interfaces;

namespace Car_Rental.Common.Classes;

public class Motorcycle : Vehicle
{  

    public Motorcycle(string regNo, string make, int odometer, int pricePerKm, VehicleTypes vehicleType, int pricePerDay, bool status)
    {
        (RegNo, Make, Odometer, PricePerKm, VehicleType, PricePerDay, Status) =
            (regNo, make, odometer, pricePerKm, vehicleType, pricePerDay, status);
    }

}
