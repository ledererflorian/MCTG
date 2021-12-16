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

           
            User user2 = new User("AI");
            Stack stack1 = new Stack();
            Stack stack2 = new Stack();
            Deck deck1 = new Deck();
            Deck deck2 = new Deck();



            user2._userstack = stack2;

            user2._userdeck = deck2;

            user2._userdeck.AddCard(card5);
            user2._userdeck.AddCard(card6);
            user2._userdeck.AddCard(card7);
            user2._userdeck.AddCard(card8);
            bool loggedin = false; 
            int select = 0;
            Console.WriteLine("Welcome to MTCG\n");
            int loggedinID = 0;

            while (!loggedin)
            {
                Console.WriteLine("1: Register\n2: Login\n3: Quit");
                select = Convert.ToInt32(Console.ReadLine());
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
                    case 4:
                        //CreateCardsInDB(); 
                        //db.getCardByID(); 
                        //db.getStack(); 
                        //db.selectCard(10, 383);
                        //db.deselectCards(10); 
                        //db.getSelectedCardCount(10); 
                        //db.getCardCount(5);
                        //db.getRandomCardID(); 
                        //db.addCardToStack(5, 400);
                        //db.getCoinsByUserID(10);
                        //db.getEloByUserID(10);
                        break;


                    default:
                        Console.WriteLine("Invalid input");
                        continue;
                }
            }

            User user1 = new User(db.getUsernameByUserID(loggedinID));
            user1._userid = loggedinID;
            user1._coins = db.getCoinsByUserID(user1._userid);
            user1._elo = db.getEloByUserID(user1._userid);
            user1._userstack = stack1;
            user1._userdeck = deck1; 

            while (true)
            {
                Console.WriteLine("1: Start a battle\n2: Create Deck\n3: Shop\n4: Quit");
                select = Convert.ToInt32(Console.ReadLine());
                //Console.Clear(); 
                switch (select)
                {
                    case 1:
                        if(db.getSelectedCardCount(user1._userid) < 4)
                        {
                            Console.WriteLine("Create a deck before fighting!");
                        } else
                        {
                            Console.WriteLine("Player " + gamelogic.BattleLogic(user1, user2) + " won the match"); //Draw output adden
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
                        user1.Shop(); 
                        break;
                    case 4:
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
            string password;

            Console.WriteLine("Enter username: ");
            username = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Enter password: ");
            password = Convert.ToString(Console.ReadLine());

            if ((success = db.loginUser(username, password)) == 0)
            {
                Console.WriteLine("Login failed!");
                return 0;

            }
            else
            {
                Console.WriteLine("Login successful!");
                return success;
            }
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
