// Пример: Создание объекта "Пицца" с помощью Builder

using System;

// Класс, представляющий пиццу
class Pizza
{
    public string Size { get; set; } // Размер пиццы
    public bool HasCheese { get; set; } // Наличие сыра
    public bool HasPepperoni { get; set; } // Наличие пепперони
    public bool HasMushrooms { get; set; } // Наличие грибов

    // Метод для отображения информации о пицце
    public void DisplayInfo()
    {
        Console.WriteLine($"Пицца размером {Size}: " +
                          $"Сыр: {(HasCheese ? "Да" : "Нет")}, " +
                          $"Пепперони: {(HasPepperoni ? "Да" : "Нет")}, " +
                          $"Грибы: {(HasMushrooms ? "Да" : "Нет")}");
    }
}

// Интерфейс строителя, который определяет методы для создания компонентов пиццы
interface IPizzaBuilder
{
    void SetSize(string size); // Установка размера пиццы
    void AddCheese(); // Добавление сыра
    void AddPepperoni(); // Добавление пепперони
    void AddMushrooms(); // Добавление грибов
    Pizza Build(); // Построить пиццу
}

// Конкретный строитель для создания пиццы
class PizzaBuilder : IPizzaBuilder
{
    private Pizza pizza; // Пицца, которую мы строим

    public PizzaBuilder()
    {
        pizza = new Pizza(); // Создаем новый экземпляр пиццы
    }

    public void SetSize(string size)
    {
        pizza.Size = size; // Устанавливаем размер пиццы
    }

    public void AddCheese()
    {
        pizza.HasCheese = true; // Добавляем сыр
    }

    public void AddPepperoni()
    {
        pizza.HasPepperoni = true; // Добавляем пепперони
    }

    public void AddMushrooms()
    {
        pizza.HasMushrooms = true; // Добавляем грибы
    }

    public Pizza Build()
    {
        return pizza; // Возвращаем готовую пиццу
    }
}

// Класс клиент, который использует строитель для создания пиццы
class Program
{
    static void Main(string[] args)
    {
        // Создаем строителя пиццы
        IPizzaBuilder pizzaBuilder = new PizzaBuilder();

        // Настраиваем пиццу
        pizzaBuilder.SetSize("Большая"); // Устанавливаем размер
        pizzaBuilder.AddCheese(); // Добавляем сыр
        pizzaBuilder.AddPepperoni(); // Добавляем пепперони
        pizzaBuilder.AddMushrooms(); // Добавляем грибы

        // Строим пиццу
        Pizza myPizza = pizzaBuilder.Build();

        // Отображаем информацию о пицце
        myPizza.DisplayInfo();

        // Ждем, пока пользователь нажмет клавишу, чтобы закрыть консоль
        Console.ReadKey();
    }
}
