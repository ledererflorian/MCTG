using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG
{
    class Stack
    {

        public List<Card> _stack = new List<Card>(); 

        public void AddCard(Card card)
        {
            _stack.Add(card);
        }

        public void PrintStack()
        {
            foreach (Card card in _stack)
            {
                card.PrintCard();
            }
        }

        public int GetStackSize()
        {
            return _stack.Count;
        }

    }
}
