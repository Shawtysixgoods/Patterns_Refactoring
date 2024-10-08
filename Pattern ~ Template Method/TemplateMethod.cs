// Давайте рассмотрим паттерн "Шаблонный метод" (Template Method) на примере системы заказа еды из разных типов ресторанов. 
// У нас будут разные типы ресторанов (например, "Итальянский ресторан" и "Японский ресторан"), 
// каждый из которых имеет свой процесс приготовления и доставки, но структура процесса одинакова: 
// приготовить блюдо, упаковать его и доставить.

using System;

// Абстрактный класс — "Шаблонный метод" для приготовления и доставки еды
abstract class Restaurant
{
    // Шаблонный метод, описывающий общий процесс приготовления и доставки заказа
    public void ProcessOrder()
    {
        PrepareDish();
        PackDish();
        DeliverDish();
    }

    // Методы, которые должны быть реализованы в подклассах
    protected abstract void PrepareDish(); // Приготовить блюдо

    protected virtual void PackDish() // Упаковать блюдо (может быть переопределено)
    {
        Console.WriteLine("Упаковываем блюдо в стандартную упаковку.");
    }

    // Метод для доставки блюда (общий для всех ресторанов)
    protected void DeliverDish()
    {
        Console.WriteLine("Доставляем блюдо клиенту.");
    }
}

// Конкретный класс — Итальянский ресторан
class ItalianRestaurant : Restaurant
{
    protected override void PrepareDish()
    {
        Console.WriteLine("Приготовление пиццы по итальянским традициям.");
    }

    // Мы можем оставить метод упаковки по умолчанию
}

// Конкретный класс — Японский ресторан
class JapaneseRestaurant : Restaurant
{
    protected override void PrepareDish()
    {
        Console.WriteLine("Приготовление суши с использованием свежих ингредиентов.");
    }

    // Переопределяем упаковку для японского ресторана
    protected override void PackDish()
    {
        Console.WriteLine("Упаковываем суши в специальную бамбуковую упаковку.");
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Создаем итальянский ресторан и обрабатываем заказ
        Console.WriteLine("Заказ в итальянском ресторане:");
        Restaurant italianRestaurant = new ItalianRestaurant();
        italianRestaurant.ProcessOrder();

        Console.WriteLine();

        // Создаем японский ресторан и обрабатываем заказ
        Console.WriteLine("Заказ в японском ресторане:");
        Restaurant japaneseRestaurant = new JapaneseRestaurant();
        japaneseRestaurant.ProcessOrder();
    }
}



