using System;
using System.Collections.Generic;

namespace TBGApp.Database.Models
{
    public class Question
    {
        private const int ID_FIELD = 0;
        private const int DESC_FIELD = 1;
        private const int THEME_FIELD = 2;
        private const int DIFFICULTY_FIELD = 3;
        private const int ALTERNATIVE_1_FIELD = 4;
        private const int ALTERNATIVE_2_FIELD = 5;
        private const int ALTERNATIVE_3_FIELD = 6;
        private const int ALTERNATIVE_4_FIELD = 7;
        private const int CORRECT_ALTERNATIVE_FIELD = 8;

        public int Id { get; set; }
        public string Description { get; set; }
        public string Theme { get; set; }
        public string Difficulty { get; set; }
        public List<Alternative> Alternatives { get; set; }

        public Question() { }

        public Question(string fileLine)
        {
            var fields = fileLine.Split(new char[] { ';' });

            Id = int.Parse(fields[ID_FIELD]);
            Description = fields[DESC_FIELD];
            Theme = fields[THEME_FIELD];
            Difficulty = fields[DIFFICULTY_FIELD];
            Alternatives = new List<Alternative>();

            Alternatives.Add(new Alternative(-1, fields[ALTERNATIVE_1_FIELD], 1, int.Parse(fields[CORRECT_ALTERNATIVE_FIELD])));
            Alternatives.Add(new Alternative(-1, fields[ALTERNATIVE_2_FIELD], 2, int.Parse(fields[CORRECT_ALTERNATIVE_FIELD])));
            Alternatives.Add(new Alternative(-1, fields[ALTERNATIVE_3_FIELD], 3, int.Parse(fields[CORRECT_ALTERNATIVE_FIELD])));
            Alternatives.Add(new Alternative(-1, fields[ALTERNATIVE_4_FIELD], 4, int.Parse(fields[CORRECT_ALTERNATIVE_FIELD])));
        }
    }
}