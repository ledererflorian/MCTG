using NUnit.Framework;
using MTCG;
namespace MTCGTests
{
    public class Tests
    {
        
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void DragonsDefeatGoblins()
        {
            //ARRANGE
            Card card1 = new Card("Goblin", 100, CardTypesEnum.CardTypes.monster, ElementTypesEnum.ElementTypes.normal, RaceTypesEnum.RaceTypes.goblin);
            Card card2 = new Card("Dragon", 1, CardTypesEnum.CardTypes.monster, ElementTypesEnum.ElementTypes.normal, RaceTypesEnum.RaceTypes.dragon);
            GameLogic gamelogic = new GameLogic();

            //ACT
            int returnvalue = gamelogic.calcWinner(card1, card2);

            //ASSERT
            Assert.AreEqual(2, returnvalue);
        }

        [Test]
        public void WizzardsDefeatOrks()
        {
            //ARRANGE
            Card card1 = new Card("Wizzard", 1, CardTypesEnum.CardTypes.monster, ElementTypesEnum.ElementTypes.normal, RaceTypesEnum.RaceTypes.wizzard);
            Card card2 = new Card("Ork", 100, CardTypesEnum.CardTypes.monster, ElementTypesEnum.ElementTypes.normal, RaceTypesEnum.RaceTypes.ork);
            GameLogic gamelogic = new GameLogic();

            //ACT
            int returnvalue = gamelogic.calcWinner(card1, card2);

            //ASSERT
            Assert.AreEqual(1, returnvalue);
        }

        [Test]
        public void WaterspellDefeatsKnight()
        {
            //ARRANGE
            Card card1 = new Card("Knight", 100, CardTypesEnum.CardTypes.monster, ElementTypesEnum.ElementTypes.normal, RaceTypesEnum.RaceTypes.knight);
            Card card2 = new Card("WaterSpell", 1, CardTypesEnum.CardTypes.spell, ElementTypesEnum.ElementTypes.water, RaceTypesEnum.RaceTypes.spell);
            GameLogic gamelogic = new GameLogic();

            //ACT
            int returnvalue = gamelogic.calcWinner(card1, card2);

            //ASSERT
            Assert.AreEqual(2, returnvalue);
        }

        [Test]
        public void NormalSpellDoesNotDefeatKnight()
        {
            //ARRANGE
            Card card1 = new Card("Knight", 100, CardTypesEnum.CardTypes.monster, ElementTypesEnum.ElementTypes.normal, RaceTypesEnum.RaceTypes.knight);
            Card card2 = new Card("Spell", 1, CardTypesEnum.CardTypes.spell, ElementTypesEnum.ElementTypes.normal, RaceTypesEnum.RaceTypes.spell);
            GameLogic gamelogic = new GameLogic();

            //ACT
            int returnvalue = gamelogic.calcWinner(card1, card2);

            //ASSERT
            Assert.AreEqual(1, returnvalue);

        }
        
        [Test]
        public void KrakenDefeatsAllSpells()
        {
            //ARRANGE
            Card card1 = new Card("Kraken", 1, CardTypesEnum.CardTypes.monster, ElementTypesEnum.ElementTypes.normal, RaceTypesEnum.RaceTypes.kraken);
            Card card2 = new Card("Spell", 100, CardTypesEnum.CardTypes.spell, ElementTypesEnum.ElementTypes.normal, RaceTypesEnum.RaceTypes.spell);
            Card card3 = new Card("FireSpell", 100, CardTypesEnum.CardTypes.spell, ElementTypesEnum.ElementTypes.fire, RaceTypesEnum.RaceTypes.spell);
            Card card4 = new Card("WaterSpell", 100, CardTypesEnum.CardTypes.spell, ElementTypesEnum.ElementTypes.water, RaceTypesEnum.RaceTypes.spell);
            GameLogic gamelogic = new GameLogic();

            //ACT
            int returnvalue = gamelogic.calcWinner(card1, card2);
            int returnvalue2 = gamelogic.calcWinner(card1, card3);
            int returnvalue3 = gamelogic.calcWinner(card1, card4);

            //ASSERT
            Assert.AreEqual(1, returnvalue);
            Assert.AreEqual(1, returnvalue2);
            Assert.AreEqual(1, returnvalue3);
        }

        [Test]
        public void FireelvesDefeatDragons()
        {
            //ARRANGE
            Card elf = new Card("Elf", 1, CardTypesEnum.CardTypes.monster, ElementTypesEnum.ElementTypes.normal, RaceTypesEnum.RaceTypes.elf);
            Card fireelf = new Card("FireElf", 1, CardTypesEnum.CardTypes.monster, ElementTypesEnum.ElementTypes.fire, RaceTypesEnum.RaceTypes.elf);
            Card waterelf = new Card("WaterElf", 1, CardTypesEnum.CardTypes.monster, ElementTypesEnum.ElementTypes.water, RaceTypesEnum.RaceTypes.elf);
            Card dragon = new Card("Dragon", 100, CardTypesEnum.CardTypes.monster, ElementTypesEnum.ElementTypes.normal, RaceTypesEnum.RaceTypes.dragon);
            GameLogic gamelogic = new GameLogic();

            //ACT
            int returnvalue1 = gamelogic.calcWinner(fireelf, dragon);
            int returnvalue2 = gamelogic.calcWinner(elf, dragon);
            int returnvalue3 = gamelogic.calcWinner(waterelf, dragon);

            //ASSERT
            Assert.AreEqual(1, returnvalue1);
            Assert.AreEqual(2, returnvalue2);
            Assert.AreEqual(2, returnvalue3);
        }

        [Test]
        public void BattleLogicP1Wins()
        {
            //ARRANGE
            Card card1 = new Card("Goblin", 2, CardTypesEnum.CardTypes.monster, ElementTypesEnum.ElementTypes.normal, RaceTypesEnum.RaceTypes.goblin);
            Card card2 = new Card("Goblin", 2, CardTypesEnum.CardTypes.monster, ElementTypesEnum.ElementTypes.normal, RaceTypesEnum.RaceTypes.goblin);
            Card card3 = new Card("Goblin", 2, CardTypesEnum.CardTypes.monster, ElementTypesEnum.ElementTypes.normal, RaceTypesEnum.RaceTypes.goblin);
            Card card4 = new Card("Goblin", 2, CardTypesEnum.CardTypes.monster, ElementTypesEnum.ElementTypes.normal, RaceTypesEnum.RaceTypes.goblin);
            Card card5 = new Card("Goblin", 1, CardTypesEnum.CardTypes.monster, ElementTypesEnum.ElementTypes.normal, RaceTypesEnum.RaceTypes.goblin);
            Card card6 = new Card("Goblin", 1, CardTypesEnum.CardTypes.monster, ElementTypesEnum.ElementTypes.normal, RaceTypesEnum.RaceTypes.goblin);
            Card card7 = new Card("Goblin", 1, CardTypesEnum.CardTypes.monster, ElementTypesEnum.ElementTypes.normal, RaceTypesEnum.RaceTypes.goblin);
            Card card8 = new Card("Goblin", 1, CardTypesEnum.CardTypes.monster, ElementTypesEnum.ElementTypes.normal, RaceTypesEnum.RaceTypes.goblin);
            
            User user1 = new User(0, "p1", 0, 0, 0, 0);
            User user2 = new User(0, "p2", 0, 0, 0, 0);

            Deck deck1 = new Deck();
            deck1.AddCard(card1);
            deck1.AddCard(card2);
            deck1.AddCard(card3);
            deck1.AddCard(card4);

            Deck deck2 = new Deck();
            deck2.AddCard(card5);
            deck2.AddCard(card6);
            deck2.AddCard(card7);
            deck2.AddCard(card8);

            user1._userdeck = deck1; 
            user2._userdeck = deck2; 


            GameLogic gamelogic = new GameLogic();

            //ACT
            int battlewinner = gamelogic.BattleLogic(user1, user2);

            //ASSERT
            Assert.AreEqual(1, battlewinner);
        }

        [Test]
        public void BattleLogicP2Wins()
        {
            //ARRANGE
            Card card1 = new Card("Goblin", 1, CardTypesEnum.CardTypes.monster, ElementTypesEnum.ElementTypes.normal, RaceTypesEnum.RaceTypes.goblin);
            Card card2 = new Card("Goblin", 1, CardTypesEnum.CardTypes.monster, ElementTypesEnum.ElementTypes.normal, RaceTypesEnum.RaceTypes.goblin);
            Card card3 = new Card("Goblin", 1, CardTypesEnum.CardTypes.monster, ElementTypesEnum.ElementTypes.normal, RaceTypesEnum.RaceTypes.goblin);
            Card card4 = new Card("Goblin", 1, CardTypesEnum.CardTypes.monster, ElementTypesEnum.ElementTypes.normal, RaceTypesEnum.RaceTypes.goblin);
            Card card5 = new Card("Goblin", 2, CardTypesEnum.CardTypes.monster, ElementTypesEnum.ElementTypes.normal, RaceTypesEnum.RaceTypes.goblin);
            Card card6 = new Card("Goblin", 2, CardTypesEnum.CardTypes.monster, ElementTypesEnum.ElementTypes.normal, RaceTypesEnum.RaceTypes.goblin);
            Card card7 = new Card("Goblin", 2, CardTypesEnum.CardTypes.monster, ElementTypesEnum.ElementTypes.normal, RaceTypesEnum.RaceTypes.goblin);
            Card card8 = new Card("Goblin", 2, CardTypesEnum.CardTypes.monster, ElementTypesEnum.ElementTypes.normal, RaceTypesEnum.RaceTypes.goblin);

            User user1 = new User(0, "p1", 0, 0, 0, 0);
            User user2 = new User(0, "p2", 0, 0, 0, 0);

            Deck deck1 = new Deck();
            deck1.AddCard(card1);
            deck1.AddCard(card2);
            deck1.AddCard(card3);
            deck1.AddCard(card4);

            Deck deck2 = new Deck();
            deck2.AddCard(card5);
            deck2.AddCard(card6);
            deck2.AddCard(card7);
            deck2.AddCard(card8);

            user1._userdeck = deck1;
            user2._userdeck = deck2;


            GameLogic gamelogic = new GameLogic();

            //ACT
            int battlewinner = gamelogic.BattleLogic(user1, user2);

            //ASSERT
            Assert.AreEqual(2, battlewinner);
        }

        [Test]
        public void BattleLogicDraw()
        {
            //ARRANGE
            Card card1 = new Card("Goblin", 1, CardTypesEnum.CardTypes.monster, ElementTypesEnum.ElementTypes.normal, RaceTypesEnum.RaceTypes.goblin);
            Card card2 = new Card("Goblin", 1, CardTypesEnum.CardTypes.monster, ElementTypesEnum.ElementTypes.normal, RaceTypesEnum.RaceTypes.goblin);
            Card card3 = new Card("Goblin", 1, CardTypesEnum.CardTypes.monster, ElementTypesEnum.ElementTypes.normal, RaceTypesEnum.RaceTypes.goblin);
            Card card4 = new Card("Goblin", 1, CardTypesEnum.CardTypes.monster, ElementTypesEnum.ElementTypes.normal, RaceTypesEnum.RaceTypes.goblin);
            Card card5 = new Card("Goblin", 1, CardTypesEnum.CardTypes.monster, ElementTypesEnum.ElementTypes.normal, RaceTypesEnum.RaceTypes.goblin);
            Card card6 = new Card("Goblin", 1, CardTypesEnum.CardTypes.monster, ElementTypesEnum.ElementTypes.normal, RaceTypesEnum.RaceTypes.goblin);
            Card card7 = new Card("Goblin", 1, CardTypesEnum.CardTypes.monster, ElementTypesEnum.ElementTypes.normal, RaceTypesEnum.RaceTypes.goblin);
            Card card8 = new Card("Goblin", 1, CardTypesEnum.CardTypes.monster, ElementTypesEnum.ElementTypes.normal, RaceTypesEnum.RaceTypes.goblin);

            User user1 = new User(0, "p1", 0, 0, 0, 0);
            User user2 = new User(0, "p2", 0, 0, 0, 0);

            Deck deck1 = new Deck();
            deck1.AddCard(card1);
            deck1.AddCard(card2);
            deck1.AddCard(card3);
            deck1.AddCard(card4);

            Deck deck2 = new Deck();
            deck2.AddCard(card5);
            deck2.AddCard(card6);
            deck2.AddCard(card7);
            deck2.AddCard(card8);

            user1._userdeck = deck1;
            user2._userdeck = deck2;


            GameLogic gamelogic = new GameLogic();

            //ACT
            int battlewinner = gamelogic.BattleLogic(user1, user2);

            //ASSERT
            Assert.AreEqual(0, battlewinner);
        }
        /*
        [Test]
        public void InputHandlerReturnsCorrectValues()
        {
            
            //ARRANGE
            int min = 3;
            int max = 15;
            int returnvalue;

            //ACT + ASSERT
            for (int i = 0; i < 3; i++)
            {
                returnvalue = InputHandler.getInstance().InputHandlerForInt(min, max);
                if(returnvalue > max || returnvalue < min)
                {
                    Assert.Fail(); 
                }
            }
            Assert.Pass();
            
        }
        */

        [Test]
        public void Element_SpellEffectivenessAgainstMonsters()
        {
            //ARRANGE
            Card card1 = new Card("FireSpell", 10, CardTypesEnum.CardTypes.spell, ElementTypesEnum.ElementTypes.fire, RaceTypesEnum.RaceTypes.none);
            Card card2 = new Card("WaterSpell", 10, CardTypesEnum.CardTypes.spell, ElementTypesEnum.ElementTypes.water, RaceTypesEnum.RaceTypes.none);
            Card card3 = new Card("Spell", 10, CardTypesEnum.CardTypes.spell, ElementTypesEnum.ElementTypes.normal, RaceTypesEnum.RaceTypes.none);

            Card card4 = new Card("FireGoblin", 30, CardTypesEnum.CardTypes.monster, ElementTypesEnum.ElementTypes.fire, RaceTypesEnum.RaceTypes.goblin);
            Card card5 = new Card("WaterGoblin", 30, CardTypesEnum.CardTypes.monster, ElementTypesEnum.ElementTypes.water, RaceTypesEnum.RaceTypes.goblin);
            Card card6 = new Card("Goblin", 30, CardTypesEnum.CardTypes.monster, ElementTypesEnum.ElementTypes.normal, RaceTypesEnum.RaceTypes.goblin);
            GameLogic gamelogic = new GameLogic();

            //ACT
            int returnvalue1 = gamelogic.calcWinner(card1, card6); //fire vs normal
            int returnvalue2 = gamelogic.calcWinner(card2, card4); //water vs fire
            int returnvalue3 = gamelogic.calcWinner(card3, card5); //normal vs water

            //ASSERT
            if (returnvalue1 == 1 && returnvalue2 == 1 && returnvalue3 == 1) // 1 = first card wins
            {
                Assert.Pass();
            } else
            {
                Assert.Fail(); 
            }
        }

        [Test]
        public void Element_MonsterEffectivenessAgainstMonsters()
        {
            //ARRANGE
            Card card1 = new Card("FireGoblin", 30, CardTypesEnum.CardTypes.monster, ElementTypesEnum.ElementTypes.fire, RaceTypesEnum.RaceTypes.goblin);
            Card card2 = new Card("WaterGoblin", 30, CardTypesEnum.CardTypes.monster, ElementTypesEnum.ElementTypes.water, RaceTypesEnum.RaceTypes.goblin);
            Card card3 = new Card("Goblin", 30, CardTypesEnum.CardTypes.monster, ElementTypesEnum.ElementTypes.normal, RaceTypesEnum.RaceTypes.goblin);
            GameLogic gamelogic = new GameLogic();

            //ACT
            int returnvalue1 = gamelogic.calcWinner(card1, card3); //fire vs normal
            int returnvalue2 = gamelogic.calcWinner(card2, card1); //water vs fire
            int returnvalue3 = gamelogic.calcWinner(card3, card2); //normal vs water

            //ASSERT
            if (returnvalue1 == 0 && returnvalue2 == 0 && returnvalue3 == 0) //0 = draw
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
        }


    }
}