using Car_Rental.Common.Classes;
using Car_Rental.Common.Interfaces;
namespace Car_Rental.Business.Classes;


public class BookingProcessor
{

    private readonly IData _data; //konstruktor-injektion, en typ av inject när man har jobbar i klassbibliotek
    private readonly InputValues _inputValues;

    public BookingProcessor(IData data, InputValues inputValues)
    {
        _data = data;
        _inputValues = inputValues; //skapar object av typerna data och inputvalues
    }

    public string error = string.Empty;
    public string loading = string.Empty;
    public bool isDisabled = false;
    

    //GET-metoder:
    public List<Vehicle> GetVehiclesGeneric()
    {
        return _data.Get<Vehicle>(null);
    }

    public List<IPerson> GetPersonsGeneric()
    {
        return _data.Get<IPerson>(null);
    }

    public List<IBooking> GetBookingsGeneric() 
    {  
        return _data.Get<IBooking>(null);
    }

    //GetSingle-metoder



    //ADD-metoder:
    public void AddVehicle()
    {
        error = string.Empty;
        _data.GenericAdd(_inputValues.Vehicle);
        _inputValues.ClearVehicle();
    }
    public void AddPerson()
    {
        error = string.Empty;
        _data.GenericAdd(_inputValues.Customer);
        _inputValues.ClearCustomer();
    }


    public async Task RentVehicle(Vehicle vehicle, int SSN)
    {
        loading = "loading";
        isDisabled = true;
        await Task.Delay(3000);
        
        try
        {
            error = string.Empty;
            var customer = _data.Single<IPerson>(p => p.SSN == SSN);

            Booking newBooking = new(vehicle, customer, null, DateTime.Now, null, null, false);
            _inputValues.Vehicle.Status = false;
            _data.AddBooking(newBooking);

            _inputValues.ClearVehicle();
            _inputValues.ClearCustomer();
            loading = string.Empty;
            isDisabled = false;
        }
        catch
        {
            error = "Couldn't find a customer.";
            isDisabled = false;
            loading = string.Empty;
            //await Task.Delay(5000);
            //error = string.Empty;
        }
    }

    public async void ReturnVehicle(Vehicle vehicle, int distance)
    {
        //måste hitta bokningen:
        try
        {
            error = string.Empty;
            var booking = _data.Single<IBooking>(b => b.Vehicle.RegNo == vehicle.RegNo && vehicle.Status == false); 
            //letar efter en bokning som innehåller samma regnummer, och som måste var uthyrd

            vehicle.Status = true; //fordonet blir tillgängligt igen
            booking.Returned = true; //bokningne är avslutad
            booking.KmReturned = vehicle.Odometer + distance;

            booking.DateReturned = DateTime.Now;
            
            int daysRented = (DateTime.Now - booking.DateRented).Days +1;
            booking.Cost = distance * vehicle.PricePerKm + daysRented * vehicle.PricePerDay;

        }
        catch
        {
            error = "Couldn't find a booking";
        }
    }
}