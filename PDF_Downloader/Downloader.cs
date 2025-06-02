using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PDFDownloader
{
    public class Downloader : IDownloader
    {
        HttpClient _client;
        string _outputFolder;
        public bool Reset { get; set; }


        public Downloader(HttpClient client, string outputFolder)
        {
            this._client = client;
            this._outputFolder = outputFolder;
        }

        public async Task<string> DownloadFileAsync(string url, string id)
        {
            if (url == "")
            {
                return "No link";
            }

            string fileName = Path.GetFileName(url);
            fileName = id + "_" + fileName;
            string filePath = Path.Combine(_outputFolder, fileName);

            if (Reset || !File.Exists(filePath))
            {
                try
                {
                    await using var stream = await _client.GetStreamAsync(url);

                    byte[] buffer = new byte[4];
                    var bufferLength = await stream.ReadAsync(buffer, 0, buffer.Length); 
                    if (!(bufferLength == 4 && buffer[0] == 0x25 && buffer[1] == 0x50 && buffer[2] == 0x44 && buffer[3] == 0x46))
                    {
                        //Probably not a PDF (hopefully)
                        return "Not a link to a PDF file.";
                    }

                    await using var fileStream = new FileStream(filePath, FileMode.Create);
                    await fileStream.WriteAsync(buffer);
                    await stream.CopyToAsync(fileStream);
                    return "Success";
                }
                catch (Exception e) {
                    if (e is InvalidOperationException)
                    {
                        return e.Message;
                    }
                    else if (e is HttpRequestException)
                    {
                        return e.Message;
                    }
                    else if (e is TaskCanceledException)
                    {
                        return "Timed out";
                    }
                    else if (e is HttpIOException)
                    {
                        return e.Message;
                    }
                    else
                    {
                        //Console.WriteLine(e);
                        return "Failure: " + e.Message;
                    }
                }
            }

            return "Success";
        }
    }
}
