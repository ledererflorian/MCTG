using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG
{
    public class User
    {
        public int _userid { get; set; }
        public string _name { get; set; }
        public int _coins {get; set; }
        public int _elo { get; set; }
        public int _wins { get; set; }
        public int _losses { get; set; }

        public Stack _userstack;
        public Deck _userdeck;
        /*
        public User(string name, int coins, int elo, int wins, int losses)
        {
            //Database db = new Database(); 
            _name = name;
            _coins = coins;
            _elo = elo;
            _wins = wins;
            _losses = losses; 
        }
        */

        public User(string name, int coins, int elo, int wins, int losses)
        {
            _name = name;
            _coins = coins;
            _elo = elo;
            _wins = wins;
            _losses = losses;
        }


        public User(int userid, string name, int coins, int elo, int wins, int losses)
        {
            _userid = userid; 
            _name = name;
            _coins = coins;
            _elo = elo;
            _wins = wins;
            _losses = losses;
        }

        public void CreateDeck()
        {
            Database db = new Database(); 
            int input = 0; 
            _userdeck._deck.Clear();

            //List<Card> tempstack = new List<Card>(_userstack._stack);
            db.deselectCards(_userid);
            List<Card> tempstack = db.getStack(_userid); 
            
            while (_userdeck._deck.Count < 4)
            {
                Console.Clear();
                for (int i = 0; i < tempstack.Count(); i++)
                {
                    Console.Write($"{i + 1}: ");
                    tempstack[i].PrintCard();
                }
                Console.WriteLine();
                Console.WriteLine($"Select the cards for your deck by entering their associated ID's. [{_userdeck._deck.Count}/4]");

                input = Convert.ToInt32(Console.ReadLine());

                if (input > tempstack.Count())
                {
                    Console.WriteLine("Invalid input");
                }
                else
                {
                    _userdeck.AddCard(tempstack[input - 1]);
                    db.selectCard(_userid, tempstack[input - 1]._cardid);
                    tempstack.RemoveAt(input - 1);
                }
            }
        }

        public void PrintUser()
        {
            Console.WriteLine("Name: " + _name + " - " + "Elo: " + _elo);
        }

        public void Shop()
        {
            Database db = new Database();
            int cardid = 0; 

            Console.Clear();
            Console.WriteLine($"Your coins: {_coins}");
            Console.WriteLine("Do you want to pay 5 Coins to buy a pack consisting of 5 cards? [y/n]"); //add coins check
            char input = 'x';
            
            while(input != 'y' || input != 'n')
            {
                input = Convert.ToChar(Console.ReadLine());

                if (input == 'n')
                {
                    return;
                } else if(input == 'y')
                {
                    break; 
                } else
                {
                    Console.WriteLine("Invalid input");
                }
            }
            if(_coins > 4)
            {
                _coins = _coins - 5;
                db.setCoins(_userid, _coins);
                Console.WriteLine("Congratulations! You have acquired new cards!: ");

                for (int i = 0; i < 5; i++)
                {
                    cardid = db.getRandomCardID();
                    db.addCardToStack(_userid, cardid);
                    db.getCardByID(cardid).PrintCard();
                }

                Console.WriteLine($"New balance: {_coins} coins");
            } else
            {
                Console.WriteLine("You don't have enought coins to buy a pack!");
            }

        }

    }
}
