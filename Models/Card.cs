namespace PokerProject.Models
{
    public class Card
    {
        public int Id { get; set; }
        public string Suite { get; set; }
        public int Value { get; set; }

        public string Code 
        {
            get => Value.ToString() + "-" + Suite; 
        }
    }
}
