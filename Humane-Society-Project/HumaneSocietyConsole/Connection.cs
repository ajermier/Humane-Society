using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSocietyConsole
{
    static class Connection
    {
        public static Animal SaveAnimalToDatabase(int species, string name, string sex, int age, double weight, string color, double food, bool shots, bool adopted )
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

            return newAnimal;
        }
        public static int SaveAdopterToDatabase(string name, string phone, bool homeOwner, bool newPetOwner, string bio)
        {
            DatabaseConnectionDataContext database = new DatabaseConnectionDataContext();

            Adopter newAdopter = new Adopter();
            newAdopter.AdopterName = name;
            newAdopter.AdopterPhone = phone;
            newAdopter.AdopterHomeOwner = homeOwner;
            newAdopter.AdopterNewPetOwner = newPetOwner;
            newAdopter.AdopterBio = bio;

            database.Adopters.InsertOnSubmit(newAdopter);

            try
            {
                database.SubmitChanges();
                Console.WriteLine($"New Adopter {newAdopter.AdopterName} successfully added.");
            }
            catch (Exception e)
            {
                Console.Write(e);
                Console.WriteLine("Something went wrong while saving the new adopter to the database.");
            }

            return 1;
        }
        public static bool SaveRoomToDatabase(int? animalID, int roomID)
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
                Console.WriteLine($"Successfully moved {room.Animal.AnimalName} to room #{room.RoomID}");
            }
            catch
            {
                Console.WriteLine("Something went wrong while searching for Animal. Check ID.");
            }
            return true;

        }
        public static void RemoveAnimalFromRoom(int? roomID)
        {
            DatabaseConnectionDataContext database = new DatabaseConnectionDataContext();
            var room = database.Rooms.Where(r => r.RoomID == roomID).Select(s => s).First();

            room.AnimalID = null;
            try
            {
                database.SubmitChanges();
            }
            catch
            {
                Console.WriteLine("Something went wrong while searching for Animal. Check ID.");
            }
        }
        public static void PrintAvailableRooms()
        {
            DatabaseConnectionDataContext database = new DatabaseConnectionDataContext();

            var room = database.Rooms.Where(r => r.AnimalID == null).Select(s => s.RoomID.ToString()).ToList();

            Console.WriteLine(room.Aggregate((x, y) => x + ", " + y));
        }
        public static void PrintCurrentAnimals()
        {
            DatabaseConnectionDataContext database = new DatabaseConnectionDataContext();

            var animals = database.Animals.Where(w => w.AnimalAdopted == false).Select(s => s.AnimalID.ToString() + " " + s.AnimalName.ToString() + " "
                            + s.AnimalSpecy.SpeciesName + "Room #" + database.Rooms.Where(a => a.AnimalID == s.AnimalID).Select(r => r.RoomID).First().ToString());
            Console.WriteLine(string.Join("\n", animals));
            Console.WriteLine();
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
        public static int? GetRoom(Animal animal)

        {
            DatabaseConnectionDataContext database = new DatabaseConnectionDataContext();

            try
            {
                var room = database.Rooms.Where(w => w.AnimalID == animal.AnimalID).Select(s => s).First();

                return room.RoomID;
            }
            catch
            {
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
        public static List<Animal> FilterBySpecies(int speciesCode)
        {
            DatabaseConnectionDataContext database = new DatabaseConnectionDataContext();

            var list = database.Animals.Where(w => w.AnimalSpecies == speciesCode).Select(s => s).ToList();

            return list;
        }
    }
}
