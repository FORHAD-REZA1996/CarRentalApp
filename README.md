# CarRentalApp
Westminster Car Rental App Using C#

Overview and UML class diagram:

A Vehicle Rental System is described in this design paper. It will help with the administration of different cars and bookings. Customers may utilise the system to see what vehicles are available and book them, while administrators are in charge of the vehicles themselves.The objective is to streamline the administration of vehicle categories and rental reservations. This design document delineates the framework and interconnections among different system elements.



<img width="527" alt="image" src="https://github.com/FORHAD-REZA1996/CarRentalApp/assets/54482125/771837cb-e73e-4b5d-baa5-b346d7f801fb">


 



Fig: UML Class Diagram of Vehicle Rental Software System









System Entities: Classes, Abstract Classes, and Interfaces:

Interface: IRentalManager

Purpose: Specifies the procedures for administering vehicle rentals within the system, including the addition, removal, and listing of cars.
Attributes: Absent (Interfaces generally lack properties)
Methods:
AddVehicle(Vehicle vehicle): bool - This function adds a vehicle to the rental system.
DeleteVehicle(string registrationNumber): bool - Removes a car from the rental system.
ListVehicles(): void - Displays a comprehensive inventory of all vehicles currently registered in the system.
ListOrderedVehicles(): The method ListOrderedVehicles() returns a void type, and it is responsible for displaying the automobiles in alphabetical order.

Interface: IRentalCustomer

Purpose: Defines procedures for clients engaging with the rental system, encompassing tasks such as displaying a catalogue of accessible automobiles and overseeing bookings
Attributes: Absent
Methods:
ListAvailableVehicles : The method ListAvailableVehicles takes a Schedule object and a Type object as parameters and returns no value. It is responsible for displaying a list of cars that are available depending on the provided schedule and type.
AddReservation(string number, Schedule wantedSchedule): bool - This method adds a reservation for a vehicle.
ChangeReservation(string number, Schedule oldSchedule, Schedule newSchedule): bool - Modifies an existing reservation.
DeleteReservation: The method DeleteReservation takes two parameters: a string called number and a Schedule object called schedule. It returns a boolean value indicating if the reservation was successfully deleted.

Class: WestminsterRentalVehicle

Purpose: Serves as the primary rental car system.
Attributes:
A collection of objects of type Vehicle. Automobiles - Assortment of automobiles stored within the system.
Methods:
AdminMenu(): void - Exhibits the administrative menu and manages administrative activities.
CustomerMenu(IRentalCustomer customer): void - This method presents the customer menu and manages activities linked to the customer.

Class: Schedule

Purpose: Represents a designated period of time for reserving vehicles, including specified dates for pickup and drop-off.
Attributes:
PickupDate (DateTime): The specific day and time at which the car will be collected.
DropOffDate (DateTime): The specific day and time when the car will be returned.
Methods: No noteworthy methods were employed.

Class: Vehicle

Purpose: Serves as a fundamental class for different types of vehicles, encapsulating shared characteristics and functionalities.
Attributes:
RegistrationNumber (string): Distinct identification assigned to each vehicle.
Maker (string): The entity responsible for producing the vehicle.
Model (string): The make and model of the vehicle.
The DailyRentalPrice (decimal) is the amount charged for each day of renting the vehicle.
Reservations (List<Reservation>): A collection of reservations linked to the vehicle.
Methods:
IsAvailableOnDate(DateTime date): Verifies the availability of the vehicle on a given date.
Print(): Exhibits fundamental details on the car and its bookings.

Class: Car, Van, ElectricCar, Motorbike

Purpose: Subclasses that represent distinct categories of vehicle, deriving from the Vehicle superclass.
Methods: Inherits methods from the Vehicle class.

Class: Reservation

Purpose: This is a reservation created by a client, which includes specific information on the dates for picking up and dropping off, the driver assigned, and the corresponding timetable.
Attributes:
Schedule (Schedule): A designated time period for making a reservation.
Driver (Driver): Details on the individual who is making the reservation.
Methods: None significant.

Class: Driver

Purpose: Contains data pertaining to the driver, such as their name, date of birth, and licence number.
Attributes:
FirstName (string): The given name of the driver.
Surname (string): Last name of the driver.
DateOfBirth (DateTime): The driver's birth date.
LicenseNumber (string): The alphanumeric code assigned to a driver's licence.
Methods: No noteworthy methods were used.

Interface IOverlappable:

Purpose: This interface is used to check for scheduling overlaps. It provides a contract for classes that need to execute overlap checks.
Methods:
Overlaps: Checks whether the implementing class and another schedule have overlapping time periods.


OOP Principles Applied:

Encapsulation: It is the process of combining the data and methods associated with a class into a single unit. As an illustration, the car class contains data on a car, such as its registration number, manufacturer, model, and bookings.

Inheritance: In the design, inheritance is employed using the base class Vehicle and its derived classes (Car, Van, ElectricCar, Motorbike). This hierarchical structure enables the utilisation of shared properties and methods, facilitating the organisation of code.

Polymorphism: It is achieved by utilising the Overlaps function inside the IOverlappable interface. This technique is implemented differently by other classes, such as Schedule and Reservation, according on their individual needs.

Relationships: Relationships in software engineering may be categorised into three types: association, composition, and aggregation.

Association between WestminsterRentalVehicle and Vehicle:

WestminsterRentalVehicle owns a collection of Vehicles.
Multiplicity: WestminsterRentalVehicle (1) has (0..*) Vehicle.
Composition: Strong ownership relationship .

Association between Vehicle and Car, Van, ElectricCar, Motorbike:

Vehicle is an abstract class, and Car, Van, ElectricCar, Motorbike are concrete classes inheriting from Vehicle.
Inheritance: Car, Van, ElectricCar, Motorbike inherit from Vehicle.

Association between Vehicle and Reservations:

Vehicle has a collection of Reservations.
Multiplicity: Vehicle (1) has (0..*) Reservation.
Aggregation: Weak ownership relationship.

Association between Reservation and Schedule, Driver:

Reservation contains Schedule and Driver attributes.
Multiplicity: Reservation (1) has (1) Schedule, Reservation (1) has (1) Driver,
Composition: Strong ownership relationship.

Association between WestminsterRentalVehicle and IRentalManager, IRentalCustomer:

WestminsterRentalVehicle implements IRentalManager(interface) and IRentalCustomer(interface).

Association between Reservation and IOverlappable:

Reservation implements IOverlappable (interface).




Analysis of Design and Execution:

System Architecture:
The system, named WestminsterRentalVehicle, is structured in a well-organized manner, with well defined components for managing vehicles, categorising them into classes like as Car, Van, ElectricCar, and Motorbike, and providing user interfaces through IRentalManager and IRentalCustomer.
Enhanced readability is achieved by a distinct segregation of administrative and customer activities.

Classification of Vehicles:
The base class (Vehicle) is effectively utilised for deriving vehicle kinds (Car, Van, ElectricCar, Motorbike) through inheritance.
Polymorphism is utilised by accessing the Type attribute in each type of vehicle.

Management of Reservations:
The Reservation class effectively manages the process of booking schedules and driver information.
The Schedule class, which implements the IOverlappable interface, enables the detection of schedule overlaps.

User Interfaces:
The interfaces IRentalManager and IRentalCustomer provide an abstract representation of the necessary functionality for managing rentals, enabling freedom in how it is implemented.

Input Processing:
Invalid user inputs are handled graciously, accompanied by suitable error warnings.
The validation of date input types is performed to guarantee accurate parsing.

Main Program:
The primary program offers a menu that is easy for users to navigate in order to pick their user type, which may be either Admin, Customer, or Exit.
Utilising a do-while loop enables users to explore the system effortlessly.

Creation of Files:
The GenerateReport function effectively exports the existing list of cars with bookings to a text file.

Error Handling:
The system's resilience is improved by implementing appropriate error messages and checks. The code leverages try-catch blocks to handle exceptions, guaranteeing resilient error handling for crucial actions such as adding, removing, and updating vehicles and reservations in the WestminsterRentalVehicle system.

Recommendations for Improvement:
The design effectively adheres to the defined system requirements, offering a modular and easily expandable framework. The implementation strictly follows to the concept, encapsulating intricate features and successfully employing Object-Oriented Programming (OOP) principles. Possible enhancements for the system are optimising the management of various reservation categories and enhancing the driver information system to provide more comprehensive features. In general, the design meets the existing requirements while also allowing for potential future improvements.







![image](https://github.com/FORHAD-REZA1996/CarRentalApp/assets/54482125/69a5d44a-21f8-48fe-901c-54b93ad57b50)
