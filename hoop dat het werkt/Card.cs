using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hoop_dat_het_werkt
{
    public class Card
    {
        public string Suit { get; private set; }
        public string Rank { get; private set; }
        public int Value
        {
            get
            {
                if (Rank == "A") return 11;
                if (Rank == "K" || Rank == "Q" || Rank == "J") return 10;
                return int.Parse(Rank);
            }
        }

        public Card(string suit, string rank)
        {
            Suit = suit;
            Rank = rank;
        }

        public override string ToString()
        {
            return $"{Rank} van {Suit}";
        }
    }

}
