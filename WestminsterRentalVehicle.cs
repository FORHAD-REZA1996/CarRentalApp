using System;
using System.Globalization;

namespace rentalapp
{
    // Rental System class
    public class WestminsterRentalVehicle : IRentalManager, IRentalCustomer
    {
        public List<Vehicle> Vehicles { get; } = new List<Vehicle>();

        public void AdminMenu()
        {
            int choice;
            do
            {
                Console.WriteLine("\nAdmin Menu");
                Console.WriteLine("1. Add Vehicle");
                Console.WriteLine("2. Delete Vehicle");
                Console.WriteLine("3. List of Vehicles");
                Console.WriteLine("4. List of Alphabetically Ordered Vehicles");
                Console.WriteLine("5. Generate Report");
                Console.WriteLine("6. Back to Text Menu");
                Console.Write("Enter your choice (1-6): ");

                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            AddVehicle(new Car()); // You can choose the vehicle type based on user input
                            break;
                        case 2:
                            Console.Write("Enter the registration number of the vehicle to delete: ");
                            string regNumberToDelete = Console.ReadLine();
                            DeleteVehicle(regNumberToDelete);
                            break;
                        case 3:
                            ListVehicles();
                            break;
                        case 4:
                            ListOrderedVehicles();
                            break;
                        case 5:
                            Console.Write("Enter the file name to generate a report: ");
                            string fileName = Console.ReadLine();
                            GenerateReport(fileName);
                            break;
                        case 6:
                            Console.WriteLine("Exiting Admin Menu.");
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please enter a number between 1 and 6.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number between 1 and 6.");
                }
            } while (choice != 6);
        }

        public void CustomerMenu(WestminsterRentalVehicle rentalSystem)
        {
            int choice;
            do
            {
                Console.WriteLine("\nCustomer Menu");
                Console.WriteLine("1. List Available Vehicles");
                Console.WriteLine("2. Add Reservation");
                Console.WriteLine("3. Change Reservation");
                Console.WriteLine("4. Delete Reservation");
                Console.WriteLine("5. Admin Menu");
                Console.WriteLine("6. Exit");
                Console.Write("Enter your choice (1-6): ");

                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            DisplayAvailableVehicleTypes();
                            Console.Write("Enter the number of the vehicle type you want to list: ");
                            try
                            {
                                if (int.TryParse(Console.ReadLine(), out int vehicleTypeChoice))
                                {
                                    Type selectedType = GetVehicleTypeByChoice(vehicleTypeChoice);
                                    ListAvailableVehicles(new Schedule(DateTime.Now, DateTime.Now.AddDays(1)), selectedType);
                                }
                                else
                                {
                                    throw new FormatException("Invalid input. Please enter a valid number.");
                                }
                            }
                            catch (FormatException ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            break;
                        case 2:
                            DisplayAvailableVehicleTypes();
                            Console.Write("Enter the number of the vehicle type for reservation: ");
                            try
                            {
                                if (int.TryParse(Console.ReadLine(), out int reservationTypeChoice))
                                {
                                    Type selectedType = GetVehicleTypeByChoice(reservationTypeChoice);
                                    ListAvailableVehicles(new Schedule(DateTime.Now, DateTime.Now.AddDays(1)), selectedType);
                                    Console.Write("Enter the registration number of the vehicle for reservation: ");
                                    string vehicleNumber = Console.ReadLine();
                                    AddReservation(vehicleNumber, new Schedule(DateTime.Now, DateTime.Now.AddDays(1)));
                                }
                                else
                                {
                                    throw new FormatException("Invalid input. Please enter a valid number.");
                                }
                            }
                            catch (FormatException ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            break;
                        case 3:
                            DisplayAvailableVehicleTypes();
                            Console.Write("Enter the number of the vehicle type for reservation: ");
                            try
                            {
                                if (int.TryParse(Console.ReadLine(), out int typeChoiceForReservation))
                                {
                                    Type selectedTypeForReservation = GetVehicleTypeByChoice(typeChoiceForReservation);
                                    ListAvailableVehicles(new Schedule(DateTime.Now, DateTime.Now.AddDays(1)), selectedTypeForReservation);
                                    Console.Write("Enter the registration number of the vehicle for reservation: ");
                                    string vehicleNumberForReservation = Console.ReadLine();

                                    // Prompt for old and new schedules
                                    Console.Write("Enter the old pickup date (DD/MM/YYYY): ");
                                    DateTime oldPickupDate;
                                    if (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out oldPickupDate))
                                    {
                                        throw new FormatException("Invalid date format. Changing reservation failed.");
                                    }

                                    Console.Write("Enter the old drop-off date (DD/MM/YYYY): ");
                                    DateTime oldDropOffDate;
                                    if (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out oldDropOffDate))
                                    {
                                        throw new FormatException("Invalid date format. Changing reservation failed.");
                                    }

                                    Console.Write("Enter the new pickup date (DD/MM/YYYY): ");
                                    DateTime newPickupDate;
                                    if (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out newPickupDate))
                                    {
                                        throw new FormatException("Invalid date format. Changing reservation failed.");
                                    }

                                    Console.Write("Enter the new drop-off date (DD/MM/YYYY): ");
                                    DateTime newDropOffDate;
                                    if (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out newDropOffDate))
                                    {
                                        throw new FormatException("Invalid date format. Changing reservation failed.");
                                    }

                                    // Call the ChangeReservation method
                                    ChangeReservation(vehicleNumberForReservation, new Schedule(oldPickupDate, oldDropOffDate), new Schedule(newPickupDate, newDropOffDate));
                                }
                                else
                                {
                                    throw new FormatException("Invalid input. Please enter a valid number.");
                                }
                            }
                            catch (FormatException ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            break;
                        case 4:
                            DisplayAvailableVehicleTypes();
                            Console.Write("Enter the number of the vehicle type for reservation: ");
                            try
                            {
                                if (int.TryParse(Console.ReadLine(), out int typeChoiceForDeletion))
                                {
                                    Type selectedTypeForDeletion = GetVehicleTypeByChoice(typeChoiceForDeletion);
                                    ListAvailableVehicles(new Schedule(DateTime.Now, DateTime.Now.AddDays(1)), selectedTypeForDeletion);
                                    Console.Write("Enter the registration number of the vehicle for reservation deletion: ");
                                    string vehicleNumberForDeletion = Console.ReadLine();

                                    // Prompt for pickup date of the reservation to delete
                                    Console.Write("Enter the pickup date (DD/MM/YYYY) of the reservation to delete: ");
                                    if (DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime pickupDateForDeletion))
                                    {
                                        Schedule scheduleForDeletion = new Schedule(pickupDateForDeletion, pickupDateForDeletion.AddDays(1));
                                        rentalSystem.DeleteReservation(vehicleNumberForDeletion, scheduleForDeletion);
                                    }
                                    else
                                    {
                                        throw new FormatException("Invalid date format. Deletion failed.");
                                    }
                                }
                                else
                                {
                                    throw new FormatException("Invalid input. Please enter a valid number.");
                                }
                            }
                            catch (FormatException ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            break;

                        case 5:
                            AdminMenu();
                            break;
                        case 6:
                            Console.WriteLine("Exiting Customer Menu.");
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please enter a number between 1 and 6.");
                            break;
                    }

                    // Additional check for returning to the main menu
                    if (choice != 6)
                    {
                        Console.WriteLine("\nPress any key to return to the main menu.");
                        Console.ReadKey();
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number between 1 and 6.");
                }

            } while (choice != 6);
        }

        private Type GetVehicleTypeByChoice(int choice)
        {
            switch (choice)
            {
                case 1:
                    return typeof(Car);
                case 2:
                    return typeof(Van);
                case 3:
                    return typeof(ElectricCar);
                case 4:
                    return typeof(Motorbike);
                default:
                    throw new ArgumentOutOfRangeException(nameof(choice), "Invalid vehicle type choice.");
            }
        }

        public bool AddVehicle(Vehicle v)
        {
            Console.Write("Enter the registration number of the vehicle: ");
            string registrationNumber = Console.ReadLine();

            try
            {
                if (string.IsNullOrWhiteSpace(registrationNumber))
                {
                    throw new ArgumentException("Invalid registration number. Vehicle not added.");
                }

                if (!Vehicles.Any(vehicle => vehicle.RegistrationNumber == registrationNumber))
                {
                    v.RegistrationNumber = registrationNumber;

                    Console.Write("Enter the maker of the vehicle: ");
                    string maker = Console.ReadLine();

                    Console.Write("Enter the model of the vehicle: ");
                    string model = Console.ReadLine();

                    v.Maker = maker;
                    v.Model = model;

                    Console.Write("Enter the type of the vehicle (Car/Van/ElectricCar/Motorbike): ");
                    string typeInput = Console.ReadLine();

                    Type providedType;
                    switch (typeInput.ToLower())
                    {
                        case "car":
                            providedType = typeof(Car);
                            break;
                        case "van":
                            providedType = typeof(Van);
                            break;
                        case "electriccar":
                            providedType = typeof(ElectricCar);
                            break;
                        case "motorbike":
                            providedType = typeof(Motorbike);
                            break;
                        default:
                            throw new ArgumentException("Invalid vehicle type. Vehicle not added.");
                    }

                    if (providedType.IsSubclassOf(typeof(Vehicle)) || providedType == typeof(Vehicle))
                    {
                        if (Vehicles.Count < 50)
                        {
                            Console.Write("Enter the daily rental price of the vehicle: ");
                            if (decimal.TryParse(Console.ReadLine(), out decimal dailyRentalPrice))
                            {
                                v.DailyRentalPrice = dailyRentalPrice;
                                Vehicles.Add(v);
                                Console.WriteLine($"Vehicle added successfully with a daily rental price of {v.DailyRentalPrice:C}. Available parking lots: {50 - Vehicles.Count}");
                                return true;
                            }
                            else
                            {
                                throw new ArgumentException("Invalid daily rental price. Vehicle not added.");
                            }
                        }
                        else
                        {
                            throw new InvalidOperationException("No available parking lots. Vehicle not added.");
                        }
                    }
                    else
                    {
                        throw new ArgumentException("Type mismatch. Vehicle not added.");
                    }
                }
                else
                {
                    throw new InvalidOperationException("Duplicate registration number. Vehicle not added.");
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        // Method to display available types of vehicles
        private void DisplayAvailableVehicleTypes()
        {
            Console.WriteLine("Available Vehicle Types:");
            Console.WriteLine("1. Car");
            Console.WriteLine("2. Van");
            Console.WriteLine("3. Electric Car");
            Console.WriteLine("4. Motorbike");
        }

        public bool DeleteVehicle(string number)
        {
            // Delete a vehicle from the system
            var vehicle = Vehicles.FirstOrDefault(v => v.RegistrationNumber == number);

            if (vehicle != null)
            {
                Vehicles.Remove(vehicle);
                Console.WriteLine($"Vehicle deleted successfully. Available parking lots: {50 - Vehicles.Count}");
                return true;
            }
            else
            {
                Console.WriteLine("Vehicle not found. Deletion failed.");
                return false;
            }
        }

        public void ListVehicles()
        {
            // Print the list of vehicles in the system
            Console.WriteLine("\nList of Vehicles:");

            foreach (var vehicle in Vehicles)
            {
                Console.WriteLine($"Registration Number: {vehicle.RegistrationNumber}, Type: {vehicle.Type}, Maker: {vehicle.Maker}, Model: {vehicle.Model}");

                if (vehicle.Reservations.Any())
                {
                    Console.WriteLine("Reservations:");
                    foreach (var reservation in vehicle.Reservations.OrderBy(r => r.Schedule.PickupDate))
                    {
                        Console.WriteLine($"  Pickup Date: {reservation.Schedule.PickupDate.ToShortDateString()}, Drop-Off Date: {reservation.Schedule.DropOffDate.ToShortDateString()}");
                    }
                }
                else
                {
                    Console.WriteLine("No reservations.");
                }

                Console.WriteLine();
            }
        }

        public class VehicleMakeComparer : IComparer<Vehicle>
        {
            public int Compare(Vehicle x, Vehicle y)
            {
                return string.Compare(x.Maker, y.Maker, StringComparison.OrdinalIgnoreCase);
            }
        }

        public void ListOrderedVehicles()
        {
            // Print vehicles ordered alphabetically by maker
            Console.WriteLine("\nList of Vehicles (Ordered Alphabetically by Maker):");

            var orderedVehicles = Vehicles.OrderBy(v => v, new VehicleMakeComparer());

            foreach (var vehicle in orderedVehicles)
            {
                Console.WriteLine($"Registration Number: {vehicle.RegistrationNumber}, Type: {vehicle.Type}, Maker: {vehicle.Maker}, Model: {vehicle.Model}");

                if (vehicle.Reservations.Any())
                {
                    Console.WriteLine("Reservations:");
                    foreach (var reservation in vehicle.Reservations.OrderBy(r => r.Schedule.PickupDate))
                    {
                        Console.WriteLine($"  Pickup Date: {reservation.Schedule.PickupDate.ToShortDateString()}, Drop-Off Date: {reservation.Schedule.DropOffDate.ToShortDateString()}");
                    }
                }
                else
                {
                    Console.WriteLine("No reservations.");
                }

                Console.WriteLine();
            }
        }

        public void GenerateReport(string fileName)
        {
            try
            {
                // Save the current list of vehicles with bookings to a text file
                using (StreamWriter writer = new StreamWriter(fileName))
                {
                    foreach (var vehicle in Vehicles)
                    {
                        writer.WriteLine($"Vehicle Type: {vehicle.Type}, Registration Number: {vehicle.RegistrationNumber}");
                        writer.WriteLine("Bookings:");

                        var orderedReservations = vehicle.Reservations.OrderBy(r => r.Schedule.PickupDate);

                        foreach (var reservation in orderedReservations)
                        {
                            writer.WriteLine($"  Pickup Date: {reservation.Schedule.PickupDate.ToShortDateString()}, Drop-Off Date: {reservation.Schedule.DropOffDate.ToShortDateString()}");
                            writer.WriteLine($"  Driver: {reservation.Driver.FirstName} {reservation.Driver.Surname}, DOB: {reservation.Driver.DateOfBirth.ToShortDateString()}, License: {reservation.Driver.LicenseNumber}");
                        }

                        writer.WriteLine();
                    }
                    Console.WriteLine($"Report generated successfully. File: {fileName}");
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error generating report: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }

        public void ListAvailableVehicles(Schedule wantedSchedule, Type type)
        {
            // List available vehicles of a given type on a specific schedule
            Console.WriteLine($"\nList of Available {type.Name}s on {wantedSchedule.PickupDate.ToShortDateString()}:");

            var availableVehicles = Vehicles
                .Where(v => v.GetType() == type && v.IsAvailableOnDate(wantedSchedule.PickupDate))
                .OrderBy(v => v.RegistrationNumber);

            foreach (var vehicle in availableVehicles)
            {
                Console.WriteLine($"Registration Number: {vehicle.RegistrationNumber}, Type: {vehicle.Type}, Make: {vehicle.Maker}, Model: {vehicle.Model}");

                if (vehicle.Reservations.Any())
                {
                    Console.WriteLine("Reservations:");
                    foreach (var reservation in vehicle.Reservations.OrderBy(r => r.Schedule.PickupDate))
                    {
                        Console.WriteLine($"  Pickup Date: {reservation.Schedule.PickupDate.ToShortDateString()}, Drop-Off Date: {reservation.Schedule.DropOffDate.ToShortDateString()}");
                    }
                }
                else
                {
                    Console.WriteLine("No reservations.");
                }

                Console.WriteLine();
            }

            Console.WriteLine($"Available {type.Name}s listed successfully.");
        }

        public bool AddReservation(string number, Schedule wantedSchedule)
        {
            Console.WriteLine("Adding a reservation...");

            // Find the vehicle by registration number
            var vehicle = Vehicles.FirstOrDefault(v => v.RegistrationNumber == number);

            if (vehicle != null)
            {
                try
                {
                    // Prompt for manual entry of pickup and drop-off dates
                    Console.Write("Enter the pickup date (DD/MM/YYYY): ");
                    if (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime pickupDate))
                    {
                        throw new ArgumentException("Invalid date format. Adding reservation failed.");
                    }

                    Console.Write("Enter the drop-off date (DD/MM/YYYY): ");
                    if (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dropOffDate))
                    {
                        throw new ArgumentException("Invalid date format. Adding reservation failed.");
                    }

                    // Check if the vehicle is available on the specified dates
                    if (vehicle.IsAvailableOnDate(pickupDate) && vehicle.IsAvailableOnDate(dropOffDate) &&
                        !vehicle.Reservations.Any(existingReservation => existingReservation.Schedule.Overlaps(new Schedule(pickupDate, dropOffDate))))
                    {
                        // Prompt for driver information
                        Console.Write("Enter the driver's First name: ");
                        string driverFirstName = Console.ReadLine();

                        Console.Write("Enter the driver's surname: ");
                        string driverSurname = Console.ReadLine();

                        Console.Write("Enter the driver's date of birth (DD/MM/YYYY): ");
                        DateTime driverDOB;
                        if (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out driverDOB))
                        {
                            throw new ArgumentException("Invalid date format. Adding reservation failed.");
                        }

                        Console.Write("Enter the driver's license number: ");
                        string licenseNumber = Console.ReadLine();

                        var driver = new Driver { FirstName = driverFirstName, Surname = driverSurname, DateOfBirth = driverDOB, LicenseNumber = licenseNumber };

                        var reservation = new Reservation(pickupDate, dropOffDate, driver);
                        vehicle.Reservations.Add(reservation);

                        // Calculate total price based on daily rental price and duration
                        var totalPrice = vehicle.DailyRentalPrice * (decimal)(dropOffDate - pickupDate).TotalDays;

                        Console.WriteLine($"Reservation added successfully for {vehicle.Type} with a total price of {totalPrice:C} for {((int)(dropOffDate - pickupDate).TotalDays)} days.");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Vehicle not available or already booked on the specified dates. Adding reservation failed.");
                    }
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Vehicle not found. Adding reservation failed.");
            }

            return false;
        }

        public bool ChangeReservation(string number, Schedule oldSchedule, Schedule newSchedule)
        {
            Console.WriteLine("\nChanging a reservation...");

            var vehicle = Vehicles.FirstOrDefault(v => v.RegistrationNumber == number);

            if (vehicle != null)
            {
                try
                {
                    // Find the reservation using the Equals method
                    var reservationToChange = vehicle.Reservations.FirstOrDefault(r => r.Schedule.Equals(oldSchedule));

                    if (reservationToChange != null)
                    {
                        vehicle.Reservations.Remove(reservationToChange);

                        // Provide valid pickupDate and dropOffDate parameters for the Schedule constructor
                        var newReservation = new Reservation(newSchedule.PickupDate, newSchedule.DropOffDate, reservationToChange.Driver);

                        // Check if the new schedule is valid
                        if (!vehicle.Reservations.Any(r => r.Schedule.Overlaps(newSchedule)) &&
                            vehicle.IsAvailableOnDate(newReservation.Schedule.PickupDate) &&
                            vehicle.IsAvailableOnDate(newReservation.Schedule.DropOffDate))
                        {
                            vehicle.Reservations.Add(newReservation);

                            var totalPrice = vehicle.DailyRentalPrice * (decimal)(newSchedule.DropOffDate - newSchedule.PickupDate).TotalDays;

                            Console.WriteLine($"Reservation changed successfully for {vehicle.Type} with a new total price of {totalPrice:C}");
                            return true;
                        }
                        else
                        {
                            Console.WriteLine("New schedule is not valid. Changing reservation failed.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Reservation not found. Changing reservation failed.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Vehicle not found. Changing reservation failed.");
            }

            return false;
        }



        public bool DeleteReservation(string number, Schedule schedule)
        {
            Console.WriteLine("\nDeleting a reservation...");

            var vehicle = Vehicles.FirstOrDefault(v => v.RegistrationNumber == number);

            if (vehicle != null)
            {
                try
                {
                    var reservationToDelete = vehicle.Reservations.FirstOrDefault(r =>
                        r.Schedule.PickupDate.Date == schedule.PickupDate.Date &&
                        r.Schedule.DropOffDate.Date == schedule.DropOffDate.Date);

                    if (reservationToDelete != null)
                    {
                        if (vehicle.Reservations.Remove(reservationToDelete))
                        {
                            Console.WriteLine($"Reservation deleted successfully for {vehicle.Type} with registration number {vehicle.RegistrationNumber}");
                            return true;
                        }
                        else
                        {
                            Console.WriteLine("Deletion failed. Please try again.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Reservation not found. Deletion failed.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Vehicle not found. Deletion failed.");
            }

            return false;
        }

    }
}

