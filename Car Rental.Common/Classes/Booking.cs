using Car_Rental.Common.Interfaces;
namespace Car_Rental.Common.Classes;

public class Booking : IBooking
{
    public IVehicle Vehicle { get; set; }
    public IPerson Customer { get; set; }
    //public int KmRented { get; set; }
    public int? KmReturned { get; set; }
    public DateTime DateRented { get; set; }
    public DateTime? DateReturned { get; set; }
    public double? Cost { get; set; }
    public bool Returned { get; set; } = false;


    //tog bort KmRented
    public Booking(Vehicle vehicle, IPerson customer, int? kmReturned, DateTime dateRented, DateTime? dateReturned, double? cost, bool returned)
         => (Vehicle, Customer, KmReturned, DateRented, DateReturned, Cost, Returned)
        = (vehicle, customer, kmReturned, dateRented, dateReturned, cost, returned);
}
