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
    }
}
