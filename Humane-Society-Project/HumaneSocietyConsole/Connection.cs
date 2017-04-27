using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSocietyConsole
{
    static class Connection
    {
        public static int SaveAnimalToDatabase(int species, string name, string sex, int age, double weight, string color, double food, bool shots, bool adopted )
        {
            DatabaseConnectionDataContext database = new DatabaseConnectionDataContext();

            Animal newAnimal = new Animal();
            newAnimal.AnimalSpecies = species;
            newAnimal.AnimalName = name;
            newAnimal.AnimalSex = sex;
            newAnimal.AnimalAge = age;
            newAnimal.AnimalWeight = weight;
            newAnimal.AnimalColor = color;
            newAnimal.AnimalFood = food;
            newAnimal.AnimalShots = shots;
            newAnimal.AnimalAdopted = adopted;

            database.Animals.InsertOnSubmit(newAnimal);

            try
            {
                database.SubmitChanges();
            }
            catch (Exception e)
            {
                Console.Write(e);
            }

            return newAnimal.AnimalID;
        }
        public static bool SaveRoomToDatabase(int animalID, int roomID)
        {
            DatabaseConnectionDataContext database = new DatabaseConnectionDataContext();

            var room =
                (from r in database.Rooms
                where r.RoomID == roomID
                select r).First();

            if (room.AnimalID == null)
            {
                room.AnimalID = animalID;
            }
            else
            {
                Console.Write("Room Occupied. Select a different Room.");
                Console.WriteLine("Available Rooms: ");
                PrintAvailableRooms();
                return false;
            }          
            try
            {
                database.SubmitChanges();
            }
            catch
            {
                Console.WriteLine("Something went wrong while searching for Animal. Check ID.");
            }
            return true;

        }
        public static void PrintAvailableRooms()
        {
            DatabaseConnectionDataContext database = new DatabaseConnectionDataContext();

            var room = database.Rooms.Where(r => r.AnimalID == null).Select(s => s.RoomID.ToString());

            Console.WriteLine(room.Aggregate((x, y) => x + y));
        }
        public static void PrintCurrentAnimals()
        {
            DatabaseConnectionDataContext database = new DatabaseConnectionDataContext();

            var animals = database.Animals.Where(w => w.AnimalAdopted == false).Select(s => s.AnimalID.ToString() + " " + s.AnimalName.ToString() + " " 
                            + (database.AnimalSpecies.Where(a => a.SpeciesID == s.AnimalSpecies).Select(a => a.SpeciesName).Single()) + " "
                            + "Room #" + (database.Rooms.Where(b => b.AnimalID == s.AnimalID).Select(c => c.RoomID).Single().ToString()));
            Console.WriteLine(string.Join("\n", animals));
            Console.WriteLine();
        }
        public static void PrintAdoptedAnimals()
        {
            DatabaseConnectionDataContext database = new DatabaseConnectionDataContext();

            var animals = database.Animals.Where(w => w.AnimalAdopted == true).Select(s => s.AnimalID.ToString() + " " + s.AnimalName.ToString() + " "
                            + (database.AnimalSpecies.Where(a => a.SpeciesID == s.AnimalSpecies).Select(a => a.SpeciesName).Single()) + " "
                            + "Room #" + (database.Rooms.Where(b => b.AnimalID == s.AnimalID).Select(c => c.RoomID).Single().ToString()));

            Console.WriteLine("Adopted Animals");
            Console.WriteLine(string.Join("\n", animals));
        }
        public static Animal GetAnimal(int animalID)

        {
            DatabaseConnectionDataContext database = new DatabaseConnectionDataContext();

            try
            {
                var animal = database.Animals.Where(w => w.AnimalID == animalID).Select(s => s).First();

                return animal;
            }
            catch
            {
                Console.WriteLine("Animal ID not found please check ID and try again.");
                return null;
            }
        }
        public static int? GetRoom(int animalID)

        {
            DatabaseConnectionDataContext database = new DatabaseConnectionDataContext();

            try
            {
                var room = database.Rooms.Where(w => w.AnimalID == animalID).Select(s => s.RoomID).First();

                return room;
            }
            catch
            {
                Console.WriteLine("Something went wrong while accessing the rooms table.");
                return null;
            }
        }
        public static void UpdateAnimal(Animal updatedAnimal)
        {
            DatabaseConnectionDataContext database = new DatabaseConnectionDataContext();

            var animal = database.Animals.Where(w => w.AnimalID == updatedAnimal.AnimalID).Select(s => s).First();

            animal.AnimalAge = updatedAnimal.AnimalAge;
            animal.AnimalWeight = updatedAnimal.AnimalWeight;
            animal.AnimalFood = updatedAnimal.AnimalFood;
            animal.AnimalShots = updatedAnimal.AnimalShots;

            try
            {
                database.SubmitChanges();
                Console.WriteLine("Animal updated successfully.");
            }
            catch
            {
                Console.WriteLine("Something went wrong while updating the animal.");
            }
        }
    }
}
