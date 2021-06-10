using PokerProject.Data;
using PokerProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static PokerProject.Data.CardLibrary;

namespace PokerProject.Utility.DeckEvaluator
{
    public class DeckEvaluator
    {
        private Deck Deck;
        private string Combination = String.Empty;
        public DeckEvaluator(Deck deck)
        {
            Deck = deck;
        }
        public string GetDeckCombination() 
        {
            List<string> possibleCombinationList = new List<string> { };

            if (!string.IsNullOrEmpty(CheckForFlash()))
            {
                possibleCombinationList.Add(CheckForFlash());
            }
            else if (!string.IsNullOrEmpty(CheckForFullHouseOrFourOfKind()))
            {
                possibleCombinationList.Add(CheckForFullHouseOrFourOfKind());
            }
            else if (!string.IsNullOrEmpty(CheckForOnePair()))
            {
                possibleCombinationList.Add(CheckForOnePair());
            }
            else if (!string.IsNullOrEmpty(CheckForStraightFlush()))
            {
                possibleCombinationList.Add(CheckForStraightFlush());
            }
            else if (!string.IsNullOrEmpty(CheckForStraightOrHighCard()))
            {
                possibleCombinationList.Add(CheckForStraightOrHighCard());
            }
            else if (!string.IsNullOrEmpty(CheckForTrioOrTwoPair()))
            {
                possibleCombinationList.Add(CheckForTrioOrTwoPair());
            }
            return possibleCombinationList.FirstOrDefault();
        }
        public int GetDeckValue()
        {
            var deckCombination = GetDeckCombination();
            var cardLibrary = new CardLibrary();

            var deckValue = cardLibrary.CardCombinationIndex[deckCombination];
            return deckValue;
        }
        public string CheckForFlash()
        {
            var cardSuites = Deck.GetAllCardSuits();

            var cardSuitesCount = cardSuites.GroupBy(s => s).Count();

            if (cardSuitesCount == 1 && String.IsNullOrEmpty(CheckForStraightOrHighCard()))
            {
                Combination = Enum.GetName(typeof(CardCombinationList), CardCombinationList.Flushes);
            }
            return Combination;
        }
        public string CheckForFullHouseOrFourOfKind()
        {
            var cardValues = Deck.GetAllCardValues();

            var groupOfCardValues = cardValues.GroupBy(v => v);

            if (groupOfCardValues.Count() == 2)
            {
                var groupDict = new Dictionary<int, int>();

                foreach (var group in groupOfCardValues)
                {
                    groupDict.Add(group.Key, group.Count());
                }

                if (groupDict.Values.Contains(3) && groupDict.Values.Contains(2))
                {
                    Combination = Enum.GetName(typeof(CardCombinationList), CardCombinationList.FullHouse);
                }
                else if (groupDict.Values.Contains(4))
                {
                    Combination = Enum.GetName(typeof(CardCombinationList), CardCombinationList.FourOfAKind);
                }
            }
            return Combination;
        }
        public string CheckForOnePair()
        {
            var cardValues = Deck.GetAllCardValues();

            var groupOfCardValues = cardValues.GroupBy(v => v);

            if (groupOfCardValues.Count() == 4)
            {
                Combination = Enum.GetName(typeof(CardCombinationList), CardCombinationList.OnePair);
            }
            return Combination;
        }
        public string CheckForStraightFlush()
        {
            List<int> cardValues = Deck.GetAllCardValues();

            int[] cardValuesSorted = cardValues.OrderBy(v => v).ToArray();

            var groupOfCardValuesCount = cardValuesSorted.GroupBy(i => i).Count();

            int suitsCount = Deck.GetAllCardSuits().GroupBy(s => s).Count();

            if (groupOfCardValuesCount == 5 && suitsCount == 1)
            {
                for (int i = 0; i < cardValuesSorted.Length - 1; i++)
                {
                    if (cardValuesSorted[i] + 1 == cardValuesSorted[i + 1])
                    {
                        Combination = Enum.GetName(typeof(CardCombinationList), CardCombinationList.StraightFlush);
                    }
                    else
                    {
                        Combination = String.Empty;
                        break;
                    }
                }
            }
            return Combination;
        }
        public string CheckForStraightOrHighCard()
        {
            List<int> cardValues = Deck.GetAllCardValues();

            int[] cardValuesSorted = cardValues.OrderBy(v => v).ToArray();

            var groupOfCardValues = cardValuesSorted.GroupBy(i => i);

            int suitsCount = Deck.GetDeckSuitsCount();

            if (groupOfCardValues.Count() == 5 && suitsCount > 1)
            {
                for (int i = 0; i < cardValuesSorted.Length - 1; i++)
                {
                    if (cardValuesSorted[i] + 1 == cardValuesSorted[i + 1])
                    {
                        Combination = Enum.GetName(typeof(CardCombinationList), CardCombinationList.Straight);
                    }
                    else
                    {
                        Combination = Enum.GetName(typeof(CardCombinationList), CardCombinationList.HighCard);
                        break;
                    }
                }
            }
            return Combination;
        }
        public string CheckForTrioOrTwoPair()
        {
            var cardValues = Deck.GetAllCardValues();

            var groupOfCardValues = cardValues.GroupBy(v => v);

            int groupsCount = groupOfCardValues.Count();

            if (groupsCount == 3)
            {
                Dictionary<int, int> groupDict = new Dictionary<int, int>();

                foreach (var group in groupOfCardValues)
                {
                    groupDict.Add(group.Key, group.Count());
                }

                if (groupDict.Values.Contains(3))
                {
                    Combination = Enum.GetName(typeof(CardCombinationList), CardCombinationList.Trio);
                }
                else if (groupDict.Values.Contains(2))
                {
                    Combination = Enum.GetName(typeof(CardCombinationList), CardCombinationList.TwoPair);
                }
            }
            return Combination;
        }
    }
}
