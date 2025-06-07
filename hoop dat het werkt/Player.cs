using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hoop_dat_het_werkt
{
    public class Player
    {
        public string Name { get; set; }
        public List<Card> Hand { get; private set; }
        public int Money { get; set; }
        public bool HasStood { get; set; }
        public int Bet { get; set; }
        public int CurrentBet { get; set; }


        public Player(string name, int startingMoney)
        {
            Name = name;
            Money = startingMoney;
            Hand = new List<Card>();
            HasStood = false;
            Bet = 0;
        }

        public void AddCard(Card card)
        {
            Hand.Add(card);
        }

        public void ResetHand()
        {
            Hand.Clear();
            HasStood = false;
            Bet = 0;
        }

        public int GetScore()
        {
            int score = Hand.Sum(c => c.Value);
            int aces = Hand.Count(c => c.Rank == "A");
            while (score > 21 && aces > 0)
            {
                score -= 10;
                aces--;
            }
            return score;
        }

        public bool IsBusted() => GetScore() > 21;

        public string ShowHand()
        {
            return string.Join(", ", Hand.Select(c => c.ToString()));
        }

        // NPC besluit: Hit of Stand
        public bool WantsToHit()
        {
            int score = GetScore();
            return score < 16 && !IsBusted();
        }
    }


}
