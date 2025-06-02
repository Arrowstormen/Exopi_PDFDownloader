using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using PDFDownloader.Consoles;

namespace PDFDownloader
{
    public class InputReader : IInputReader
    {
        IConsole Console;
        public InputReader(IConsole console)
        {
            Console = console;
        }

        public string ReadSourceFilePath()
        {
            Console.WriteLine("Please write the file path of the file containing your PDF links:");
            bool pathValid = false;

            while (!pathValid)
            {
                string filePath = Console.ReadLine();

                if (File.Exists(filePath))
                {
                    if (filePath.EndsWith(".csv"))
                    {
                        return filePath;
                    }
                    else
                    {
                        Console.WriteLine("Please provide a csv file");
                    }
                }
                else
                {
                    Console.WriteLine("Please enter a valid, existing file path");
                }
            }

            return null;
        }

        public string ReadOutputFilePath()
        {
            Console.WriteLine("Please write the file path of the folder where the PDFs should be stored");
            bool pathValid = false;

            while (!pathValid)
            {
                string filePath = Console.ReadLine();

                try
                {
                    Directory.CreateDirectory(filePath);
                    return filePath;
                }
                catch (IOException e)
                {
                    Console.WriteLine("Please enter a valid file path");
                }
            }

            return null;
        }

        public bool ReadReset()
        {
            Console.WriteLine("Reset or Skip files already in folder? (Reset/Skip)");
            bool responseValid = false;

            while (!responseValid)
            {
                switch (Console.ReadLine())
                {
                    case "Reset":
                    case "reset":
                    case "RESET":
                    case "r":
                    case "R":
                        return true;
                    case "Skip":
                    case "skip":
                    case "SKIP":
                    case "s":
                    case "S":
                        return false;
                    default:
                        Console.WriteLine("Please enter a valid command (Reset/Skip)");
                        break;
                }
            }

            return false;

        }

    }
}
