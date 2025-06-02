using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;

namespace PDFDownloader
{
    public class FileWriter : IFileWriter
    {
        public List<Entry> entries;
        string _outputFolder;

        public FileWriter(string outputFolder) 
        {
            this._outputFolder = outputFolder;
            entries = new List<Entry>();
        }

        public void AddEntry(List<string> entry)
        {
            if (entry.Count == 3)
            {
                Entry newEntry = new Entry
                {
                    Id = entry[0],
                    Link1 = entry[1],
                    Link2 = entry[2]
                };
                entries.Add(newEntry);
            }
            else if (entry.Count == 0)
            {
                Entry newEntry = new Entry
                {
                    Id = "None",
                    Link1 = "",
                    Link2 = ""
                };
                entries.Add(newEntry);
            }
            else
            {
                Entry newEntry = new Entry
                {
                    Id = entry[0],
                    Link1 = "Error: Incorrect amount of fields given",
                    Link2 = ""
                };
                entries.Add(newEntry);
            }
            {

            }
           
        }

        public void WriteFile()
        {
            using (var writer = new StreamWriter(_outputFolder + "_PDF_Downloader_Results.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(entries);
            }
        }
    }
}
