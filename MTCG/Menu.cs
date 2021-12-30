using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG
{
    class Menu
    {

        public void Start()
        {
            Database db = Database.getInstance(); 


            User user2 = new User(0, "AI", 0, 0, 0, 0);
            Stack stack1 = new Stack();
            Stack stack2 = new Stack();
            Deck deck1 = new Deck();
            Deck deck2 = new Deck();
            bool loggedin = false;
            int select;
            int loggedinID = 0;
            user2._userstack = stack2;
            user2._userdeck = deck2;

            Console.WriteLine("Welcome to MTCG\n");


            while (!loggedin)
            {
                Console.WriteLine("1: Register\n2: Login\n3: Quit");
                select = InputHandler.getInstance().InputHandlerForInt(1, 3);
                //Console.Clear(); 
                switch (select)
                {
                    case 1:
                        RegisterUser(); 
                        break;
                    case 2:
                        if((loggedinID = LoginUser()) != 0)
                        {
                            loggedin = true;
                        }  else
                        {
                            loggedinID = 0; 
                        }
                        break;
                    case 3:
                        return;
                    default:
                        Console.WriteLine("Invalid input");
                        continue;
                }
            }

            User user1 = db.getUserByUserID(loggedinID);
            user1._userstack = stack1;
            user1._userdeck = deck1;

            //Shop shop = new Shop();
            //Trading trading = new Trading();
            //CraftCard craftcard = new CraftCard();
            //Friends friends = new Friends();
            //GameLogic gamelogic = new GameLogic();
            //Scoreboard scoreboard = new Scoreboard();
            //ProfilePage profilepage = new ProfilePage();

            Console.Clear(); 
            while (true)
            {
                Console.WriteLine("1: Start a battle\n2: Create Deck\n3: Shop\n4: Trade Center\n5: Scoreboard\n6: Profile\n7: Craft cards\n8: Friends\n9: Quit");
                select = InputHandler.getInstance().InputHandlerForInt(1, 9);
                switch (select)
                {
                    case 1:
                        if(db.getSelectedCardCount(user1._userid) < 4)
                        {
                            Console.WriteLine("Create a deck before fighting!");
                        } else
                        {
                            user1._userdeck._deck = LoadCurrentDeck(user1._userid);
                            Console.WriteLine("Looking for opponent...");
                          
                            user2._userdeck._deck = LoadOpponentDeck(user1._userid, user2);

                            Console.WriteLine("You are playing against " + db.getUsernameByUserID(user2._userid) + ".\nPress any key to continue");
                            Console.ReadKey();

                            int battlewinner = GameLogic.BattleLogic(user1, user2);
                            Console.WriteLine("\nPlayer " + battlewinner + " won the battle!");

                            if (battlewinner == 1)
                            {
                                user1.UpdateWin();
                                user2.UpdateLoss();
                                if(user1._wins % 10 == 0)
                                {
                                    user1._coins = user1._coins + 1; 
                                    db.setCoins(user1._userid, db.getCoinsByUserID(user1._userid) + 1);
                                    db.updateTransactionHistory(user1._userid, "[" + DateTime.Now + "]: Received 1 coin for winning 10 battles!\n");
                                    Console.WriteLine("You received a coin for winning 10 games!");
                                }
                            } else if(battlewinner == 2)
                            {
                                user1.UpdateLoss();
                                user2.UpdateWin(); 

                                if(db.getWinsByUserID(user2._userid) % 10 == 0)
                                {
                                    db.setCoins(user2._userid, db.getCoinsByUserID(user2._userid) + 1);
                                    db.updateTransactionHistory(user2._userid, "[" + DateTime.Now + "]: Received 1 coin for winning 10 battles!\n");
                                }
                            } else
                            {
                                Console.WriteLine("The round limit of 100 is exceeded... Nobody won the match.");
                            }
                            Console.WriteLine("Press any key to continue");
                            Console.ReadKey();
                            Console.Clear();
                        }
                        break;
                    case 2:
                        if(db.getCardCount(user1._userid) < 4)
                        {
                            Console.WriteLine("You have to buy some cards before you can create a deck!");
                        } else
                        {
                        user1.CreateDeck();
                        Console.WriteLine("Deck erstellt");
                        user1._userdeck.PrintDeck();
                        Console.WriteLine("----"); //remove
                        user1._userstack.PrintStack(); //remove
                        }

                        break;
                    case 3:
                        //user1.Shop();
                        //shop.ShopHub(user1);
                        Shop.ShopHub(user1);
                        break;
                    case 4:
                        Trading.TradingHub(user1);
                        break;
                    case 5:
                        Scoreboard.PrintScoreboard();
                        break; 
                    case 6:
                        ProfilePage.ProfileHub(user1);
                        break;
                    case 7:
                        CraftCard.CraftingHub(user1); 
                        break;
                    case 8:
                        Friends.FriendsHub(user1, user2); 
                        break; 
                    case 9:
                        return; 
                    default:
                        Console.WriteLine("Invalid input");
                        continue;
                }
            }

        }

        public void RegisterUser()
        {
            Database db = Database.getInstance();
            string username;
            string password;

            Console.WriteLine("Enter username: ");
            username = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Enter password: ");
            password = Convert.ToString(Console.ReadLine());

            if(db.userExists(username) == true)
            {
                Console.WriteLine("Error: Username already exists!");
            } else
            {
                db.addUser(username, password, 20, 100);
                Console.WriteLine("Account successfuly created!");
            }
        }

        public int LoginUser()
        {
            Database db = Database.getInstance();
            int success = 0; 
            string username;
            var password = string.Empty;

            Console.WriteLine("Enter username: ");
            username = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Enter password: ");

            ConsoleKey key;
            do
            {
                var keyInfo = Console.ReadKey(intercept: true);
                key = keyInfo.Key;

                if (key == ConsoleKey.Backspace && password.Length > 0)
                {
                    Console.Write("\b \b");
                    password = password[0..^1];
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    Console.Write("*");
                    password += keyInfo.KeyChar;
                }
            } while (key != ConsoleKey.Enter);

            if ((success = db.loginUser(username, password)) == 0)
            {
                Console.WriteLine();
                Console.WriteLine("Login failed!");
                return 0;

            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Login successful!\nPress any key to continue");
                Console.ReadKey(); 
                return success;
            }
        }

        public List<Card> LoadCurrentDeck(int uid)
        {
            Database db = Database.getInstance();
            List<Card> deck = db.getSelectedStack(uid);
            return deck; 
        }

        public List<Card> LoadOpponentDeck(int uid, User user2)
        {
            Database db = Database.getInstance();
            List<Card> opponentdeck = new List<Card>();
            int opponentid = 0; 

            while (opponentdeck.Count() == 0)
            {
                opponentid = db.getOtherRandomUserID(uid);
                opponentdeck = db.getSelectedStack(opponentid);
            }
            user2._userid = opponentid; 
            return opponentdeck; 

        }


        public void CreateCardsInDB()
        {
            Database db = Database.getInstance(); 
            for(int i = 0; i < 101; i++)
            {
                string name = "";
                int damage = 0;
                int cardtype = 0;
                int elementtype = 0;
                int racetype = 0;

                var rand = new Random();
                int random = rand.Next(0, 8);

                switch (random)
                {
                    case 0:
                        name = "Goblin";
                        racetype = 0;
                        cardtype = 0;
                        break;
                    case 1:
                        name = "Dragon";
                        racetype = 1;
                        cardtype = 0;
                        break;
                    case 2:
                        name = "Wizzard";
                        racetype = 2;
                        cardtype = 0;
                        break;
                    case 3:
                        name = "Ork";
                        racetype = 3;
                        cardtype = 0;
                        break;
                    case 4:
                        name = "Knight";
                        racetype = 4;
                        cardtype = 0;
                        break;
                    case 5:
                        name = "Kraken";
                        racetype = 5;
                        cardtype = 0;
                        break;
                    case 6:
                        name = "Elf";
                        racetype = 6;
                        cardtype = 0;
                        break;
                    case 7:
                        name = "Spell";
                        racetype = 7;
                        cardtype = 1;
                        break;

                }

                random = rand.Next(10, 101);
                damage = random;

                random = rand.Next(0, 3);
                elementtype = random;

                switch (random)
                {
                    case 0:
                        break;
                    case 1:
                        name = "Fire" + name;
                        break;
                    case 2:
                        name = "Water" + name;
                        break;
                }

                db.addCard(name, damage, cardtype, elementtype, racetype);
            }
            
        }



    }
}
