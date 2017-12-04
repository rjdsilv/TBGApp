using System.Collections.Generic;
using System.IO;
using TBGApp.Database.Models;

namespace TBGApp.Helpers
{
    public class DataFileReaderHelper
    {
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
    }
}