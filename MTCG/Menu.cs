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
            Database db = new Database();

            Card card1 = new Card("Dragon", 10, CardTypesEnum.CardTypes.monster, ElementTypesEnum.ElementTypes.normal, RaceTypesEnum.RaceTypes.dragon);
            Card card2 = new Card("Firedragon", 30, CardTypesEnum.CardTypes.monster, ElementTypesEnum.ElementTypes.fire, RaceTypesEnum.RaceTypes.dragon);
            Card card3 = new Card("Waterdragon", 15, CardTypesEnum.CardTypes.monster, ElementTypesEnum.ElementTypes.water, RaceTypesEnum.RaceTypes.dragon);

            Card card4 = new Card("Goblin", 40, CardTypesEnum.CardTypes.monster, ElementTypesEnum.ElementTypes.normal, RaceTypesEnum.RaceTypes.goblin);
            Card card5 = new Card("Firegoblin", 20, CardTypesEnum.CardTypes.monster, ElementTypesEnum.ElementTypes.fire, RaceTypesEnum.RaceTypes.goblin);
            Card card6 = new Card("Watergoblin", 15, CardTypesEnum.CardTypes.monster, ElementTypesEnum.ElementTypes.water, RaceTypesEnum.RaceTypes.goblin);

            Card card7 = new Card("Knight", 20, CardTypesEnum.CardTypes.monster, ElementTypesEnum.ElementTypes.normal, RaceTypesEnum.RaceTypes.knight);
            Card card8 = new Card("Firenight", 30, CardTypesEnum.CardTypes.monster, ElementTypesEnum.ElementTypes.fire, RaceTypesEnum.RaceTypes.knight);
            Card card9 = new Card("Waterknight", 25, CardTypesEnum.CardTypes.monster, ElementTypesEnum.ElementTypes.water, RaceTypesEnum.RaceTypes.knight);


            Card card10 = new Card("Wizzard", 30, CardTypesEnum.CardTypes.monster, ElementTypesEnum.ElementTypes.normal, RaceTypesEnum.RaceTypes.wizzard);
            Card card11 = new Card("Fireizzard", 15, CardTypesEnum.CardTypes.monster, ElementTypesEnum.ElementTypes.fire, RaceTypesEnum.RaceTypes.wizzard);
            Card card12 = new Card("Waterwizzard", 25, CardTypesEnum.CardTypes.monster, ElementTypesEnum.ElementTypes.water, RaceTypesEnum.RaceTypes.wizzard);

            Card card13 = new Card("Normal Spell", 25, CardTypesEnum.CardTypes.spell, ElementTypesEnum.ElementTypes.normal, RaceTypesEnum.RaceTypes.spell);
            Card card14 = new Card("Firespell", 25, CardTypesEnum.CardTypes.spell, ElementTypesEnum.ElementTypes.fire, RaceTypesEnum.RaceTypes.spell);
            Card card15 = new Card("Waterspell", 15, CardTypesEnum.CardTypes.spell, ElementTypesEnum.ElementTypes.water, RaceTypesEnum.RaceTypes.spell);

            GameLogic gamelogic = new GameLogic();
            Scoreboard scoreboard = new Scoreboard();
            ProfilePage profilepage = new ProfilePage(); 

            User user2 = new User(0, "AI", 0, 0, 0, 0);
            Stack stack1 = new Stack();
            Stack stack2 = new Stack();
            Deck deck1 = new Deck();
            Deck deck2 = new Deck();



            user2._userstack = stack2;

            user2._userdeck = deck2;
            /*
            user2._userdeck.AddCard(card5);
            user2._userdeck.AddCard(card6);
            user2._userdeck.AddCard(card7);
            user2._userdeck.AddCard(card8);
            */
            bool loggedin = false; 
            int select = 0;
            Console.WriteLine("Welcome to MTCG\n");
            int loggedinID = 0;

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
            //user1._userid = loggedinID;
            //user1._coins = db.getCoinsByUserID(user1._userid);
            //user1._elo = db.getEloByUserID(user1._userid);
            user1._userstack = stack1;
            user1._userdeck = deck1;

            Shop shop = new Shop();
            Trading trading = new Trading();
            CraftCard craftcard = new CraftCard();
            Friends friends = new Friends(); 

            while (true)
            {
                Console.WriteLine("1: Start a battle\n2: Create Deck\n3: Shop\n4: Trade Center\n5: Scoreboard\n6: Profile\n7: Craft cards\n8: Friends\n9: Quit");
                select = InputHandler.getInstance().InputHandlerForInt(1, 9);
                //Console.Clear(); 
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

                            int battlewinner = gamelogic.BattleLogic(user1, user2);
                            Console.WriteLine("Player " + battlewinner + " won the match!"); //Draw output adden

                            if (battlewinner == 1)
                            {
                                user1.UpdateWin();
                                user2.UpdateLoss();
                            } else if(battlewinner == 2)
                            {
                                user1.UpdateLoss();
                                user2.UpdateWin(); 
                            } else
                            {

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
                        shop.ShopHub(user1);
                        break;
                    case 4:
                        trading.TradingHub(user1);
                        break;
                    case 5:
                        scoreboard.PrintScoreboard();
                        break; 
                    case 6:
                        profilepage.ProfileHub(user1);
                        break;
                    case 7:
                        craftcard.CraftingHub(user1); 
                        break;
                    case 8:
                        friends.FriendsHub(user1); 
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
            Database db = new Database();
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
            Database db = new Database();
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
                Console.WriteLine("Login successful!");
                return success;
            }
        }

        public List<Card> LoadCurrentDeck(int uid)
        {
            Database db = new Database();
            List<Card> deck = db.getSelectedStack(uid);
            return deck; 
        }

        public List<Card> LoadOpponentDeck(int uid, User user2)
        {
            Database db = new Database();
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
            Database db = new Database(); 
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
