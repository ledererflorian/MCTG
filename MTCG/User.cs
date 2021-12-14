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
        public int _coins {get; set; }
        public int _elo { get; set; }

        public Stack _userstack;
        public Deck _userdeck; 
        public User(string name)
        {
            _name = name;
            _coins = 20;
            _elo = 100; 
        }

        public User(User prevUser)
        {
            /*
            Console.WriteLine("EINS");
            _name = prevUser._name;
            _userstack = prevUser._userstack;
            _userstack._stack = prevUser._userstack._stack;
            
            Console.WriteLine("ZWEI");
            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine("DREI");
                Console.WriteLine(prevUser._userdeck._deck[i]._name);
                Card card = new Card(prevUser._userdeck._deck[i]._name, prevUser._userdeck._deck[i]._damage, prevUser._userdeck._deck[i]._cardtype, prevUser._userdeck._deck[i]._elementtype, prevUser._userdeck._deck[i]._racetype);
                Console.WriteLine("PRINTED CARD:");
                card.PrintCard();
                _userdeck._deck.AddCard(card);
                    //_userdeck.AddCard(new Card(prevUser._userdeck._deck[i]._name, prevUser._userdeck._deck[i]._damage, prevUser._userdeck._deck[i]._cardtype, prevUser._userdeck._deck[i]._elementtype, prevUser._userdeck._deck[i]._racetype));
                Console.WriteLine("VIER");
            }
            //new Card("Dragon", 10, CardTypesEnum.CardTypes.monster, ElementTypesEnum.ElementTypes.normal, RaceTypesEnum.RaceTypes.dragon);


            
            Console.WriteLine("Neuer Constructor verwendet!!!!!!!!!!!!!!!!!!!!!!!!!!");
            //_userstack.PrintStack();
            */
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
