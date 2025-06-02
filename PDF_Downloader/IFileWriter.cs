using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFDownloader
{
    public interface IFileWriter
    {
        public void AddEntry(List<string> entry);

        public void WriteFile();
    }
}
