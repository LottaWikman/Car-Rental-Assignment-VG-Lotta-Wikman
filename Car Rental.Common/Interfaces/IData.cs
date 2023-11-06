using Car_Rental.Common.Classes;
using System.Linq.Expressions;

namespace Car_Rental.Common.Interfaces;

public interface IData
{
    List<T> Get<T>(Func<T, bool>? expression) where T : class;
    public void GenericAdd<T>(T item) where T : class;
    T? Single<T>(Expression<Func<T, bool>>? expression);
    public void AddBooking(Booking booking);
    public int NextBookingId { get; }


}
