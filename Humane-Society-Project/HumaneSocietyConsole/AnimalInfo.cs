using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSocietyConsole
{
    partial class Manager : IAnimal
    {
        public void UpdateAnimal(Manager manager)
        {
            UpdateAnimalNewPage();
            PrintUnadoptedAnimals();
            DisplayUpdateMenu(GetAnimalID());
        }
        public void GetAnimalInfo(Manager manager)
        {
            AnimalInfoNewPage();
            PrintAdoptedAnimals();
            PrintUnadoptedAnimals();
            var animal = GetAnimalID();
            DisplayAnimalInfo(animal);
        }

        public Animal GetAnimalID()
        {
            var animal = Connection.GetAnimal(UI.GetInt("Select ID: "));

            while(animal == null)
            {
                animal = Connection.GetAnimal(UI.GetInt("Select ID: "));
            }
            selectedAnimalID = animal.AnimalID;
            return animal;
        }
        public int GetAnimalID(List<Animal> list)
        { 
            int selection = UI.GetInt("Select ID: ");

            while (!list.Exists(a => a.AnimalID == selection))
            {
                selection = UI.GetInt("Error: ID not found. Select ID: ");
            }
            return selection;
        }
        private void DisplayAnimalInfo(Animal animal)
        {
            var room = Connection.GetRoom(animal);
            AnimalInfoNewPage();
            Console.WriteLine($"Animal ID: {animal.AnimalID}");
            Console.WriteLine($"Species: {animal.AnimalSpecy.SpeciesName}");
            Console.WriteLine($"Name: {animal.AnimalName}");
            Console.WriteLine($"Gender: {(animal.AnimalSex == "M" ? "Male" : "Female")}");
            Console.WriteLine($"Age: {animal.AnimalAge} years");
            if (animal.AnimalAdopted == false) Console.WriteLine($"Weight: {animal.AnimalWeight} lbs");
            Console.WriteLine($"Color: {animal.AnimalColor.ToLower()}");
            if (animal.AnimalAdopted == false) Console.WriteLine($"Food: {animal.AnimalFood} lbs/wk");
            Console.WriteLine($"Immunizations Recieved: {(animal.AnimalShots ? "Yes" : "No")}");
            if (animal.AnimalAdopted == false) Console.WriteLine($"Room: {room.RoomID}");
            if (animal.AnimalAdopted == true) Console.WriteLine($"Adopted by: {animal.Adopter.AdopterName}");
            if (animal.AnimalAdopted == true) Console.WriteLine($"Adopter ID: {animal.Adopter.AdopterID}");
            Console.WriteLine();
            Console.Write("Press Enter to return to Main Menu.");
            Console.ReadLine();
            Console.Clear();
            UI.DisplayMainMenu();
        }
        private void DisplayUpdateMenu(Animal animal)
        {
            UpdateAnimalNewPage();
            Console.WriteLine(" 1 - Age");
            Console.WriteLine(" 2 - Weight");
            Console.WriteLine(" 3 - Food");
            Console.WriteLine(" 4 - Shots");
            Console.WriteLine(" 5 - Room");
            Console.WriteLine(" 6 - Exit");
            NavigateUpdateMenu(UI.GetInt("Your Selection: "), animal);
        }
        private void NavigateUpdateMenu(int selection, Animal animal)
        {
            switch (selection)
            {
                case 1:
                    animal.AnimalAge = UI.GetInt("Enter new age: ");
                    Connection.UpdateAnimal(animal);
                    NavigateUpdateMenu(UI.GetInt("Your Selection: "), animal);
                    break;
                case 2:
                    animal.AnimalWeight = UI.GetDouble("Enter new weight: ");
                    Connection.UpdateAnimal(animal);
                    NavigateUpdateMenu(UI.GetInt("Your Selection: "), animal);
                    break;
                case 3:
                    animal.AnimalFood = UI.GetDouble("Enter new food: ");
                    Connection.UpdateAnimal(animal);
                    NavigateUpdateMenu(UI.GetInt("Your Selection: "), animal);
                    break;
                case 4:
                    animal.AnimalShots = UI.GetYesNoBool("Is animal immunized ");
                    Connection.UpdateAnimal(animal);
                    NavigateUpdateMenu(UI.GetInt("Your Selection: "), animal);
                    break;
                case 5:
                    var temp = Connection.GetRoom(animal);
                    AssignRoom(animal);
                    Connection.RemoveAnimalFromRoom(temp.RoomID);
                    NavigateUpdateMenu(UI.GetInt("Your Selection: "), animal);
                    break;
                case 6:
                    Console.Clear();
                    UI.DisplayMainMenu();
                    break;
                default:
                    Console.WriteLine("Invalid Input. Try again");
                    NavigateUpdateMenu(UI.GetInt("Your Selection: "), animal);
                    break;
            }
        }
        private void PrintAdoptedAnimals()
        {
            Console.WriteLine("Adopted Animals: ");
            Connection.PrintAnimals(true);
        }
        private void PrintUnadoptedAnimals()
        {
            Console.WriteLine("Unadopted Animals: ");
            Connection.PrintAnimals(false);
        }
        private void UpdateAnimalNewPage()
        {
            Console.Clear();
            Console.WriteLine("----------UPDATE ANIMAL---------");
        }
        private void AnimalInfoNewPage()
        {
            Console.Clear();
            Console.WriteLine("----------ANIMAL INFO---------");
        }
    }
}
