using Car_Rental.Common.Interfaces;
using Car_Rental.Common.Classes;

namespace Car_Rental.Common.Classes;

public class InputValues
{
    public Vehicle Vehicle { get; set; } = new(); // avnänder den nya instansen av ett "tomt" vehicle
    public IPerson Customer { get; set; } = new Customer(); //använder den tomma kunden
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