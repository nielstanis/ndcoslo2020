namespace Services
{
    public interface IPDFService
    {
        string GeneratePDF(string title, string image, string text);
    }
}