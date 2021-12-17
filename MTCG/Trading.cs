using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG
{
    class Trading
    {

        public void TradingHub(User user1)
        {

            Console.WriteLine("Welcome to the Trade Center!");
            Console.WriteLine();
            Console.WriteLine("1: Trade Cards\n2:Offer Card\n3:Withdraw Trade Offer\n4:Leave Trade Center\n");
            int select = InputHandler.getInstance().InputHandlerForInt(1, 4);

            switch (select)
            {

                case 1:
                    Console.WriteLine("one");
                    break;
                case 2:
                    Console.WriteLine("two");
                    break;
                case 3:
                    Console.WriteLine("three");
                    break;
                case 4:
                    Console.WriteLine("four");
                    break;
            }

        }

        


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
                
                List<Card> filteredstack = db.getCardsByRequirement(user1._userid, (int) tradeoffers[input - 1]._typerequirement, tradeoffers[input -1]._damagerequirement);
                
                if(filteredstack.Count() == 0)
                {
                    Console.WriteLine("You don't have any cards, that meet the requirement!");
                    return; 
                }
                
                Console.WriteLine($"Select the card you want to give away by entering its associated ID:");


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
                    //db.deleteCardFromStack(tradeoffers[input - 1]._ownerid, tradeoffers[input - 1]._cardid);
                    db.deleteCardFromStack(user1._userid, filteredstack[input - 1]._cardid); //user looses old card

                    db.addCardToStack(user1._userid, tradeoffers[input - 1]._cardid); //user gets new card
                    db.addCardToStack(tradeoffers[input - 1]._ownerid, filteredstack[input - 1]._cardid);//provider gets new card

                    db.deleteTradingOffer(tradeoffers[input - 1]._tradingid); //provider looses old card

                    Console.WriteLine("Successfully traded!");
                }

            }

            abort = true;

        }

        public void OfferCard(User user1)
        {
            Database db = new Database();
            int input = 0;
            int typerequirement = 0;
            int damagerequirement = 0; 

            Console.WriteLine("Select a card you want to offer by entering its associated ID: ");


            List<Card> tempstack = db.getUnselectedStack(user1._userid);

            if(tempstack.Count() == 0)
            {
                Console.WriteLine("You don't own any cards to create a trading offer!"); //move to menu later
                return; 
            }


            Console.Clear();
            for (int i = 0; i < tempstack.Count(); i++)
            {
                Console.Write($"{i + 1}: ");
                tempstack[i].PrintCard();
            }
            Console.WriteLine();
            Console.WriteLine("Select the card you want to offer for trading by entering its associated ID");

            input = Convert.ToInt32(Console.ReadLine());

            if (input > tempstack.Count())
            {
                Console.WriteLine("Invalid input");
            }
            else
            {
                Console.WriteLine("Enter whether your desired card should be a monster(0) or a spell(1)");
                typerequirement = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Enter the min. damage your desired card must have. [10-100]");
                damagerequirement = Convert.ToInt32(Console.ReadLine());

                db.deleteCardFromStack(user1._userid, tempstack[input - 1]._cardid);
                db.addTradingOffer(user1._userid, tempstack[input - 1]._cardid, typerequirement, damagerequirement);
                
            }
        }


        public void WithdrawOffer(User user1)
        {
            Database db = new Database();
            int input = 0;
            

            List<Tradeoffer> tradeoffers = db.getOwnTradingOffers(user1._userid);

            if (tradeoffers.Count() == 0)
            {
                Console.WriteLine("You haven't submitted any cards yet!");
                return;
            }


            Console.Clear();
            Console.WriteLine("Select a card you want to withdraw by entering its associated ID! ");
            Console.WriteLine(); 
            for (int i = 0; i < tradeoffers.Count(); i++)
            {
                Console.WriteLine("---------------------------------------------------------------");
                Console.Write($"{i + 1}: ");
                Console.WriteLine("Offer by " + db.getUsernameByUserID(tradeoffers[i]._ownerid));
                Card card = db.getCardByID(tradeoffers[i]._cardid);
                card.PrintCard();
                Console.WriteLine("Requirements for Trade: Cardtype: " + tradeoffers[i]._typerequirement + " | min. Damage: " + tradeoffers[i]._damagerequirement);
            }

            input = Convert.ToInt32(Console.ReadLine());

            if (input > tradeoffers.Count())
            {
                Console.WriteLine("Invalid input");
            }
            else
            {
                db.deleteTradingOffer(tradeoffers[input - 1]._tradingid);
                db.addCardToStack(user1._userid, tradeoffers[input - 1]._cardid);
                Console.WriteLine("Your card has been removed from the market and returned to your stack!"); 
            }

        }


    }
}
