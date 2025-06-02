using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFDownloader.FileOps
{
    public class FileOperations : IFileOperations
    {
        public bool CheckFileExists(string filePath)
        {
            return File.Exists(filePath);
        }
    }
}
