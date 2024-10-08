// Давайте возьмем тему технической поддержки пользователей в IT-компании. 
// Допустим, у нас есть система, где разные уровни технической поддержки (1-й уровень, 2-й уровень и 3-й уровень) 
// обрабатывают запросы пользователей в зависимости от сложности проблемы.

using System;

// Абстрактный класс обработчика запроса от клиента
abstract class SupportHandler
{
    protected SupportHandler nextHandler;

    // Устанавливаем следующий уровень поддержки
    public void SetNext(SupportHandler handler)
    {
        this.nextHandler = handler;
    }

    // Абстрактный метод для обработки запроса
    public abstract void HandleRequest(string issue, int severity);
}

// 1-й уровень поддержки (Базовый уровень: простые проблемы)
class Level1Support : SupportHandler
{
    public override void HandleRequest(string issue, int severity)
    {
        if (severity <= 1) // Уровень сложности проблемы 1
        {
            Console.WriteLine($"Level 1 Support обработал запрос: {issue}");
        }
        else if (nextHandler != null)
        {
            Console.WriteLine($"Level 1 Support передал запрос на уровень 2: {issue}");
            nextHandler.HandleRequest(issue, severity); // Передаем на следующий уровень
        }
    }
}

// 2-й уровень поддержки (Средний уровень: более сложные проблемы)
class Level2Support : SupportHandler
{
    public override void HandleRequest(string issue, int severity)
    {
        if (severity <= 2) // Уровень сложности проблемы 2
        {
            Console.WriteLine($"Level 2 Support обработал запрос: {issue}");
        }
        else if (nextHandler != null)
        {
            Console.WriteLine($"Level 2 Support передал запрос на уровень 3: {issue}");
            nextHandler.HandleRequest(issue, severity); // Передаем на следующий уровень
        }
    }
}

// 3-й уровень поддержки (Высокий уровень: сложные и критические проблемы)
class Level3Support : SupportHandler
{
    public override void HandleRequest(string issue, int severity)
    {
        if (severity <= 3) // Уровень сложности проблемы 3
        {
            Console.WriteLine($"Level 3 Support обработал запрос: {issue}");
        }
        else
        {
            Console.WriteLine($"Проблема не может быть обработана на текущем уровне поддержки: {issue}");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Создаем уровни поддержки
        SupportHandler level1 = new Level1Support();
        SupportHandler level2 = new Level2Support();
        SupportHandler level3 = new Level3Support();

        // Формируем цепочку: Level1 -> Level2 -> Level3
        level1.SetNext(level2);
        level2.SetNext(level3);

        // Пример запросов на поддержку
        string[] issues = { "Проблема с подключением к Wi-Fi", "Ошибка приложения", "Сервер не отвечает" };
        int[] severities = { 1, 2, 3 }; // Уровень сложности запросов

        // Проходим по каждому запросу и передаем его первому уровню поддержки
        for (int i = 0; i < issues.Length; i++)
        {
            Console.WriteLine($"\nЗапрос: {issues[i]} (Уровень сложности: {severities[i]})");
            level1.HandleRequest(issues[i], severities[i]);
        }
    }
}
