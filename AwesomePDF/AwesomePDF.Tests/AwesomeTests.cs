using System;
using Xunit;

namespace AwesomePDF.Tests
{
    public class AwesomeTests
    {
        const string TEXT = @"The Fennec fox, or fennec (Vulpes zerda), is a small crepuscular fox found in the Sahara of North Africa, the Sinai Peninsula, South West Israel (Arava desert)[2] and the Arabian desert. Its most distinctive feature is its unusually large ears, which also serve to dissipate heat. Its name comes from the Berber word (fanak), which means fox.[citation needed] The fennec is the smallest species of canid. Its coat, ears, and kidney functions have adapted to high-temperature, low-water, desert environments. Also, its hearing is sensitive enough to hear prey moving underground. It mainly eats insects, small mammals, and birds.";
        
        public AwesomeTests()
        {
            
        }

        [Fact]
        public void PDFShouldBeCreatedWithLocalImage()
        {
            //arrange
            PDFGenerator gen = new PDFGenerator("PDF");
            
            //act
            var result = gen.GeneratePDF("Fennec", "Content/Fennec.png", TEXT);
            
            //assert
            Assert.True(new System.IO.FileInfo(result).Length > 0);
        }

        [Fact]
        public void PDFShouldBeCreatedWithInternetImage()
        {
            //arrange
            PDFGenerator gen = new PDFGenerator("PDF");
            
            //act
            var result = gen.GeneratePDF("FennecTwo", "https://animals.sandiegozoo.org/sites/default/files/2016-10/fennec_fox_0.jpg", TEXT);
            
            //assert
            Assert.True(new System.IO.FileInfo(result).Length > 0);
        }

        [Fact]
        public void PDFShouldBeCreatedDirectoryTraversal()
        {
            const string ROOT = "PDF/Test";
            const string FILE = "../FennecThree";
            
            //arrange
            PDFGenerator gen = new PDFGenerator(ROOT);
            
            //act
            var result = gen.GeneratePDF(FILE, "https://animals.sandiegozoo.org/sites/default/files/2016-10/fennec_fox_0.jpg", TEXT);
            
            //assert
            Assert.Equal($"{ROOT}/{FILE}.pdf", result);
            Assert.True(new System.IO.FileInfo(result).Length > 0);
            Assert.True(new System.IO.DirectoryInfo(ROOT).GetFiles().Length == 0);
        }
    }
}
