using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace PhoneBookTestApp
{
    public class PhoneBook : IPhoneBook
    {
        public void AddPerson(Person newPerson)
        {
            DatabaseUtil.InsertPerson(newPerson.Name, newPerson.PhoneNumber,
                newPerson.Address);
        }

        public Person FindPerson(string firstName, string lastName)
        {
            return Search($"{firstName} {lastName}").FirstOrDefault()??null;
        }


        public List<Person> GetAll()
        {
            var paramaters = new KeyValuePair<string, object>[1];
            paramaters[0] = new KeyValuePair<string, object>
            ("@name", "Cynthia Smith");
            try
            {
                var reader = DatabaseUtil.Query
                ("Select * from PHONEBOOK", null);
                return GetResults(reader);
            }
            catch
            {
                throw;
            }
            finally
            {
                var dbConnection = DatabaseUtil.GetConnection();
                dbConnection.Close();
            }
        }

        private List<Person> Search(string name)
        {
            var paramaters = new KeyValuePair<string, object>[1];
            paramaters[0] = new KeyValuePair<string, object>
            ("@name", name.ToLower());
            try
            {
                var reader = DatabaseUtil.Query
                ("Select * from PHONEBOOK where LOWER (name) = @name", paramaters);
                return GetResults(reader);
            }
            catch
            {
                throw;
            }
            finally
            {
                var dbConnection = DatabaseUtil.GetConnection();
                dbConnection.Close();
            }
        }

        private static List<Person> GetResults(SQLiteDataReader reader)
        {
            var people = new List<Person>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    people.Add(new Person
                    {
                        Name = reader["NAME"].ToString(),
                        Address = reader["ADDRESS"].ToString(),
                        PhoneNumber = reader["PHONENUMBER"].ToString()
                    });
                }
                return people;
            }
            return people;
        }

        public Person GetNewPerson(string firstName, 
            string lastname,
            string address, 
            string number)
        {
            return new Person
            {
                Name = $"{firstName} {lastname}",
                Address = address,
                PhoneNumber = number,
            };
        }
    }
}