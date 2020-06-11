namespace MyPDFLibrary
{
    public class MyPDFGenerator : Services.IPDFService
    {
        public string GeneratePDF(string title, string image, string text)
        {
            string location = typeof(MyPDFGenerator).Assembly.CodeBase;
            string httpClient = typeof(System.Net.Http.HttpClient).Assembly.CodeBase; 
            
            System.Console.ForegroundColor = System.ConsoleColor.Red;
            System.Console.WriteLine($"MyPDFGenerator: {location}");
            System.Console.WriteLine($"System.Net.Http.HttpClient: {httpClient}");
            System.Console.ResetColor();

            var generator = new AwesomePDF.PDFGenerator("PDF");
            return generator.GeneratePDF(title,image,text);
        }
    }

}