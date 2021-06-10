using PokerProject.Data;
using PokerProject.Models;
using PokerProject.Utility;
using PokerProject.Utility.DeckEvaluator;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PokerProject
{
    public partial class Form1 : Form
    {
        private GameData GameData = new GameData();
        private const string ImagesPath = @"C:\Users\MAR\source\repos\PokerProject\images\Cards\";
        private string PlayerName = "";
        private List<string> PlayerCombCollector = new List<string> { };
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            PlayerName = "Renz";
            label1.Text = PlayerName;
            DrawCards(PlayerName, new Point(15, 80));
            textBox1.Text = GetDeckCombination(PlayerName);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            PlayerName = "Wa";
            label2.Text = PlayerName;
            DrawCards(PlayerName, new Point(15, 200));
            textBox2.Text = GetDeckCombination(PlayerName);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            PlayerName = "Kith";
            label3.Text = PlayerName;
            DrawCards(PlayerName, new Point(15, 320));
            textBox3.Text = GetDeckCombination(PlayerName);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            PickWinner();
        }
        private void DrawCards(string playerName, Point coordinates) 
        {
            var playerDeck = GameData.Players.Where(p => p.Name == playerName).FirstOrDefault().PlayerDeck;
            
            foreach (var card in playerDeck.Cards)
            {
                PictureBox pb = new PictureBox
                {
                    Image = Image.FromFile(ImagesPath + card.Code + ".jpg"),
                    Width = 75,
                    Height = 100,
                    BorderStyle = BorderStyle.Fixed3D,
                    Location = coordinates
                };

                coordinates.X = coordinates.X + pb.Width + 10;

                this.Controls.Add(pb);
            }
        }
        private string GetDeckCombination(string playerName) 
        {
            var playerDeck = GameData.Players.Where(p => p.Name == playerName).FirstOrDefault().PlayerDeck;
            var deckEvaluator = new DeckEvaluator(playerDeck);
            
            return deckEvaluator.GetDeckCombination();
        }
        public void PickWinner() 
        {
            var gameData = new GameData();
            gameData.SetPlayerDeckScoreAndDeckCombinationProperty();
            var winnerPicker = new WinnerPicker(gameData);
            textBox4.Text = winnerPicker.GetWinningPlayer().Name;
        }
    }
}
