using Car_Rental.Common.Enums;

namespace Car_Rental.Common.Interfaces;

public interface IVehicle
{
    public string RegNo { get; set; }
    public string Make { get; set; }
    public int Odometer { get; set; }
    public int PricePerKm { get; set; }
    public VehicleTypes VehicleType { get; set; }
    public int PricePerDay { get; set; }
    public bool Status {  get; set; }
}
