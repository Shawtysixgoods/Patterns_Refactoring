using System;

// Абстрактный продукт (интерфейс) — это общий интерфейс для всех типов документов
interface IDocument
{
    void Print(); // Метод, который будет реализован в каждом конкретном документе
}

// Конкретный продукт — класс, представляющий PDF документ
class PdfDocument : IDocument
{
    public void Print()
    {
        Console.WriteLine("Печать PDF документа.");
    }
}

// Конкретный продукт — класс, представляющий Word документ
class WordDocument : IDocument
{
    public void Print()
    {
        Console.WriteLine("Печать Word документа.");
    }
}

// Абстрактный создатель (фабрика) — определяет метод, который будет создавать документы
abstract class DocumentCreator
{
    // Фабричный метод, который будет реализован в подклассах
    public abstract IDocument CreateDocument();

    // Метод, который использует фабричный метод для создания и работы с документом
    public void PrintDocument()
    {
        // Используем фабричный метод для создания документа
        IDocument document = CreateDocument();
        // Вызываем метод печати у созданного документа
        document.Print();
    }
}

// Конкретный создатель для PDF документов
class PdfDocumentCreator : DocumentCreator
{
    // Реализация фабричного метода для создания PDF документа
    public override IDocument CreateDocument()
    {
        return new PdfDocument();
    }
}

// Конкретный создатель для Word документов
class WordDocumentCreator : DocumentCreator
{
    // Реализация фабричного метода для создания Word документа
    public override IDocument CreateDocument()
    {
        return new WordDocument();
    }
}

// Класс клиент, который использует создателя документов для работы
class Program
{
    static void Main(string[] args)
    {
        // Создаем создателя для PDF документов и печатаем документ
        DocumentCreator pdfCreator = new PdfDocumentCreator();
        pdfCreator.PrintDocument();

        // Создаем создателя для Word документов и печатаем документ
        DocumentCreator wordCreator = new WordDocumentCreator();
        wordCreator.PrintDocument();

        // Ждем, пока пользователь нажмет клавишу, чтобы закрыть консоль
        Console.ReadKey();
    }
}
