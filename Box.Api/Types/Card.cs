namespace Box.Api.Types
{
    public class Card
    {
        public int IdCard { get; set; }                    // Primary Key (UQ) of the card
        public string Question { get; set; }               // Question-side of card
        public string Answer { get; set; }                 // Answer-side of card
        public Fold Fold { get; set; }                     // Referenced Fold
    }
}