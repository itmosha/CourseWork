using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Windows;

// ReSharper disable once IdentifierTypo
namespace KRv1
{
    [Serializable]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public enum HairColor //enum который содержит перечисление возможного цвета волоса
    {
        Brown,
        Yellow,
        Black,
        Red,
        Orange
    }
    [Serializable]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class Doll : Toy, ICloneable, IAdditional //Класс: Кукла наследуется от Toy
    {
        private HairColor _hair; //Цвет волос у куклы

        public Doll(string ageLimit, string name, int price, HairColor hair)
        : base(ageLimit, name, price) 
        { //Конструктор с вводом всех параметров
            _hair = hair;
        }

        public Doll(string name, int price, HairColor hair)
        : base(name, price)
        { //Конструктор без ввода возрастного лимита
            _hair = hair;
        }

        public override string Play()
        { //Override метод: играть с куклой
            var baseReturn = base.Play();
            if (Unpacked == true) return baseReturn;
            return baseReturn +
                   $"You take out a comb and comb the {Name}'s {_hair} hair.";
        }
        public override string LookAtTheLabel()
        { //Override метод: нужен для просмотра всех полей класса
            var baseReturn = base.LookAtTheLabel();
            return baseReturn +
                   $@"Doll hair color - {_hair}";
        }
        public object Clone()
        { //Метод: используем для копирования класса
            var dollClone = new Doll(AgeLimit, Name, Price, _hair);
            return dollClone;
        }
        public override string ToString()
        { //Перегрузка ToString: используем для добавления в listbox
            return Convert.ToString("Object: (2)Doll, Name: " + Name + ", Price: " + Price + ", Age limit: " + AgeLimit);
        }
        public List<Func<string>> DelegateList()
        { //Метод: возвращает делегат с методами класса
            return new List<Func<string>>() { Play, LookAtTheLabel, ChangeHairColor, Unpack, PutAway };
        }

        public string ChangeHairColor()
        { //Метод: изменение цвета волос у куклы
            var inputWindow = new InputWindow("HAIR");
            var window = new Window();
            window = inputWindow.CreateSpecialWindow();
            window.ShowDialog();
            _hair = inputWindow.ReturnHair;
            return $"You dyed the doll's hair in {_hair}";
        }
    }
}