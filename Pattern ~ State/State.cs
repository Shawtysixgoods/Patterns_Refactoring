// Для демонстрации паттерна "Состояние" (State) давайте возьмем пример устройства проигрывателя музыки. 
// В зависимости от текущего состояния проигрывателя (например, "Воспроизведение", "Пауза" или "Остановлено"), 
// действия пользователя будут вызывать разные результаты. 
// Паттерн "Состояние" позволяет изменять поведение объекта в зависимости от его состояния.


using System;

// Интерфейс состояния
interface IPlayerState
{
    void Play(MusicPlayer player);    // Метод для начала воспроизведения
    void Pause(MusicPlayer player);   // Метод для паузы воспроизведения
    void Stop(MusicPlayer player);    // Метод для остановки воспроизведения
}

// Конкретное состояние "Воспроизведение"
class PlayingState : IPlayerState
{
    public void Play(MusicPlayer player)
    {
        Console.WriteLine("Музыка уже воспроизводится.");
    }

    public void Pause(MusicPlayer player)
    {
        Console.WriteLine("Пауза воспроизведения.");
        player.SetState(new PausedState());  // Переход в состояние "Пауза"
    }

    public void Stop(MusicPlayer player)
    {
        Console.WriteLine("Остановка воспроизведения.");
        player.SetState(new StoppedState()); // Переход в состояние "Остановлено"
    }
}

// Конкретное состояние "Пауза"
class PausedState : IPlayerState
{
    public void Play(MusicPlayer player)
    {
        Console.WriteLine("Продолжение воспроизведения.");
        player.SetState(new PlayingState());  // Переход в состояние "Воспроизведение"
    }

    public void Pause(MusicPlayer player)
    {
        Console.WriteLine("Музыка уже на паузе.");
    }

    public void Stop(MusicPlayer player)
    {
        Console.WriteLine("Остановка воспроизведения.");
        player.SetState(new StoppedState());  // Переход в состояние "Остановлено"
    }
}

// Конкретное состояние "Остановлено"
class StoppedState : IPlayerState
{
    public void Play(MusicPlayer player)
    {
        Console.WriteLine("Начало воспроизведения.");
        player.SetState(new PlayingState());  // Переход в состояние "Воспроизведение"
    }

    public void Pause(MusicPlayer player)
    {
        Console.WriteLine("Невозможно поставить на паузу. Музыка остановлена.");
    }

    public void Stop(MusicPlayer player)
    {
        Console.WriteLine("Музыка уже остановлена.");
    }
}

// Класс MusicPlayer — контекст, в котором меняются состояния
class MusicPlayer
{
    private IPlayerState _state;

    public MusicPlayer()
    {
        _state = new StoppedState();  // Начальное состояние — "Остановлено"
    }

    // Изменение состояния
    public void SetState(IPlayerState state)
    {
        _state = state;
    }

    // Воспроизведение музыки
    public void Play()
    {
        _state.Play(this);
    }

    // Пауза
    public void Pause()
    {
        _state.Pause(this);
    }

    // Остановка воспроизведения
    public void Stop()
    {
        _state.Stop(this);
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Создаем проигрыватель
        MusicPlayer player = new MusicPlayer();

        // Тестируем различные состояния
        player.Play();   // Начать воспроизведение
        player.Pause();  // Поставить на паузу
        player.Play();   // Возобновить воспроизведение
        player.Stop();   // Остановить воспроизведение
        player.Pause();  // Попробовать поставить на паузу, когда музыка остановлена
    }
}


