using System;
namespace rentalapp
{
    // Schedule class
    public class Schedule : IOverlappable, IEquatable<Schedule>
    {
        public DateTime PickupDate { get; }
        public DateTime DropOffDate { get; }
        public List<Reservation> Reservations { get; } = new List<Reservation>();

        public Schedule(DateTime pickupDate, DateTime dropOffDate)
        {
            PickupDate = pickupDate;
            DropOffDate = dropOffDate;
        }

        public bool Overlaps(Schedule other)
        {
            return PickupDate < other.DropOffDate && DropOffDate > other.PickupDate;
        }

        public bool OverlapsExistingReservations()
        {
            return Reservations.Any(existingReservation => Overlaps(existingReservation.Schedule));
        }

        public bool Equals(Schedule other)
        {
            return PickupDate == other.PickupDate && DropOffDate == other.DropOffDate;
        }
    }
}

