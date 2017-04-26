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
            bool occupied = false;
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
                return occupied;
            }          
            try
            {
                database.SubmitChanges();
            }
            catch (Exception e)
            {
                Console.Write(e);
            }
            return true;

        }
        public static void PrintAvailableRooms()
        {
            DatabaseConnectionDataContext database = new DatabaseConnectionDataContext();

            var room =
                from r in database.Rooms
                where r.AnimalID == null
                select r;

            foreach(Room r in room)
            {
                Console.Write($"{r.RoomID} ");
            }
        }
    }
}
