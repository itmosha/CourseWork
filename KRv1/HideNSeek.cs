using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Windows;

namespace KRv1;

[Serializable]
[SuppressMessage("ReSharper", "CommentTypo")]
public class HideNSeek : IAdditional
{
    private string _street;
    private string _house;
    private byte _numberOfPeople;
    private bool _inGame = true;
    private short _numberOfWins;

    public HideNSeek(string street, string house, byte numberOfPeople, short numberOfWins)
    {
        _street = street;
        _house = house;
        _numberOfPeople = numberOfPeople;
        _numberOfWins = numberOfWins;
    }
    public HideNSeek(string street, byte numberOfPeople, short numberOfWins)
    {
        _street = street;
        _house = "outside";
        _numberOfPeople = numberOfPeople;
        _numberOfWins = numberOfWins;
    }

    public string Play()
    { //Метод: начать играть в прятки(в зависимости от выпавшего числа выбирается путь игры
        var rand = new Random();
        if (_numberOfPeople < 2)
        {
            var inputWindow = new InputWindow("BYTE");
            var window = new Window();
            window = inputWindow.CreateSpecialWindow();
            window.ShowDialog();
            _numberOfPeople = inputWindow.ReturnByte;
            if (_inGame == true)
            {
                _numberOfWins++;
                _inGame = true;
                return "You win... and game start again";
            }
            _inGame = true;
            return "Game is over... and start again";
        }
        if (rand.Next(1, _numberOfPeople) == 1 && _inGame == true)
        {
            _numberOfPeople--;
            _inGame = false;
            return "You were noticed and you lost" +
                   $"There are {_numberOfPeople} people left";;
        }
        if (_inGame == false)
        {
            _numberOfPeople = 1;
            return
                "You wait until the game is over... the rest of the people have finished playing and you have joined them again";
        }
        else
        {
            _numberOfPeople--;
            return "You have not been noticed, you are still in the game\n" +
                   $"There are {_numberOfPeople} people left";
        }
    }
    public List<Func<string>> DelegateList()
    { //Метод: возвращает делегат с методами класса
        return new List<Func<string>>() { Play, CheckSituation };
    }
    public string CheckSituation()
    { //Метод: нужен для просмотра всех полей класса
        return $"There are {_numberOfPeople} people in the game\n" +
               $"You have {_numberOfWins} wins\n" +
               $"You are playing on the street {_street}, {_house}";
    }

    public override string ToString()
    { //Перегрузка ToString: используем для добавления в listbox
        return Convert.ToString("Object: (6)Hide and Seek, Street: " + _street + ", House: " + _house);
    }
    public object Clone()
    { //Метод: используем для копирования класса
        var hnsClone = new HideNSeek(_street, _house, _numberOfPeople, _numberOfWins);
        return hnsClone;
    }
}