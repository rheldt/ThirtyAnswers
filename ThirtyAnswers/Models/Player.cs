namespace ThirtyAnswers.Models
{
    public class Player
    {
        public string Name { get; set; }

        public int Score { get; set; }

        /// <summary>
        /// Initialization constructor.
        /// </summary>
        /// <param name="name"></param>
        public Player(string name)
        {
            this.Name = name;
            this.Score = 0;
        }
    }
}