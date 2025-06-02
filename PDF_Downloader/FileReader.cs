using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.FileIO;

namespace PDFDownloader
{
    public class FileReader : IFileReader
    {
        IDownloader _downloader;
        IFileWriter _writer;

        public FileReader(IDownloader downloader, IFileWriter fileWriter)
        {
            _downloader = downloader;
            _writer = fileWriter;
        }

        public async Task HandleRow(string[] row)
        {
                List<string> entry = new List<string>();
                int i = 0;
                string id = "";
            foreach (string col in row)
            {
                if (i == 0)
                {
                    id = row[0];
                    entry.Add(id);
                }
                else
                {
                    if (!(col == "") && !entry.Contains("Success"))
                        {
                            var res = await _downloader.DownloadFileAsync(col, id);
                            //Console.WriteLine(id + ":" + res);
                            entry.Add(res.ToString());
                        }
                    else
                    {
                        entry.Add(String.Empty);
                    }
                    }

                    i++;
                }

            _writer.AddEntry(entry);
        }

        public async Task ReadCSVFile(string filename) 
        {
            using (TextFieldParser parser = new TextFieldParser(filename))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                List<Task> tasks = new List<Task>();
                while (!parser.EndOfData)
                {
                    //Processing row
                    string[] currentRow = parser.ReadFields();
                    if (!(currentRow[0] == "id"))
                    {
                        var task = HandleRow(currentRow);
                        tasks.Add(task);
                    }
                }
               
                await Task.WhenAll(tasks.ToArray());
            }
        }

    }
}
