using System;
namespace rentalapp
{
    // Reservation class
    public class Reservation
    {
        public Schedule Schedule { get; }
        public Driver Driver { get; }

        public Reservation(DateTime pickupDate, DateTime dropOffDate, Driver driver)
        {
            Schedule = new Schedule(pickupDate, dropOffDate);
            Driver = driver;
        }
    }
}

