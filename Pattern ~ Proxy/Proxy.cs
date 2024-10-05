// управлении доступом к объекту «Книга» через прокси, который проверяет, можно ли получить доступ к содержимому книги.

using System;

// Интерфейс IBook представляет функционал, который реализуют как реальные объекты, так и прокси.
public interface IBook
{
    void DisplayContent(); // Метод для отображения содержимого книги.
}

// Класс RealBook представляет реальный объект (книга), который содержит информацию и выполняет основную работу.
public class RealBook : IBook
{
    private string _content; // Хранит содержимое книги.

    public RealBook(string content)
    {
        _content = content;
    }

    // Реализует метод для отображения содержимого книги.
    public void DisplayContent()
    {
        Console.WriteLine("Содержимое книги: " + _content);
    }
}

// Класс ProxyBook является "заместителем" и управляет доступом к реальной книге.
public class ProxyBook : IBook
{
    private RealBook _realBook; // Ссылка на реальный объект (RealBook).
    private string _password; // Пароль для доступа к книге.

    // В конструкторе прокси передаётся пароль для контроля доступа.
    public ProxyBook(string password)
    {
        _password = password;
    }

    // Метод проверки пароля перед получением доступа к содержимому книги.
    private bool CheckAccess(string inputPassword)
    {
        return inputPassword == _password;
    }

    // Реализация метода DisplayContent через прокси. Здесь добавляется проверка доступа.
    public void DisplayContent()
    {
        Console.WriteLine("Введите пароль для доступа к содержимому книги:");
        string inputPassword = Console.ReadLine();

        if (CheckAccess(inputPassword))
        {
            // Если пароль правильный, создаём реальный объект и показываем содержимое.
            if (_realBook == null)
            {
                _realBook = new RealBook("Это секретная информация книги!");
            }
            _realBook.DisplayContent();
        }
        else
        {
            Console.WriteLine("Неверный пароль! Доступ запрещён.");
        }
    }
}

// Класс Program демонстрирует работу паттерна Proxy.
public class Program
{
    public static void Main(string[] args)
    {
        // Создаём прокси с паролем.
        IBook proxyBook = new ProxyBook("12345");

        // Попытка доступа к содержимому книги через прокси.
        proxyBook.DisplayContent();

        // Программа продолжает работу, можно снова попытаться получить доступ.
    }
}
