using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDElasticsearch
{
    public class Document
    {
        public DateTime UploadDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string FileName { get; set; }
        public int Folder { get; set; }
        public string DocumentType { get; set; }
        public int AccessControl { get; set; }
        public int NumberOfPages { get; set; }

        public List<string> pages { get; set; }


        private readonly String[] _docTypes = new String[] { "Email", "Agreement", "BusinessReport", "Policy", "BusinessLetter", "Contract", "Invoice", "Receipt" };

        private Random myRandom = new Random();

        public Document()
        {

        }

        public Document(bool generateTestData)
        {
            pages = new List<string>();
            if (!generateTestData) return;
            // set document key to a Guid
            // get 2 dates and set the earlier one to the upload date
            var firstDate = getRandomDate();
            var secondDate = getRandomDate();
            if (firstDate < secondDate)
            {
                UploadDate = firstDate;
                LastModifiedDate = secondDate;
            }
            else
            {
                UploadDate = secondDate;
                LastModifiedDate = firstDate;
            }
            // set the filname 
            FileName = CleanString(WordGeneratorUtil.GetFileName());

            // set the folder to a number between 2 and 102
            Folder = 2 + myRandom.Next(100);
            AccessControl = GetAccessControlType();
            // set the doctype
            DocumentType = getRandomDocType();

            NumberOfPages = WordGeneratorUtil.GetNumberOfPagesInDocument();
            // Generate OCR Data for Document
            for (int i = 0; i < NumberOfPages; i++)
            {
                pages.Add(WordGeneratorUtil.GetPage());
            }
        }

        public int GetAccessControlType()
        {
            Random rand = new Random();
            int accessControll;
            int random;

            lock (rand)
            {
                accessControll = rand.Next(3) + 1;
            }

            return accessControll;
        }

        // returns a random doc type from the _docTypes list
        private string getRandomDocType()
        {
            var rand = myRandom.Next(_docTypes.Length);
            return _docTypes[rand];
        }


        // returns a date between 2000 and 2018 but will not produce a day after 28th of the month
        private DateTime getRandomDate()
        {
            var year = 2000 + myRandom.Next(19);
            var month = 1 + myRandom.Next(12);
            var day = 1 + myRandom.Next(28);
            return new DateTime(year, month, day);
        }

        public string CleanString(string oldString)
        {
            return oldString.Replace(" ", string.Empty);
        }

        public string ReadDocument()
        {
            return this.FileName + this.DocumentType + this.Folder + this.LastModifiedDate + this.UploadDate;
        }
    }
}
