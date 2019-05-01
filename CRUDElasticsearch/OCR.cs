using IronOcr;
using System;
using System.Collections.Generic;

namespace CRUDElasticsearch
{
    public class OCR
    {
        public OCR()
        {
            
        }
        public List<File> GetFilesInfo(List<string> Files, string folderPath) {
            var Ocr = new AutoOcr();
            int i = 0;
            System.IO.StreamWriter errorLog = new System.IO.StreamWriter("error.txt", true);

            var fileList = new List<File>();
            foreach (var file in Files)
            {
                try
                {
                    File theFile = new File();
                    var Result = Ocr.Read(folderPath + @"\" + file.ToString());

                    theFile.Id = Files.IndexOf(file);
                    theFile.FileName = file.ToString();
                    theFile.OCRData = Result.Text;
                    fileList.Add(theFile);
                }
                catch (Exception ex)
                {
                    errorLog.WriteLine(ex);
                }
                i++;
            }
            return fileList;
        }

    }
}
