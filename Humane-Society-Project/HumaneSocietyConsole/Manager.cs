using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSocietyConsole
{
    class Manager : IAnimal
    {
        private int species;
        private string name;
        private string sex;
        private int age;
        private double weight;
        private string color;
        private double food;
        private int room;
        private bool shots;
        private bool adopted;
    
        public int Species { get { return species; } }
        public string Name { get { return name; } set { Name = value; } }
        public string Sex { get { return sex; } }
        public int Age { get { return age; } set { Age = value; } }
        public double Weight { get { return weight; } set { Weight = value; } }
        public string Color { get { return color; } set { Color = value; } }
        public double Food { get { return food; } set { Food = value; } }
        public int Room { get { return room; } set { Room = value; } }
        public bool Shots { get { return shots; } set { Shots = value; } }
        public bool Adopted { get { return adopted; } set { Adopted = value; } }

        public Manager()
        {
        }

        public void GetNewAnimal()
        {
            AddNewAnimalPage();
            species = GetSpecies();
            name = UI.GetString("Enter name: ");
            sex = GetSex();
            age = UI.GetInt("Enter Age: ");
            weight = UI.GetDouble("Enter weight: ");
            color = UI.GetString("Enter color: ");
            food = UI.GetDouble("Enter lbs. of food consumed per week: ");
            shots = UI.GetYesNoBool("Recieved immunizations ");
            adopted = false;
            var animal = Connection.SaveAnimalToDatabase(Species, Name, Sex, Age, Weight, Color, Food, Shots, Adopted);
            room = AssignRoom(animal);
        }
        protected string GetSex()
        {
            string input;

            Console.Write("Enter sex (m or f): ");

            input = Console.ReadLine();

            switch (input.ToUpper())
            {
                case "M":
                    return "M";
                case "F":
                    return "F";
                default:
                    Console.Write("Error, invalid input. ");
                    return GetSex();
            }
        }
        protected int GetSpecies()
        {
            string input;
            int speciesCode = 0;
            bool check = true;

            Console.WriteLine("Select Species:");
            Console.WriteLine("   1- Dog");
            Console.WriteLine("   2- Cat");
            Console.WriteLine("   3- Bird");
            Console.WriteLine("   4- Rabbit");
            Console.WriteLine("   5- Ferret");

            while (check)
            {
                Console.Write("Enter number: ");
                input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        speciesCode = 1;
                        check = false;
                        break;
                    case "2":
                        speciesCode = 2;
                        check = false;
                        break;
                    case "3":
                        speciesCode = 3;
                        check = false;
                        break;
                    case "4":
                        speciesCode = 4;
                        check = false;
                        break;
                    case "5":
                        speciesCode = 5;
                        check = false;
                        break;
                    default:
                        Console.Write("Error, invalid input. ");
                        break;
                }
            }
            return speciesCode;
        }
        protected int AssignRoom(Animal animal)
        {

            room = UI.GetInt("Enter room number (1-250): ");

            while (!Connection.SaveRoomToDatabase(animal.AnimalID, room))
            {
                room = UI.GetInt("Enter room number: ");
            }
            return room;       
        }
        private void AddNewAnimalPage()
        {
            Console.Clear();
            Console.WriteLine("----------ADD NEW ANIMAL---------");
        }
    }
}
