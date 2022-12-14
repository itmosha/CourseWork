using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

// ReSharper disable once IdentifierTypo
namespace KRv1
{
    [Serializable]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class Toy : ICloneable
    {
        protected static string AgeLimit; //Возрастное ограничение (от 2 до 6 лет)
        protected readonly string Name; //Название
        protected readonly int Price; //Цена
        protected bool Unpacked; //Поле для определения открыта ли коробка или нет
        
        public Toy(string ageLimit, string name, int price)
        { //Конструктор с вводом всех параметров
            AgeLimit = ageLimit;
            Name = name;
            Price = price;
            Unpacked = true;
        }
        
        public Toy(string name, int price)
        { //Конструктор без ввода возрастного лимита
            Name = name;
            Price = price;
            Unpacked = true;
        }
        
        public virtual string Play()
        {
            //Метод: играть с игрушкой
            return Unpacked == true ? "You didn't unpack the toy" : $"You have a lot of fun playing with {Name}\n";
        }
        public List<Func<string>> DelegateList()
        {
            return new List<Func<string>>() { Play, LookAtTheLabel, Unpack, PutAway };
        }
        public virtual string LookAtTheLabel()
        { //Virtual метод: узнать характеристики игрушки
            return "You take out the box with the toy to see its characteristics.\n" + 
                   $"Toy name {Name}\n" +
                   $"{Price}RUB\n" +
                   $"Age limit {AgeLimit}\n";
        }
        public object Clone()
        {
            var toyClone = new Toy(AgeLimit, Name, Price);
            return toyClone;
        }
        public string Unpack()
        { //Метод: распаковать игрушку
            Unpacked = false;
            return $"You take out the box with the {Name} and unpack it\n";
        }
        public string PutAway()
        { //Метод: отложить и запаковать игрушку
            Unpacked = true;
            return "You are tired of playing and you decide to close the box and put it away";
        }
        public override string ToString()
        {
            return Convert.ToString("Object: (5)Toy, Name: " + Name + ", Price: " + Price + ", Age limit: " + AgeLimit);
        }
    }
}