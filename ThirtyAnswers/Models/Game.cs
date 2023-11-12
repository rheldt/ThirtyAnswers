namespace ThirtyAnswers.Models
{
    public class Game
    {
        public Category Category1 { get; set; }

        public Category Category2 { get; set; }

        public Category Category3 { get; set; }

        public Category Category4 { get; set; }

        public Category Category5 { get; set; }

        public Category Category6 { get; set; }

        // The properties below are not part of the JSON game file

        public Player Player1 { get; set; }

        public Player Player2 { get; set; }

        public Player Player3 { get; set; }

        public bool UseDoubleValues { get; set; }
    }
}