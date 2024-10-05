// создадим структуру каталога и файловой системы, где папки могут содержать как файлы, так и другие папки

using System;
using System.Collections.Generic;

// Общий интерфейс для всех компонентов (файлов и папок)
interface IFileSystemComponent
{
    void Display(int indentLevel);  // Метод для отображения содержимого с отступами
}

// Класс для отдельных файлов (лист в структуре Composite)
class File : IFileSystemComponent
{
    private string _name;

    public File(string name)
    {
        _name = name;
    }

    // Реализация метода для отображения файла
    public void Display(int indentLevel)
    {
        Console.WriteLine(new String(' ', indentLevel) + "File: " + _name);
    }
}

// Класс для папок, которые могут содержать другие компоненты (композит)
class Folder : IFileSystemComponent
{
    private string _name;
    private List<IFileSystemComponent> _components = new List<IFileSystemComponent>();

    public Folder(string name)
    {
        _name = name;
    }

    // Добавление компонента в папку
    public void AddComponent(IFileSystemComponent component)
    {
        _components.Add(component);
    }

    // Удаление компонента из папки
    public void RemoveComponent(IFileSystemComponent component)
    {
        _components.Remove(component);
    }

    // Реализация метода для отображения содержимого папки
    public void Display(int indentLevel)
    {
        Console.WriteLine(new String(' ', indentLevel) + "Folder: " + _name);

        // Отображение всех вложенных компонентов
        foreach (var component in _components)
        {
            component.Display(indentLevel + 2);  // Увеличиваем отступ для вложенных элементов
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Создаем файлы
        IFileSystemComponent file1 = new File("Document.txt");
        IFileSystemComponent file2 = new File("Picture.jpg");
        IFileSystemComponent file3 = new File("Music.mp3");

        // Создаем папки
        Folder rootFolder = new Folder("Root");
        Folder documentsFolder = new Folder("Documents");
        Folder mediaFolder = new Folder("Media");

        // Добавляем файлы и папки в соответствующие директории
        rootFolder.AddComponent(documentsFolder);  // Папка Documents в Root
        rootFolder.AddComponent(mediaFolder);      // Папка Media в Root
        documentsFolder.AddComponent(file1);       // Файл Document.txt в Documents
        mediaFolder.AddComponent(file2);           // Файл Picture.jpg в Media
        mediaFolder.AddComponent(file3);           // Файл Music.mp3 в Media

        // Отображаем структуру каталога
        rootFolder.Display(0);  // Начальный уровень отступов — 0
    }
}
