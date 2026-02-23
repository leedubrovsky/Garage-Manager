using System;

namespace ConsoleUI
{
    internal class MenuPrinter
    {
        public static void PrintMenu()
        {
            Console.Clear();
            Console.WriteLine("Welcome to our garage management system!!");
            Console.WriteLine("Choose the activity you would like to do:");
            Console.WriteLine("Press 1 to load vehicles from data");
            Console.WriteLine("Press 2 to enter a new vehicle into the garage");
            Console.WriteLine("Press 3 to get the license IDs of the vehicles in the garage");
            Console.WriteLine("Press 4 to change the state of a vehicle in the garage");
            Console.WriteLine("Press 5 to inflate wheels to the maximum capacity");
            Console.WriteLine("Press 6 to refuel a vehicle");
            Console.WriteLine("Press 7 to charge a vehicle");
            Console.WriteLine("Press 8 to get the full status of a vehicle");
            Console.WriteLine("Press 0 to quit");
            Console.Write("Your choice: ");
        }

        public static void PrintError(string i_Message)
        {
            Console.WriteLine($"Error: {i_Message}");
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        public static void PrintSuccess(string i_Message)
        {
            Console.WriteLine(i_Message);
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }
    }
}

