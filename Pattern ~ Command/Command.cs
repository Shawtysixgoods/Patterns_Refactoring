// Теперь давайте рассмотрим паттерн "Команда" (Command) на примере системы удаленной работы с компьютером. 
// Мы реализуем команды для включения и выключения компьютера, а также для перезагрузки системы.

using System;
using System.Collections.Generic;

// Интерфейс команды
interface ICommand
{
    void Execute();
    void Undo();
}

// Класс получателя команды (тот, над кем будут выполняться команды)
class Computer
{
    public void TurnOn()
    {
        Console.WriteLine("Компьютер включен.");
    }

    public void TurnOff()
    {
        Console.WriteLine("Компьютер выключен.");
    }

    public void Reboot()
    {
        Console.WriteLine("Компьютер перезагружен.");
    }
}

// Команда для включения компьютера
class TurnOnCommand : ICommand
{
    private Computer _computer;

    public TurnOnCommand(Computer computer)
    {
        _computer = computer;
    }

    public void Execute()
    {
        _computer.TurnOn();
    }

    public void Undo()
    {
        _computer.TurnOff();
    }
}

// Команда для выключения компьютера
class TurnOffCommand : ICommand
{
    private Computer _computer;

    public TurnOffCommand(Computer computer)
    {
        _computer = computer;
    }

    public void Execute()
    {
        _computer.TurnOff();
    }

    public void Undo()
    {
        _computer.TurnOn();
    }
}

// Команда для перезагрузки компьютера
class RebootCommand : ICommand
{
    private Computer _computer;

    public RebootCommand(Computer computer)
    {
        _computer = computer;
    }

    public void Execute()
    {
        _computer.Reboot();
    }

    public void Undo()
    {
        Console.WriteLine("Отмена перезагрузки невозможна.");
    }
}

// Класс инвокера (тот, кто вызывает команды)
class RemoteControl
{
    private ICommand _command;

    // Привязываем команду к кнопке
    public void SetCommand(ICommand command)
    {
        _command = command;
    }

    // Выполняем команду
    public void PressButton()
    {
        _command.Execute();
    }

    // Отменяем команду
    public void PressUndo()
    {
        _command.Undo();
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Создаем получателя — компьютер
        Computer computer = new Computer();

        // Создаем команды для различных операций
        ICommand turnOnCommand = new TurnOnCommand(computer);
        ICommand turnOffCommand = new TurnOffCommand(computer);
        ICommand rebootCommand = new RebootCommand(computer);

        // Создаем пульт (инвокер)
        RemoteControl remoteControl = new RemoteControl();

        // Включаем компьютер
        Console.WriteLine("\nВключаем компьютер:");
        remoteControl.SetCommand(turnOnCommand);
        remoteControl.PressButton();

        // Выключаем компьютер
        Console.WriteLine("\nВыключаем компьютер:");
        remoteControl.SetCommand(turnOffCommand);
        remoteControl.PressButton();

        // Перезагружаем компьютер
        Console.WriteLine("\nПерезагружаем компьютер:");
        remoteControl.SetCommand(rebootCommand);
        remoteControl.PressButton();

        // Отмена команды включения
        Console.WriteLine("\nОтмена команды включения:");
        remoteControl.SetCommand(turnOnCommand);
        remoteControl.PressUndo();

        // Отмена команды перезагрузки
        Console.WriteLine("\nПопытка отменить перезагрузку:");
        remoteControl.SetCommand(rebootCommand);
        remoteControl.PressUndo();
    }
}