using System;

// Основной интерфейс для кофе
public interface ICoffee
{
    string GetDescription();
    double GetCost();
}

// Конкретный класс кофе (базовый класс)
public class BasicCoffee : ICoffee
{
    public string GetDescription()
    {
        return "Простой кофе";
    }

    public double GetCost()
    {
        return 50.0; // Стоимость простого кофе
    }
}

// Базовый класс обёртки (декоратора)
public abstract class CoffeeDecorator : ICoffee
{
    protected ICoffee _coffee;  // Ссылка на объект, который мы оборачиваем

    public CoffeeDecorator(ICoffee coffee)
    {
        _coffee = coffee;
    }

    public virtual string GetDescription()
    {
        return _coffee.GetDescription(); // Возвращаем описание оборачиваемого объекта
    }

    public virtual double GetCost()
    {
        return _coffee.GetCost(); // Возвращаем стоимость оборачиваемого объекта
    }
}

// Класс-декоратор для добавления молока в кофе
public class MilkDecorator : CoffeeDecorator
{
    public MilkDecorator(ICoffee coffee) : base(coffee) {}

    public override string GetDescription()
    {
        return _coffee.GetDescription() + ", с молоком";
    }

    public override double GetCost()
    {
        return _coffee.GetCost() + 10.0; // Добавляем стоимость молока
    }
}

// Класс-декоратор для добавления сахара в кофе
public class SugarDecorator : CoffeeDecorator
{
    public SugarDecorator(ICoffee coffee) : base(coffee) {}

    public override string GetDescription()
    {
        return _coffee.GetDescription() + ", с сахаром";
    }

    public override double GetCost()
    {
        return _coffee.GetCost() + 5.0; // Добавляем стоимость сахара
    }
}

// Пример использования
class Program
{
    static void Main(string[] args)
    {
        // Создаём простой кофе
        ICoffee myCoffee = new BasicCoffee();
        Console.WriteLine(myCoffee.GetDescription() + " стоит " + myCoffee.GetCost() + " рублей");

        // Добавляем молоко
        myCoffee = new MilkDecorator(myCoffee);
        Console.WriteLine(myCoffee.GetDescription() + " стоит " + myCoffee.GetCost() + " рублей");

        // Добавляем сахар
        myCoffee = new SugarDecorator(myCoffee);
        Console.WriteLine(myCoffee.GetDescription() + " стоит " + myCoffee.GetCost() + " рублей");
    }
}
