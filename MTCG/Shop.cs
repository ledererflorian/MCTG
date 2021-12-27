using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG
{
    class Shop
    {

        public void ShopHub(User user1)
        {
            Console.Clear();
            Console.WriteLine("Welcome to the Shop!");

            while (true)
            {

                Console.WriteLine();
                Console.WriteLine("1: Buy Cards\n2: View Transaction History\n3: Leave Shop\n");
                int select = InputHandler.getInstance().InputHandlerForInt(1, 3);

                switch (select)
                {
                    case 1:
                        BuyCards(user1);
                        break;
                    case 2:
                        ListTransactions(user1);
                        break;
                    case 3:
                        return;
                }
            }
        }


        public void BuyCards(User user1)
        {
            Database db = new Database();
            int cardid = 0;

            Console.Clear();
            Console.WriteLine($"Your coins: {user1._coins}");
            Console.WriteLine("Do you want to pay 5 Coins to buy a pack consisting of 5 cards? [y/n]"); //add coins check
            string input = "";

            while (input != "y" || input != "n")
            {
                input = InputHandler.getInstance().InputHandlerForString(1, 1);

                if (input == "n")
                {
                    return;
                }
                else if (input == "y")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input");
                }
            }
            if (user1._coins > 4)
            {
                user1._coins = user1._coins - 5;
                db.setCoins(user1._userid, user1._coins);
                
                for (int i = 0; i < 5; i++)
                {
                    cardid = db.getRandomCardID();
                    db.addCardToStack(user1._userid, cardid);
                    db.getCardByID(cardid).PrintCard();
                }

                Console.WriteLine("Congratulations! You have acquired new cards!: ");
                db.updateTransactionHistory(user1._userid, "[" + DateTime.Now + "]: Spent 5 coins for a card pack\n");
                Console.WriteLine($"New balance: {user1._coins} coins");
            }
            else
            {
                Console.WriteLine("You don't have enought coins to buy a pack!");
            }
        }

        public void ListTransactions(User user1)
        {
            Database db = new Database(); 

            Console.Clear();
            string thistory = db.getTransactionHistory(user1._userid);
            Console.WriteLine(thistory);
            Console.WriteLine("Press any key to continue");
            Console.ReadKey(); 
        }

    }
}
