using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG
{
    class Tradeoffer
    {

        public int _tradingid { get; set; }
        public int _ownerid { get; set; }
        public int _cardid { get; set; }
        public CardTypesEnum.CardTypes _typerequirement { get; set; }
        public int _damagerequirement { get; set; }

        public Tradeoffer(int tradingid, int ownerid, int cardid, CardTypesEnum.CardTypes typerequirement, int damagerequirement)
        {
            _tradingid = tradingid;
            _ownerid = ownerid;
            _cardid = cardid;
            _typerequirement = typerequirement;
            _damagerequirement = damagerequirement;
        }


    }
}
