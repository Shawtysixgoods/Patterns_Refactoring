// Давайте рассмотрим паттерн "Посредник" (Mediator) на примере системы чата, где несколько пользователей могут 
// отправлять сообщения друг другу через посредника (медиатора). Медиатор управляет взаимодействиями между пользователями, 
// чтобы они не взаимодействовали напрямую, а делали это через него.

using System;
using System.Collections.Generic;

// Интерфейс посредника (Медиатора)
interface IChatMediator
{
    void SendMessage(string message, User user);  // Отправка сообщения
    void AddUser(User user);                      // Добавление пользователя в чат
}

// Абстрактный класс пользователя
abstract class User
{
    protected IChatMediator _mediator;
    protected string _name;

    public User(IChatMediator mediator, string name)
    {
        _mediator = mediator;
        _name = name;
    }

    // Отправка сообщения через посредника
    public abstract void Send(string message);
    
    // Получение сообщения
    public abstract void Receive(string message);
}

// Конкретный класс пользователя
class ChatUser : User
{
    public ChatUser(IChatMediator mediator, string name) : base(mediator, name)
    {
    }

    // Отправка сообщения через медиатор
    public override void Send(string message)
    {
        Console.WriteLine($"{_name} отправляет сообщение: {message}");
        _mediator.SendMessage(message, this);
    }

    // Получение сообщения
    public override void Receive(string message)
    {
        Console.WriteLine($"{_name} получил сообщение: {message}");
    }
}

// Конкретный посредник (медиатор) — группа чата
class ChatMediator : IChatMediator
{
    private List<User> _users;

    public ChatMediator()
    {
        _users = new List<User>();
    }

    // Добавление пользователя в чат
    public void AddUser(User user)
    {
        _users.Add(user);
    }

    // Отправка сообщения всем пользователям чата, кроме отправителя
    public void SendMessage(string message, User sender)
    {
        foreach (User user in _users)
        {
            if (user != sender)  // Не отправляем сообщение самому себе
            {
                user.Receive(message);
            }
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Создаем посредника (чат)
        IChatMediator chat = new ChatMediator();

        // Создаем пользователей и добавляем их в чат
        User user1 = new ChatUser(chat, "Алиса");
        User user2 = new ChatUser(chat, "Боб");
        User user3 = new ChatUser(chat, "Чарли");

        chat.AddUser(user1);
        chat.AddUser(user2);
        chat.AddUser(user3);

        // Пользователи отправляют сообщения через посредника
        user1.Send("Привет всем!");
        user2.Send("Привет, Алиса!");
        user3.Send("Привет, как дела?");
    }
}


