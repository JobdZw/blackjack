using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hoop_dat_het_werkt
{
    public class Deck
    {
        private List<Card> cards;
        private Random random = new Random();

        public Deck()
        {
            cards = new List<Card>();
            string[] suits = { "Harten", "Ruiten", "Klaveren", "Schoppen" };
            string[] ranks = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };

            foreach (string suit in suits)
            {
                foreach (string rank in ranks)
                {
                    cards.Add(new Card(suit, rank));
                }
            }
        }

        public Card DrawCard()
        {
            if (cards.Count == 0) return null;

            int index = random.Next(cards.Count);
            Card drawn = cards[index];
            cards.RemoveAt(index);
            return drawn;
        }

        public void Reset()
        {
            cards.Clear();
            new Deck(); // opnieuw vullen
        }
    }

}
