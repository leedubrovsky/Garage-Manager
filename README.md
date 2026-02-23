# Garage Management Console App

## Overview
C# console application for managing vehicles in a garage.  
The solution is split into a logic library and a console UI:

- Loads vehicle data from a text database.
- Supports multiple vehicle types (e.g. fuel cars, electric cars, motorcycles, trucks).
- Uses enums and collections to model fuel types, energy sources, and vehicle states.
- Validates input and throws typed exceptions for invalid values.
- Provides a text-based menu for viewing, adding, and updating vehicles.

The focus is on clean separation between domain logic and user interface.

## Solution Structure

The Visual Studio solution contains two projects:

- `GarageLogic` (Class Library, DLL)  
  - Contains all the business logic and domain classes.
  - Examples: `Vehicle`, `Car`, `Motorcycle`, `Truck`, `Wheel`, `FuelEngine`, `ElectricEngine`.
  - Manages the garage data (e.g. collections / dictionaries of vehicles).
  - Handles validation and custom exceptions.

- `ConsoleUI` (Console Application, EXE)  
  - References `GarageLogic`.
  - Provides the user interface: menus, prompts, and input/output.
  - Uses the logic layer to perform all operations.

## Main Features

- Load vehicles from a text file (e.g. `Vehicles.db`) using `System.IO.File.ReadAllLines`.
- Store vehicles in collections (such as `List<T>` and `Dictionary<TKey, TValue>`).
- Use enums for:
  - Fuel type (e.g. `Octan95`, `Octan96`, `Octan98`, `Soler`).
  - Vehicle status / other domain options.
- Input parsing and validation:
  - String parsing to numeric types (`int`, `float`, etc.).
  - Clear error messages for invalid input.
  - Custom exceptions such as:
    - `FormatException` for parsing errors.
    - `ArgumentException` for invalid arguments.
    - `ValueRangeException` for values outside the allowed range.
- Simple console menu for:
  - Adding a new vehicle.
  - Viewing existing vehicles.
  - Updating details (for example refueling, charging, status).

## Getting Started

1. Open the solution in Visual Studio.
2. Build the solution to compile both projects:
   - `GarageLogic` (Class Library)
   - `ConsoleUI` (Console App)
3. Set `ConsoleUI` as the startup project.
4. Run the console app (F5).

You should see a text menu that lets you interact with the garage data.

## Typical Flow

- The console UI shows a main menu with options such as:
  - Register a new vehicle.
  - List all vehicles.
  - View details of a specific vehicle.
  - Perform actions (fuel, charge, inflate wheels, etc.).
- The user enters values andd the UI parses them and passes them to the logic layer.
- If input is invalid, the logic layer throws meaningful exceptions which are handled and converted into friendly error messages.



