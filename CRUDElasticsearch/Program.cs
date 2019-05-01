using Nest;
using System;
using System.Collections.Generic;

namespace CRUDElasticsearch
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        ///// </summary>
        //[STAThread]
        //static void Main()
        //{
        //    Stopwatch stopwatch = new Stopwatch();
        //    int noOfDocs = 5000000;
        //    stopwatch.Start();
        //    ParallelOptions options = new ParallelOptions { MaxDegreeOfParallelism = 16 };
        //    Parallel.For(5000000, noOfDocs+ 5000000, options, i =>
        //    {
        //        var document = new Document(true);
        //        CRUD.insertDocument(i, document);
        //    });
        //    stopwatch.Stop();
        //    //Application.EnableVisualStyles();
        //    //Application.SetCompatibleTextRenderingDefault(false);
        //    //Application.Run(new Form1());
        //}






        [STAThread]
        private static void Main()
        {
            //string test = WordGeneratorUtil.GetPage();

            /*
            FuzzySearchModel fs = new FuzzySearchModel();

            fs.documentName = "Freds.txt";
            fs.upload_date_time = System.DateTime.Now;
            fs.upload_user = "Pranita";

            CRUD.insertDocument(1, fs);

            FuzzySearchModel fs1 = new FuzzySearchModel();

            fs.documentName = "Friends.text";
            fs.upload_date_time = System.DateTime.Now;
            fs.upload_user = "Praveen";

            CRUD.insertDocument(2, fs1);

            FuzzySearchModel fs2 = new FuzzySearchModel();

            fs.documentName = "Fried.txt";
            fs.upload_date_time = System.DateTime.Now;
            fs.upload_user = "Pratheeba";

            CRUD.insertDocument(3, fs2);

            FuzzySearchModel fs3 = new FuzzySearchModel();

            fs.documentName = "phillip.txt";
            fs.upload_date_time = System.DateTime.Now;
            fs.upload_user = "Prashanth";

            CRUD.insertDocument(4, fs3); bool  
            */
            
            int files = 100;

            Random rand;
            var document = new DocumentModel();
;

            for (int i = 0; i < files; i++)
            {
                rand = new Random();
                string docType = WordGeneratorUtil.GetDocType();

                document = new DocumentModel
                {
                    Metadata = new List<Metadata>() { },

                    Comments = new List<Comments>() { },
                };

                // 5 System Metadatas
                //document.Metadata.Add(new Metadata { Key = "sys_filename", Value = WordGeneratorUtil.GetFileName()});
                document.Filename = WordGeneratorUtil.GetFileName();

                if (rand.Next(1) == 1)
                {
                    //document.MetadataKeys.Add(new MetadataKey { Key = "sys_AccesControl", Value = "true" });
                    document.AccessControl = true;
                }
                else
                {
                    //document.MetadataKeys.Add(new MetadataKey { Key = "sys_AccesControl", Value = "false" });
                    document.AccessControl = false;
                }

                //document.MetadataKeys.Add(new MetadataKey { Key = "sys_FolderId", Value = "" + rand.Next(7) });
                document.FolderID = rand.Next(7);
                //document.MetadataKeys.Add(new MetadataKey { Key = "sys_upload_user", Value = WordGeneratorUtil.GetName().Replace(" ", "") });
                document.uploadUser = WordGeneratorUtil.GetName().Replace(" ", "");
                //document.MetadataKeys.Add(new MetadataKey { Key = "sys_upload_date", Value = System.DateTime.Now.ToString() });
                document.UploadDate = System.DateTime.Now;
                //document.MetadataKeys.Add(new MetadataKey { Key = "sys_doc_type", Value = docType });
                document.DocumentType = docType;


                // 1-4 Comments
                for (int f = 0; f < rand.Next(4); f++)
                {
                    document.Comments.Add(new Comments { User = WordGeneratorUtil.GetName().Replace(" ", ""), CreateDate = getRandomDate(), Comment = WordGeneratorUtil.GetSentence() });
                }

                // 4 User Metadatas
                switch (docType)
                {
                    case "Passport":
                        string ppname = WordGeneratorUtil.GetName();
                        if(ppname.Length - 3 > 0)
                        document.Metadata.Add(new Metadata { Key = "def_passport_no", Value = ppname.Substring(ppname.Length-3) + rand.Next(6) });

                        document.Metadata.Add(new Metadata { Key = "def_date_of_birth", Value = "" + getRandomDate() });
                        break;
                    case "Invoice":
                        document.Metadata.Add(new Metadata { Key = "def_account_no", Value = "" + rand.Next(9999999)});
                        document.Metadata.Add(new Metadata { Key = "def_invoice_no", Value = "" + rand.Next(9999999) });
                        break;
                    case "Contract":
                        string name = WordGeneratorUtil.GetName();

                        if (rand.Next(1) == 1)
                        {
                            document.Metadata.Add(new Metadata { Key = "def_employee_Status", Value = "employed" });
                        }
                        else
                        {
                            document.Metadata.Add(new Metadata { Key = "def_employee_Status", Value = "un-employed" });
                        }

                        document.Metadata.Add(new Metadata { Key = "def_anual_income", Value = "" + rand.Next(100000) });
                        if (name.IndexOf(" ") > 0)
                        document.Metadata.Add(new Metadata { Key = "def_employer_name", Value = name.Substring(name.IndexOf(" ")) });

                        document.Metadata.Add(new Metadata { Key = "def_employee_number", Value = "" + rand.Next(9999999) });
                        break;
                }


                // 1-5 OCR Text - To be added later...


                CRUD.insertDocument(i, document);

            }
            //{
            //    var document2 = new DocumentModel
            //    {
            //        DocumentName = "b.txt",
            //        Id = 2,
            //        FolderId = 2,
            //        MetadataKeys = new Dictionary<string, string>(),
            //        DocumentUploadDate = DateTime.Now,
            //        UploadedUser = "user user"
            //    };
            //    document2.MetadataKeys.Add("content-type", "application/pdf");
            //    document2.MetadataKeys.Add("invoicenumber", "1214");
            //    document2.MetadataKeys.Add("AccesControl", "true");
            //    CRUD.insertDocument(2, document2);
            //}
            //{
            //    var document2 = new DocumentModel
            //    {
            //        DocumentName = "c.txt",
            //        Id = 3,
            //        FolderId = 3,
            //        MetadataKeys = new Dictionary<string, string>(),
            //        DocumentUploadDate = DateTime.Now,
            //        UploadedUser = "user user"
            //    };
            //    document2.MetadataKeys.Add("content-type", "application/pdf");
            //    document2.MetadataKeys.Add("invoicenumber", "1214");
            //    document2.MetadataKeys.Add("AccesControl", "false");
            //    CRUD.insertDocument(3, document2);
            //}
            //{
            //    var id = 4;
            //    var document2 = new DocumentModel
            //    {
            //        DocumentName = "j.txt",
            //        Id = id,
            //        FolderId = id,
            //        MetadataKeys = new Dictionary<string, string>(),
            //        DocumentUploadDate = DateTime.Now,
            //        UploadedUser = "user user"
            //    };
            //    document2.MetadataKeys.Add("content-type", "application/pdf");
            //    document2.MetadataKeys.Add("employeenumber", "1214");
            //    document2.MetadataKeys.Add("AccesControl", "true");
            //    CRUD.insertDocument(id, document2);
            //}


        }
        private static DateTime getRandomDate()
        {
            Random myRandom = new Random();
            var year = 1980 + myRandom.Next(19);
            var month = 1 + myRandom.Next(12);
            var day = 1 + myRandom.Next(28);
            return new DateTime(year, month, day);
        }

    }
}
