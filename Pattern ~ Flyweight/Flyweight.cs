//  создание и использование объектов "деревьев" в лесу, где каждый тип дерева (цвет, текстура) является повторно используемым объектом.

using System;
using System.Collections.Generic;

// Flyweight - интерфейс для общих свойств объектов, которые будут повторно использоваться
public interface ITree
{
    void Display(int x, int y);  // Метод для отображения дерева в координатах
}

// ConcreteFlyweight - конкретная реализация общего типа дерева
public class TreeType : ITree
{
    private string color;  // Цвет дерева
    private string texture;  // Текстура дерева

    public TreeType(string color, string texture)
    {
        this.color = color;
        this.texture = texture;
    }

    // Метод для отображения дерева с координатами
    public void Display(int x, int y)
    {
        Console.WriteLine($"Дерево [{color}, {texture}] на координатах ({x},{y})");
    }
}

// FlyweightFactory - фабрика, которая управляет объектами Flyweight
public class TreeFactory
{
    private Dictionary<string, ITree> treeTypes = new Dictionary<string, ITree>();

    // Метод для получения экземпляра TreeType, если его нет — создаём новый
    public ITree GetTreeType(string color, string texture)
    {
        string key = color + "-" + texture;
        
        if (!treeTypes.ContainsKey(key))
        {
            // Если дерева с такими параметрами нет, создаем новое
            treeTypes[key] = new TreeType(color, texture);
            Console.WriteLine($"Создаём новое дерево: {key}");
        }
        else
        {
            Console.WriteLine($"Используем существующее дерево: {key}");
        }

        return treeTypes[key];
    }
}

// Контекст для каждого конкретного дерева в лесу
public class Tree
{
    private int x, y;  // Координаты дерева
    private ITree treeType;  // Ссылка на общий объект Flyweight (TreeType)

    public Tree(int x, int y, ITree treeType)
    {
        this.x = x;
        this.y = y;
        this.treeType = treeType;
    }

    // Метод для отображения дерева в лесу
    public void Display()
    {
        treeType.Display(x, y);
    }
}

// Класс, представляющий лес, где создаются деревья
public class Forest
{
    private List<Tree> trees = new List<Tree>();  // Список всех деревьев

    private TreeFactory treeFactory = new TreeFactory();  // Фабрика для создания/управления типами деревьев

    // Метод для добавления нового дерева в лес
    public void PlantTree(int x, int y, string color, string texture)
    {
        // Получаем или создаём нужный тип дерева через фабрику
        ITree treeType = treeFactory.GetTreeType(color, texture);
        
        // Создаём конкретное дерево с координатами и ссылкой на тип
        Tree tree = new Tree(x, y, treeType);
        
        // Добавляем дерево в лес
        trees.Add(tree);
    }

    // Метод для отображения всех деревьев в лесу
    public void DisplayTrees()
    {
        foreach (var tree in trees)
        {
            tree.Display();
        }
    }
}

// Демонстрация работы паттерна Flyweight
class Program
{
    static void Main(string[] args)
    {
        Forest forest = new Forest();

        // Сажаем деревья с разными и одинаковыми характеристиками
        forest.PlantTree(1, 2, "Зелёный", "Гладкая");
        forest.PlantTree(3, 4, "Зелёный", "Гладкая");
        forest.PlantTree(5, 6, "Красный", "Шероховатая");
        forest.PlantTree(7, 8, "Зелёный", "Гладкая");  // Это дерево будет использовать уже существующий объект Flyweight

        // Выводим все деревья на экран
        forest.DisplayTrees();
    }
}
