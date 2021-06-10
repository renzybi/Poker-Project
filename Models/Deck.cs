using PokerProject.Data;
using System.Collections.Generic;
using System.Linq;

namespace PokerProject.Models
{
    public class Deck
    {
        public int Id { get; set; }
        public List<Card> Cards { get; set; }

        public List<int> GetAllCardValues() 
        {
            return Cards.Select(c => c.Value).ToList();
        }
        public List<string> GetAllCardSuits()
        {
            return Cards.Select(c => c.Suite).ToList();
        }
        public int GetDeckSuitsCount() 
        {
            return Cards.Select(c => c.Suite).GroupBy(s => s).Count();
        }
        public int GetHighestCardOfDeck() 
        {
            return GetAllCardValues().Max();
        }
        public string GetSuitOfHighestCardOfDeck()
        {
            var highestCardOfDeck = GetAllCardValues().Max();
            return Cards.Where(c => c.Value == highestCardOfDeck).FirstOrDefault().Suite;
        }
        //gets the value of group of cards (two-pair, trio, four-of-a-kind which is represented by "groupsOf")
        public int GetValueOfSpecificGroupOfCards(int groupsOf)
        {
            var groupsOfCardValues = GetAllCardValues().GroupBy(v => v).ToList();

            var groupsOfSpecificCardValues = groupsOfCardValues.Where(g => g.Count() == groupsOf).Select(g => g.Key).ToList();

            if (groupsOfSpecificCardValues.Count() > 1)
            {
                return groupsOfSpecificCardValues.Max();
            }
            else
            {
                return groupsOfSpecificCardValues.FirstOrDefault();
            }
        }
    }
}
