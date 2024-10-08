// Для демонстрации паттерна "Хранитель" (Memento) давайте возьмем пример текстового редактора. 
// Мы реализуем возможность отмены изменений текста через сохранение и восстановление состояний (моментов) текста.


using System;
using System.Collections.Generic;

// Класс "Memento" — хранит состояние текста
class TextMemento
{
    public string TextState { get; }

    public TextMemento(string state)
    {
        TextState = state;
    }
}

// Класс "Originator" — редактор текста, который может создавать и восстанавливать свои состояния
class TextEditor
{
    private string _text;

    public void SetText(string text)
    {
        _text = text;
        Console.WriteLine($"Текущий текст: {_text}");
    }

    // Сохраняем текущее состояние текста в объект Memento
    public TextMemento SaveText()
    {
        Console.WriteLine("Сохранено текущее состояние.");
        return new TextMemento(_text);
    }

    // Восстанавливаем текст из объекта Memento
    public void RestoreText(TextMemento memento)
    {
        _text = memento.TextState;
        Console.WriteLine($"Восстановлено состояние: {_text}");
    }
}

// Класс "Caretaker" — менеджер состояний, хранит и восстанавливает моменты
class TextHistory
{
    private Stack<TextMemento> _history = new Stack<TextMemento>();

    // Сохраняем состояние текста в историю
    public void Save(TextMemento memento)
    {
        _history.Push(memento);
    }

    // Возвращаем последнее сохранённое состояние
    public TextMemento Undo()
    {
        if (_history.Count > 0)
        {
            Console.WriteLine("Отмена последнего изменения.");
            return _history.Pop();
        }
        return null;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Создаем текстовый редактор и историю изменений
        TextEditor editor = new TextEditor();
        TextHistory history = new TextHistory();

        // Пишем текст и сохраняем его
        editor.SetText("Первая версия текста.");
        history.Save(editor.SaveText());

        // Изменяем текст и снова сохраняем
        editor.SetText("Вторая версия текста.");
        history.Save(editor.SaveText());

        // Еще одно изменение текста без сохранения
        editor.SetText("Третья версия текста.");

        // Восстанавливаем предыдущее состояние
        editor.RestoreText(history.Undo());

        // Восстанавливаем еще одно предыдущее состояние
        editor.RestoreText(history.Undo());
    }
}