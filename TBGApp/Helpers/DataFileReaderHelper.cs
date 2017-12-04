using System.Collections.Generic;
using System.IO;
using TBGApp.Database.Models;

namespace TBGApp.Helpers
{
    public class DataFileReaderHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="basePath"></param>
        /// <returns></returns>
        public static List<Card> ReadCardData(string basePath)
        {
            var cardDataPath = basePath + "TBG_CardData.txt";
            var cardList = new List<Card>();

            if (File.Exists(cardDataPath))
            {
                foreach (string line in File.ReadAllLines(cardDataPath))
                {
                    cardList.Add(new Card(line));
                }
            }

            return cardList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="basePath"></param>
        /// <returns></returns>
        public static List<Question> ReadQuestionData(string basePath)
        {
            var questionDataPath = basePath + "TBG_QuestionData.txt";
            var questionList = new List<Question>();

            if (File.Exists(questionDataPath))
            {
                foreach (string line in File.ReadAllLines(questionDataPath))
                {
                    questionList.Add(new Question(line));
                }
            }

            return questionList;
        }
    }
}