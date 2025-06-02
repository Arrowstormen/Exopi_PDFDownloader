// See https://aka.ms/new-console-template for more information
using System.Diagnostics;
using System.Runtime.CompilerServices;
using PDFDownloader;
using PDFDownloader.Consoles;

HttpClient client = new HttpClient();
client.Timeout = TimeSpan.FromSeconds(30);
StandardConsole console = new StandardConsole();
InputReader inputReader = new InputReader(console);
console.WriteLine("Welcome to the PDF Downloader Console App!\n");


var filePath = inputReader.ReadSourceFilePath();
var outputFolder = inputReader.ReadOutputFilePath();
var toReset = inputReader.ReadReset();

Downloader downloader = new Downloader(client, outputFolder);
downloader.Reset = toReset;
FileWriter fileWriter = new FileWriter(outputFolder);

FileReader fileReader = new FileReader(downloader, fileWriter);
var watch = Stopwatch.StartNew();
await fileReader.ReadCSVFile(filePath);
watch.Stop();
console.WriteLine(watch.Elapsed.ToString());
fileWriter.WriteFile();