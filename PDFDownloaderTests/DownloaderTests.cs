using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using PDFDownloader;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;
using static System.Net.Mime.MediaTypeNames;

namespace PDFDownloaderTests
{
    [TestClass]
    public sealed class DownloaderTests
    {
        [TestMethod]
        public async Task DownloadFileAsync_Returns_Expected_Message_When_Empty_URL()
        {
            //Arrange
            var mockMessageHandler = new Mock<HttpMessageHandler>();
            var outFolder = "TestFiles";
            var downloader = new Downloader(new HttpClient(mockMessageHandler.Object), outFolder);


            //Act
            var res = await downloader.DownloadFileAsync("", "1");

            //Assert
            var expected = "No link";
            Assert.AreEqual(expected, res, "DownloadFileAsync did not return expected string when given empty URL");
        }

        [TestMethod]
        public async Task DownloadFileAsync_Returns_Success_When_Given_Existing_File_And_Skip()
        {
            //Arrange
            var mockMessageHandler = new Mock<HttpMessageHandler>();
            // Set up the SendAsync method behavior.
            mockMessageHandler
                .Protected() // <= here is the trick to set up protected!!!
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage());
            // create the HttpClient

            var outFolder = "C:\\Users\\Mikkel\\source\\repos\\PDF_Downloader\\PDFDownloaderTests\\TestFiles\\";
            var downloader = new Downloader(new HttpClient(mockMessageHandler.Object), outFolder);
            downloader.Reset = false;

            

            //Act
            var res = await downloader.DownloadFileAsync("https://www.fakesite.com/Test.pdf", "1");

            //Assert
            var fileName = "C:\\Users\\Mikkel\\source\\repos\\PDF_Downloader\\PDFDownloaderTests\\TestFiles\\1_Test.pdf";
            
            var expected = "Success";
            Assert.IsTrue(File.Exists(fileName), "does not exist?");
            Assert.AreEqual(expected, res, "DownloadFileAsync did not return expected string when given existing file + skip");
        }
    }
}
