using PDFDownloader;
using PDFDownloader.Consoles;
using Moq;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Numerics;

namespace PDFDownloaderTests
{
    [TestClass]
    public sealed class TestConsole : IConsole
    {
        public List<string> output = new List<string>();
        IConsole Console { get; set; }

        public TestConsole(IConsole readConsole)
        {
            Console = readConsole;
        }
        public string ReadLine()
        {
           return Console.ReadLine();
        }

        public void Write(string message)
        {
            output.Add(message);
        }

        public void WriteLine(string message)
        {
            output.Add(message);
        }
    }
    [TestClass]
    public sealed class InputReaderTests
    {
        [TestMethod]
        public void ReadSourceFilePath_Returns_Existing_CSV_File()
        {
            //Arrange
            var mockConsole = new Mock<IConsole>();
            InputReader inputReader = new InputReader(mockConsole.Object);
            var input = "TestFiles/Test.csv";
            mockConsole.Setup(m => m.ReadLine()).Returns(input);

            //Act
            var res = inputReader.ReadSourceFilePath();

            //Assert
            Assert.AreEqual(input, res, "The file given as input was not returned");
        }

        [TestMethod]
        public void ReadSourceFilePath_Returns_Expected_Prompt_When_Given_NoneCSV_File()
        {
            //Arrange
            var mockConsole = new Mock<IConsole>();
            var testConsole = new TestConsole(mockConsole.Object);
            InputReader inputReader = new InputReader(testConsole);
            var input = "TestFiles/Test.txt";
            var input2 = "TestFiles/Test.csv";
            mockConsole.SetupSequence(m => m.ReadLine()).Returns(input).Returns(input2);
           
            //Act
            inputReader.ReadSourceFilePath();

            //Assert
            var res = testConsole.output[1];
            Assert.AreEqual("Please provide a csv file", res, "The expected output when given non-CSV file was not returned");
        }

        [TestMethod]
        public void ReadSourceFilePath_Returns_Expected_Prompt_When_Given_Nonexisting_File()
        {
            //Arrange
            var mockConsole = new Mock<IConsole>();
            var testConsole = new TestConsole(mockConsole.Object);
            InputReader inputReader = new InputReader(testConsole);
            var input = "qwewqewqe/qwewe";
            var input2 = "TestFiles/Test.csv";
            mockConsole.SetupSequence(m => m.ReadLine()).Returns(input).Returns(input2);

            //Act
            inputReader.ReadSourceFilePath();

            //Assert
            var res = testConsole.output[1];
            Assert.AreEqual("Please enter a valid, existing file path", res, "The expected output when given nonexisting file was not returned");
        }

        [TestMethod]
        public void ReadOutputFilePath_Returns_Valid_Directory()
        {
            //Arrange
            var mockConsole = new Mock<IConsole>();
            InputReader inputReader = new InputReader(mockConsole.Object);
            var input = "TestFiles";
            mockConsole.Setup(m => m.ReadLine()).Returns(input);

            //Act
            var res = inputReader.ReadOutputFilePath();

            //Assert
            Assert.AreEqual(input, res, "The folder given as input was not returned");
        }

        [TestMethod]
        public void ReadOutputFilePath_Returns_Expected_Prompt_When_Given_Invalid_Directory()
        {
            //Arrange
            var mockConsole = new Mock<IConsole>();
            var testConsole = new TestConsole(mockConsole.Object);
            InputReader inputReader = new InputReader(testConsole);
            var input = "Qa://qwewqewqeqwewe";
            var input2 = "TestFiles";
            mockConsole.SetupSequence(m => m.ReadLine()).Returns(input).Returns(input2);

            //Act
            inputReader.ReadOutputFilePath();

            //Assert
            var res = testConsole.output[1];
            Assert.AreEqual("Please enter a valid file path", res, "The expected output when given invalid folder was not returned");
        }

        [TestMethod]
        public void ReadReset_Returns_True_When_Given_Reset_Input()
        {
            //Arrange
            var mockConsole = new Mock<IConsole>();
            var testConsole = new TestConsole(mockConsole.Object);
            InputReader inputReader = new InputReader(testConsole);
            var input = "Reset";
            mockConsole.Setup(m => m.ReadLine()).Returns(input);

            //Act
            var res = inputReader.ReadReset();

            //Assert
            Assert.AreEqual(true, res, "True was not returned when given Reset as input");
        }

        [TestMethod]
        public void ReadReset_Returns_False_When_Given_Skip_Input()
        {
            //Arrange
            var mockConsole = new Mock<IConsole>();
            var testConsole = new TestConsole(mockConsole.Object);
            InputReader inputReader = new InputReader(testConsole);
            var input = "Skip";
            mockConsole.Setup(m => m.ReadLine()).Returns(input);

            //Act
            var res = inputReader.ReadReset();

            //Assert
            Assert.AreEqual(false, res, "False was not returned when given Skip as input");
        }

        [TestMethod]
        public void ReadReset_Returns_Expected_Output_When_Given_Invalid_Input()
        {
            //Arrange
            var mockConsole = new Mock<IConsole>();
            var testConsole = new TestConsole(mockConsole.Object);
            InputReader inputReader = new InputReader(testConsole);
            var input = "blahblah";
            var input2 = "Reset";
            mockConsole.SetupSequence(m => m.ReadLine()).Returns(input).Returns(input2);

            //Act
            inputReader.ReadReset();

            //Assert
            var res = testConsole.output[1];
            Assert.AreEqual("Please enter a valid command (Reset/Skip)", res, "Expected output was not returned when given invalid input");
        }
    }
}
