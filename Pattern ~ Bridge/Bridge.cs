// создание системы сообщений с разными способами отправки

using System;

// Абстракция (Message) — определяет интерфейс для управления сообщениями
abstract class Message
{
    // Поле для хранения конкретной реализации отправки
    protected IMessageSender messageSender;

    // Конструктор принимает объект отправителя сообщений (реализация)
    public Message(IMessageSender sender)
    {
        messageSender = sender;
    }

    // Абстрактный метод для отправки сообщения
    public abstract void Send(string text);
}

// Реализация отправки (IMessageSender) — интерфейс для конкретных способов отправки
interface IMessageSender
{
    void SendMessage(string text);
}

// Конкретная реализация отправки сообщения через Email
class EmailSender : IMessageSender
{
    public void SendMessage(string text)
    {
        Console.WriteLine("Sending Email: " + text);
    }
}

// Конкретная реализация отправки сообщения через SMS
class SmsSender : IMessageSender
{
    public void SendMessage(string text)
    {
        Console.WriteLine("Sending SMS: " + text);
    }
}

// Конкретная абстракция для коротких сообщений (ShortMessage)
class ShortMessage : Message
{
    // Конструктор вызывает базовый конструктор для привязки реализации отправки
    public ShortMessage(IMessageSender sender) : base(sender) { }

    // Переопределение метода отправки для короткого сообщения
    public override void Send(string text)
    {
        // Проверка длины сообщения для короткого типа
        if (text.Length <= 20)
        {
            messageSender.SendMessage(text);
        }
        else
        {
            Console.WriteLine("Message is too long for short message.");
        }
    }
}

// Конкретная абстракция для длинных сообщений (LongMessage)
class LongMessage : Message
{
    public LongMessage(IMessageSender sender) : base(sender) { }

    // Переопределение метода отправки для длинного сообщения
    public override void Send(string text)
    {
        messageSender.SendMessage(text); // Длинное сообщение можно отправить без ограничений
    }
}

// Пример использования паттерна Bridge
class Program
{
    static void Main(string[] args)
    {
        // Создаем отправителей сообщений (Email и SMS)
        IMessageSender emailSender = new EmailSender();
        IMessageSender smsSender = new SmsSender();

        // Создаем конкретные сообщения с разными способами отправки
        Message shortEmailMessage = new ShortMessage(emailSender);
        Message longSmsMessage = new LongMessage(smsSender);

        // Отправляем короткое сообщение через Email
        shortEmailMessage.Send("Hello via Email!");

        // Пытаемся отправить слишком длинное сообщение через SMS
        longSmsMessage.Send("This is a very long message that should be sent via SMS.");
    }
}
