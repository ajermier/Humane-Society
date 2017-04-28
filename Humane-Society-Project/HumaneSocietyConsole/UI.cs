using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSocietyConsole
{
    static class UI
    {
        public static string GetString(string question)
        {
            string input;
            Console.Write(question);
            input = Console.ReadLine();

            if (string.IsNullOrEmpty(input))
            {
                Console.Write($"Error: no input entered. ");
                GetString(question);
            }

            return input;
        }
        public static int GetInt(string question)
        {
            int input;

            Console.Write(question);

            while (!int.TryParse(Console.ReadLine(), out input))
            {
                Console.Write($"Error: enter whole number. {question}");
            }

            return input;
        }
        public static double GetDouble(string question)
        {
            double input;

            Console.Write(question);

            while (!double.TryParse(Console.ReadLine(), out input))
            {
                Console.Write($"Error: enter whole number. {question}");
            }

            return input;
        }
        public static bool GetYesNoBool(string question)
        {
            string input;

            Console.Write(question + "(y or n): ");

            input = Console.ReadLine();

            switch (input.ToLower())
            {
                case "y":
                    return true;
                case "n":
                    return false;
                default:
                    Console.Write("Error, invalid input. ");
                    return GetYesNoBool(question);
            }
        }
        public static void DisplayMainMenu()
        {
            Console.WriteLine("----------MAIN MENU----------");
            Console.WriteLine(" 1 - Add New Animal");
            Console.WriteLine(" 2 - Import Animals From CSV");
            Console.WriteLine(" 3 - Update Existing Animal");
            Console.WriteLine(" 4 - Display Animal Information");
            Console.WriteLine(" 5 - Add Adopter");
            Console.WriteLine(" 6 - Search/Adopt Animals");
            Console.WriteLine(" 7 - Exit");
            NavigateMainMenu(UI.GetInt("Your Selection: "));
        }
        private static void NavigateMainMenu(int selection)
        {
            switch (selection)
            {
                case 1:
                    Manager newSession1 = new Manager();
                    newSession1.GetNewAnimal();
                    break;
                case 2:
                    //
                    break;
                case 3:
                    Manager newSession3 = new Manager();
                    newSession3.UpdateAnimal(newSession3);
                    break;
                case 4:
                    Manager newSession4 = new Manager();
                    newSession4.GetAnimalInfo(newSession4);
                    break;
                case 5:
                    Manager newSession5 = new Manager();
                    newSession5.AddAdopter(newSession5);
                    break;
                case 6:
                    Manager newSession6 = new Manager();
                    newSession6.AdopterSearchMenu(newSession6);
                    break;
                case 7:
                    break;
                default:
                    NavigateMainMenu(UI.GetInt("Invalid selection. Try again: "));
                    break;
            }
        }
    }
}
