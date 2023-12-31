﻿using Car_Rental.Common.Enums;

namespace Car_Rental.Common.Classes;

public class Airplane : Vehicle
{

    public Airplane(string regNo, string make, int odometer, int pricePerKm, VehicleTypes vehicleType, int pricePerDay, bool status) 
    {
        (RegNo, Make, Odometer, PricePerKm, VehicleType, PricePerDay, Status) =
           (regNo, make, odometer, pricePerKm, vehicleType, pricePerDay, status);
    }
}
