using PokerProject.Data;
using PokerProject.Models;
using System.Collections.Generic;
using System.Linq;

namespace PokerProject.Utility
{
    public class TieBreaker
    {
        private List<Player> FilteredPlayers;
        public Player WinningPlayer { get; set; }
        public int HighestCardOfAllDecks { get; set; }

        public TieBreaker(GameData gameData)
        {
            var maxDeckScore = gameData.GetMaxDeckScore();

            FilteredPlayers = gameData.Players.Where(p => p.PlayerDeckScore == maxDeckScore).ToList();
        }
        private List<int> GetDeckHighCardList()
        {
            List<int> DeckHighCardList = new List<int> { };
            var PlayerDecks = FilteredPlayers.Select(fp => fp.PlayerDeck);
            foreach (var deck in PlayerDecks)
            {
                DeckHighCardList.Add(deck.GetAllCardValues().Max());
            }
            return DeckHighCardList;
        }
        private int GetHighestCardOfAllDecks() 
        {
            return GetDeckHighCardList().Max();
        }
        public Player GetHighestFlush() 
        {
            //Method checks the highest suit value Deck first.
            //If more than one, filter further to get deck with Highest card and previously highest suit
            foreach (var filteredPlayer in FilteredPlayers)
            {
                var filteredPlayerSuit = filteredPlayer.PlayerDeck.GetAllCardSuits().FirstOrDefault();
                var cardLibrary = new CardLibrary();
                filteredPlayer.PlayerDeckBonusPoints += cardLibrary.CardSuitIndex[filteredPlayerSuit];
            }

            var maxDeckBonusPoints = FilteredPlayers.Select(fp => fp.PlayerDeckBonusPoints).Max();

            if (FilteredPlayers.Where(fp => fp.PlayerDeckBonusPoints == maxDeckBonusPoints).Count() == 1)
            {
                WinningPlayer = FilteredPlayers.Where(fp => fp.PlayerDeckBonusPoints == maxDeckBonusPoints).FirstOrDefault();
            }
            else
            {
                HighestCardOfAllDecks = GetHighestCardOfAllDecks();
                foreach (var filteredPlayer in FilteredPlayers)
                {
                    if (filteredPlayer.PlayerDeck.GetAllCardValues().Contains(HighestCardOfAllDecks))
                    {
                        WinningPlayer = filteredPlayer;
                    }
                }
            }
            return WinningPlayer;
        }
        public Player GetHighestStraight()
        {
            HighestCardOfAllDecks = GetHighestCardOfAllDecks();

            var refilteredPlayers = new List<Player>{ };

            foreach (var filteredPlayer in FilteredPlayers)
            {
                if (filteredPlayer.PlayerDeck.GetAllCardValues().Contains(HighestCardOfAllDecks))
                {
                    var suitOfHighestCardOfDeck = filteredPlayer.PlayerDeck.GetSuitOfHighestCardOfDeck();
                    
                    var cardLibrary = new CardLibrary();

                    filteredPlayer.PlayerDeckBonusPoints += cardLibrary.CardSuitIndex[suitOfHighestCardOfDeck];
                    refilteredPlayers.Add(filteredPlayer);
                }
            }
            var maxDeckBonusPoint = refilteredPlayers.Select(rp => rp.PlayerDeckBonusPoints).Max();

            WinningPlayer = refilteredPlayers.Where(rp => rp.PlayerDeckBonusPoints == maxDeckBonusPoint).FirstOrDefault();
            
            return WinningPlayer;
        }
        public Player GetHighestFullHouseOrHighestTrio()
        {
            var filteredPlayerDecks = FilteredPlayers.Select(fp => fp.PlayerDeck);
            List<int> listOfTrioCards = new List<int>{ };

            foreach (var deck in filteredPlayerDecks)
            {
                var trioCardValue = deck.GetValueOfSpecificGroupOfCards(3);
                listOfTrioCards.Add(trioCardValue);
            }
            var maxTrioCardValueOfAllFilteredPlayerDecks = listOfTrioCards.Max();

            var WinningPlayer = FilteredPlayers.Where(fp =>
                fp.PlayerDeck.GetValueOfSpecificGroupOfCards(3) == maxTrioCardValueOfAllFilteredPlayerDecks).FirstOrDefault();
            
            return WinningPlayer;
        }
        public Player GetHighestFourOfAKind()
        {
            var filteredPlayerDecks = FilteredPlayers.Select(fp => fp.PlayerDeck);
            List<int> listOfFourOfAKindCards = new List<int> { };

            foreach (var deck in filteredPlayerDecks)
            {
                var FourOfAKindCardValue = deck.GetValueOfSpecificGroupOfCards(4);
                listOfFourOfAKindCards.Add(FourOfAKindCardValue);
            }
            var maxTrioCardValueOfAllFilteredPlayerDecks = listOfFourOfAKindCards.Max();

            var WinningPlayer = FilteredPlayers.Where(fp =>
                fp.PlayerDeck.GetValueOfSpecificGroupOfCards(4) == maxTrioCardValueOfAllFilteredPlayerDecks).FirstOrDefault();

            return WinningPlayer;
        }
        public Player GetHighestTwoPairOrGetHighestOnePair()
        {
            var filteredPlayerDecks = FilteredPlayers.Select(fp => fp.PlayerDeck);
            List<int> MaxPairCardValueList = new List<int>{ };

            foreach (var deck in filteredPlayerDecks)
            {
                var maxPairCardValue = deck.GetValueOfSpecificGroupOfCards(2);
                MaxPairCardValueList.Add(maxPairCardValue);
            }

            if (MaxPairCardValueList.Count(v => v == MaxPairCardValueList.Max()) > 1)
            {
                var refilteredPlayers = FilteredPlayers.Where(fp => 
                    fp.PlayerDeck.GetAllCardValues().Count(v => v == MaxPairCardValueList.Max()) == 2).ToList();

                foreach (var refilteredPlayer in refilteredPlayers)
                {
                    Card HighestTwoPairCard = refilteredPlayer.PlayerDeck.Cards
                        .Where(c => c.Value == MaxPairCardValueList.Max() && c.Suite == "Diamond")
                        .FirstOrDefault();

                    if (HighestTwoPairCard == null)
                    {
                        
                    }
                    else
                    {
                        WinningPlayer = refilteredPlayer;
                    }
                }
            }
            else
            {
               WinningPlayer = FilteredPlayers
                    .Where(fp => fp.PlayerDeck.GetAllCardValues()
                    .Count(v => v == MaxPairCardValueList.Max()) == 2)
                    .FirstOrDefault();
            }
            return WinningPlayer;
        }
        public Player GetHighestHighCard()
        {
            var highestCardValueList = FilteredPlayers.Select(fp => fp.PlayerDeck.GetHighestCardOfDeck()).ToList();

            if (highestCardValueList.Count(v => v == highestCardValueList.Max()) > 1)
            {
                var refilteredPlayers = FilteredPlayers.Where(fp =>
                    fp.PlayerDeck.GetAllCardValues().Count(v => v == highestCardValueList.Max()) == 1).ToList();
                
                List<int> suitValueOfHighestCardOfDeckList = new List<int> { };
                var cardLibrary = new CardLibrary();
                foreach (var refilteredPlayer in refilteredPlayers)
                {
                    var suitOfHighestCardOfDeck = refilteredPlayer.PlayerDeck.GetSuitOfHighestCardOfDeck();
                    
                    var currentHighestSuitValue = cardLibrary.CardSuitIndex[suitOfHighestCardOfDeck];
                    suitValueOfHighestCardOfDeckList.Add(currentHighestSuitValue);
                }
                var highestSuitCard = cardLibrary.CardSuitIndex
                        .Where(c => c.Value == suitValueOfHighestCardOfDeckList.Max())
                        .FirstOrDefault();
                WinningPlayer = refilteredPlayers
                    .Where(rp => rp.PlayerDeck.GetHighestCardOfDeck() == highestCardValueList.Max()
                            && rp.PlayerDeck.GetSuitOfHighestCardOfDeck() == highestSuitCard.Key)
                    .FirstOrDefault();
            }
            else
            {
                WinningPlayer = FilteredPlayers
                     .Where(fp => fp.PlayerDeck.GetAllCardValues()
                     .Count(v => v == highestCardValueList.Max()) == 1)
                     .FirstOrDefault();
            }
            return WinningPlayer;

        }
    }
}
