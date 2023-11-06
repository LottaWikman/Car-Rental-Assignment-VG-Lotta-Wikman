

using Car_Rental.Common.Classes;

namespace Car_Rental.Common.Extensions;

public static class VehicleExtensions
{
    public static int Duration(this DateTime input)
    {
        return (DateTime.Now - input).Days + 1;
    }
}
