using System.Globalization;
using rentalapp;

// Interface for checking schedule overlaps
public interface IOverlappable
{
    bool Overlaps(Schedule other);
}

// IRentalManager interface
public interface IRentalManager
{
    bool AddVehicle(Vehicle v);
    bool DeleteVehicle(string number);
    void ListVehicles();
    void ListOrderedVehicles();
    void GenerateReport(string fileName);
}

// IRentalCustomer interface
public interface IRentalCustomer
{
    void ListAvailableVehicles(Schedule wantedSchedule, Type type);
    bool AddReservation(string number, Schedule wantedSchedule);
    bool ChangeReservation(string number, Schedule oldSchedule, Schedule newSchedule);
    bool DeleteReservation(string number, Schedule schedule);
}

class Program
{
    static void Main()
    {
        WestminsterRentalVehicle rentalSystem = new WestminsterRentalVehicle();

        int userType;
        do
        {
            Console.WriteLine("\nSelect User Type");
            Console.WriteLine("1. Admin");
            Console.WriteLine("2. Customer");
            Console.WriteLine("3. Exit");
            Console.Write("Enter your choice (1-3): ");

            if (int.TryParse(Console.ReadLine(), out userType))
            {
                switch (userType)
                {
                    case 1:
                        rentalSystem.AdminMenu();
                        break;
                    case 2:
                        rentalSystem.CustomerMenu(rentalSystem);
                        break;
                    case 3:
                        Console.WriteLine("Exiting the program.");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please enter a number between 1 and 3.");
                        break;
                }

                // Additional check for returning to the main menu
                if (userType != 3)
                {
                    Console.WriteLine("\nPress any key to return to the main menu.");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number between 1 and 3.");
            }

        } while (userType != 3);
    }
}