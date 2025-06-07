using System;
using System.Windows.Forms;

namespace hoop_dat_het_werkt
{
    public partial class Form1 : Form
    {
        private bool dealerPlayed = false;

        private GameManager game;
        private int currentPlayerIndex = 0;
        private bool roundStarted = false;
        private bool player1Done = false;
        private bool allPlayersDone = false;



        public Form1()
        {
            InitializeComponent();
            game = new GameManager();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateUI();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!roundStarted)
            {
                for (int i = 0; i < game.Players.Count; i++)
                {
                    game.DealCardToPlayer(i);
                }

                game.DealCardToDealer();

                roundStarted = true;
                currentPlayerIndex = 0;
            }
            else if (currentPlayerIndex == 0)
            {
                var player = game.Players[0];

                // Jij bestuurt speler 1: alleen als je niet gepast of bust bent
                if (!player.HasStood && !player.IsBusted())
                {
                    game.DealCardToPlayer(0);

                    // Check of speler 1 klaar is
                    if (player.IsBusted() || player.GetScore() >= 21)
                    {
                        player.HasStood = true;
                        currentPlayerIndex++;
                    }
                }
                else
                {
                    currentPlayerIndex++; // speler 1 heeft gepast of bust
                }
            }
            else if (currentPlayerIndex > 0 && currentPlayerIndex < game.Players.Count)
            {
                var player = game.Players[currentPlayerIndex];

                // Andere spelers spelen automatisch HIT tot 17
                while (!player.HasStood && !player.IsBusted() && player.GetScore() < 17)
                {
                    game.DealCardToPlayer(currentPlayerIndex);
                }

                // Als score 17 of meer of bust, stop
                player.HasStood = true;
                currentPlayerIndex++;
            }
            else if (!dealerPlayed)
            {
                DealerTurn();
                DetermineWinners();
                dealerPlayed = true;
            }

            UpdateUI();
        }



        private void button2_Click(object sender, EventArgs e)
        {
            game.ResetGame();

            // Reset interne state
            roundStarted = false;
            currentPlayerIndex = 0;
            dealerPlayed = false;

            UpdateUI();
        }



        private void UpdateUI()
        {
            label1.Text = $"Speler 1 (€{game.Players[0].Money}): {game.Players[0].ShowHand()} ({game.Players[0].GetScore()} punten)";
            label2.Text = $"Speler 2 (€{game.Players[1].Money}): {game.Players[1].ShowHand()} ({game.Players[1].GetScore()} punten)";
            label3.Text = $"Speler 3 (€{game.Players[2].Money}): {game.Players[2].ShowHand()} ({game.Players[2].GetScore()} punten)";
            label4.Text = $"Speler 4 (€{game.Players[3].Money}): {game.Players[3].ShowHand()} ({game.Players[3].GetScore()} punten)";
            label5.Text = $"Dealer: {game.Dealer.ShowHand()} ({game.Dealer.GetScore()} punten)";
            label6.Text = $"Pot: €{game.Pot}";
        }


        private string GetPlayerText(int index)
        {
            var player = game.Players[index];
            string status;

            if (player.GetScore() > 21)
                status = "BUSTED";
            else if (player.HasStood)
                status = "STAND";
            else
                status = "HIT";

            return $"{player.Name} (€{player.Money}): {player.ShowHand()} ({player.GetScore()} punten) - {status}";
        }
        private void DealerTurn()
        {
            while (game.Dealer.GetScore() < 17)
            {
                game.DealCardToDealer();
            }
        }

        private void DetermineWinners()
        {
            int dealerScore = game.Dealer.GetScore();
            bool dealerBust = dealerScore > 21;

            foreach (var player in game.Players)
            {
                int score = player.GetScore();

                if (score > 21)
                {
                    game.Dealer.Money += player.CurrentBet;
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
                    game.Dealer.Money += player.CurrentBet;
                }
            }
        }

    }
}
