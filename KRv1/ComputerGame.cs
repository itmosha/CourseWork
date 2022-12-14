using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace KRv1;
[Serializable]
[SuppressMessage("ReSharper", "IdentifierTypo")]
public enum GameGenre
{
    Arcade,
    Platformer,
    Shooter,
    Simulator,
    Strategy
}
[Serializable]
[SuppressMessage("ReSharper", "CommentTypo")]
public class ComputerGame : IPlayable, IAdditional, ICloneable
{
    private readonly string _name;
    private readonly GameGenre _genre;
    private readonly int _price;
    private readonly byte _gameSize;
    private bool _isStarted;
    public bool IsPlayable { get; set; }

    public ComputerGame(string name, GameGenre genre, int price, byte gameSize)
    {
        _name = name;
        _genre = genre;
        _price = price;
        _gameSize = gameSize;
        _isStarted = false;
    }

    public bool Playable()
    { //Метод: проверяет есть возможность начать играть в эту игрушку
        return IsPlayable = _isStarted == true ? true : false;
    }
    public List<Func<string>> DelegateList()
    { //Метод: возвращает делегат с методами класса
        return new List<Func<string>>() { LaunchTheGame, CloseTheGame, PlayTheGame, InstallTheExtension, ViewDocumentation };
    }

    public override string ToString()
    { //Перегрузка ToString: используем для добавления в listbox
        return Convert.ToString("Object: (4)Computer games, Name: " + _name + ", Price: " + _price);
    }

    public object Clone()
    { //Метод: используем для копирования класса
        var computerGameClone = new ComputerGame(_name, _genre, _price, _gameSize);
        return computerGameClone;
    }

    public string LaunchTheGame()
    { //Метод: запуска игры
        if (_isStarted == false) _isStarted = true;
        return "The game is already running";
    }
    public string CloseTheGame()
    {
        if (_isStarted == true) _isStarted = false;
        return "The game has already been stopped";
    }
    public string PlayTheGame()
    {
        if (Playable() == false) return "Game is turn off";
        return $"You are playing a {_name} {_genre} genre game, you have a lot of fun";
    }
    public string InstallTheExtension()
    {
        return $"You got bored, so you install the DLC for the {_name} game";
    }
    public string ViewDocumentation()
    { //Метод: нужен для просмотра всех полей класса
        return $"Name - {_name}\n" +
               $"Genre - {_genre}\n" +
               $"Price - {_price} RUB\n" +
               $"Game Size - {_gameSize} GB";
    }
}