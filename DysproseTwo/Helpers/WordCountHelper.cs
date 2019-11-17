using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DysproseTwo.Helpers
{
    public static class WordCountHelper
    {
        public static int CalculateWordCount(string words)
        {
            int numOfWords = GetWordCountFromString(words);
            return numOfWords;
        }


        private static int GetWordCountFromString(string words)
        {
            string textToUse = words.Replace("\r", " ");
            string[] listOfWords = textToUse.Split(' ');
            int wordCount = listOfWords.Length;
            int emptyWords = 0;
            for (uint i = 0; i < wordCount; i++)
            {
                if (string.IsNullOrEmpty(listOfWords[i]))
                {
                    emptyWords += 1;
                }
            }
            wordCount -= emptyWords;
            return wordCount;
        }
    }
}
