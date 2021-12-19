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

        public int loginUser(string name, string password)
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
                    reader.Read();
                    //Console.WriteLine("UserID: " + reader["id"]);
                    int result = Convert.ToInt32(reader["id"]);
                    disconnect();
                    return result;
                }
                else
                {
                    disconnect();
                    return 0;
                }
            }
        }

        public List<User> getAllUsersOrderedByElo() //anker
        {
            connect();
            using (var cmd = new NpgsqlCommand("SELECT * FROM users ORDER BY elo DESC", connection))
            {
                NpgsqlDataReader reader = cmd.ExecuteReader();
                List<User> userlist = new List<User>(); 
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        User user = new User((int)reader["id"],(string)reader["name"], (int)reader["coins"], (int)reader["elo"], (int)reader["wins"], (int)reader["losses"]); 
                        userlist.Add(user);
                    }
                }
                disconnect();
                return userlist;
            }
        }

        public string getProfiletextByUserID(int userid)
        {
            connect();
            using (var cmd = new NpgsqlCommand("SELECT profiletext FROM users WHERE id = @uid", connection))
            {
                cmd.Parameters.AddWithValue("uid", userid);
                NpgsqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                }
                string profiletext = Convert.ToString(reader["profiletext"]);
                disconnect();
                return profiletext;
            }
        }

        public void editProfileText(int userid, string profiletext)
        {
            connect();
            using (var cmd = new NpgsqlCommand("UPDATE users SET profiletext = @pt WHERE id = @uid", connection))
            {
                cmd.Parameters.AddWithValue("uid", userid);
                cmd.Parameters.AddWithValue("pt",profiletext);
                NpgsqlDataReader reader = cmd.ExecuteReader();
            }
            disconnect();
        }

        public void addCard(string name, int damage, int cardtype, int elementtype, int racetype)
        {
            connect();
            using (var cmd = new NpgsqlCommand("INSERT INTO cards (name, damage, cardtype, elementtype, racetype) VALUES (@n, @d, @c, @e, @r)", connection))
            {
                cmd.Parameters.AddWithValue("n", name);
                cmd.Parameters.AddWithValue("d", damage);
                cmd.Parameters.AddWithValue("c", cardtype);
                cmd.Parameters.AddWithValue("e", elementtype);
                cmd.Parameters.AddWithValue("r", racetype);
                cmd.ExecuteNonQuery();
            }
            disconnect();
        }


        public Card getCardByID(int cardid)
        {
            connect();
            using (var cmd = new NpgsqlCommand("SELECT * FROM cards WHERE id = @i", connection))
            {
                cmd.Parameters.AddWithValue("i", cardid);
                NpgsqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();                    
                }
                Card card = new Card((string)reader["name"], (int)reader["damage"], (CardTypesEnum.CardTypes)reader["cardtype"], (ElementTypesEnum.ElementTypes)reader["elementtype"], (RaceTypesEnum.RaceTypes)reader["racetype"]);
                disconnect();
                return card; 

            }
        }
        /*
        public Card getCard(int cardid)
        {
            int id = 381; //delete later
            connect();
            using (var cmd = new NpgsqlCommand("SELECT * FROM cards WHERE id = @i", connection))
            {
                cmd.Parameters.AddWithValue("i", id);
                NpgsqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    Card card = new Card((string)reader["name"], (int)reader["damage"], (CardTypesEnum.CardTypes)reader["cardtype"], (ElementTypesEnum.ElementTypes)reader["elementtype"], (RaceTypesEnum.RaceTypes)reader["racetype"]);
                    Console.WriteLine("CARD FROM DB:");
                    card.PrintCard();
                    disconnect();

                }

            }
        }
        */

        public List<Card> getStack(int userid)
        {

            connect();
            using (var cmd = new NpgsqlCommand("SELECT * FROM cards JOIN stack ON cards.id=stack.cardid WHERE userid = @i", connection))
            {
                cmd.Parameters.AddWithValue("i", userid);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                List<Card> stack = new List<Card>();

                if (reader.HasRows)
                {
                    
                    while (reader.Read())
                    {
                        Card card = new Card((int)reader["cardid"], (string)reader["name"], (int)reader["damage"], (CardTypesEnum.CardTypes)reader["cardtype"], (ElementTypesEnum.ElementTypes)reader["elementtype"], (RaceTypesEnum.RaceTypes)reader["racetype"]);
                        stack.Add(card);
                    }
                }
                disconnect(); 
                return stack; 
            }
        }

        public List<Card> getUnselectedStack(int userid)
        {

            connect();
            using (var cmd = new NpgsqlCommand("SELECT * FROM cards JOIN stack ON cards.id=stack.cardid WHERE userid = @i AND selected = false", connection))
            {
                cmd.Parameters.AddWithValue("i", userid);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                List<Card> stack = new List<Card>();

                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        Card card = new Card((int)reader["cardid"], (string)reader["name"], (int)reader["damage"], (CardTypesEnum.CardTypes)reader["cardtype"], (ElementTypesEnum.ElementTypes)reader["elementtype"], (RaceTypesEnum.RaceTypes)reader["racetype"]);
                        stack.Add(card);
                    }
                }
                disconnect();
                return stack;
            }
        }

        public void addCardToStack(int userid, int cardid)
        {
            connect();
            using (var cmd = new NpgsqlCommand("INSERT INTO stack (userid, cardid) VALUES (@uid, @cid)", connection))
            {
                cmd.Parameters.AddWithValue("uid", userid);
                cmd.Parameters.AddWithValue("cid", cardid);
                cmd.ExecuteNonQuery();
            }
            disconnect();
        }

        public void addTradingOffer(int ownerid, int cardid, int typerequirement, int damagerequirement)
        {
            connect();
            using (var cmd = new NpgsqlCommand("INSERT INTO trading (ownerid, cardid, typerequirement, damagerequirement) VALUES (@oid, @cid, @treq, @dmgreq)", connection))
            {
                cmd.Parameters.AddWithValue("oid", ownerid);
                cmd.Parameters.AddWithValue("cid", cardid);
                cmd.Parameters.AddWithValue("treq", typerequirement);
                cmd.Parameters.AddWithValue("dmgreq", damagerequirement);
                cmd.ExecuteNonQuery();
            }
            disconnect();
        }

        public void selectCard(int userid, int cardid)
        {
            connect();
            using (var cmd = new NpgsqlCommand("UPDATE stack SET selected = true WHERE userid = @uid AND cardid = @cid", connection))
            {
                cmd.Parameters.AddWithValue("uid", userid);
                cmd.Parameters.AddWithValue("cid", cardid);
                NpgsqlDataReader reader = cmd.ExecuteReader();
            }
            disconnect(); 
        }

        public void deselectCards(int userid) 
        {
            connect();
            using (var cmd = new NpgsqlCommand("UPDATE stack SET selected = false WHERE userid = @uid", connection))
            {
                cmd.Parameters.AddWithValue("uid", userid);
                NpgsqlDataReader reader = cmd.ExecuteReader();
            }
            disconnect();
        }

        public void deleteCardFromStack(int userid, int cardid) //fix duplicate deletion problem, i guess mit stackID
        {
            connect();
            using (var cmd = new NpgsqlCommand("DELETE FROM stack WHERE userid = @uid AND cardid = @cid", connection))
            {
                cmd.Parameters.AddWithValue("uid", userid);
                cmd.Parameters.AddWithValue("cid", cardid);
                NpgsqlDataReader reader = cmd.ExecuteReader();
            }
            disconnect();
        }

        public int getCardCount(int userid)
        {
            int ocards = 0;
            connect();
            using (var cmd = new NpgsqlCommand("SELECT COUNT(userid) AS ownedcards FROM stack WHERE userid = @uid", connection))
            {
                cmd.Parameters.AddWithValue("uid", userid);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                ocards = Convert.ToInt32(reader["ownedcards"]);
                Console.WriteLine(reader["ownedcards"]);
            }
            disconnect();
            return ocards;
        }

        public int getSelectedCardCount(int userid)
        {
            int selcards = 0;
            connect();
            using (var cmd = new NpgsqlCommand("SELECT COUNT(selected) AS selectedcards FROM stack WHERE userid = @uid AND selected = true", connection))
            {
                cmd.Parameters.AddWithValue("uid", userid);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                selcards = Convert.ToInt32(reader["selectedcards"]);
            }
            disconnect();
            return selcards; 
        }

        public List<Card> getSelectedStack(int userid)
        {
            connect();
            using (var cmd = new NpgsqlCommand("SELECT * FROM cards JOIN stack ON cards.id=stack.cardid WHERE userid = @uid AND selected = true", connection))
            {
                cmd.Parameters.AddWithValue("uid", userid);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                List<Card> cardlist = new List<Card>();

                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        Card card = new Card((int)reader["cardid"], (string)reader["name"], (int)reader["damage"], (CardTypesEnum.CardTypes)reader["cardtype"], (ElementTypesEnum.ElementTypes)reader["elementtype"], (RaceTypesEnum.RaceTypes)reader["racetype"]);
                        cardlist.Add(card);
                    }
                    disconnect();
                    return cardlist;
                }
                return cardlist; //evtl errorhandling in gamelogic/menu, je nach dem
            }
        }


        public int getRandomCardID()
        {
            connect();
            using (var cmd = new NpgsqlCommand("SELECT * FROM cards ORDER BY RANDOM()", connection))
            {
                NpgsqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                }
                int cardid = Convert.ToInt32(reader["id"]);
                disconnect();
                return cardid; 
                /*
                Card card = new Card((string)reader["name"], (int)reader["damage"], (CardTypesEnum.CardTypes)reader["cardtype"], (ElementTypesEnum.ElementTypes)reader["elementtype"], (RaceTypesEnum.RaceTypes)reader["racetype"]);
                Console.WriteLine("CARD FROM DB:");
                card.PrintCard();
                disconnect();
                return card; 
                */
            }
        }
        
        public int getCoinsByUserID(int userid)
        {
            connect();
            using (var cmd = new NpgsqlCommand("SELECT coins FROM users WHERE id = @i", connection))
            {
                cmd.Parameters.AddWithValue("i", userid);
                NpgsqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                }
                int coins = Convert.ToInt32(reader["coins"]);
                disconnect();
                return coins;
            }
        }

        public void setCoins(int userid, int coins)
        {
            connect();
            using (var cmd = new NpgsqlCommand("UPDATE users SET coins = @c WHERE id = @uid", connection))
            {
                cmd.Parameters.AddWithValue("uid", userid);
                cmd.Parameters.AddWithValue("c", coins);
                NpgsqlDataReader reader = cmd.ExecuteReader();
            }
            disconnect();
        }

        public void setElo(int userid, int elo)
        {
            connect();
            using (var cmd = new NpgsqlCommand("UPDATE users SET elo = @e WHERE userid = @uid", connection))
            {
                cmd.Parameters.AddWithValue("uid", userid);
                cmd.Parameters.AddWithValue("e", elo);
                NpgsqlDataReader reader = cmd.ExecuteReader();
            }
            disconnect();
        }

        public string getUsernameByUserID(int userid)
        {
            connect();
            using (var cmd = new NpgsqlCommand("SELECT name FROM users WHERE id = @i", connection))
            {
                cmd.Parameters.AddWithValue("i", userid);
                NpgsqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                }
                string username = Convert.ToString(reader["name"]);
                disconnect();
                return username;
            }
        }

        public User getUserByUserID(int userid) //onka
        {
            connect();
            using (var cmd = new NpgsqlCommand("SELECT * FROM users WHERE id = @i", connection))
            {
                cmd.Parameters.AddWithValue("i", userid);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                }
                User user = new User((int)reader["id"],(string)reader["name"], (int)reader["coins"], (int)reader["elo"], (int)reader["wins"], (int)reader["losses"]);
                disconnect();
                return user;
            }
        }

        public List<Tradeoffer> getTradingOffers()
        {
            connect();
            using (var cmd = new NpgsqlCommand("SELECT * FROM trading", connection))
            {
                NpgsqlDataReader reader = cmd.ExecuteReader();
                List<Tradeoffer> tradinglist = new List<Tradeoffer>();

                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        Tradeoffer tradeoffer = new Tradeoffer((int)reader["id"], (int)reader["ownerid"], (int)reader["cardid"], (CardTypesEnum.CardTypes)reader["typerequirement"], (int)reader["damagerequirement"]);
                        tradinglist.Add(tradeoffer);
                    }
                }
                disconnect();
                return tradinglist;
            }
        }

        public void deleteTradingOffer(int tradingid)
        {
            connect();
            using (var cmd = new NpgsqlCommand("DELETE FROM trading WHERE id = @tid", connection))
            {
                cmd.Parameters.AddWithValue("tid", tradingid);
                NpgsqlDataReader reader = cmd.ExecuteReader();
            }
            disconnect();
        }

        public List<Tradeoffer> getOwnTradingOffers(int userid)
        {
            connect();
            using (var cmd = new NpgsqlCommand("SELECT * FROM trading WHERE ownerid = @uid", connection))
            {
                cmd.Parameters.AddWithValue("uid", userid);
                NpgsqlDataReader reader = cmd.ExecuteReader();


                List<Tradeoffer> tradinglist = new List<Tradeoffer>();

                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        Tradeoffer tradeoffer = new Tradeoffer((int)reader["id"], (int)reader["ownerid"], (int)reader["cardid"], (CardTypesEnum.CardTypes)reader["typerequirement"], (int)reader["damagerequirement"]);
                        tradinglist.Add(tradeoffer);
                    }
                }
                disconnect(); //CHECKEN OB ALLES GEHT NOCHM FOODEN xd
                return tradinglist;


            }
        }

        public List<Card> getCardsByRequirement(int userid, int cardtype, int mindamage)
        {
            connect();
            using (var cmd = new NpgsqlCommand("SELECT * FROM cards JOIN stack ON cards.id=stack.cardid WHERE userid = @uid AND damage >= @mindmg AND cardtype = @ct AND selected = false;", connection))
            {
                cmd.Parameters.AddWithValue("uid", userid);
                cmd.Parameters.AddWithValue("mindmg", mindamage);
                cmd.Parameters.AddWithValue("ct", cardtype);

                NpgsqlDataReader reader = cmd.ExecuteReader();
                List<Card> cardlist = new List<Card>();

                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        Card card = new Card((int)reader["cardid"], (string)reader["name"], (int)reader["damage"], (CardTypesEnum.CardTypes)reader["cardtype"], (ElementTypesEnum.ElementTypes)reader["elementtype"], (RaceTypesEnum.RaceTypes)reader["racetype"]);
                        cardlist.Add(card);
                    }
                    disconnect();
                    return cardlist;
                }
                return cardlist; //ERROR HANDLING IF NO CARDS

            }
        }


        public int getEloByUserID(int userid)
        {
            connect();
            using (var cmd = new NpgsqlCommand("SELECT elo FROM users WHERE id = @i", connection))
            {
                cmd.Parameters.AddWithValue("i", userid);
                NpgsqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                }
                int elo = Convert.ToInt32(reader["elo"]);
                disconnect();
                return elo;
            }
        }

        public int getWinsByUserID(int userid)
        {
            connect();
            using (var cmd = new NpgsqlCommand("SELECT wins FROM users WHERE id = @uid", connection))
            {
                cmd.Parameters.AddWithValue("uid", userid);
                NpgsqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                }
                int wins = Convert.ToInt32(reader["wins"]);
                disconnect();
                return wins;
            }
        }

        public int getLossesByUserID(int userid)
        {
            connect();
            using (var cmd = new NpgsqlCommand("SELECT losses FROM users WHERE id = @uid", connection))
            {
                cmd.Parameters.AddWithValue("uid", userid);
                NpgsqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                }
                int losses = Convert.ToInt32(reader["losses"]);
                disconnect();
                return losses;
            }
        }

        public void incrementWins(int userid)
        {
            connect();
            using (var cmd = new NpgsqlCommand("UPDATE users SET wins = wins + 1 WHERE id = @uid", connection))
            {
                cmd.Parameters.AddWithValue("uid", userid);
                NpgsqlDataReader reader = cmd.ExecuteReader();
            }
            disconnect();
        }
        public void incrementLosses(int userid)
        {
            connect();
            using (var cmd = new NpgsqlCommand("UPDATE users SET losses = losses + 1 WHERE id = @uid", connection))
            {
                cmd.Parameters.AddWithValue("uid", userid);
                NpgsqlDataReader reader = cmd.ExecuteReader();
            }
            disconnect();
        }
        public void increaseElo(int userid)
        {
            connect();
            using (var cmd = new NpgsqlCommand("UPDATE users SET elo = elo + 3 WHERE id = @uid", connection))
            {
                cmd.Parameters.AddWithValue("uid", userid);
                NpgsqlDataReader reader = cmd.ExecuteReader();
            }
            disconnect();
        }

        public void decreaseElo(int userid)
        {
            connect();
            using (var cmd = new NpgsqlCommand("UPDATE users SET elo = elo - 5 WHERE id = @uid", connection))
            {
                cmd.Parameters.AddWithValue("uid", userid);
                NpgsqlDataReader reader = cmd.ExecuteReader();
            }
            disconnect();
        }



    }
}
