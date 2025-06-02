using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFDownloader.FileOps
{
    public interface IFileOperations
    {
        
    bool CheckFileExists(string filePath);
    
}
}
