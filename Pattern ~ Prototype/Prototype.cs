// Пример: Клонирование объектов с помощью Prototype

using System;

// Интерфейс для прототипа, который должен поддерживать клонирование
interface IPrototype<T>
{
    T Clone(); // Метод для клонирования
}

// Класс, представляющий автомобиль
class Car : IPrototype<Car>
{
    public string Model { get; set; } // Модель автомобиля
    public string Color { get; set; } // Цвет автомобиля

    // Конструктор для установки свойств
    public Car(string model, string color)
    {
        Model = model;
        Color = color;
    }

    // Метод для клонирования объекта Car
    public Car Clone()
    {
        // Возвращаем новый объект Car с теми же значениями свойств
        return new Car(Model, Color);
    }

    // Метод для отображения информации об автомобиле
    public void DisplayInfo()
    {
        Console.WriteLine($"Модель: {Model}, Цвет: {Color}");
    }
}

// Класс клиент, который использует прототипы для создания объектов
class Program
{
    static void Main(string[] args)
    {
        // Создаем оригинальный объект Car
        Car originalCar = new Car("Tesla Model S", "Красный");
        
        // Отображаем информацию об оригинальном автомобиле
        Console.WriteLine("Оригинальный автомобиль:");
        originalCar.DisplayInfo();

        // Клонируем оригинальный автомобиль
        Car clonedCar = originalCar.Clone();
        
        // Меняем свойства клонированного автомобиля
        clonedCar.Color = "Синий";

        // Отображаем информацию о клонированном автомобиле
        Console.WriteLine("Клонированный автомобиль:");
        clonedCar.DisplayInfo();

        // Отображаем информацию об оригинальном автомобиле снова, чтобы показать, что он не изменился
        Console.WriteLine("Оригинальный автомобиль после клонирования:");
        originalCar.DisplayInfo();

        // Ждем, пока пользователь нажмет клавишу, чтобы закрыть консоль
        Console.ReadKey();
    }
}
