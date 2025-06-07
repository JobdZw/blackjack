using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hoop_dat_het_werkt
{
    public class GameManager
    {
        public List<Player> Players { get; private set; }
        public Player Dealer { get; private set; }
        private Deck deck;

        public GameManager()
        {
            deck = new Deck();
            Players = new List<Player>
        {
            new Player("Speler 1", 100),
            new Player("Speler 2", 100),
            new Player("Speler 3", 100),
            new Player("Speler 4", 100)
        };

            Dealer = new Player("Dealer", 0);
        }

        public void DealCardToPlayer(int playerIndex)
        {
            var card = deck.DrawCard();
            Players[playerIndex].AddCard(card);
        }

        public void DealCardToDealer()
        {
            Dealer.AddCard(deck.DrawCard());
        }

        public void ResetGame()
        {
            Pot = 0;

            foreach (var player in Players)
            {
                player.ResetHand();

               
                int inzet = new Random().Next(10, 21);
                player.CurrentBet = inzet;
                player.Money -= inzet;
                Pot += inzet;
            }

            Dealer.ResetHand();
            deck = new Deck();
        }


        public void DealerTurn()
        {
            while (Dealer.GetScore() < 17)
            {
                DealCardToDealer();
            }
        }
        public void PlayNPCs()
        {
            foreach (var player in Players)
            {
                while (player.WantsToHit())
                {
                    DealCardToPlayer(Players.IndexOf(player));
                }

                player.HasStood = true;
            }
        }
        private void DetermineWinners()
        {
            int dealerScore = Dealer.GetScore();
            bool dealerBust = dealerScore > 21;

            foreach (var player in Players)
            {
                int score = player.GetScore();

                if (score > 21)
                {
                    
                    Dealer.Money += player.CurrentBet;
                }
                else if (dealerBust || score > dealerScore)
                {
                  
                    player.Money += player.CurrentBet * 2;
                }
                else if (score == dealerScore)
                {
                   
                    player.Money += player.CurrentBet;
                }
                else
                {
                   
                    Dealer.Money += player.CurrentBet;
                }
            }
        }
        public int Pot { get; private set; }

        public void AddToPot(int amount)
        {
            Pot += amount;
        }

        public void ClearPot()
        {
            Pot = 0;
        }



    }

}
