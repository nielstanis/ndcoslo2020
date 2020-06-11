using System;
using System.IO;
using System.Net.Http;
using iText.IO.Font;
using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace AwesomePDF
{
    public class PDFGenerator
    {
        private readonly string _outputFolder;
        public PDFGenerator(string outputFolder)
        {
            _outputFolder = outputFolder;

            if (!System.IO.Directory.Exists(_outputFolder))
            {
                System.IO.Directory.CreateDirectory(_outputFolder);
            }
        }

        public string GeneratePDF(string title, string image, string text)
        {
            string resultPDF = $"{_outputFolder}/{title}.pdf";
            using (var writer = new PdfWriter(resultPDF))
            using (var pdfDocument = new PdfDocument(writer))
            {
                Document doc = new Document(pdfDocument);
                
                Paragraph titleParagraph = new Paragraph();
                Text titleText = new Text(title)
                    .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA))
                    .SetFontSize(20)
                    .SetBold();
                    
                titleParagraph.Add(titleText);
                doc.Add(titleParagraph);

                ImageData imageData = ImageDataFactory.Create(image);
                Image img = new Image(imageData).SetAutoScale(true);
                Paragraph imageParagraph = new Paragraph();
                imageParagraph.Add(img);
                doc.Add(imageParagraph);

                Paragraph bodyParagraph = new Paragraph();
                Text bodyText = new Text(text);
                bodyParagraph.Add(bodyText);
                doc.Add(bodyParagraph);
            }
            return resultPDF;
        }
    }
}