// Для демонстрации паттерна "Посетитель" (Visitor) давайте рассмотрим пример системы обработки разных типов файлов. 
// Допустим, у нас есть файлы разных типов — аудио, видео и документы, и нам нужно выполнить для каждого типа файлов 
// определенные действия, такие как воспроизведение, открытие или отображение информации. 
// С помощью паттерна "Посетитель" мы можем вынести логику обработки файлов в отдельные классы, 
// а сами файлы лишь принимают посетителей, которые выполняют соответствующие действия.

using System;

// Интерфейс посетителя
interface IFileVisitor
{
    void Visit(AudioFile audio);
    void Visit(VideoFile video);
    void Visit(DocumentFile document);
}

// Интерфейс элемента (файл)
interface IFile
{
    void Accept(IFileVisitor visitor);  // Метод для принятия посетителя
}

// Конкретный элемент — Аудиофайл
class AudioFile : IFile
{
    public string Title { get; }

    public AudioFile(string title)
    {
        Title = title;
    }

    // Принятие посетителя для обработки аудиофайла
    public void Accept(IFileVisitor visitor)
    {
        visitor.Visit(this);
    }
}

// Конкретный элемент — Видеофайл
class VideoFile : IFile
{
    public string Title { get; }

    public VideoFile(string title)
    {
        Title = title;
    }

    // Принятие посетителя для обработки видеофайла
    public void Accept(IFileVisitor visitor)
    {
        visitor.Visit(this);
    }
}

// Конкретный элемент — Документ
class DocumentFile : IFile
{
    public string Title { get; }

    public DocumentFile(string title)
    {
        Title = title;
    }

    // Принятие посетителя для обработки документа
    public void Accept(IFileVisitor visitor)
    {
        visitor.Visit(this);
    }
}

// Конкретный посетитель — Обработка файла (например, воспроизведение или отображение)
class FileActionVisitor : IFileVisitor
{
    public void Visit(AudioFile audio)
    {
        Console.WriteLine($"Воспроизведение аудиофайла: {audio.Title}");
    }

    public void Visit(VideoFile video)
    {
        Console.WriteLine($"Воспроизведение видеофайла: {video.Title}");
    }

    public void Visit(DocumentFile document)
    {
        Console.WriteLine($"Открытие документа: {document.Title}");
    }
}

// Другой посетитель — Показать информацию о файле
class FileInfoVisitor : IFileVisitor
{
    public void Visit(AudioFile audio)
    {
        Console.WriteLine($"Информация об аудиофайле: {audio.Title}");
    }

    public void Visit(VideoFile video)
    {
        Console.WriteLine($"Информация о видеофайле: {video.Title}");
    }

    public void Visit(DocumentFile document)
    {
        Console.WriteLine($"Информация о документе: {document.Title}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Создаем список файлов
        IFile[] files = new IFile[]
        {
            new AudioFile("Песня.mp3"),
            new VideoFile("Фильм.mp4"),
            new DocumentFile("Документ.pdf")
        };

        // Создаем посетителей
        IFileVisitor actionVisitor = new FileActionVisitor();
        IFileVisitor infoVisitor = new FileInfoVisitor();

        // Обрабатываем каждый файл разными посетителями
        Console.WriteLine("Действия с файлами:");
        foreach (IFile file in files)
        {
            file.Accept(actionVisitor);  // Вызовем действия для каждого файла
        }

        Console.WriteLine("\nИнформация о файлах:");
        foreach (IFile file in files)
        {
            file.Accept(infoVisitor);    // Получим информацию о каждом файле
        }
    }
}


