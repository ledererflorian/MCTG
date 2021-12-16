using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG
{
    class Card
    {
        public int _cardid { get; set; }
        public string _name { get; set; }
        public readonly int _damage;
        public CardTypesEnum.CardTypes _cardtype { get; set; }
        public ElementTypesEnum.ElementTypes _elementtype { get; set; } 
        public RaceTypesEnum.RaceTypes _racetype { get; set; }

        public RaceTypesEnum.RaceTypes _weakness { get; set; }
        public ElementTypesEnum.ElementTypes _elementweakness { get; set; }

        public Card(string name, int damage, CardTypesEnum.CardTypes cardtype, ElementTypesEnum.ElementTypes elementtype, RaceTypesEnum.RaceTypes racetype)
        {
            _name = name;
            _damage = damage;
            _cardtype = cardtype;
            _elementtype = elementtype;
            _racetype = racetype; 

            switch(racetype)
            {
                case RaceTypesEnum.RaceTypes.goblin:
                    _weakness = RaceTypesEnum.RaceTypes.dragon;
                    _elementweakness = ElementTypesEnum.ElementTypes.none; 
                    break;
                case RaceTypesEnum.RaceTypes.ork:
                    _weakness = RaceTypesEnum.RaceTypes.wizzard;
                    _elementweakness = ElementTypesEnum.ElementTypes.none;
                    break;

                case RaceTypesEnum.RaceTypes.knight:
                    _weakness = RaceTypesEnum.RaceTypes.spell;
                    _elementweakness = ElementTypesEnum.ElementTypes.water; 
                    break;
                case RaceTypesEnum.RaceTypes.spell:
                    _weakness = RaceTypesEnum.RaceTypes.kraken;
                    _elementweakness = ElementTypesEnum.ElementTypes.none;
                    break;
                case RaceTypesEnum.RaceTypes.dragon:
                    _weakness = RaceTypesEnum.RaceTypes.elf;
                    _elementweakness = ElementTypesEnum.ElementTypes.fire; 
                    break;


                default:
                    _weakness = RaceTypesEnum.RaceTypes.none; 
                    break; 
            }
        }

        public Card(int id, string name, int damage, CardTypesEnum.CardTypes cardtype, ElementTypesEnum.ElementTypes elementtype, RaceTypesEnum.RaceTypes racetype)
        {
            _cardid = id; 
            _name = name;
            _damage = damage;
            _cardtype = cardtype;
            _elementtype = elementtype;
            _racetype = racetype;

            switch (racetype)
            {
                case RaceTypesEnum.RaceTypes.goblin:
                    _weakness = RaceTypesEnum.RaceTypes.dragon;
                    _elementweakness = ElementTypesEnum.ElementTypes.none;
                    break;
                case RaceTypesEnum.RaceTypes.ork:
                    _weakness = RaceTypesEnum.RaceTypes.wizzard;
                    _elementweakness = ElementTypesEnum.ElementTypes.none;
                    break;

                case RaceTypesEnum.RaceTypes.knight:
                    _weakness = RaceTypesEnum.RaceTypes.spell;
                    _elementweakness = ElementTypesEnum.ElementTypes.water;
                    break;
                case RaceTypesEnum.RaceTypes.spell:
                    _weakness = RaceTypesEnum.RaceTypes.kraken;
                    _elementweakness = ElementTypesEnum.ElementTypes.none;
                    break;
                case RaceTypesEnum.RaceTypes.dragon:
                    _weakness = RaceTypesEnum.RaceTypes.elf;
                    _elementweakness = ElementTypesEnum.ElementTypes.fire;
                    break;


                default:
                    _weakness = RaceTypesEnum.RaceTypes.none;
                    break;
            }
        }

        public void PrintCard()
        {
            Console.WriteLine($"Name: {_name}, Damage: {_damage}, Cardtype: {_cardtype}, Elementtype: {_elementtype}, Racetype: {_racetype}");
        }

        public double IsEffective(Card enemycard)
        {
            if((this._elementtype == ElementTypesEnum.ElementTypes.water && enemycard._elementtype == ElementTypesEnum.ElementTypes.fire) || (this._elementtype == ElementTypesEnum.ElementTypes.fire && enemycard._elementtype == ElementTypesEnum.ElementTypes.normal) || (this._elementtype == ElementTypesEnum.ElementTypes.normal && enemycard._elementtype == ElementTypesEnum.ElementTypes.water))
            {
                return 2; 
            } else if((this._elementtype == ElementTypesEnum.ElementTypes.fire && enemycard._elementtype == ElementTypesEnum.ElementTypes.water) || (this._elementtype == ElementTypesEnum.ElementTypes.normal && enemycard._elementtype == ElementTypesEnum.ElementTypes.fire) || (this._elementtype == ElementTypesEnum.ElementTypes.water && enemycard._elementtype == ElementTypesEnum.ElementTypes.normal))
            {
                return 0.5; 
            } else
            {
                return 1; 
            }
        }


    }
}
