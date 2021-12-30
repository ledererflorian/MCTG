using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG
{
    static class Scoreboard
    {
        public static void PrintScoreboard()
        {
            Database db = Database.getInstance();
            List<User> userlist = db.getAllUsersOrderedByElo();

            Console.Clear(); 
            for(int i = 0; i < userlist.Count(); i++)
            {
                Console.Write($"Rank {i + 1}: ");
                userlist[i].PrintUser(); 
            }
            Console.WriteLine(); 
            Console.WriteLine("Press any key to return to the menu");
            Console.ReadKey();
            Console.Clear(); 
        }        



    }
}
