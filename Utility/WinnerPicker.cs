using PokerProject.Data;
using PokerProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokerProject.Utility
{
    public class WinnerPicker
    {
        private GameData GameData;
        public Player WinningPlayer { get; set; }
        public List<Player> WinningPlayers { get; set; }
        public WinnerPicker(GameData gameData)
        {
            GameData = gameData;
            
        }
        public Player GetWinningPlayer() 
        {
            int maxDeckScore = GameData.GetMaxDeckScore();
            List<int> deckScoresList = GameData.GetAllDeckScores();
            
            //check "number of players" to "groups of players with similar Combination",
            //if True, means A winner can already be picked by getting highest Deck Score
            if (deckScoresList.Count(s => s == maxDeckScore) == 1)
            {
                WinningPlayer = GameData.Players.Where(p => p.PlayerDeckScore == maxDeckScore).FirstOrDefault();
            }
            else
            {
                string maxDeckCombination = GameData.Players
                    .Where(p => p.PlayerDeckScore == maxDeckScore)
                    .FirstOrDefault().PlayerDeckCombination;

                WinningPlayer = SelectTieBreakMethod(maxDeckCombination);
            }

            return WinningPlayer;
        }
        private Player SelectTieBreakMethod(string deckCombination) 
        {
            var tieBreaker = new TieBreaker(GameData);
            
            if (deckCombination == Enum.GetName(typeof(CardLibrary.CardCombinationList), 
                CardLibrary.CardCombinationList.Flushes))
            {
                return tieBreaker.GetHighestFlush();
            }
            else if (deckCombination == Enum.GetName(typeof(CardLibrary.CardCombinationList),
                CardLibrary.CardCombinationList.FourOfAKind))
            {
                return tieBreaker.GetHighestFourOfAKind();
            }
            else if (deckCombination == Enum.GetName(typeof(CardLibrary.CardCombinationList),
               CardLibrary.CardCombinationList.FullHouse))
            {
                return tieBreaker.GetHighestFullHouseOrHighestTrio();
            }
            else if (deckCombination == Enum.GetName(typeof(CardLibrary.CardCombinationList),
               CardLibrary.CardCombinationList.HighCard))
            {
                return tieBreaker.GetHighestHighCard();
            }
            else if (deckCombination == Enum.GetName(typeof(CardLibrary.CardCombinationList),
               CardLibrary.CardCombinationList.OnePair))
            {
                return tieBreaker.GetHighestTwoPairOrGetHighestOnePair();
            }
            else if (deckCombination == Enum.GetName(typeof(CardLibrary.CardCombinationList),
               CardLibrary.CardCombinationList.Straight))
            {
                return tieBreaker.GetHighestStraight();
            }
            else if (deckCombination == Enum.GetName(typeof(CardLibrary.CardCombinationList),
               CardLibrary.CardCombinationList.Trio))
            {
                return tieBreaker.GetHighestFullHouseOrHighestTrio();
            }
            else if (deckCombination == Enum.GetName(typeof(CardLibrary.CardCombinationList),
               CardLibrary.CardCombinationList.TwoPair))
            {
                return tieBreaker.GetHighestTwoPairOrGetHighestOnePair();
            }
            else
            {
                return null;
            }
        }
    }
}
