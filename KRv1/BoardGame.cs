using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Windows;

// ReSharper disable once IdentifierTypo
namespace KRv1
{
    [Serializable]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class BoardGame : Toy, IPlayable, ICloneable, IAdditional //Класс: Настольная игра наследуется от Toy
    {
        private readonly string _theme; //Тематика настольной игры
        private byte _numberOfPlayers; //Количество игроков
        private short _numberOfWins; //Количество побед
        public bool IsPlayable { get; set; } = true;

        public BoardGame(string ageLimit, string name, int price, string theme, byte numberOfPlayers)
        : base(ageLimit, name, price)
        { //Конструктор с вводом всех параметров
            _theme = theme;
            _numberOfPlayers = numberOfPlayers;
            _numberOfWins = 0;
        }

        public BoardGame(string name, int price, string theme, byte numberOfPlayers)
        : base(name, price)
        { //Конструктор без ввода возрастного лимита
            _theme = theme;
            _numberOfPlayers = numberOfPlayers;
            _numberOfWins = 0;
        }

        public bool Playable()
        { //Метод: проверяет есть возможность начать играть в эту игрушку
            return IsPlayable = _numberOfPlayers != 0 ? true : false;
        }
        public List<Func<string>> DelegateList()
        { //Метод: возвращает делегат с методами класса
            return new List<Func<string>>() { Play, LookAtTheLabel, ExpelOrAddAPerson, Unpack, PutAway };
        }

        public override string Play()
        { //Override метод: играть в настольную игру
            if (Playable() == false) return "You don't have enough people to start playing";
            var baseReturn = base.Play();
            if (Unpacked == true) return baseReturn;
            var rand = new Random();
            
            if (rand.Next(1, _numberOfPlayers) == 1)
            {
                _numberOfWins++;
                return $"You are calling all your {_numberOfPlayers} friends to play this {_theme} themed board game. " +
                       "You are the very first to reach the goal. Congratulations you have won. " +
                       $"You have won {_numberOfWins} times";
            }
            else
            {
                return $"You are calling all your {_numberOfPlayers} friends to play this {_theme} themed board game. " +
                       "You were bypassed and reached first. You've lose";
            }
        }
        public override string ToString()
        { //Перегрузка ToString: используем для добавления в listbox
            return Convert.ToString("Object: (3)Board game, Name: " + Name + ", Price: " + Price + ", Age limit: " + AgeLimit);
        }
        public override string LookAtTheLabel()
        { //Override метод: нужен для просмотра всех полей класса
            var baseReturn = base.LookAtTheLabel();
            return baseReturn +
                   $"Theme: {_theme}\n" +
                   $"Number of players: {_numberOfPlayers}\n" +
                   $"Number of wins: {_numberOfWins}";
        }
        public object Clone()
        { //Метод: используем для копирования класса
            var boardGameClone = new BoardGame(AgeLimit, Name, Price, _theme, _numberOfPlayers);
            return boardGameClone;
        }

        public string ExpelOrAddAPerson()
        { //Метод: добавить или убрать игрока
            var result = MessageBox.Show("Add or expel person", "Question!", MessageBoxButton.YesNo,
                MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
            {
                _numberOfPlayers--;
                return "You are expelling one person in disgrace";
            }
            else if (result == MessageBoxResult.Yes)
            {
                _numberOfPlayers++;
                return "You receive one person with honors";
            }

            return "Have you changed your mind";
        }
    }
}