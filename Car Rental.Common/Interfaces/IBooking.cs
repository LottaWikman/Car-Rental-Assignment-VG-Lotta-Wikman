﻿namespace Car_Rental.Common.Interfaces;

public interface IBooking
{
    public int Id { get; set; }
    public IVehicle Vehicle { get; set; }
    public IPerson Customer { get; set; }
    public int? KmReturned { get; set; }
    public DateTime DateRented { get; set; }
    public DateTime? DateReturned { get; set; }
    public double? Cost { get; set; }
    public bool Returned { get; set; }
}
