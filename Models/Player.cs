using System;
using System.Collections.Generic;
using System.Text;

namespace PokerProject.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Deck PlayerDeck { get; set; }
        public int PlayerDeckScore { get; set; }
        public string PlayerDeckCombination { get; set; }

        public int PlayerDeckBonusPoints = 0;
    }
}
