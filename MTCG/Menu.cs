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

            User user1 = new User("Flo");
            User user2 = new User("AI");
            Stack stack1 = new Stack();
            Stack stack2 = new Stack();
            Deck deck1 = new Deck();
            Deck deck2 = new Deck(); 
            

            user1._userstack = stack1;
            user2._userstack = stack2;

            user1._userdeck = deck1;
            user2._userdeck = deck2;

            user1._userstack.AddCard(card1);
            user1._userstack.AddCard(card2);
            user1._userstack.AddCard(card3);
            user1._userstack.AddCard(card4);
            user1._userstack.AddCard(card5);
            user1._userstack.AddCard(card6);
            user1._userstack.AddCard(card7);
            user1._userstack.AddCard(card8);

            
            user1._userdeck.AddCard(card1);
            user1._userdeck.AddCard(card2);
            user1._userdeck.AddCard(card3);
            user1._userdeck.AddCard(card4);
            

            user2._userdeck.AddCard(card5);
            user2._userdeck.AddCard(card6);
            user2._userdeck.AddCard(card7);
            user2._userdeck.AddCard(card8);
            bool loggedin = false; 
            int select = 0;
            Console.WriteLine("Welcome to MTCG\n");

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
                        if(LoginUser() != "false")
                        {
                            loggedin = true; 
                        } 
                        break;
                    case 3:
                        return; 
                        
                    default:
                        Console.WriteLine("Invalid input");
                        continue;
                }
            }

            while (true)
            {
                Console.WriteLine("1: Start a battle\n2: Create Deck\n3: Shop\n4: Quit");
                select = Convert.ToInt32(Console.ReadLine());
                //Console.Clear(); 
                switch (select)
                {
                    case 1:
                        Console.WriteLine("Player " + gamelogic.BattleLogic(user1, user2) + " won the match"); //Draw output adden
                        Console.WriteLine(user1._userdeck._deck.Count);
                        Console.WriteLine(user2._userdeck._deck.Count);

                        break;
                    case 2:
                        user1.CreateDeck();
                        Console.WriteLine("Deck erstellt");
                        user1._userdeck.PrintDeck();
                        Console.WriteLine("----");
                        user1._userstack.PrintStack();

                        break;
                    case 3:
                        //db.addUser();
                        //db.outputTest(); 
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

        public string LoginUser()
        {
            Database db = new Database();
            string username;
            string password;

            Console.WriteLine("Enter username: ");
            username = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Enter password: ");
            password = Convert.ToString(Console.ReadLine());

            if (db.loginUser(username, password) == true)
            {
                Console.WriteLine("Login successful!");
                return username; 
            }
            else
            {
                db.addUser(username, password, 20, 100);
                Console.WriteLine("Login failed!");
                return "false"; 
            }
        }

    }
}
