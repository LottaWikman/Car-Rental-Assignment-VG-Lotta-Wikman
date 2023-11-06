using Car_Rental.Common.Interfaces;
using Car_Rental.Common.Classes;

namespace Car_Rental.Common.Classes;

public class InputValues
{
    public Vehicle Vehicle { get; set; } = new();
    public IPerson Customer { get; set; } = new Customer();
    public int distance {  get; set; }






    public void ClearVehicle()
    {
        Vehicle = new();
    }
    public void ClearCustomer()
    {
        Customer = new Customer();
    }
    public void ClearDistance()
    {
        distance = default; 
    }

}