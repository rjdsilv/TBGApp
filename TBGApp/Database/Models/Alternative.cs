﻿namespace TBGApp.Database.Models
{
    public class Alternative
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public bool IsCorrect { get; set; }

        public Alternative(int id, string description, int order, int orderCorrect)
        {
            Id = id;
            Description = description;
            Order = order;
            IsCorrect = order == orderCorrect;
        }

        public Alternative(int id, string description, int order, bool isCorrect)
        {
            Id = id;
            Description = description;
            Order = order;
            IsCorrect = isCorrect;
        }
    }
}