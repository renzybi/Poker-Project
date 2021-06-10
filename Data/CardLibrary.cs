using System.Collections.Generic;

namespace PokerProject.Data
{
    public class CardLibrary
    {
        public Dictionary<string, int> CardCombinationIndex = new Dictionary<string, int> { };
        public Dictionary<string, int> CardSuitIndex = new Dictionary<string, int> { };
        public CardLibrary()
        {
            //CardLibrary1
            CardCombinationIndex.Add("StraightFlush", 9);
            CardCombinationIndex.Add("FourOfAKind", 8);
            CardCombinationIndex.Add("FullHouse", 7);
            CardCombinationIndex.Add("Flushes", 6);
            CardCombinationIndex.Add("Straight", 5);
            CardCombinationIndex.Add("Trio", 4);
            CardCombinationIndex.Add("TwoPair", 3);
            CardCombinationIndex.Add("OnePair", 2);
            CardCombinationIndex.Add("HighCard", 1);

            //CardLibrary2
            CardSuitIndex.Add("Diamond", 4);
            CardSuitIndex.Add("Heart", 3);
            CardSuitIndex.Add("Spade", 2);
            CardSuitIndex.Add("Clover", 1);
        }
        public enum CardCombinationList
        {
            StraightFlush,
            FourOfAKind,
            FullHouse,
            Flushes,
            Straight,
            Trio,
            TwoPair,
            OnePair,
            HighCard
        }
        
        
    }
}
