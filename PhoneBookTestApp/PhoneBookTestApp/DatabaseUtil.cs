using System;
using System.Data.SQLite;
using System.Linq;
using System.Data.SQLite.Linq;
using System.Collections.Generic;

namespace PhoneBookTestApp
{
    public class DatabaseUtil
    {
        public static void initializeDatabase()
        {
            var dbConnection = new SQLiteConnection("Data Source= MyDatabase.sqlite;Version=3;");
            dbConnection.Open();

            try
            {
                SQLiteCommand command =
                    new SQLiteCommand(
                        "create table PHONEBOOK (NAME varchar(255), PHONENUMBER varchar(255), ADDRESS varchar(255))",
                        dbConnection);
                command.ExecuteNonQuery();

                command =
                    new SQLiteCommand(
                        "INSERT INTO PHONEBOOK (NAME, PHONENUMBER, ADDRESS) VALUES('Chris Johnson','(321) 231-7876', '452 Freeman Drive, Algonac, MI')",
                        dbConnection);
                command.ExecuteNonQuery();

                command =
                    new SQLiteCommand(
                        "INSERT INTO PHONEBOOK (NAME, PHONENUMBER, ADDRESS) VALUES('Dave Williams','(231) 502-1236', '285 Huron St, Port Austin, MI')",
                        dbConnection);
                command.ExecuteNonQuery();

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dbConnection.Close();
            }
        }

        public static SQLiteConnection GetConnection()
        {
            var dbConnection = new SQLiteConnection("Data Source= MyDatabase.sqlite;Version=3;");

            return dbConnection;
        }

        public static void CleanUp()
        {
            var dbConnection = new SQLiteConnection("Data Source= MyDatabase.sqlite;Version=3;");
            dbConnection.Open();

            try
            {
                SQLiteCommand command =
                    new SQLiteCommand(
                        "drop table PHONEBOOK",
                        dbConnection);
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dbConnection.Close();
            }
        }

        public static bool InsertPerson(string name, string number, string address)
        {
            try
            {
                var dbConnection = new SQLiteConnection("Data Source= MyDatabase.sqlite;Version=3;");
                dbConnection.Open();
                var command =
                    new SQLiteCommand(
                        $"INSERT INTO PHONEBOOK (NAME, PHONENUMBER, ADDRESS) "
                        +$"VALUES('{name}','{number}', '{address}')",
                        dbConnection);
                command.ExecuteNonQuery();
                dbConnection.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static SQLiteDataReader Query(string selectCommand,
    params KeyValuePair<string, object>[] parameters)
        {
            var connection = GetConnection();
            var command = new SQLiteCommand(selectCommand, connection);

            if (parameters != null)
            {
                foreach (var p in parameters)
                    command.Parameters.Add(new SQLiteParameter(p.Key, p.Value));
            }

            connection.Open();
            var result = command.ExecuteReader();
            return result;
        }

        
    }
}