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
            Console.WriteLine(" 5 - Adopt Animal");
            Console.WriteLine(" 6 - Add Adopter");
            Console.WriteLine(" 7 - Search For Animals");
            Console.WriteLine(" 8 - Exit");
            NavigateMainMenu(UI.GetInt("Your Selection: "));
        }
        private static void NavigateMainMenu(int selection)
        {
            switch (selection)
            {
                case 1:
                    new Manager().GetNewAnimal();
                    break;
                case 2:
                    //
                    break;
                case 3:
                    new AnimalInfo().UpdateAnimal();
                    break;
                case 4:
                    new AnimalInfo().GetAnimalInfo();
                    break;
                case 5:
                    //
                    break;
                case 6:
                    //
                    break;
                case 7:
                    //
                    break;
                case 8:
                    break;
                default:
                    NavigateMainMenu(UI.GetInt("Invalid selection. Try again: "));
                    break;
            }
        }
    }
}
