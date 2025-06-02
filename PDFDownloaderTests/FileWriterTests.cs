using PDFDownloader;
using PDFDownloader.Consoles;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFDownloaderTests
{
    [TestClass]
    public sealed class FileWriterTests
    {
        [TestMethod]
        public void AddEntry_Adds_StringList_As_Entry_To_Entries()
        {
            //Arrange
            var fileWriter = new FileWriter("TestFiles/");
            List<string> stringEntry = new List<string>
            {
                "22",
                "http://link.com/Page/File.pdf",
                "http://link.com/Page/File.pdf"
            };

            var entry = new Entry
            {
                Id = "22",
                Link1 = "http://link.com/Page/File.pdf",
                Link2 = "http://link.com/Page/File.pdf"
            }; 

            //Act
            fileWriter.AddEntry(stringEntry);

            //Assert
            Assert.AreEqual(entry.Id, fileWriter.entries[0].Id, "AddEntry did not turn stringList into Entry.Id as expected");
            Assert.AreEqual(entry.Link1, fileWriter.entries[0].Link1, "AddEntry did not turn stringList into Entry.Link1 as expected");
            Assert.AreEqual(entry.Link2, fileWriter.entries[0].Link2, "AddEntry did not turn stringList into Entry.Link2 as expected");
        }

        [TestMethod]
        public void AddEntry_Adds_Expected_Entry_When_Given_Empty_StringList()
        {
            //Arrange
            var fileWriter = new FileWriter("TestFiles/");
            List<string> stringEntry = new List<string>
            {
                "None",
                "",
                ""
            };

            var entry = new Entry
            {
                Id = "None",
                Link1 = "",
                Link2 = ""
            };

            //Act
            fileWriter.AddEntry(stringEntry);

            //Assert
            Assert.AreEqual(entry.Id, fileWriter.entries[0].Id, "AddEntry did not turn stringList into Entry.Id as expected");
            Assert.AreEqual(entry.Link1, fileWriter.entries[0].Link1, "AddEntry did not turn stringList into Entry.Link1 as expected");
            Assert.AreEqual(entry.Link2, fileWriter.entries[0].Link2, "AddEntry did not turn stringList into Entry.Link2 as expected");
        }

        [TestMethod]
        public void AddEntry_Adds_Expected_Entry_When_Given_Incorrect_Amount_of_Fields()
        {
            //Arrange
            var fileWriter = new FileWriter("TestFiles/");
            List<string> stringEntry = new List<string>
            {
                "31WHOOPS",
                "Chi",
                "Cha",
                "Cho"
            };

            var entry = new Entry
            {
                Id = "31WHOOPS",
                Link1 = "Error: Incorrect amount of fields given",
                Link2 = ""
            };

            //Act
            fileWriter.AddEntry(stringEntry);

            //Assert
            Assert.AreEqual(entry.Id, fileWriter.entries[0].Id, "AddEntry did not turn stringList into Entry.Id as expected");
            Assert.AreEqual(entry.Link1, fileWriter.entries[0].Link1, "AddEntry did not turn stringList into Entry.Link1 as expected");
            Assert.AreEqual(entry.Link2, fileWriter.entries[0].Link2, "AddEntry did not turn stringList into Entry.Link2 as expected");
        }

    }
}
