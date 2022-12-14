using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Windows;

// ReSharper disable once IdentifierTypo
namespace KRv1
{

    public enum AssemblyComplexity
    {
        Easy,
        Middle,
        Hard
    }
    
    [Serializable]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class Constructor : Toy, IPlayable, ICloneable, IAdditional //Класс: Конструктор наследуется от Toy
    {
        private readonly string _theme; //Тематика игрушки
        private short _numberOfDetails; //Количество деталей в наборе
        private readonly AssemblyComplexity _difficulty; //Сложность сборки конструктора
        public bool IsPlayable { get; set; } = true;

        
        public Constructor(string ageLimit, string theme, string name, int price, short numberOfDetails, AssemblyComplexity difficulty)
        : base(ageLimit, name, price)
        { //Конструктор с вводом всех параметров
            _theme = theme;
            _numberOfDetails = numberOfDetails;
            _difficulty = difficulty;
            Unpacked = true;
        }
        
        public Constructor(string name, int price, short numberOfDetails, AssemblyComplexity difficulty)
            : base(name, price)
        { //Конструктор без возрастного ограничения и тематики
            AgeLimit = "no age restrictions";
            _theme = "no specific theme";
            _numberOfDetails = numberOfDetails;
            _difficulty = difficulty;
            Unpacked = true;
        }

        public bool Playable()
        { //Метод: проверяет есть возможность начать играть в эту игрушку
            return IsPlayable = _numberOfDetails > 1 ? true : false;
        }
        public List<Func<string>> DelegateList()
        { //Метод: возвращает делегат с методами класса
            return new List<Func<string>>() { Play, LookAtTheLabel, Unpack, PutAway, CheckForAge };
        }

        public override string Play()
        { //Метод: играть с конструктором
            if (Playable() == false) return "You can't start playing constructor because you have too few parts";
            var baseReturn = base.Play();
            var calc = "";
            if (Unpacked == true) return baseReturn;
            var rand = new Random();
            switch (_difficulty)
            {
                case AssemblyComplexity.Easy:
                    calc = CalculatingTheProbabilityOfCollectingASet(Convert.ToByte(rand.Next(1, 10)));
                    break;
                case AssemblyComplexity.Middle:
                    calc = CalculatingTheProbabilityOfCollectingASet(Convert.ToByte(rand.Next(1, 5)));
                    break;
                case AssemblyComplexity.Hard:
                    calc = CalculatingTheProbabilityOfCollectingASet(Convert.ToByte(rand.Next(1, 3)));
                    break;
            }
            if (rand.Next(1, 5) == 5)
            {
                return baseReturn + calc +
                       $"When collecting all the details of the {Name}, you missed one detail, " +
                       "it seems that during the game it was lost somewhere";
            }
            else
            {
                return baseReturn + calc +
                       $"By collecting all the details of the {Name}, " +
                       "you collect all the details without losing them during the game";
            }
        }
        public override string LookAtTheLabel()
        { //Override метод: нужен для просмотра всех полей класса
            var baseReturn = base.LookAtTheLabel();
            return baseReturn + 
                   $"Theme {_theme}\n" +
                   $"Difficulty: {_difficulty}\n" +
                   $"Number of details: {_numberOfDetails}";
        }
        
        public string AddDetails(short numberOfDetails)
        { //Метод: добавить деталей в конструктор
            if (Unpacked == true) return "You didn't unpack the toy";
            var inputWindow = new InputWindow("SHORT");
            var window = new Window();
            window = inputWindow.CreateSpecialWindow();
            window.ShowDialog();
            _numberOfDetails += inputWindow.ReturnShort;
            return $"You are missing details in the {Name}, so you decide to add {numberOfDetails} more details\n" +
                   $"Now you have {_numberOfDetails} parts in the {Name}";
        }

        public static string CheckForAge()
        { //Статический метод: проверить соостветсвие возрасту
            return "You decide to check the age on the box... " +
                   $"It says that it is intended for people of age {AgeLimit}";
        }
        public static Constructor operator +(Constructor cur, Constructor other)
        { //Перегрузка оператора +: высыпаем все детали 2 конструктора в 1
            cur._numberOfDetails += other._numberOfDetails;
            other._numberOfDetails = 0;
            return cur;
        }
        public override string ToString()
        { //Перегрузка ToString: используем для добавления в listbox
            return Convert.ToString("Object: (0)Constructor, Name: " + Name + ", Price: " + Price + ", Age limit: " + AgeLimit);
        }
        public object Clone()
        { //Метод: используем для копирования класса
            var constructorClone = new Constructor(AgeLimit, _theme, Name, Price, _numberOfDetails, _difficulty);
            return constructorClone;
        }

        private string CalculatingTheProbabilityOfCollectingASet(byte chance)
        { //Дополнительный метод: высчитываение вероятности сборки конструктора
            if (chance == 2)
            {
                return
                    "You successfully assemble an unimaginable building from the constructor and are very happy about it\n";
            }
            else
            {
                _numberOfDetails -= 2;
                return "You fail to assemble anything from the constructor and in a fit of anger you" +
                       "scatter all the details around the room\n" +
                       "After counting all the details, you understand that 2 of them are lost\n";
            }
        }
    }
}