// Пример: Реализация паттерна Singleton

using System;

// Класс Logger, который будет реализовывать паттерн Singleton
public class Logger
{
    // Статическое поле для хранения единственного экземпляра класса
    private static Logger _instance;

    // Объект для синхронизации потоков
    private static readonly object _lock = new object();

    // Приватный конструктор, чтобы предотвратить создание экземпляров извне
    private Logger() 
    {
        // Инициализация ресурсов, если необходимо
    }

    // Публичный метод для получения единственного экземпляра класса
    public static Logger Instance
    {
        get
        {
            // Проверяем, создан ли экземпляр
            if (_instance == null)
            {
                // Если нет, блокируем поток, чтобы предотвратить создание нескольких экземпляров
                lock (_lock)
                {
                    // Дублируем проверку внутри блокировки
                    if (_instance == null)
                    {
                        _instance = new Logger(); // Создаем экземпляр
                    }
                }
            }
            return _instance; // Возвращаем единственный экземпляр
        }
    }

    // Метод для логирования сообщений
    public void Log(string message)
    {
        Console.WriteLine($"[{DateTime.Now}] {message}"); // Записываем сообщение в консоль
    }
}

// Класс клиент, который использует Logger
class Program
{
    static void Main(string[] args)
    {
        // Получаем экземпляр логгера
        Logger logger = Logger.Instance;

        // Логируем несколько сообщений
        logger.Log("Программа запущена.");
        logger.Log("Происходит выполнение операций...");

        // Получаем еще один экземпляр логгера
        Logger anotherLogger = Logger.Instance;

        // Проверяем, что оба экземпляра указывают на один и тот же объект
        Console.WriteLine("Оба логгера равны: " + (logger == anotherLogger)); // Должно быть true

        // Ждем, пока пользователь нажмет клавишу, чтобы закрыть консоль
        Console.ReadKey();
    }
}
