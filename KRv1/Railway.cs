using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Windows;

// ReSharper disable once IdentifierTypo
namespace KRv1
{
    [Serializable]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public sealed class Railway : Toy, IPlayable, ICloneable, IAdditional //Класс: Железная дорога наследуется от Toy
    {
        private short _railroadLength; //Длина пути поезда
        private readonly string _trainModel; //Модель поезда
        public bool IsPlayable { get; set; } = true;

        public Railway(string ageLimit, string name, int price, short railroadLength, string trainModel)
            : base(ageLimit, name, price)
        { //Конструктор с вводом всех параметров
            _railroadLength = railroadLength;
            _trainModel = trainModel;
        }

        public Railway(string name, int price, short railroadLength, string trainModel)
            : base(name, price)
        { //Конструктор без ввода возрастного лимита
            _railroadLength = railroadLength;
            _trainModel = trainModel;
        }

        public bool Playable()
        { //Метод: проверяет есть возможность начать играть в эту игрушку
            return IsPlayable = _railroadLength != 0 ? true : false;
        }
        public List<Func<string>> DelegateList()
        { //Метод: возвращает делегат с методами класса
            return new List<Func<string>>() { Play, LookAtTheLabel, RideTheTrain, AddRails, Unpack, PutAway };
        }
        public string RideTheTrain()
        { //Метод: покататься на поезде
            if (Playable() == false) return "You are missing the length of the railway";
            if (_railroadLength - 4 > 0)
            {
                _railroadLength -= 4;
            }
            else
            {
                _railroadLength = 0;
            }
            return "You decide to sit on the roof of a small train and ride it, the train began" +
                   "to go slower, but still managed to reach the end of the road";
        }
        public string AddRails()
        {
            var inputWindow = new InputWindow("SHORT");
            var window = new Window();
            window = inputWindow.CreateSpecialWindow();
            window.ShowDialog();
            var length = inputWindow.ReturnShort;
            _railroadLength += length;
            return $"You decide to extend the rails by {length} meters";
        }
        public object Clone()
        { //Метод: используем для копирования класса
            var railwayClone = new Railway(AgeLimit, Name, Price, _railroadLength, _trainModel);
            return railwayClone;
        }
        public override string Play()
        { //Override метод: играть с железной дорогой
            if (Playable() == false) return "You are missing the length of the railway";
            var baseReturn = base.Play();
            if (Unpacked == true) return baseReturn;
            return baseReturn +
                   $"You watch as your {_trainModel} train travels {_railroadLength} meters on the railroad and stops";
        }
        public override string LookAtTheLabel()
        { //Override метод: нужен для просмотра всех полей класса
            var baseReturn = base.LookAtTheLabel();
            return baseReturn +
                   $"Train Model - {_trainModel}\n" +
                   $"Railroad length - {_railroadLength}";
        }
        public override string ToString()
        { //Перегрузка ToString: используем для добавления в listbox
            return Convert.ToString("Object: (1)Railway, Name: " + Name + ", Price: " + Price + ", Age limit: " + AgeLimit);
        }
    }
}