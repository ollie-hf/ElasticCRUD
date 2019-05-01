using System;

namespace CRUDElasticsearch
{
    public class File
    {
        int id = 0;
        string _FileName = string.Empty;
        DateTime _Date = System.DateTime.Now;

        public string FileName { get => _FileName; set => _FileName = value; }
        public int Id { get => id; set => id = value; }

        public string OCRData { get; set; }
        public DateTime Date()
        {
            return _Date;
        }
    }
}