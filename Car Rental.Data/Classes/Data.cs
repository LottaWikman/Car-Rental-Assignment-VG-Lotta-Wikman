using Car_Rental.Common.Classes;
using Car_Rental.Common.Interfaces;
using Car_Rental.Common.Enums;
using System.Reflection;
using System.Linq.Expressions;

namespace Car_Rental.Data.Classes;


public class Data : IData
{
    readonly List<Vehicle> _vehicles = new();
    readonly List<IPerson> _persons = new();
    readonly List<IBooking> _bookings = new();

    public Data()
    {
        _vehicles.Add(new Car("ABC123", "Subaru", 100_000, 2, VehicleTypes.Combi, 100, true));
        _vehicles.Add(new Car("HEJ666", "Fiat", 200_000, 1, VehicleTypes.Sedan, 80, false));
        _vehicles.Add(new Car("TRE210", "Ford", 60_000, 3, VehicleTypes.Van, 110, true));
        _vehicles.Add(new Motorcycle("OOJ112", "Suzuki", 60_000, 1, VehicleTypes.Motorcycle, 50, true));
        _vehicles.Add(new Airplane("OWC747", "Boeing", 7_000_000, 100, VehicleTypes.Airplane, 1000, false));

        _persons.Add(new Customer(777777, "Johansson", "Johan"));
        _persons.Add(new Customer(444444, "Sigridsdotter", "Sigrid"));
        _persons.Add(new Customer(999999, "Eriksson", "Erik"));

        _bookings.Add(new Booking(_vehicles[2], _persons[1], 60500, DateTime.Now.AddDays(-7).Date, DateTime.Now, 300, true));
        _bookings.Add(new Booking(_vehicles[3], _persons[0], 60050, DateTime.Now.AddDays(-16).Date, DateTime.Now.AddDays(-14), 200, true));
        _bookings.Add(new Booking(_vehicles[1], _persons[2], null, DateTime.Now.AddDays(-2), null, null, false));
        _bookings.Add(new Booking(_vehicles[4], _persons[1], null, DateTime.Now.AddDays(-1).Date, null, null, false));
    }

    
    public List<T> Get<T>(Func<T, bool>? expression) where T : class
    {
        try
        {
            var collections = GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .FirstOrDefault(f => f.FieldType == typeof(List<T>) && f.IsInitOnly)
                ?? throw new InvalidOperationException("Unsupported type");

            var value = collections.GetValue(this) ?? throw new InvalidDataException();

            var collection = ((List<T>)value).AsQueryable();

            if (expression is null) return collection.ToList();

            return collection.Where(expression).ToList();
            //mycket av det här går att använda även i Add-metoden och single-metoden
        }
        catch
        {
            throw;
        }

    }


    T? IData.Single<T>(Expression<Func<T, bool>> expression) where T : default
    {
        try
        {
        //collection är en variabel, inte en lista!
            var collections = GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .FirstOrDefault(f => f.FieldType == typeof(List<T>) && f.IsInitOnly)
                ?? throw new InvalidOperationException("Unsupported type");
            var value = collections.GetValue(this) ?? throw new InvalidDataException();
            var collection = ((List<T>)value).AsQueryable();
            return collection.Single(expression) ?? throw new InvalidCastException();
        }
        catch
        {
            throw;
        }
    
    }



    public void GenericAdd<T>(T item) where T : class // där T är det aktuella objektet<T>
    {
        try
        {
            //reflections:
            var collections = GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .FirstOrDefault(f => f.FieldType == typeof(List<T>) && f.IsInitOnly)
                ?? throw new InvalidOperationException("Unsupported type");
            //hitta rätt collection-property i klassen (allt ovanför)

            //arbetar med datat i listan som hämtats:
            var value = collections.GetValue(this) ?? throw new InvalidDataException();
            ((List<T>)value).Add(item); //vi castar om value-variabeln av typen object
                                        //till en lista<T> för att Add-metoden ska fungera,
                                        //sen skickar vi in item i Add-funktionen
        }
        catch
        {
            throw;
        }
    }

    public void AddBooking(Booking booking)
    {
        _bookings.Add(booking);
    }


    public void EndBooking(Booking booking) 
    {

    }

}
