// Давайте рассмотрим паттерн "Наблюдатель" (Observer) на примере системы оповещения пользователей о курсе акций. 
// У нас будет объект "Акция" (Subject), который отслеживает изменение цены, и несколько наблюдателей (Observers), 
// которые получают уведомления при изменении цены.

using System;
using System.Collections.Generic;

// Интерфейс наблюдателя (Observer)
interface IObserver
{
    void Update(float price);  // Метод для обновления состояния при изменении цены
}

// Интерфейс субъекта (Subject)
interface IStock
{
    void Attach(IObserver observer);   // Присоединение наблюдателя
    void Detach(IObserver observer);   // Отсоединение наблюдателя
    void Notify();                     // Уведомление всех наблюдателей
}

// Класс Акции (Subject)
class Stock : IStock
{
    private List<IObserver> _observers = new List<IObserver>();
    private float _price;

    public float Price
    {
        get { return _price; }
        set
        {
            _price = value;
            Notify();  // Уведомляем всех наблюдателей об изменении цены
        }
    }

    // Присоединяем наблюдателя
    public void Attach(IObserver observer)
    {
        _observers.Add(observer);
    }

    // Отсоединяем наблюдателя
    public void Detach(IObserver observer)
    {
        _observers.Remove(observer);
    }

    // Уведомляем всех наблюдателей
    public void Notify()
    {
        foreach (var observer in _observers)
        {
            observer.Update(_price);
        }
    }
}

// Конкретный наблюдатель — приложение для мониторинга акций
class StockApp : IObserver
{
    private string _name;

    public StockApp(string name)
    {
        _name = name;
    }

    // Обновляем цену в приложении, когда приходит уведомление
    public void Update(float price)
    {
        Console.WriteLine($"{_name} получил уведомление: новая цена акции - {price}");
    }
}

// Конкретный наблюдатель — пользователь, получающий оповещения по email
class EmailAlert : IObserver
{
    private string _email;

    public EmailAlert(string email)
    {
        _email = email;
    }

    // Отправляем email с новой ценой
    public void Update(float price)
    {
        Console.WriteLine($"Email на {_email}: новая цена акции - {price}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Создаем акцию и присоединяем наблюдателей
        Stock stock = new Stock();
        StockApp app1 = new StockApp("Приложение Акции 1");
        StockApp app2 = new StockApp("Приложение Акции 2");
        EmailAlert emailAlert = new EmailAlert("user@example.com");

        stock.Attach(app1);
        stock.Attach(app2);
        stock.Attach(emailAlert);

        // Изменяем цену акции и уведомляем всех наблюдателей
        stock.Price = 120.5f;  // Все наблюдатели получат уведомление

        // Отсоединяем одного наблюдателя
        stock.Detach(app2);

        // Изменяем цену снова и уведомляем оставшихся наблюдателей
        stock.Price = 125.75f;  // Теперь уведомление получат только app1 и emailAlert
    }
}

Для демонстрации паттерна "Хранитель" (Memento) давайте возьмем пример текстового редактора. Мы реализуем возможность отмены изменений текста через сохранение и восстановление состояний (моментов) текста.

