using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Emit;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace KRv1;

[SuppressMessage("ReSharper", "StringLiteralTypo")]
[SuppressMessage("ReSharper", "CommentTypo")]
public class InputWindow //В этом классе выставляются определенные параметры для окна ввода и возвращаются объекты, также используется для ввода short, byte, HairColor
{
    private Window _window = new Window();
    private string _className;
    private StackPanel stack = new StackPanel();
    private object _returnInput;
    public byte ReturnByte;
    public short ReturnShort;
    public HairColor ReturnHair;

    public InputWindow(string? className)
    {
        _className = className ?? "NoClass";
    }
    public object ReturnObject()
    {
        return _returnInput;
    }

    public void InputByteDataWindow()
    { 
        var title = new[] { "BYTE DATA" };
        byte input;
        _window.Height = 100;
        _window.Width = 200;
        InputFields(title);
        var button = new Button
        { Height = 40, Width = 60 };
        button.Click += InputByteData;
        button.Content = "OK";
        stack.Children.Add(button);
        _window.Content = stack;
    }
    public void InputShortDataWindow()
    {
        var title = new[] { "SHORT DATA" };
        short input;
        _window.Height = 100;
        _window.Width = 200;
        InputFields(title);
        var button = new Button
            { Height = 40, Width = 60 };
        button.Click += InputShortData;
        button.Content = "OK";
        stack.Children.Add(button);
        _window.Content = stack;
    }
    public void InputHairDataWindow()
    {
        var hair = new[] { "BROWN", "YELLOW", "BLACK", "RED", "ORANGE" };
        _window.Height = 100;
        _window.Width = 200;
        ComboBox comboBox;
        comboBox = InputComboBox(hair);
        stack.Children.Add(comboBox);
        var button = new Button
            { Height = 40, Width = 60 };
        button.Click += InputHairData;
        button.Content = "OK";
        stack.Children.Add(button);
        _window.Content = stack;
    }
    private void InputByteData(object sender, RoutedEventArgs e)
    {
        var textBox = (TextBox)stack.Children[0];
        try
        {
            ReturnByte = Convert.ToByte(textBox.Text);
        }
        catch
        {
            MessageBox.Show("Invalid data!", "Attention");
        }
        finally
        {
            _window.Close();
        }
    }
    private void InputShortData(object sender, RoutedEventArgs e)
    {
        var textBox = (TextBox)stack.Children[0];
        try
        {
            ReturnShort = Convert.ToInt16(textBox.Text);
        }
        catch
        {
            MessageBox.Show("Invalid data!", "Attention");
        }
        finally
        {
            _window.Close();
        }
    }
    private void InputHairData(object sender, RoutedEventArgs e)
    {
        var hairComboBox = (ComboBox)stack.Children[0];
        try
        {
            ReturnHair = hairComboBox.Text switch
            {
                "BROWN" => HairColor.Brown,
                "YELLOW" => HairColor.Yellow,
                "BLACK" => HairColor.Black,
                "RED" => HairColor.Red,
                "ORANGE" => HairColor.Orange,
                _ => HairColor.Black
            };
        }
        catch
        {
            MessageBox.Show("Invalid data!", "Attention");
        }
        finally
        {
            _window.Close();
        }
    }

    private void InputFields(string[] title)
    {
        _window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        _window.ResizeMode = ResizeMode.NoResize;
        _window.WindowStyle = WindowStyle.ToolWindow;
        for (var i = 0; i < title.Length; i++)
        {
            var textBox = new xTextBox();
            textBox.PlaceHolder = title[i];
            stack.Children.Add(textBox);
        }
    }

    private ComboBox InputComboBox(string[] cm)
    {
        _window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        _window.ResizeMode = ResizeMode.NoResize;
        _window.WindowStyle = WindowStyle.ToolWindow;
        var combo = new ComboBox();
        for (var i = 0; i < cm.Length; i++)
        {
            var textBlock = new TextBlock();
            var comboItem = new ComboBoxItem();
            textBlock.Text = cm[i];
            comboItem.Content = textBlock;
            combo.Items.Add(comboItem);
        }
        return combo;
    }

    public Window CreateSpecialWindow()
    {
        switch (_className)
        {
            case "Constructor":
                CreateConstructorInputForm();
                break;
            case "Railway":
                CreateRailwayInputForm();
                break;
            case "Doll":
                CreateDollInputForm();
                break;
            case "Board game":
                CreateBoardGameInputForm();
                break;
            case "Toy":
                CreateToyInputForm();
                break;
            case "Computer game":
                CreateComputerGameInputForm();
                break;
            case "Hide and Seek":
                CreateHideNSeekInputForm();
                break;
            case "SHORT":
                InputShortDataWindow();
                break;
            case "BYTE":
                InputByteDataWindow();
                break;
            case "HAIR":
                InputHairDataWindow();
                break;
        }
        return _window;
    }

    private void CreateConstructorInputForm()
    {
        var title = new[] { "NAME", "PRICE", "AGE LIMIT", "THEME", "NUMBER OF DETAILS" };
        var dif = new[] { "EASY", "MIDDLE", "HARD" };
        var combo = new ComboBox();
        _window.Height = 190;
        _window.Width = 200;
        InputFields(title);
        combo = InputComboBox(dif);
        stack.Children.Add(combo);
        var button = new Button();
        button.Height = 40;
        button.Width = 60;
        button.Click += ObjectConstruct;
        button.Content = "OK";
        stack.Children.Add(button);
        _window.Content = stack;
    }
    private void ObjectConstruct(object sender, RoutedEventArgs e)
    {
        TextBox name, priceTBox, ageLimit, theme, numberOfDetailsTBox;
        var temp = new TextBox();
        temp.Text = "NONE";
        var difficultyComboBox = (ComboBox)stack.Children[5];
        short numberOfDetails;
        int price;
        try
        {
            name = (TextBox)stack.Children[0];
            priceTBox = (TextBox)stack.Children[1];
            ageLimit = (TextBox)stack.Children[2];
            theme = (TextBox)stack.Children[3];
            numberOfDetailsTBox = (TextBox)stack.Children[4];
            price = Convert.ToInt32(priceTBox.Text);
            numberOfDetails = Convert.ToInt16(numberOfDetailsTBox.Text);
            
        }
        catch
        {
            MessageBox.Show("Invalid data", "Attention");
            name = theme = ageLimit = temp;
            price = 0;
            numberOfDetails = 0;
        }

        var dif = difficultyComboBox.Text switch
        {
            "EASY" => AssemblyComplexity.Easy,
            "MIDDLE" => AssemblyComplexity.Middle,
            "HARD" => AssemblyComplexity.Hard,
            _ => AssemblyComplexity.Easy
        };
        var constructor =
            new Constructor(ageLimit.Text, theme.Text, name.Text, price, numberOfDetails, dif);
        _returnInput = constructor.Clone();
        _window.Close();
    }
    private void CreateRailwayInputForm()
    {
        var title = new[] { "AGELIMIT", "NAME", "PRICE", "RAILROADLENGTH", "TRAINMODEL" };
        _window.Height = 170;
        _window.Width = 200;
        InputFields(title);
        var button = new Button();
        button.Height = 40;
        button.Width = 60;
        button.Click += ObjectRailway;
        button.Content = "OK";
        stack.Children.Add(button);
        _window.Content = stack;
    }
    private void ObjectRailway(object sender, RoutedEventArgs e)
    {
        TextBox name, priceTBox, ageLimit, trainModel, railRoadLengthTBox;
        var temp = new TextBox();
        temp.Text = "NONE";
        int price;
        short railRoadLength;
        try
        {
            ageLimit = (TextBox)stack.Children[0];
            name = (TextBox)stack.Children[1];
            priceTBox = (TextBox)stack.Children[2];
            railRoadLengthTBox = (TextBox)stack.Children[3];
            trainModel = (TextBox)stack.Children[4];
            price = Convert.ToInt32(priceTBox.Text);
            railRoadLength = Convert.ToInt16(railRoadLengthTBox.Text);
            
        }
        catch
        {
            MessageBox.Show("Invalid data", "Attention");
            name = trainModel = ageLimit = temp;
            price = 0;
            railRoadLength = 0;
        }

        var railway = new Railway(ageLimit.Text, name.Text, price, railRoadLength, trainModel.Text);
        _returnInput = railway.Clone();
        _window.Close();
    }
    private void CreateDollInputForm()
    {
        var title = new[] { "NAME", "PRICE", "AGE LIMIT"};
        var hair = new[] { "BROWN", "YELLOW", "BLACK", "RED", "ORANGE" };
        var combo = new ComboBox();
        _window.Height = 150;
        _window.Width = 200;
        InputFields(title);
        combo = InputComboBox(hair);
        stack.Children.Add(combo);
        var button = new Button();
        button.Height = 40;
        button.Width = 60;
        button.Click += ObjectDoll;
        button.Content = "OK";
        stack.Children.Add(button);
        _window.Content = stack;
    }
    private void ObjectDoll(object sender, RoutedEventArgs e)
    {
        TextBox name, priceTBox, ageLimit;
        var temp = new TextBox();
        temp.Text = "NONE";
        var hairComboBox = (ComboBox)stack.Children[3];
        int price;
        try
        {
            name = (TextBox)stack.Children[0];
            priceTBox = (TextBox)stack.Children[1];
            ageLimit = (TextBox)stack.Children[2];
            price = Convert.ToInt32(priceTBox.Text);

        }
        catch
        {
            MessageBox.Show("Invalid data", "Attention");
            name = ageLimit = temp;
            price = 0;
        }
        var hair = hairComboBox.Text switch
        {
            "BROWN" => HairColor.Brown,
            "YELLOW" => HairColor.Yellow,
            "BLACK" => HairColor.Black,
            "RED" => HairColor.Red,
            "ORANGE" => HairColor.Orange,
            _ => HairColor.Black
        };
        var doll = new Doll(ageLimit.Text, name.Text, price, hair);
        _returnInput = doll.Clone();
        _window.Close();
    }
    private void CreateBoardGameInputForm()
    {
        var title = new[] { "NAME", "PRICE", "AGE LIMIT", "THEME", "NUMBER OF PLAYERS"};
        _window.Height = 170;
        _window.Width = 200;
        InputFields(title);
        var button = new Button();
        button.Height = 40;
        button.Width = 60;
        button.Click += ObjectBoardGame;
        button.Content = "OK";
        stack.Children.Add(button);
        _window.Content = stack;
    }
    private void ObjectBoardGame(object sender, RoutedEventArgs e)
    {
        TextBox name, priceTBox, ageLimit, theme, playersTBox;
        var temp = new TextBox();
        temp.Text = "NONE";
        int price;
        byte players;
        try
        {
            name = (TextBox)stack.Children[0];
            priceTBox = (TextBox)stack.Children[1];
            ageLimit = (TextBox)stack.Children[2];
            theme = (TextBox)stack.Children[3];
            playersTBox = (TextBox)stack.Children[4];
            price = Convert.ToInt32(priceTBox.Text);
            players = Convert.ToByte(playersTBox.Text);
        }
        catch
        {
            MessageBox.Show("Invalid data", "Attention");
            name = ageLimit = theme = temp;
            price = 0;
            players = 0;
        }
        var boardGame = new BoardGame(ageLimit.Text, name.Text, price, theme.Text, players);
        _returnInput = boardGame.Clone();
        _window.Close();
    }
    private void CreateToyInputForm()
    {
        var title = new[] { "AGELIMIT", "NAME", "PRICE" };
        _window.Height = 135;
        _window.Width = 200;
        InputFields(title);
        var button = new Button();
        button.Height = 40;
        button.Width = 60;
        button.Click += ObjectToy;
        button.Content = "OK";
        stack.Children.Add(button);
        _window.Content = stack;
    }
    private void ObjectToy(object sender, RoutedEventArgs e)
    {
        TextBox name, priceTBox, ageLimit;
        var temp = new TextBox();
        temp.Text = "NONE";
        int price;
        try
        {
            ageLimit = (TextBox)stack.Children[0];
            name = (TextBox)stack.Children[1];
            priceTBox = (TextBox)stack.Children[2];
            price = Convert.ToInt32(priceTBox.Text);

        }
        catch
        {
            MessageBox.Show("Invalid data", "Attention");
            name = ageLimit = temp;
            price = 0;
        }

        var toy = new Toy(ageLimit.Text, name.Text, price);
        _returnInput = toy.Clone();
        _window.Close();
    }
    private void CreateComputerGameInputForm()
    {
        var title = new[] { "NAME", "GAME SIZE", "PRICE"};
        var genre = new[] { "ARCADE", "PLATFORMER", "SHOOTER", "SIMULATOR", "STRATEGY" };
        var combo = new ComboBox();
        _window.Height = 170;
        _window.Width = 200;
        InputFields(title);
        combo = InputComboBox(genre);
        stack.Children.Add(combo);
        var button = new Button();
        button.Height = 40;
        button.Width = 60;
        button.Click += ObjectComputerGame;
        button.Content = "OK";
        stack.Children.Add(button);
        _window.Content = stack;
    }
    private void ObjectComputerGame(object sender, RoutedEventArgs e)
    {
        TextBox name, priceTBox, sizeTBox;
        var temp = new TextBox();
        temp.Text = "NONE";
        var genreComboBox = (ComboBox)stack.Children[3];
        int price;
        byte size;
        try
        {
            name = (TextBox)stack.Children[0];
            sizeTBox = (TextBox)stack.Children[1];
            priceTBox = (TextBox)stack.Children[2];
            price = Convert.ToInt32(priceTBox.Text);
            size = Convert.ToByte(sizeTBox.Text);

        }
        catch
        {
            MessageBox.Show("Invalid data", "Attention");
            name = temp;
            price = 0;
            size = 0;
        }

        var genre = genreComboBox.Text switch
        {
            "ARCADE" => GameGenre.Arcade,
            "PLATFORMER" => GameGenre.Platformer,
            "SHOOTER" => GameGenre.Shooter,
            "SIMULATOR" => GameGenre.Simulator,
            "STRATEGY" => GameGenre.Strategy,
            _ => GameGenre.Arcade
        };
        var computerGame = new ComputerGame(name.Text, genre, price, size);
        _returnInput = computerGame.Clone();
        _window.Close();
    }
    private void CreateHideNSeekInputForm()
    {
        var title = new[] { "STREET", "HOUSE", "NUMBER OF PEOPLE" };
        _window.Height = 135;
        _window.Width = 200;
        InputFields(title);
        var button = new Button();
        button.Height = 40;
        button.Width = 60;
        button.Click += ObjectHideNSeek;
        button.Content = "OK";
        stack.Children.Add(button);
        _window.Content = stack;
    }
    private void ObjectHideNSeek(object sender, RoutedEventArgs e)
    {
        TextBox street, peopleTBox, house;
        var temp = new TextBox();
        temp.Text = "NONE";
        byte people;
        try
        {
            street = (TextBox)stack.Children[0];
            house = (TextBox)stack.Children[1];
            peopleTBox = (TextBox)stack.Children[2];
            people = Convert.ToByte(peopleTBox.Text);

        }
        catch
        {
            MessageBox.Show("Invalid data", "Attention");
            street = house = temp;
            people = 0;
        }

        var hns = new HideNSeek(street.Text, house.Text, people, 0);
        _returnInput = hns.Clone();
        _window.Close();
    }
}