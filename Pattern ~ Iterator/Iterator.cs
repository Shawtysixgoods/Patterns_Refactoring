// Давайте рассмотрим паттерн "Итератор" (Iterator) на примере коллекции песен в плейлисте музыкального приложения. 
// Мы создадим итератор для обхода песен в плейлисте.

using System;
using System.Collections.Generic;

// Интерфейс итератора
interface IIterator<T>
{
    bool HasNext();  // Есть ли следующий элемент
    T Next();        // Возвращает следующий элемент
}

// Интерфейс коллекции, для которой будет создан итератор
interface IPlaylist<T>
{
    IIterator<T> CreateIterator();  // Метод создания итератора
}

// Конкретная коллекция — Плейлист песен
class Playlist : IPlaylist<Song>
{
    private List<Song> _songs = new List<Song>();

    // Метод добавления песни в плейлист
    public void AddSong(Song song)
    {
        _songs.Add(song);
    }

    // Метод создания итератора для плейлиста
    public IIterator<Song> CreateIterator()
    {
        return new SongIterator(_songs);
    }
}

// Конкретный итератор для обхода песен в плейлисте
class SongIterator : IIterator<Song>
{
    private List<Song> _songs;
    private int _position = 0;

    public SongIterator(List<Song> songs)
    {
        _songs = songs;
    }

    // Проверяет, есть ли ещё песни в плейлисте
    public bool HasNext()
    {
        return _position < _songs.Count;
    }

    // Возвращает следующую песню
    public Song Next()
    {
        return _songs[_position++];
    }
}

// Класс для песен
class Song
{
    public string Title { get; }
    public string Artist { get; }

    public Song(string title, string artist)
    {
        Title = title;
        Artist = artist;
    }

    public override string ToString()
    {
        return $"{Title} от {Artist}";
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Создаем плейлист и добавляем песни
        Playlist playlist = new Playlist();
        playlist.AddSong(new Song("Bohemian Rhapsody", "Queen"));
        playlist.AddSong(new Song("Imagine", "John Lennon"));
        playlist.AddSong(new Song("Hotel California", "Eagles"));

        // Создаем итератор для плейлиста
        IIterator<Song> songIterator = playlist.CreateIterator();

        // Используем итератор для обхода песен
        Console.WriteLine("Песни в плейлисте:");
        while (songIterator.HasNext())
        {
            Song song = songIterator.Next();
            Console.WriteLine(song);
        }
    }
}