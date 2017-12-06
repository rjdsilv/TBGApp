namespace TBGApp.Database.Models
{
    public class Card
    {
        private const int ID_FIELD = 0;
        private const int NAME_FIELD = 1;
        private const int THEME_FIELD = 2;
        private const int DIFFICULTY_FIELD = 3;

        public int Id { get; set;  }
        public string Name { get; set; }
        public string Theme { get; set; }
        public string Difficulty { get; set; }

        public Card()
        {

        }

        public Card(string fileLine)
        {
            var fields = fileLine.Split(new char[] { ';' });

            Id = int.Parse(fields[ID_FIELD]);
            Name = fields[NAME_FIELD];
            Theme = fields[THEME_FIELD];
            Difficulty = fields[DIFFICULTY_FIELD];
        }
    }
}