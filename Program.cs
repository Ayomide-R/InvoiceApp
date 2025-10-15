using System;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter Customer Name:");
        string customerName = Console.ReadLine() ?? "Unknown Customer";

        Console.WriteLine("Enter Item Name:");
        string itemName = Console.ReadLine() ?? "Unnamed Item";

        Console.WriteLine("Enter Quantity:");
        string? quantityInput = Console.ReadLine();
        int quantity = int.TryParse(quantityInput, out int q) ? q : 0;

        Console.WriteLine("Enter Unit Price:");
        string? priceInput = Console.ReadLine();
        decimal price = decimal.TryParse(priceInput, out decimal p) ? p : 0;

        decimal total = quantity * price;

        Console.WriteLine("\nGenerating Invoice...");

        var invoice = new InvoiceData
        {
            CustomerName = customerName,
            ItemName = itemName,
            Quantity = quantity,
            UnitPrice = price,
            Total = total,
            Date = DateTime.Now
        };

        InvoiceGenerator.Generate(invoice);
        Console.WriteLine("✅ Invoice generated successfully! Check your project folder.");
    }
}

public class InvoiceData

{
    public string CustomerName { get; set; } = "";
    public string ItemName { get; set; } = "";
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Total { get; set; }
    public DateTime Date { get; set; }
}
