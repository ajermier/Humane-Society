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
            Console.WriteLine(" 5 - Display Adopter Information");
            Console.WriteLine(" 6 - Add Adopter");
            Console.WriteLine(" 7 - Search/Adopt Animals");
            Console.WriteLine(" 8 - Exit");
            NavigateMainMenu(GetInt("Your Selection: "));
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
                    Manager newSession2 = new Manager();
                    newSession2.ImportFromCSV();
                    break;
                case 3:
                    Manager newSession3 = new Manager();
                    newSession3.UpdateAnimal();
                    break;
                case 4:
                    Manager newSession4 = new Manager();
                    newSession4.GetAnimalInfo();
                    break;
                case 5:
                    Manager newSession5 = new Manager();
                    newSession5.GetAdopterInfoForDisplay();
                    break;
                case 6:
                    Manager newSession6 = new Manager();
                    newSession6.AddAdopter();
                    break;
                case 7:
                    Manager newSession7 = new Manager();
                    newSession7.AdopterSearchMenu();
                    break;
                case 8:
                    break;
                default:
                    NavigateMainMenu(UI.GetInt("Invalid selection. Try again: "));
                    break;
            }
        }
        public static void ReturnToMainMenu()
        {
            Console.WriteLine("Press enter to return to Main Menu");
            Console.ReadLine();
            Console.Clear();
            DisplayMainMenu();
        }
    }
}
