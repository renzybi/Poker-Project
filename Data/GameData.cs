using PokerProject.Models;
using PokerProject.Utility.DeckEvaluator;
using System.Collections.Generic;
using System.Linq;

namespace PokerProject.Data
{
    public class GameData
    {
        public List<Player> Players = new List<Player> { };
        public GameData()
        {
            Players.Add(
                new Player
                {
                    Name = "Renz",
                    PlayerDeck = new Deck
                    {
                        Cards = new List<Card> 
                        {
                            new Card { Id = 1, Value = 7, Suite = "Clover" },
                            new Card { Id = 2, Value = 13, Suite = "Heart" },
                            new Card { Id = 3, Value = 3, Suite = "Diamond" },
                            new Card { Id = 4, Value = 13, Suite = "Clover" },
                            new Card { Id = 5, Value = 7, Suite = "Diamond" },
                        }
                    }
                }
            );
            Players.Add(
                new Player
                {
                    Name = "Wa",
                    PlayerDeck = new Deck
                    {
                        Cards = new List<Card>
                        {
                            new Card { Id = 1, Value = 11, Suite = "Clover" },
                            new Card { Id = 2, Value = 8, Suite = "Spade" },
                            new Card { Id = 3, Value = 11, Suite = "Heart" },
                            new Card { Id = 4, Value = 2, Suite = "Spade" },
                            new Card { Id = 5, Value = 8, Suite = "Diamond" },
                        }
                    }
                }
            );
            Players.Add(
                new Player
                {
                    Name = "Kith",
                    PlayerDeck = new Deck
                    {
                        Cards = new List<Card>
                        {
                            new Card { Id = 1, Value = 5, Suite = "Heart" },
                            new Card { Id = 2, Value = 13, Suite = "Spade" },
                            new Card { Id = 3, Value = 13, Suite = "Diamond" },
                            new Card { Id = 4, Value = 5, Suite = "Clover" },
                            new Card { Id = 5, Value = 9, Suite = "Heart" },
                        }
                    }
                }
            );
        }
        public void SetPlayerDeckScoreAndDeckCombinationProperty() 
        {
            foreach (var player in Players)
            {
                var evaluator = new DeckEvaluator(player.PlayerDeck);
                player.PlayerDeckScore = evaluator.GetDeckValue();
                player.PlayerDeckCombination = evaluator.GetDeckCombination();
            }
        }
        public List<int> GetAllDeckScores()
        {
            return Players.Select(p => p.PlayerDeckScore).ToList();
        }
        public int GetMaxDeckScore()
        {
            return Players.Select(p => p.PlayerDeckScore).Max();
        }
        public int GetMaxDeckBonusPoints()
        {
            return Players.Select(p => p.PlayerDeckBonusPoints).Max();
        }
    }
}
