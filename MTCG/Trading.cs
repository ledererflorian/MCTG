using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG
{
    class Trading
    {



        public void Trade(User user1)
        {
            Database db = new Database();
            bool abort = false;
            int input = 0;
            int input2 = 0; 
            List<Tradeoffer> tradeoffers = db.getTradingOffers();

            if(tradeoffers.Count() == 0)
            {
                Console.WriteLine("At the moment nobody wants to trade any cards");
                return; 
            }


            Console.Clear();
            for (int i = 0; i < tradeoffers.Count(); i++)
            {
                Console.WriteLine("---------------------------------------------------------------");
                Console.Write($"{i + 1}: ");
                Console.WriteLine("Offer by " + db.getUsernameByUserID(tradeoffers[i]._ownerid));
                Card card = db.getCardByID(tradeoffers[i]._cardid);
                card.PrintCard();
                Console.WriteLine("Requirements for Trade: Cardtype: " + tradeoffers[i]._typerequirement + " | min. Damage: " + tradeoffers[i]._damagerequirement);
            }

            

            Console.WriteLine();
            Console.WriteLine("Select the card you want to acquire by entering its associated ID.");


            input = Convert.ToInt32(Console.ReadLine());

            if (input > tradeoffers.Count())
            {
                Console.WriteLine("Invalid input");
            }
            else
            {
                Console.WriteLine($"Select the card you want to give away by entering its associated ID:");
                List<Card> filteredstack = db.getCardsByRequirement(user1._userid, (int) tradeoffers[input - 1]._typerequirement, tradeoffers[input -1]._damagerequirement);

                for (int i = 0; i < filteredstack.Count(); i++)
                {
                    Console.Write($"{i + 1}: ");
                    filteredstack[i].PrintCard();
                }

                input2 = Convert.ToInt32(Console.ReadLine());

                if(input > filteredstack.Count())
                {
                    Console.WriteLine("Invalid input");

                } else
                {
                    db.deleteCardFromStack(tradeoffers[input - 1]._ownerid, tradeoffers[input - 1]._cardid);
                    db.deleteCardFromStack(user1._userid, filteredstack[input - 1]._cardid);

                    db.addCardToStack(user1._userid, tradeoffers[input - 1]._cardid);
                    db.addCardToStack(tradeoffers[input - 1]._ownerid, filteredstack[input - 1]._cardid);

                    db.deleteTradingOffer(tradeoffers[input - 1]._tradingid);

                    Console.WriteLine("Successfully traded!");
                }

            }

            abort = true;

        }


    }
}

/*
                    _userdeck.AddCard(tempstack[input - 1]);
                    db.selectCard(_userid, tempstack[input - 1]._cardid);
                    tempstack.RemoveAt(input - 1);
*/