using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG
{
    static class Friends
    {
        public static void FriendsHub(User user1, User user2)
        {
            Database db = Database.getInstance(); 

            Console.Clear();
            Console.WriteLine("Welcome to the Friends Menu!");
            Console.WriteLine();

            while (true)
            {
                Console.WriteLine($"1: List Friends\n2: Send Friend Request\n3: Incoming Friend Requests({db.getFriendRequestsCount(user1._userid)})\n4: Return to main menu\n");
                int select = InputHandler.getInstance().InputHandlerForInt(1, 4);

                switch (select)
                {
                    case 1:
                        ListFriends(user1, user2);
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

        public static void ListFriends(User user1, User user2)
        {
            Database db = Database.getInstance();
            List<Friendrequest> friendlist = db.getFriendsbyUserID(user1._userid);
            
            if(friendlist.Count() == 0)
            {
                Console.WriteLine("You don't have any friends!\nPress any key to continue");
                Console.ReadKey();
                Console.Clear(); 
                return; 

            }

            Console.Clear();
            for (int i = 0; i < friendlist.Count(); i++)
            {
                Console.Write($"{i + 1}: ");
                Console.WriteLine(db.getUsernameByUserID(friendlist[i]._otheruserid));
            }

            Console.WriteLine("\nEnter a friend you want to interact with by entering the associated ID!");

            int input = InputHandler.getInstance().InputHandlerForInt(1, friendlist.Count());
            Console.Clear(); 
            int selectedfriend = friendlist[input - 1]._otheruserid;
            string friendname = db.getUsernameByUserID(selectedfriend);
            Console.WriteLine($"1: Battle {friendname}\n2: View stats of {friendname}\n3: Delete {friendname} from friendlist\n4: Return to friends menu");
            user2 = db.getUserByUserID(selectedfriend);

            int input2 = InputHandler.getInstance().InputHandlerForInt(1, 4);


            switch (input2)
            {
                case 1:
                    FriendBattle(user1, user2);
                    break;
                case 2:
                    Console.Clear(); 
                    user2.PrintUser();
                    Console.WriteLine("\nPress any key to return to the menu!");
                    Console.ReadKey();
                    Console.Clear(); 
                    break;
                case 3:
                    db.deleteFriend(user1._userid, selectedfriend);
                    Console.WriteLine($"You have deleted {friendname} from your friendlist!\nPress any key to continue.");
                    Console.ReadKey();
                    Console.Clear(); 
                    break;
                case 4:
                    Console.Clear(); 
                    return; 
            }
        }

        public static void FriendBattle(User user1, User user2)
        {
            Database db = Database.getInstance();
            if (db.getSelectedCardCount(user1._userid) < 4)
            {
                Console.WriteLine("Create a deck before fighting!");
                return;
            }
            else if (db.getSelectedCardCount(user2._userid) < 4)
            {
                Console.WriteLine("Your friend doesn't have created a deck yet!");
            }
            else
            {
                
                Stack stack2 = new Stack(); 
                Deck deck2 = new Deck();

                user2._userstack = stack2; 
                user2._userdeck = deck2;

                user1._userdeck._deck = LoadCurrentDeck(user1._userid);
                user2._userdeck._deck = LoadCurrentDeck(user2._userid);

                Console.Clear(); 
                Console.WriteLine("This battle won't affect your elo or winrate!\nPress any key to begin the battle!");
                Console.ReadKey();
                Console.Clear(); 

                int battlewinner = GameLogic.BattleLogic(user1, user2);
                Console.WriteLine("\nPlayer " + battlewinner + " won the match!\nPress any key to return to the friends menu!");
                Console.ReadKey();
                Console.Clear(); 
                
            }
        }

        public static List<Card> LoadCurrentDeck(int uid)
        {
            Database db = Database.getInstance();
            List<Card> deck = db.getSelectedStack(uid);
            return deck;
        }


        public static void AddFriend(User user1)
        {
            Database db = Database.getInstance();
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
                Console.ReadKey();
                Console.Clear(); 
                return; 
            }

            if (db.friendRequestExists(user1._userid, otherid) > 0)
            {
                Console.WriteLine($"\nA friendrequest to {db.getUsernameByUserID(otherid)} has already been sent!\nPress any key to return to the menu!");
                Console.ReadKey();
                Console.Clear(); 
                return; 
            }
            db.sendFriendRequest(user1._userid, otherid);

            Console.WriteLine();
            Console.WriteLine("Friendrequest successfully sent!\nPress any key to return to the menu");
            Console.ReadKey();
            Console.Clear();
        }

        public static void ManageFriendRequests(User user1)
        {
            Database db = Database.getInstance();
            List<Friendrequest> friendrequests = db.getFriendRequests(user1._userid);

            if (friendrequests.Count() == 0)
            {
                Console.WriteLine("You don't have any friendrequests!\nPress any key to return to the menu!");
                Console.ReadKey();
                Console.Clear(); 
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

            if (input2 == 1)
            {
                db.acceptFriendRequest(friendrequests[input - 1]._friendid);
                db.acceptFriendRequestMirror(user1._userid, friendrequests[input - 1]._thisuserid);
            }
            else { 
                db.declineFriendRequest(friendrequests[input - 1]._friendid);
            }

        }


    }
}
