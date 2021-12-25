using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG
{
    class Merge
    {

        public void MergeCards(User user1)
        {
            Database db = new Database();
            List<Card> mergelist = new List<Card>();
            int input = 0; 
            List<Card> tempstack = db.getUnselectedStack(user1._userid);
            if(tempstack.Count < 2) {
                Console.WriteLine("You don't own enough cards to execute a merge!");
            }

            while (mergelist.Count < 2)
            {
                Console.Clear();
                for (int i = 0; i < tempstack.Count(); i++)
                {
                    Console.Write($"{i + 1}: ");
                    tempstack[i].PrintCard();
                }
                Console.WriteLine();
                Console.WriteLine($"Select 2 Cards of the same race, that you want to merge together! [{mergelist.Count()}/2]");

                input = InputHandler.getInstance().InputHandlerForInt(1, tempstack.Count());

                mergelist.Add(tempstack[input - 1]);
                tempstack.RemoveAt(input - 1);
            }

            if(mergelist[0]._racetype != mergelist[1]._racetype)
            {
                Console.WriteLine("You can't merge cards with different races!");
            }

            Console.Clear(); 

            for(int i = 0; i < mergelist.Count(); i++)
            {
                Console.Write($"{i + 1}: ");
                mergelist[i].PrintCard();
            }
            




            
            
            
        }


    }
}
