using Car_Rental.Common.Classes;
using Car_Rental.Common.Extensions;
using Car_Rental.Common.Interfaces;
namespace Car_Rental.Business.Classes;


public class BookingProcessor
{
    private readonly IData _data;
    private readonly InputValues _inputValues;

    public BookingProcessor(IData data, InputValues inputValues)
    {
        _data = data;
        _inputValues = inputValues;
    }

    public string error = string.Empty;
    public string loading = string.Empty;
    public bool isDisabled = false;
    

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


    public void AddVehicle()
    {
        error = string.Empty;

        if (_inputValues.Vehicle.RegNo != string.Empty && 
            _inputValues.Vehicle.Make != string.Empty && 
            _inputValues.Vehicle.Odometer != default && 
            _inputValues.Vehicle.PricePerKm != default &&
            _inputValues.Vehicle.PricePerDay != default)
        {
            _data.GenericAdd(_inputValues.Vehicle);
            _inputValues.ClearVehicle();
        }
        else
        {
            error = "Must have all information about the vehicle.";
        }


    }

    public void AddPerson()
    {
        error = string.Empty;


        if (_inputValues.Customer.SSN != default && 
            _inputValues.Customer.LastName != string.Empty && 
            _inputValues.Customer.FirstName != string.Empty)
        {
            _data.GenericAdd(_inputValues.Customer);
            _inputValues.ClearCustomer();
        }
        else
        {
            error = "Must have all information about the customer.";
        }

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

            Booking newBooking = new(_data.NextBookingId, vehicle, customer, null, DateTime.Now, null, null, false);
            _inputValues.Vehicle.Status = false;
            _data.AddBooking(newBooking);

            _inputValues.ClearVehicle();
            _inputValues.ClearCustomer();
            loading = string.Empty;
            isDisabled = false;
        }
        catch (Exception ex)
        {
            error = "Couldn't find a customer.";
            isDisabled = false;
            loading = string.Empty;
        }
    }

    public void ReturnVehicle(IVehicle vehicle, int distance)
    {
        //måste hitta bokningen:
        try
        {
            error = string.Empty;
            var booking = _data.Single<IBooking>(b => b.Vehicle.RegNo == vehicle.RegNo && b.Returned == false);

            vehicle.Status = true;
            booking.Returned = true;
            booking.KmReturned = vehicle.Odometer + distance;
            booking.DateReturned = DateTime.Now;            

            int daysRented = VehicleExtensions.Duration(booking.DateRented);
            booking.Cost = distance * vehicle.PricePerKm + daysRented * vehicle.PricePerDay;
        }
        catch(Exception ex)
        {
            error = "Couldn't find a booking";
        }
    }
}