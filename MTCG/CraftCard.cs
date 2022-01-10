using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG
{
    static class CraftCard
    {
        public static void CraftingHub(User user1)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1: Craft Elementfragment\n2: Craft new Card\n3: Return to main menu\n");
                int select = InputHandler.getInstance().InputHandlerForInt(1, 3);

                switch (select)
                {
                    case 1:
                        Console.Clear();
                        CraftElementFragment(user1);
                        break;
                    case 2:
                        Console.Clear();
                        CraftNewCard(user1);
                        break;
                    case 3:
                        Console.Clear(); 
                        return;
                }
            }
        }

        public static void CraftElementFragment(User user1)
        {
            Database db = Database.getInstance();
            List<Card> filteredstack = db.getUnselectedStack(user1._userid); 

            if (filteredstack.Count() == 0)
            {
                Console.WriteLine("You don't have any cards that can be transformed into fragments!\nPress any key to continue!");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"Select the card you want to give away by entering its associated ID:");

            for (int i = 0; i < filteredstack.Count(); i++)
            {
                Console.Write($"{i + 1}: ");
                filteredstack[i].PrintCard();
            }
            int input = InputHandler.getInstance().InputHandlerForInt(1, filteredstack.Count());

            ElementTypesEnum.ElementTypes elementofcard = filteredstack[input - 1]._elementtype;

            db.deleteCardFromStack(user1._userid, filteredstack[input - 1]._cardid);
            
            switch(elementofcard)
            {
                case ElementTypesEnum.ElementTypes.normal:
                    db.addNormalFragments(user1._userid);
                    Console.WriteLine("You acquired a normal orb!");
                    break;
                case ElementTypesEnum.ElementTypes.fire:
                    db.addFireFragments(user1._userid);
                    Console.WriteLine("You acquired a fire orb!");
                    break;
                case ElementTypesEnum.ElementTypes.water:
                    db.addWaterFragments(user1._userid);
                    Console.WriteLine("You acquired a water orb!");
                    break;
            }

            Console.WriteLine("Press any key to continue!");
            Console.ReadKey(); 

        }

        public static void CraftNewCard(User user1)
        {
            const int REQUIRED_FRAGMENTS = 2;

            Database db = Database.getInstance(); 

            Console.WriteLine($"Spend {REQUIRED_FRAGMENTS} fragments of the same element type to acquire a card of the same element with a min. Damage of 40!\n");

            List<int> fragmentcount = db.getElementFragmentsByUserId(user1._userid);

            Console.WriteLine($"Select the element of the card you want to craft away by entering its associated ID:");

            Console.WriteLine($"1: Normal fragments:{fragmentcount[0]}/{REQUIRED_FRAGMENTS} ");
            Console.WriteLine($"2: Fire fragments:{fragmentcount[1]}/{REQUIRED_FRAGMENTS} ");
            Console.WriteLine($"3: Water fragments:{fragmentcount[2]}/{REQUIRED_FRAGMENTS} ");
            int input = InputHandler.getInstance().InputHandlerForInt(1, fragmentcount.Count());

            if(fragmentcount[input - 1] < 2)
            {
                Console.WriteLine("You don't have enough fragments of this element to craft a card!");
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
                return; 
            }

            switch(input)
            {
                case 1:
                    db.removeNormalFragments(user1._userid); 
                    break;
                case 2:
                    db.removeFireFragments(user1._userid);
                    break;
                case 3:
                    db.removeWaterFragments(user1._userid);
                    break;
            }

            int cardid = db.getRandomCardIDWithSpecificElementAndDmgGreaterX(input - 1, 39); 
            db.addCardToStack(user1._userid, cardid); 
            Card craftedcard = db.getCardByID(cardid);
            Console.WriteLine("Congratulations! You crafted this card:");
            craftedcard.PrintCard();
            Console.WriteLine("Press any key to continue");
            Console.ReadKey(); 

        }


    }
}
