using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDElasticsearch
{
    public class FileController
    {
        List<string> Files = new List<string>();
        string folderPath;

        public FileController()
        {

        }
        public void SetLocationFolder(string pathToFolder)
        {
            folderPath = pathToFolder;
        }
        public string GetLocationFolder()
        {
            return folderPath;
        }
        public void ScanFolderForFiles()
        {
            DirectoryInfo d = new DirectoryInfo(folderPath);

            foreach (var file in d.GetFiles())
            {
                Files.Add(file.Name);
            }
        }
        public List<string> GetFilesInFolder()
        {
            return Files;
        }
    }
}
