using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Library.Core;

namespace Library.ReportsGenerator.Reports
{
    public class DataSumReport
    {
        private readonly DataContext context;
        public DataSumReport(DataContext context) => this.context = context;

        public void GenerateDataSumReport()
        {
            try
            {
                var authorsCount = context.Authors.Count();
                var booksCount = context.Books.Count();
                var publishersCount = context.Publishers.Count();
                var usersCount = context.User.Count();
                
                string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Reports");

                string fileName = $"Raport_{DateTime.Now:yyyy-MM-dd}.pdf";

                string filePath = Path.Combine(folderPath, fileName);
                Console.WriteLine($"Generowanie raportu: {filePath}");

                using (PdfWriter writer = new PdfWriter(filePath))
                using (PdfDocument pdf = new PdfDocument(writer))
                using (Document document = new Document(pdf))
                {
                    document.Add(new Paragraph($"Raport Dzienny {DateTime.Now:HH:mm}")
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetFontSize(18));

                    Table table = new Table(4).UseAllAvailableWidth();
                    table.AddHeaderCell("Liczba autorów");
                    table.AddHeaderCell("Liczba książek");
                    table.AddHeaderCell("Liczba wydawnictw");
                    table.AddHeaderCell("Liczba uzytkowników");

                    Console.WriteLine("Dodawanie danych do tabeli...");
                    table.AddCell($"{authorsCount}");
                    table.AddCell($"{booksCount}");
                    table.AddCell($"{publishersCount}");
                    table.AddCell($"{usersCount}");

                    document.Add(table);
                }

                string base64String = Convert.ToBase64String(File.ReadAllBytes(filePath));

                var model = new Core.Entities.Reports()
                {
                    RGID = Guid.NewGuid(),
                    RName = fileName,
                    RGenerationDate = DateTime.Now,
                    RBase64 = base64String
                };

                context.Reports.Add(model);
                context.SaveChanges();

                Console.WriteLine("Raport PDF wygenerowany pomyślnie!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd generowania PDF: {ex.Message}");
            }
        }
    }
}