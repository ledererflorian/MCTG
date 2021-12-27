using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG
{
    class Friends
    {
        public void FriendsHub(User user1)
        {
            Console.Clear();
            Console.WriteLine("Welcome to the Friends Menu!");
            Console.WriteLine();

            while (true)
            {
                Console.WriteLine("1: List Friends\n2: Send Friend Request\n3: Incoming Friend Requests\n4: Return to main menu\n");
                int select = InputHandler.getInstance().InputHandlerForInt(1, 4);

                switch (select)
                {
                    case 1:
                        ListFriends(user1);
                        break;
                    case 2:
                        AddFriend(user1);
                        break;
                    case 3:
                        ManageFriendRequests(user1); 
                        break; 
                    case 4:
                        Console.Clear();
                        return;
                }
            }
        }

        public void ListFriends(User user1)
        {
            Database db = new Database();
            List<Friendrequest> friendlist = db.getFriendsbyUserID(user1._userid);
            
            if(friendlist.Count() == 0)
            {
                Console.WriteLine("You don't have any friends!\nPress any key to continue");
                Console.ReadKey();
                return; 

            }

            Console.Clear();
            for (int i = 0; i < friendlist.Count(); i++)
            {
                Console.Write($"{i + 1}: ");
                Console.WriteLine(db.getUsernameByUserID(friendlist[i]._otheruserid));
            }

            Console.WriteLine("Enter a friend you want to interact with by entering the associated ID!");
        }
        

        public void AddFriend(User user1)
        {
            Database db = new Database();
            List<User> userlist = db.getAllUsersOrderedByElo();

            Console.Clear();
            for (int i = 0; i < userlist.Count(); i++)
            {
                Console.Write($"{i + 1}: ");
                Console.WriteLine(userlist[i]._name); 
            }

            Console.WriteLine("Enter a username to send a friendrequest to that user!");
            string input = Console.ReadLine();
            int otherid = db.getUserIDByUsername(input);

            if(otherid == -1)
            {
                Console.WriteLine("This username does not exist!");
                Console.WriteLine("Press any key to return to the menu");
                Console.Clear(); 
                return; 
            }

            db.sendFriendRequest(user1._userid, otherid);

            Console.WriteLine();
            Console.WriteLine("Press any key to return to the menu");
            Console.ReadKey();
            Console.Clear();
        }

        public void ManageFriendRequests(User user1)
        {
            Database db = new Database();
            List<Friendrequest> friendrequests = db.getFriendRequests(user1._userid);

            if (friendrequests.Count() == 0)
            {
                Console.WriteLine("You don't have any friendrequests!"); 
                return;
            }

            Console.Clear();
            for (int i = 0; i < friendrequests.Count(); i++)
            {
                Console.Write($"{i + 1}: ");
                friendrequests[i].PrintFriendrequest();
            }
            Console.WriteLine();
            Console.WriteLine("Select the friend request you want to manage by entering its associated ID!");

            int input = InputHandler.getInstance().InputHandlerForInt(1, friendrequests.Count());

            Console.WriteLine($"Do you want to accept(1) or decline(2) the friendrequest by {db.getUsernameByUserID(friendrequests[input - 1]._thisuserid)}");

            int input2 = InputHandler.getInstance().InputHandlerForInt(1, 2);

            if(input2 == 1)
            {
                db.acceptFriendRequest(friendrequests[input - 1]._friendid);
                db.acceptFriendRequestMirror(user1._userid, friendrequests[input - 1]._thisuserid);
            }

        }


    }
}
