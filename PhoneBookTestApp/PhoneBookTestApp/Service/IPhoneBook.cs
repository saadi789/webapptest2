using System.Collections.Generic;

namespace PhoneBookTestApp
{
    public interface IPhoneBook
    {
        Person FindPerson(string firstName, string lastName);
        void AddPerson(Person newPerson);
        Person GetNewPerson(string firstName, string lastname, 
            string address, string number);
        List<Person> GetAll();
    }
}