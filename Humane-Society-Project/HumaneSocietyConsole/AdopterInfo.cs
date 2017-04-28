using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSocietyConsole
{
    partial class Manager : IAnimal
    {
        private string adopterName;
        private string phone;
        private bool homeOwner;
        private bool newPetOwner;
        private string bio;
        private int count = 0;
        private List<Animal> searchList;

        public void AddAdopter()
        {
            AddAdopterPage();
            GetAdopterInfo();
            Connection.SaveAdopterToDatabase(adopterName, phone, homeOwner, newPetOwner, bio);
            Console.Write("Press Enter to return to Main Menu.");
            Console.ReadLine();
            Console.Clear();
            UI.DisplayMainMenu();
        }
        public Adopter AddNewAdopter()
        {
            AddAdopterPage();
            GetAdopterInfo();
            var adopter = Connection.SaveAdopterToDatabase(adopterName, phone, homeOwner, newPetOwner, bio);
            Console.Write("Press Enter to return.");
            Console.ReadLine();
            Console.Clear();

            return adopter;
        }
        private void GetAdopterInfo()
        {
            adopterName = UI.GetString("Enter name: ");
            phone = UI.GetString("Enter phone number: ");
            homeOwner = UI.GetYesNoBool("Owns/Rents house ");
            newPetOwner = UI.GetYesNoBool("New pet owner ");
            bio = UI.GetString("Enter adopter bio: ");
        }
        public void GetAdopterInfoForDisplay()
        {
            AdopterInfoPage();
            var adopterList = Connection.ReturnAdopterList();
            DisplaySelectFromList(adopterList);
            Adopter adopter = SelectAdopterFromList(adopterList);
            DisplayAdopter(adopter);
        }
        public void AdopterSearchMenu()
        {
            if (count < 1) AdopterSearchPage();
            if (count < 4) Console.WriteLine(" 1 - Refine Search");
            if (count > 0) Console.WriteLine(" 2 - Select animal from list to adopt");
            Console.WriteLine(" 3 - Exit");
            int input = UI.GetInt("Your Selection: ");
            NavigateAdopterSearchMenu(input);
        }
        private void NavigateAdopterSearchMenu(int selection)
        {
            switch (selection)
            {
                case 1:
                    if(count < 1)
                    {
                        AdopterSearchPage();
                        searchList = Connection.FilterBySpecies(GetSpecies());
                        DisplaySearchResults(searchList);
                        count++;
                        AdopterSearchMenu();
                    }
                    else if(count < 2)
                    {
                        AdopterSearchPage();
                        searchList = FilterBySex(GetSex(), searchList);
                        DisplaySearchResults(searchList);
                        count++;
                        AdopterSearchMenu();
                    }
                    else if(count < 3)
                    {
                        AdopterSearchPage();
                        searchList = FilterByAge(UI.GetInt("Enter maximum age to search for: "), searchList);
                        DisplaySearchResults(searchList);
                        count++;
                        AdopterSearchMenu();
                    }
                    break;
                case 2:
                    DisplaySelectFromList(searchList);
                    AdoptAnimal(GetAnimalID(searchList));
                    break;
                case 3:
                    Console.Clear();
                    UI.DisplayMainMenu();
                    break;
                default:
                    Console.WriteLine("Invalid input. Try again.");
                    NavigateAdopterSearchMenu(UI.GetInt("Your Selection: "));
                    break;
            }
        }
        private List<Animal> FilterBySex(string sex, List<Animal> list)
        {
            var newList = list.Where(a => a.AnimalSex == sex).OrderBy(o => o.AnimalSex).Select(s => s).ToList();

            return newList;
        }
        private List<Animal> FilterByAge(int age, List<Animal> list)
        {
            var newList = list.Where(a => a.AnimalAge <= age).OrderBy(o => o.AnimalAge).Select(s => s).ToList();

            return newList;
        }
        private void DisplaySearchResults(List<Animal> list)
        {
            Console.WriteLine();
            if (list.Count == 0)
            {
                Console.Write("Sorry we have no animals meeting that search criteria.");
                UI.ReturnToMainMenu();
            }
            else
            {
                foreach (Animal a in list)
                {
                    Console.WriteLine($"ID {a.AnimalID} is a {a.AnimalColor} {a.AnimalSpecy.SpeciesName} named {a.AnimalName} who is a {a.AnimalAge} year old {(a.AnimalSex == "M" ? "male" : "female")}.");
                }
            }
        }
        private void DisplaySelectFromList(List<Animal> list)
        {
            Console.WriteLine();
            if (list.Count == 0)
            {
                Console.Write("Sorry we have no animals on record.");
                UI.ReturnToMainMenu();
            }
            else
            {
                foreach (Animal a in list)
                {              
                    Console.WriteLine($"ID# {a.AnimalID} {a.AnimalName}.");
                }
            }
        }
        private void DisplaySelectFromList(List<Adopter> list)
        {
            if (list.Count == 0)
            {
                Console.Write("Sorry we have no adopters on record.");
                UI.ReturnToMainMenu();
            }
            else
            {
                Console.WriteLine("Adopters: ");
                foreach (Adopter a in list)
                {
                    Console.WriteLine($"ID# {a.AdopterID} {a.AdopterName}.");
                }
            }
        }
        private Adopter SelectAdopterFromList(List<Adopter> list)
        {
            int adopterID = UI.GetInt("Enter ID: ");

            while (!list.Exists(e => e.AdopterID == adopterID))
            {
                Console.WriteLine("ID not found. Try again.");
                adopterID = UI.GetInt("Enter ID: ");
            }

            return list.Where(w => w.AdopterID == adopterID).Select(s => s).First();
        }
        private void DisplayAdopter(Adopter adopter)
        {
            AdopterInfoPage();

            Console.WriteLine($"Adopter ID: {adopter.AdopterID}");
            Console.WriteLine($"Adopter Name: {adopter.AdopterName}");
            Console.WriteLine($"Phone: {adopter.AdopterPhone}");
            Console.WriteLine($"Homeowner/renter: {(adopter.AdopterHomeOwner ? "Yes." : "No.")}");
            Console.WriteLine($"New pet owner: {(adopter.AdopterNewPetOwner ? "Yes." : "No.")}");
            Console.WriteLine($"Bio: {adopter.AdopterBio}");
            Console.WriteLine();

            UI.ReturnToMainMenu();
        }
        private Adopter GetAdopter()
        {
            if(UI.GetYesNoBool("Is the adopter in the system "))
            {
                int adopterID = UI.GetInt("Enter adopter's ID: ");
                var adopter = Connection.GetAdopter(adopterID);
                if (adopter == null)
                {
                    Console.WriteLine("Invalid ID. Try again.");
                    return GetAdopter();
                }
                return adopter;
            }
            else
            {              
                return AddNewAdopter();
            }
        }
        private void AddAdopterPage()
        {
            Console.Clear();
            Console.WriteLine("----------ADD ADOPTER---------");
        }
        private void AdopterSearchPage()
        {
            Console.Clear();
            Console.WriteLine("----------SEARCH ANIMALS---------");
        }
        private void AdopterInfoPage()
        {
            Console.Clear();
            Console.WriteLine("----------ADOPTER INFO---------");
        }
    }
}
