using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDElasticsearch
{
    public static class WordGeneratorUtil
    {
        private static string[] words;
        private static string[] fileExtensions = new string[] { "pdf", "tiff", "docx", "jpeg", "doc", "xlsx", "xls", "txt", "rtf", "png", "gif", "html", "htm", "xml", "pptx", "ppt", "odt", "csv" };
        private static Char[] delimiterChars = { ' ', ',', '.', ':', '\t', '\r', '\n' };
        private static Random random = new Random();
        private static readonly int NUMBER_OF_WORDS;
        private static readonly int NUMBER_OF_FILE_EXTENSIONS;
        static WordGeneratorUtil()
        {
            string text = System.IO.File.ReadAllText(@"C:\logs\test.txt", Encoding.UTF8);
            var tempwords = text.Split(delimiterChars);
            List<string> myWords = new List<string>();
            foreach (var word in tempwords)
            {
                if (word == "") continue;
                if (word.Contains('\r')) continue;
                if (word.Contains('\n')) continue;
                myWords.Add(word);
            }

             words = myWords.ToArray();

            NUMBER_OF_WORDS = words.Length;
            NUMBER_OF_FILE_EXTENSIONS = fileExtensions.Length;
        }

        public static string GetName()
        {
            string text = System.IO.File.ReadAllText(@"C:\test\usernames.txt", Encoding.UTF8);
            var tempwords = text.Split(delimiterChars);
            List<string> myNames = new List<string>();

            foreach (var word in tempwords)
            {
                if (word == "") continue;
                if (word.Contains('\r')) continue;
                if (word.Contains('\n')) continue;
                myNames.Add(word);
            }

            Random rand = new Random();

            return myNames[rand.Next(myNames.Count)];
        }

        public static string GetDocType()
        {
            string text = System.IO.File.ReadAllText(@"C:\test\doctype1.txt", Encoding.UTF8);
            var tempwords = text.Split(delimiterChars);
            List<string> myNames = new List<string>();

            foreach (var word in tempwords)
            {
                if (word == "") continue;
                if (word.Contains('\r')) continue;
                if (word.Contains('\n')) continue;
                myNames.Add(word);
            }

            Random rand = new Random();

            return myNames[rand.Next(myNames.Count)];
        }


        public static string GetRandomWord()
        {
            int rand;
            lock (random)
            {
                rand = random.Next(NUMBER_OF_WORDS);
            }
            return words[rand];
        }


        public static int GetNumberOfWordsPerPage()
        {
            int result = 300;
            lock (random)
            {
                result += random.Next(200);
            }
            return result;
        }

        public static string GetSentence()
        {
            StringBuilder sb = new StringBuilder();

            int sentancelength = getWordsInSentence();

            for (int i = 0; i < sentancelength; i++)
            {
                sb.Append(GetRandomWord());

                if (i == sentancelength)
                {
                    sb.Append('.');
                    sentancelength += getWordsInSentence();
                }
                else
                {
                    sb.Append(' ');
                }
            }

            return sb.ToString();
        }

        public static string GetPage()
        {
            StringBuilder sb = new StringBuilder();
            int wordsPerPage = GetNumberOfWordsPerPage();
            int sentancelength = getWordsInSentence();
            for (int i = 0; i < wordsPerPage; i++)
            {
                sb.Append(GetRandomWord());
                if (i != wordsPerPage - 1)
                {
                    if (i == sentancelength)
                    {
                        sb.Append('.');
                        sentancelength += getWordsInSentence();
                    }
                    else
                    {
                        sb.Append(' ');
                    }
                }
            }
            return sb.ToString();
        }

        public static int GetSentence(in StringBuilder sb)
        {
            int numberOfWords = getWordsInSentence();
            for (int j = 0; j < numberOfWords; j++)
            {
                sb.Append(WordGeneratorUtil.GetRandomWord());

                if (j == numberOfWords)
                {
                    sb.Append(".");
                }
            }
            return numberOfWords;
        }


        public static int getWordsInSentence()
        {
            int rand;
            int numberOfWords;
            lock (random)
            {
                rand = random.Next(5440);
            }
            if (rand < 100)
            {
                numberOfWords = 2;
            }
            else if (rand < 240)
            {
                numberOfWords = 3;
            }
            else if (rand < 530)
            {
                numberOfWords = 4;
            }
            else if (rand < 900)
            {
                numberOfWords = 5;
            }
            else if (rand < 1390)
            {
                numberOfWords = 6;
            }
            else if (rand < 1920)
            {
                numberOfWords = 7;
            }
            else if (rand < 2450)
            {
                numberOfWords = 8;
            }
            else if (rand < 2970)
            {
                numberOfWords = 9;
            }
            else if (rand < 3450)
            {
                numberOfWords = 10;
            }
            else if (rand < 3870)
            {
                numberOfWords = 11;
            }
            else if (rand < 4250)
            {
                numberOfWords = 12;
            }
            else if (rand < 4550)
            {
                numberOfWords = 13;
            }
            else if (rand < 4800)
            {
                numberOfWords = 14;
            }
            else if (rand < 5000)
            {
                numberOfWords = 15;
            }
            else if (rand < 5150)
            {
                numberOfWords = 16;
            }
            else if (rand < 5260)
            {
                numberOfWords = 17;
            }
            else if (rand < 5330)
            {
                numberOfWords = 18;
            }
            else if (rand < 5380)
            {
                numberOfWords = 19;
            }
            else if (rand < 5410)
            {
                numberOfWords = 20;
            }
            else if (rand < 5430)
            {
                numberOfWords = 21;
            }
            else
            {
                // else 5440 so chance of 10
                numberOfWords = 22;
            }

            return numberOfWords;
        }

        public static int GetNumberOfPagesInDocument()
        {
            int lucky;
            lock (random)
            {
                lucky = random.Next(1000);
            }

            if (lucky < 500)
            {
                return 1;
            }
            else if (lucky < 750)
            {
                return 2;
            }
            else if (lucky < 950)
            {
                return 10;
            }
            else if (lucky < 999)
            {
                return 50;
            }
            else
            {
                return 2000;
            }
        }

        public static string GetFileName()
        {
            int rand;
            int numberOfWords;
            lock (random)
            {
                rand = random.Next(100);
            }
            if (rand < 50)
            {
                numberOfWords = 1;
            }
            else if (rand < 80)
            {
                numberOfWords = 2;
            }
            else
            {
                numberOfWords = 3;
            }

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < numberOfWords; i++)
            {
                sb.Append(GetRandomWord());
                if (i < numberOfWords - 1) sb.Append(" ");
            }
            sb.Append(".");
            lock (random)
            {
                rand = random.Next(NUMBER_OF_FILE_EXTENSIONS);
            }
            sb.Append(fileExtensions[rand]);
            return sb.ToString();
        }
    }
}
