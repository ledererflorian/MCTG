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



    }
}