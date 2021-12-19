using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG
{
    public class GameLogic
    {

        public int BattleLogic(User user1, User user2)
        {
            Database db = new Database();  
            var rand = new Random();
            int winner = 0;
            int rounds = 1;

            List<Card> tempdeck1 = new List<Card>(user1._userdeck._deck);

            List<Card> tempdeck2 = new List<Card>(user2._userdeck._deck); 

            
            Card cardp1;
            Card cardp2;

            while (true)
            {
                
                int rnumber1 = rand.Next(0, tempdeck1.Count);
                int rnumber2 = rand.Next(0, tempdeck2.Count);


                cardp1 = tempdeck1[rnumber1];
                cardp2 = tempdeck2[rnumber2];

                Console.WriteLine($"Round {rounds}");
                Console.WriteLine($"Cards left P1: {tempdeck1.Count}");
                Console.WriteLine($"Cards left P2: {tempdeck2.Count}");
                Console.WriteLine(); 
                
                Console.Write("Chosen Card P1: ");
                cardp1.PrintCard();
                Console.Write("Chosen Card P2: ");
                cardp2.PrintCard();

                winner = calcWinner(cardp1, cardp2);


                if (winner == 1)
                {
                    tempdeck1.Add(tempdeck2[rnumber2]);
                    tempdeck2.RemoveAt(rnumber2);
                }
                else if (winner == 2)
                {
                    tempdeck2.Add(tempdeck1[rnumber1]);
                    tempdeck1.RemoveAt(rnumber1);
                }
                else 
                { 
                
                }

                if (tempdeck1.Count == 0) {
                    /*
                    db.decreaseElo(user1._userid);
                    db.incrementLosses(user1._userid);
                    user1._elo = user1._elo - 5;
                    user1._losses++;
                    */
                    return 2; 
                }
                if (tempdeck2.Count == 0) 
                {
                    /*
                    db.increaseElo(user1._userid);
                    db.incrementWins(user1._userid);
                    user1._elo = user1._elo + 3;
                    user1._wins++;
                    */
                    return 1;
                }
                if (rounds == 100) { return 0; }

                rounds++;
                Console.WriteLine(); 
            }
        }

        public int calcWinner(Card cardp1, Card cardp2)
        {
            if (cardp1._weakness == cardp2._racetype && (cardp1._elementweakness == cardp2._elementtype || cardp1._elementweakness == ElementTypesEnum.ElementTypes.none))
            {
                Console.WriteLine("Player 2 won due to weakness");
                return 2;

            }
            else if (cardp2._weakness == cardp1._racetype && (cardp2._elementweakness == cardp1._elementtype || cardp2._elementweakness == ElementTypesEnum.ElementTypes.none))
            {
                Console.WriteLine("Player 1 won due to weakness");
                return 1;
            }
            else
            {
                if (cardp2._damage * cardp2.IsEffective(cardp1) > cardp1._damage * cardp1.IsEffective(cardp2))
                {
                    Console.WriteLine("Player 2 won the battle");
                    return 2;
                }
                else if (cardp2._damage * cardp2.IsEffective(cardp1) < cardp1._damage * cardp1.IsEffective(cardp2))
                {
                    Console.WriteLine("Player 1 won the battle");
                    return 1;
                }
                else
                {
                    Console.WriteLine("DRAW!");
                    return 0;

                }
            }
        }



    }
}
