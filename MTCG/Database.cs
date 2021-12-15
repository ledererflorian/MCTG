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

        public void addUser(string name, string password, int coins, int elo)
        {
            connect(); 
            using (var cmd = new NpgsqlCommand("INSERT INTO users (name, password, coins, elo) VALUES (@u, @p, @c, @e)", connection))
            {
                cmd.Parameters.AddWithValue("u", name);
                cmd.Parameters.AddWithValue("p", password);
                cmd.Parameters.AddWithValue("c", coins);
                cmd.Parameters.AddWithValue("e", elo);
                cmd.ExecuteNonQuery();
            }
            disconnect(); 
        }

        public bool userExists(string name)
        {
            connect(); 
            using (var cmd = new NpgsqlCommand("SELECT * FROM users WHERE name = @n ", connection))
            {
                cmd.Parameters.AddWithValue("n", name);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                //string name; 
                
                if(reader.HasRows)
                {
                    //reader.Read();
                    disconnect(); 
                    return true; 
                } else
                {
                    disconnect(); 
                    return false; 
                }

                /*
                name = reader["name"].ToString();

                Console.WriteLine(reader["coins"].ToString());

                if (name == "Simon")
                {
                    Console.WriteLine("USER EXISTS");
                    return 1; 
                } 
                */

            }
        }

        public bool loginUser(string name, string password)
        {
            connect();
            using (var cmd = new NpgsqlCommand("SELECT * FROM users WHERE name = @n AND password = @p", connection))
            {
                cmd.Parameters.AddWithValue("n", name);
                cmd.Parameters.AddWithValue("p", password);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                //string name; 

                if (reader.HasRows)
                {
                    //reader.Read();
                    disconnect();
                    return true;
                }
                else
                {
                    disconnect();
                    return false;
                }

                /*
                name = reader["name"].ToString();

                Console.WriteLine(reader["coins"].ToString());

                if (name == "Simon")
                {
                    Console.WriteLine("USER EXISTS");
                    return 1; 
                } 
                */
            }
        }


        public void outputTest()
        {
            connect(); 
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
            disconnect(); 
        }

    }
}
