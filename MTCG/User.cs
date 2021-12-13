using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG
{
    class User
    {
        public string _name { get; set; }

        public Stack _userstack;
        public Deck _userdeck; 
        public User(string name)
        {
            _name = name;
        }

        public User(User prevUser)
        {
            
            _name = prevUser._name;
            _userstack = prevUser._userstack;
            _userdeck = prevUser._userdeck;

            _userstack._stack = prevUser._userstack._stack; 
            _userdeck._deck = prevUser._userdeck._deck;


            Console.WriteLine("Neuer Constructor verwendet!!!!!!!!!!!!!!!!!!!!!!!!!!");
            _userstack.PrintStack();
        }


        public void CreateDeck()
        {
            int input = 0; 
            _userdeck._deck.Clear();

            List<Card> tempstack = new List<Card>(_userstack._stack);
            
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
                    tempstack.RemoveAt(input - 1);
                }
            }
        }

    }
}
