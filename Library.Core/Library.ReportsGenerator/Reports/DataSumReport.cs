using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Library.Core;
using Library.Core.Context;
using Microsoft.EntityFrameworkCore;

namespace Library.ReportsGenerator.Reports
{
    public class DataSumReport
    {
        //private readonly IDataBaseContext context;
        //public DataSumReport(IDataBaseContext context) => this.context = context;

        private readonly DataContext context;
        public DataSumReport(DataContext context) => this.context = context;

        public void GenerateDataSumReport()
        {
            try
            {
                var xd = context.Books.Count();
                Console.WriteLine("Tworzenie katalogu dla pliku PDF...");
                string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Reports");
                Directory.CreateDirectory(folderPath);

                string filePath = Path.Combine(folderPath, $"Raport_{DateTime.Now:yyyy-MM-dd}.pdf");
                Console.WriteLine($"Generowanie raportu: {filePath}");

                using (PdfWriter writer = new PdfWriter(filePath))
                using (PdfDocument pdf = new PdfDocument(writer))
                using (Document document = new Document(pdf))
                {
                    Console.WriteLine("Dodawanie tytułu raportu...");
                    document.Add(new Paragraph("Raport Dzienny")
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetFontSize(20));

                    Console.WriteLine("Tworzenie tabeli...");
                    Table table = new Table(3).UseAllAvailableWidth();
                    table.AddHeaderCell("ABC");
                    table.AddHeaderCell("DEF");
                    table.AddHeaderCell("GHI");

                    Console.WriteLine("Dodawanie danych do tabeli...");
                    table.AddCell("1");
                    table.AddCell("2");
                    table.AddCell("3");

                    table.AddCell("4");
                    table.AddCell("5");
                    table.AddCell("6");

                    document.Add(table);
                    Console.WriteLine("Raport PDF wygenerowany pomyślnie!");
                }

                string base64String = Convert.ToBase64String(File.ReadAllBytes(filePath));
                Console.WriteLine("Plik przekonwertowany do Base64!");

                // Zapis do bazy danych
                //SavePdfToDatabase(base64String);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd generowania PDF: {ex.Message}");
            }
        }
    }
}