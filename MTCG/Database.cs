using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql; 

namespace MTCG
{
    class Database
    {
        const string connectionstring = "Host=localhost;Username=postgres;Password=;Database=postgres";
        private NpgsqlConnection connection; 

        public NpgsqlConnection connect()
        {
            connection = new NpgsqlConnection(connectionstring);
            connection.Open();
            return connection; 
        }

        public void disconnect()
        {
            connection.Close(); 
        }

        public void addUser()
        {
            using (var cmd = new NpgsqlCommand("INSERT INTO users (name, password, coins, elo) VALUES (@u, @p, @c, @e)", connection))
            {
                cmd.Parameters.AddWithValue("u", "testUser123");
                cmd.Parameters.AddWithValue("p", "testPassword123");
                cmd.Parameters.AddWithValue("c", 13);
                cmd.Parameters.AddWithValue("e", 20);
                cmd.ExecuteNonQuery();
            }
        }


        public void outputTest()
        {
            using (var cmd = new NpgsqlCommand("SELECT * FROM users", connection))
            {
                NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        Console.Write(reader[i] + "  ");
                    }
                    Console.WriteLine();
                    Console.WriteLine("------------------------------------------------------------------------");
                }
            }
        }

    }
}
