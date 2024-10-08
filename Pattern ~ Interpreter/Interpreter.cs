// Для демонстрации паттерна "Интерпретатор" (Interpreter) давайте возьмем тематику расчета выражений с 
// использованием простого языка запросов. Например, мы создадим интерпретатор для арифметических выражений, 
// который будет разбирать и вычислять выражения, такие как "10 + 2 - 5".

using System;
using System.Collections.Generic;

// Интерфейс выражения
interface IExpression
{
    int Interpret();
}

// Класс для чисел, просто возвращает число
class NumberExpression : IExpression
{
    private int _number;

    public NumberExpression(int number)
    {
        _number = number;
    }

    public int Interpret()
    {
        return _number;
    }
}

// Класс для сложения
class AddExpression : IExpression
{
    private IExpression _leftExpression;
    private IExpression _rightExpression;

    public AddExpression(IExpression left, IExpression right)
    {
        _leftExpression = left;
        _rightExpression = right;
    }

    public int Interpret()
    {
        return _leftExpression.Interpret() + _rightExpression.Interpret();
    }
}

// Класс для вычитания
class SubtractExpression : IExpression
{
    private IExpression _leftExpression;
    private IExpression _rightExpression;

    public SubtractExpression(IExpression left, IExpression right)
    {
        _leftExpression = left;
        _rightExpression = right;
    }

    public int Interpret()
    {
        return _leftExpression.Interpret() - _rightExpression.Interpret();
    }
}

// Класс контекста, который интерпретирует выражение
class ExpressionParser
{
    public static IExpression Parse(string expression)
    {
        Stack<IExpression> stack = new Stack<IExpression>();
        string[] tokens = expression.Split(' ');

        foreach (string token in tokens)
        {
            if (int.TryParse(token, out int number))
            {
                // Если токен — число, создаем NumberExpression и добавляем в стек
                stack.Push(new NumberExpression(number));
            }
            else
            {
                // Если токен — оператор, извлекаем два выражения из стека
                IExpression rightExpression = stack.Pop();
                IExpression leftExpression = stack.Pop();

                switch (token)
                {
                    case "+":
                        stack.Push(new AddExpression(leftExpression, rightExpression));
                        break;
                    case "-":
                        stack.Push(new SubtractExpression(leftExpression, rightExpression));
                        break;
                }
            }
        }

        // Возвращаем последнее выражение в стеке — это итоговая интерпретация
        return stack.Pop();
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Пример выражения: "10 + 2 - 5"
        string expression = "10 + 2 - 5";
        Console.WriteLine($"Вычисляем выражение: {expression}");

        // Интерпретируем выражение
        IExpression parsedExpression = ExpressionParser.Parse(expression);
        int result = parsedExpression.Interpret();

        Console.WriteLine($"Результат: {result}");
    }
}