// Управление домашними устройствами — телевизором, аудиосистемой и светом.

using System;

// Подсистема 1: Телевизор
class Television
{
    public void TurnOn()
    {
        Console.WriteLine("Телевизор включён.");
    }

    public void TurnOff()
    {
        Console.WriteLine("Телевизор выключён.");
    }
}

// Подсистема 2: Аудиосистема
class AudioSystem
{
    public void SetVolume(int level)
    {
        Console.WriteLine($"Громкость аудиосистемы установлена на {level}.");
    }

    public void TurnOn()
    {
        Console.WriteLine("Аудиосистема включена.");
    }

    public void TurnOff()
    {
        Console.WriteLine("Аудиосистема выключена.");
    }
}

// Подсистема 3: Освещение
class Lights
{
    public void Dim(int level)
    {
        Console.WriteLine($"Освещение затемнено до уровня {level}.");
    }

    public void TurnOff()
    {
        Console.WriteLine("Освещение выключено.");
    }

    public void TurnOn()
    {
        Console.WriteLine("Освещение включено.");
    }
}

// Класс Facade, который упрощает управление всеми подсистемами
class HomeTheaterFacade
{
    private Television tv;
    private AudioSystem audio;
    private Lights lights;

    // Конструктор принимает все необходимые подсистемы
    public HomeTheaterFacade(Television television, AudioSystem audioSystem, Lights lightsSystem)
    {
        this.tv = television;
        this.audio = audioSystem;
        this.lights = lightsSystem;
    }

    // Метод, который включает все устройства для киносеанса
    public void WatchMovie()
    {
        Console.WriteLine("Настройка системы для просмотра фильма...");
        lights.Dim(20);  // Затемняем свет
        tv.TurnOn();     // Включаем телевизор
        audio.TurnOn();  // Включаем аудиосистему
        audio.SetVolume(30);  // Устанавливаем громкость
        Console.WriteLine("Все готово для просмотра фильма!\n");
    }

    // Метод для завершения киносеанса
    public void EndMovie()
    {
        Console.WriteLine("Выключение системы после просмотра фильма...");
        lights.TurnOn();  // Включаем свет
        tv.TurnOff();     // Выключаем телевизор
        audio.TurnOff();  // Выключаем аудиосистему
        Console.WriteLine("Киносеанс завершён.\n");
    }
}

// Основной класс программы
class Program
{
    static void Main(string[] args)
    {
        // Создаем объекты всех подсистем
        Television tv = new Television();
        AudioSystem audio = new AudioSystem();
        Lights lights = new Lights();

        // Создаем объект фасада, передавая в него все подсистемы
        HomeTheaterFacade homeTheater = new HomeTheaterFacade(tv, audio, lights);

        // Теперь вместо того, чтобы управлять каждой подсистемой вручную,
        // мы используем фасад, который делает всё за нас
        homeTheater.WatchMovie();  // Включаем всё для просмотра фильма
        homeTheater.EndMovie();    // Завершаем киносеанс
    }
}
