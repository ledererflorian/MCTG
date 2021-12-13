using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG
{
    class GameLogic
    {

        public int BattleLogic(User originaluser1, User originaluser2)
        {
            var rand = new Random();
            int winner = 0;
            int rounds = 1;

            User user1 = new User(originaluser1);
            User user2 = new User(originaluser2);

            Card cardp1;
            Card cardp2;

            while (true)
            {

                int rnumber1 = rand.Next(0, user1._userdeck._deck.Count);
                int rnumber2 = rand.Next(0, user2._userdeck._deck.Count);


                cardp1 = user1._userdeck._deck[rnumber1];
                cardp2 = user2._userdeck._deck[rnumber2];

                Console.WriteLine($"Round {rounds}");
                Console.WriteLine($"Left Cards P1: {user1._userdeck._deck.Count}");
                Console.WriteLine($"Left Cards P2: {user2._userdeck._deck.Count}");
                Console.WriteLine(); 
                
                Console.Write("Chosen Card P1: ");
                cardp1.PrintCard();
                Console.Write("Chosen Card P2: ");
                cardp2.PrintCard(); 
                


                if (cardp1._weakness == cardp2._racetype && (cardp1._elementweakness == cardp2._elementtype || cardp1._elementweakness == ElementTypesEnum.ElementTypes.none))
                {
                    Console.WriteLine("Player 2 won due to weakness");
                    winner = 2;

                }
                else if (cardp2._weakness == cardp1._racetype && (cardp2._elementweakness == cardp1._elementtype || cardp2._elementweakness == ElementTypesEnum.ElementTypes.none))
                {
                    Console.WriteLine("Player 1 won due to weakness");
                    winner = 1;
                }
                else
                {
                    if (cardp2._damage * cardp2.IsEffective(cardp1) > cardp1._damage * cardp1.IsEffective(cardp2))
                    {
                        Console.WriteLine("Player 2 won the battle");
                        winner = 2;

                    }
                    else if (cardp2._damage * cardp2.IsEffective(cardp1) < cardp1._damage * cardp1.IsEffective(cardp2))
                    {
                        Console.WriteLine("Player 1 won the battle");
                        winner = 1;
                    }
                    else
                    {
                        Console.WriteLine("DRAW!");
                        winner = 0;

                    }
                }

                if (winner == 1)
                {
                    user1._userdeck.AddCard(user2._userdeck._deck[rnumber2]);
                    user2._userdeck._deck.RemoveAt(rnumber2);
                }
                else if (winner == 2)
                {
                    user2._userdeck.AddCard(user1._userdeck._deck[rnumber1]);
                    user1._userdeck._deck.RemoveAt(rnumber1);
                }
                else
                {

                }

                /*
                Console.WriteLine("Deck von Spieler 1:");
                user1._userdeck.PrintDeck();
                Console.WriteLine("Deck von Spieler 2:");
                user2._userdeck.PrintDeck();
                */

                if (user1._userdeck._deck.Count == 0) { return 2; }
                if (user2._userdeck._deck.Count == 0) { return 1; }
                if (rounds == 100) { return 0; }

                rounds++;
                Console.WriteLine(); 
            }
        }

    }
}
