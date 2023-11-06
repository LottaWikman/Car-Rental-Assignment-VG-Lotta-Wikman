using Car_Rental.Common.Interfaces;

namespace Car_Rental.Common.Classes;

public class Customer : IPerson
{
    public int SSN { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }

    public Customer(int ssn, string lastName, string firstName) =>
        (SSN, LastName, FirstName) = (ssn, lastName, firstName);

    public Customer() { } //tom konstruktor för selecten
}
