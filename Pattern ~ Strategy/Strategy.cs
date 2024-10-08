// Давайте рассмотрим паттерн "Стратегия" (Strategy) на примере системы расчета стоимости доставки для интернет-магазина. 
// У нас будут разные стратегии расчета доставки (например, "Экспресс-доставка", "Обычная доставка" и "Самовывоз"), 
// которые можно динамически менять в зависимости от предпочтений пользователя.


using System;

// Интерфейс стратегии расчета доставки
interface IShippingStrategy
{
    decimal CalculateShippingCost(decimal orderAmount); // Метод для расчета стоимости доставки
}

// Конкретная стратегия — Экспресс-доставка
class ExpressShipping : IShippingStrategy
{
    public decimal CalculateShippingCost(decimal orderAmount)
    {
        return 20.00m; // Фиксированная стоимость за экспресс-доставку
    }
}

// Конкретная стратегия — Обычная доставка
class RegularShipping : IShippingStrategy
{
    public decimal CalculateShippingCost(decimal orderAmount)
    {
        return 10.00m; // Фиксированная стоимость за обычную доставку
    }
}

// Конкретная стратегия — Самовывоз (бесплатно)
class PickupShipping : IShippingStrategy
{
    public decimal CalculateShippingCost(decimal orderAmount)
    {
        return 0.00m; // Бесплатная доставка при самовывозе
    }
}

// Класс заказа — контекст, в котором используется стратегия
class Order
{
    private IShippingStrategy _shippingStrategy;
    public decimal OrderAmount { get; set; } // Общая сумма заказа

    // Установка стратегии доставки
    public void SetShippingStrategy(IShippingStrategy shippingStrategy)
    {
        _shippingStrategy = shippingStrategy;
    }

    // Расчет стоимости доставки с использованием текущей стратегии
    public decimal CalculateShippingCost()
    {
        if (_shippingStrategy == null)
        {
            throw new InvalidOperationException("Стратегия доставки не установлена.");
        }

        return _shippingStrategy.CalculateShippingCost(OrderAmount);
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Создаем заказ с общей суммой
        Order order = new Order { OrderAmount = 100.00m };

        // Устанавливаем стратегию для экспресс-доставки
        order.SetShippingStrategy(new ExpressShipping());
        Console.WriteLine($"Экспресс-доставка: {order.CalculateShippingCost()}");

        // Устанавливаем стратегию для обычной доставки
        order.SetShippingStrategy(new RegularShipping());
        Console.WriteLine($"Обычная доставка: {order.CalculateShippingCost()}");

        // Устанавливаем стратегию для самовывоза
        order.SetShippingStrategy(new PickupShipping());
        Console.WriteLine($"Самовывоз: {order.CalculateShippingCost()}");
    }
}