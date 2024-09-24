// Пример: Создание UI-компонентов с помощью Abstract Factory

using System;

// Абстрактные продукты — интерфейсы для различных UI-компонентов
interface IButton
{
    void Render(); // Метод для отрисовки кнопки
}

interface ITextBox
{
    void Render(); // Метод для отрисовки текстового поля
}

// Конкретные продукты для светлой темы
class LightButton : IButton
{
    public void Render()
    {
        Console.WriteLine("Отрисовка кнопки в светлой теме.");
    }
}

class LightTextBox : ITextBox
{
    public void Render()
    {
        Console.WriteLine("Отрисовка текстового поля в светлой теме.");
    }
}

// Конкретные продукты для темной темы
class DarkButton : IButton
{
    public void Render()
    {
        Console.WriteLine("Отрисовка кнопки в темной теме.");
    }
}

class DarkTextBox : ITextBox
{
    public void Render()
    {
        Console.WriteLine("Отрисовка текстового поля в темной теме.");
    }
}

// Абстрактная фабрика — интерфейс для создания продуктов
interface IGUIFactory
{
    IButton CreateButton(); // Метод для создания кнопки
    ITextBox CreateTextBox(); // Метод для создания текстового поля
}

// Конкретная фабрика для светлой темы
class LightThemeFactory : IGUIFactory
{
    public IButton CreateButton()
    {
        return new LightButton(); // Возвращаем светлую кнопку
    }

    public ITextBox CreateTextBox()
    {
        return new LightTextBox(); // Возвращаем светлое текстовое поле
    }
}

// Конкретная фабрика для темной темы
class DarkThemeFactory : IGUIFactory
{
    public IButton CreateButton()
    {
        return new DarkButton(); // Возвращаем темную кнопку
    }

    public ITextBox CreateTextBox()
    {
        return new DarkTextBox(); // Возвращаем темное текстовое поле
    }
}

// Класс клиент, который использует фабрики для создания UI-компонентов
class Program
{
    static void Main(string[] args)
    {
        // Выбор темы: светлая или темная
        IGUIFactory factory;

        // Для примера, выберем светлую тему
        factory = new LightThemeFactory();

        // Создаем и отрисовываем кнопки и текстовые поля для светлой темы
        IButton lightButton = factory.CreateButton();
        ITextBox lightTextBox = factory.CreateTextBox();
        lightButton.Render();
        lightTextBox.Render();

        // Теперь выберем темную тему
        factory = new DarkThemeFactory();

        // Создаем и отрисовываем кнопки и текстовые поля для темной темы
        IButton darkButton = factory.CreateButton();
        ITextBox darkTextBox = factory.CreateTextBox();
        darkButton.Render();
        darkTextBox.Render();

        // Ждем, пока пользователь нажмет клавишу, чтобы закрыть консоль
        Console.ReadKey();
    }
}
