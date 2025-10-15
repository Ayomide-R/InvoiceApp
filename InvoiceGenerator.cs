using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.IO;

public static class InvoiceGenerator
{
    public static void Generate(InvoiceData data)
    {
        // ✅ Required for QuestPDF 2024+
        QuestPDF.Settings.License = LicenseType.Community;

        // ✅ Define a folder to save files (you can change this)
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Invoices");
        Directory.CreateDirectory(outputFolder);

        // ✅ Generate a unique file name
        string fileName = Path.Combine(outputFolder, $"Invoice_{data.CustomerName}_{DateTime.Now:yyyyMMddHHmmss}.pdf");

        // ✅ Build a valid QuestPDF document
        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(50);
                page.DefaultTextStyle(x => x.FontSize(12));
                page.PageColor(Colors.White);

                // Header section
                page.Header().Column(column =>
                {
                    column.Item().Text("ROY RESOURCE CONSULT")
                        .FontSize(20).SemiBold().FontColor(Colors.Blue.Medium);
                    column.Item().Text("INVOICE")
                        .FontSize(16).SemiBold().FontColor(Colors.Grey.Darken2);
                    column.Item().Text($"Date: {data.Date:dd MMM yyyy}")
                        .FontSize(10).FontColor(Colors.Grey.Darken1);
                });

                // Content section
                page.Content().PaddingVertical(20).Column(column =>
                {
                    column.Spacing(15);

                    // Customer details
                    column.Item().Text($"Customer: {data.CustomerName}");
                    column.Item().LineHorizontal(1).LineColor(Colors.Grey.Lighten2);

                    // Table
                    column.Item().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(3);
                            columns.RelativeColumn(1);
                            columns.RelativeColumn(1);
                            columns.RelativeColumn(1);
                        });

                        table.Header(header =>
                        {
                            header.Cell().Text("Item").SemiBold();
                            header.Cell().AlignRight().Text("Qty").SemiBold();
                            header.Cell().AlignRight().Text("Unit Price").SemiBold();
                            header.Cell().AlignRight().Text("Total").SemiBold();
                        });

                        table.Cell().Text(data.ItemName);
                        table.Cell().AlignRight().Text(data.Quantity.ToString());
                        table.Cell().AlignRight().Text($"₦{data.UnitPrice:F2}");
                        table.Cell().AlignRight().Text($"₦{data.Total:F2}");
                    });

                    column.Item().LineHorizontal(1).LineColor(Colors.Grey.Lighten2);
                    column.Item().AlignRight().Text($"Total Amount: ₦{data.Total:F2}")
                        .FontSize(14).SemiBold().FontColor(Colors.Blue.Darken1);
                });

                // Footer section
                page.Footer().AlignCenter().Text("Thank you for choosing ROY RESOURCE CONSULT")
                    .FontSize(10).FontColor(Colors.Grey.Darken1);
            });
        });

        // ✅ Save the PDF properly
        document.GeneratePdf(fileName);

        Console.WriteLine($"Invoice saved to: {fileName}");
    }
}
