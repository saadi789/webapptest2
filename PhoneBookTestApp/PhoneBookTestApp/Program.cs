using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBookTestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //DatabaseUtil.CleanUp();
                DatabaseUtil.initializeDatabase();
                var phonebook = new PhoneBook();
                /* TODO: create person objects and put them in the PhoneBook and database
                * John Smith, (248) 123-4567, 1234 Sand Hill Dr, Royal Oak, MI
                * Cynthia Smith, (824) 128-8758, 875 Main St, Ann Arbor, MI
                */
                AddInitialPeople(phonebook);

                // TODO: print the phone book out to System.out
                PrintPhonebook(phonebook);


                // TODO: find Cynthia Smith and print out just her entry
                SearchForAnEntry(phonebook);

                // TODO: insert the new person objects into the database
            }
            finally
            {
                DatabaseUtil.CleanUp();
            }
        }

        private static void SearchForAnEntry(PhoneBook phonebook)
        {
            Console.Write("Search for an entry: ");
            Console.WriteLine("Please enter first name");
            var firstname = Console.ReadLine();

            Console.WriteLine("Please enter surname");
            var surname = Console.ReadLine();

            var person = phonebook.FindPerson(firstname, surname);
            if (person == null)
            {
                Console.WriteLine("No entry found");
                return;
            }
            Console.WriteLine($"Name: {person.Name} " +
                $"Address: {person.Address} " +
                $"Phone: {person.PhoneNumber}");
        }

        private static void PrintPhonebook(PhoneBook phonebook)
        {
            var people = phonebook.GetAll();
            if (people != null && people.Count > 0)
            {
                foreach (var person in people)
                {
                    Console.WriteLine($"Name: {person.Name} " +
                    $"Address: {person.Address} " +
                    $"Phone: {person.PhoneNumber}");
                }
            }
        }

        private static void AddInitialPeople(PhoneBook phonebook)
        {
            var people = GetPeople(phonebook);

            foreach (var person in people)
            {
                phonebook.AddPerson(person);
            }
        }

        static List<Person> GetPeople(PhoneBook phoneBook)
        {
            var list = new List<Person>();

            list.Add(phoneBook.GetNewPerson("John",
                    "Smith",
                    "1234 Sand Hill Dr, Royal Oak, MI",
                    "(248) 123-4567"));

            list.Add(phoneBook.GetNewPerson("Cynthia",
                    "Smith",
                    "875 Main St, Ann Arbor, MI",
                    "(824) 128-8758"));

            return list;
        }
    }
}
