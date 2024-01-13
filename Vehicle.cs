using System;
namespace rentalapp
{
    // Vehicle class (base class for all vehicle types)
    public abstract class Vehicle
    {
        public string RegistrationNumber { get; set; }
        public string Maker { get; set; }
        public string Model { get; set; }
        public decimal DailyRentalPrice { get; set; }
        public List<Reservation> Reservations { get; } = new List<Reservation>();

        public abstract string Type { get; } // Abstract property to be implemented by derived classes

        public bool IsAvailableOnDate(DateTime date)
        {
            // Check if the vehicle is available on a specific date
            return Reservations.All(r => !r.Schedule.Overlaps(new Schedule(date, date)));
        }

        public void Print()
        {
            // Print basic information about the vehicle
            Console.WriteLine($"Registration Number: {RegistrationNumber}, Type: {Type}, Make: {Maker}, Model: {Model}, Daily Rental Price: {DailyRentalPrice:C}");

            if (Reservations.Any())
            {
                Console.WriteLine("Reservations:");
                foreach (var reservation in Reservations.OrderBy(r => r.Schedule.PickupDate))
                {
                    // Print reservation details
                    Console.WriteLine($"  Pickup Date: {reservation.Schedule.PickupDate.ToShortDateString()}, Drop-Off Date: {reservation.Schedule.DropOffDate.ToShortDateString()}");
                    Console.WriteLine($"  Driver: {reservation.Driver.FirstName} {reservation.Driver.Surname}, DOB: {reservation.Driver.DateOfBirth.ToShortDateString()}, License: {reservation.Driver.LicenseNumber}");
                }
            }
            else
            {
                Console.WriteLine("No reservations.");
            }

            Console.WriteLine();
        }
    }
}

