﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG
{
    static class ProfilePage
    {

        public static void ProfileHub(User user1)
        {
            int input = 0; 
            Console.Clear();       
            while(true)
            {
                PrintProfile(user1);
                Console.WriteLine();
                Console.WriteLine("1: Edit Profile\n2: View other Profiles\n3: Return to main menu");
                input = InputHandler.getInstance().InputHandlerForInt(1, 3);
                switch (input)
                {
                    case 1:
                        EditProfile(user1);
                        break;
                    case 2:
                        ViewOtherUsers(user1);
                        break;
                    case 3:
                        Console.Clear();
                        return;
                }
            }
        }
        public static void PrintProfile(User user1)
        {
            Database db = Database.getInstance(); 
            Console.WriteLine("Username: " + user1._name);
            Console.WriteLine("Coins: " + user1._coins);
            Console.WriteLine("Elo: " + user1._elo);
            Console.WriteLine("Status: " + db.getProfiletextByUserID(user1._userid));
            Console.WriteLine("Wins: " + user1._wins);
            Console.WriteLine("Losses: " + user1._losses);
            if(user1._wins == 0 && user1._losses == 0)
            {
                Console.WriteLine("Winrate: No games played yet");
            } else
            {
                Console.WriteLine("Winrate:" + Math.Round(((float) user1._wins / (user1._wins + user1._losses)) *100, 2) + "%");
            }
        }

        public static void EditProfile(User user1)
        {
            Database db = Database.getInstance(); 
            string input = "";
            Console.WriteLine("Enter a new Status Message (max. 30 characters)");
            input = InputHandler.getInstance().InputHandlerForString(0, 30);
            db.editProfileText(user1._userid, input);
            Console.Clear();
            Console.WriteLine("Profile text successfully changed!");
        }

        public static void ViewOtherUsers(User user1)
        {
            Database db = Database.getInstance();
            List<User> userlist = db.getAllUsersOrderedByElo();
            int input = 0; 
            Console.Clear();


            for (int i = 0; i < userlist.Count(); i++)
            {
                Console.Write($"{i + 1}: ");
                Console.WriteLine(userlist[i]._name);
            }

            Console.WriteLine("Select a userprofile you want to look at by entering its associated ID.");
            input = InputHandler.getInstance().InputHandlerForInt(1, userlist.Count());
            Console.Clear();
            PrintProfile(userlist[input - 1]);
            Console.WriteLine();
            Console.WriteLine("Press any key to return to the profile menu");
            Console.ReadKey();
            Console.Clear();

        }

    }
}
