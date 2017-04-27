using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSocietyConsole
{
    class AdopterInfo: Manager
    {
        private string name;
        private string phone;
        private bool homeOwner;
        private bool newPetOwner;
        private string bio;
        private int count = 0;
        private List<Animal> searchList;

        public AdopterInfo()
        {
            
        }

        public void AddAdopter()
        {
            AddAdopterPage();
            GetAdopterInfo();
            Connection.SaveAdopterToDatabase(name, phone, homeOwner, newPetOwner, bio);
            Console.Write("Press Enter to return to Main Menu.");
            Console.ReadLine();
            Console.Clear();
            UI.DisplayMainMenu();
        }
        private void GetAdopterInfo()
        {
            name = UI.GetString("Enter name: ");
            phone = UI.GetString("Enter phone number: ");
            homeOwner = UI.GetYesNoBool("Owns/Rents house ");
            newPetOwner = UI.GetYesNoBool("New pet owner ");
            bio = UI.GetString("Enter adopter bio: ");
        }
        public void AdopterSearchMenu()
        {
            if (count < 1) AdopterSearchPage();
            if (count < 4) Console.WriteLine(" 1 - Refine Search");
            Console.WriteLine(" 2 - Exit");
            NavigateAdopterSearchMenu(UI.GetInt("Your Selection: "), count);
        }
        private void NavigateAdopterSearchMenu(int selection, int count)
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
                    Console.Clear();
                    UI.DisplayMainMenu();
                    break;
                default:
                    Console.WriteLine("Invalid input. Try again.");
                    NavigateAdopterSearchMenu(UI.GetInt("Your Selection: "), count);
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
                Console.Write("Sorry we have no animals meeting that search criteria. Press enter to return to Main Menu.");
                Console.ReadLine();
                Console.Clear();
                UI.DisplayMainMenu();
            }
            else
            {
                foreach (Animal a in list)
                {
                    Console.WriteLine($"ID {a.AnimalID} is a {a.AnimalColor} {a.AnimalSpecy.SpeciesName} named {a.AnimalName} who is a {a.AnimalAge} year old {a.AnimalSex}.");
                }
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
    }
}
