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

            for(int i = 0; i < userlist.Count(); i++)
            {
                if((userlist[i]._wins + userlist[i]._losses) < 5)
                {
                    userlist.RemoveAt(i);
                    i--;
                }
            }

            Console.Clear();
            Console.WriteLine("A user gets displayed on the scoreboard, as soon as he played at least 5 games!\n");
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
