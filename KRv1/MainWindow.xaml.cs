using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace KRv1
{
    [Serializable]
    public partial class MainWindow : Window
    {
        private Window _windowParameters = new Window();
        private List<object> _listObject;
        private List<Func<string>> _delegateList = new List<Func<string>>();
        private string? _currentClass = null;
        private bool _isClosing = false;
        
        public MainWindow()
        {
            InitializeComponent();
            _listObject = new List<object>();
            LoadFile();
        }

        private void DeleteButton(object sender, RoutedEventArgs e)
        {
            if (ListObjectListBox.SelectedIndex == -1) return;
            var index = ListObjectListBox.SelectedIndex;
            ListObjectListBox.Items.RemoveAt(index);
            _listObject.RemoveAt(index);
            if (ListObjectListBox.SelectedIndex == -1)
                MessageBox.Show("Successful deletion", "Delete");
            ComboBoxMethod.Text = "";
            ComboBoxMethod.IsEnabled = false;
        }

        private void GroupBoxSwitch(object sender, RoutedEventArgs e)
        {
            if (_currentClass == null) return;
            switch (_currentClass)
            {
                case "Constructor":
                {
                    var inputWindow = new InputWindow(_currentClass);
                    _windowParameters = inputWindow.CreateSpecialWindow();
                    _windowParameters.ShowDialog();
                    if (inputWindow.ReturnObject() == null) break;
                    var constructor = (Constructor)inputWindow.ReturnObject();
                    _listObject.Add(constructor);
                    ListObjectListBox.Items.Add(constructor.ToString());
                    break;
                }
                case "Railway":
                {
                    var inputWindow = new InputWindow(_currentClass);
                    _windowParameters = inputWindow.CreateSpecialWindow();
                    _windowParameters.ShowDialog();
                    if (inputWindow.ReturnObject() == null) break;
                    var railway = (Railway)inputWindow.ReturnObject();
                    _listObject.Add(railway);
                    ListObjectListBox.Items.Add(railway.ToString());
                    break;
                }
                case "Doll":
                {
                    var inputWindow = new InputWindow(_currentClass);
                    _windowParameters = inputWindow.CreateSpecialWindow();
                    _windowParameters.ShowDialog();
                    if (inputWindow.ReturnObject() == null) break;
                    var doll = (Doll)inputWindow.ReturnObject();
                    _listObject.Add(doll);
                    ListObjectListBox.Items.Add(doll.ToString());
                    break;
                }
                case "Board game":
                {
                    var inputWindow = new InputWindow(_currentClass);
                    _windowParameters = inputWindow.CreateSpecialWindow();
                    _windowParameters.ShowDialog();
                    if (inputWindow.ReturnObject() == null) break;
                    var boardGame = (BoardGame)inputWindow.ReturnObject();
                    _listObject.Add(boardGame);
                    ListObjectListBox.Items.Add(boardGame.ToString());
                    break;
                }
                case "Computer game":
                {
                    var inputWindow = new InputWindow(_currentClass);
                    _windowParameters = inputWindow.CreateSpecialWindow();
                    _windowParameters.ShowDialog();
                    if (inputWindow.ReturnObject() == null) break;
                    var computerGame = (ComputerGame)inputWindow.ReturnObject();
                    _listObject.Add(computerGame);
                    ListObjectListBox.Items.Add(computerGame.ToString());
                    break;
                }
                case "Toy":
                {
                    var inputWindow = new InputWindow(_currentClass);
                    _windowParameters = inputWindow.CreateSpecialWindow();
                    _windowParameters.ShowDialog();
                    if (inputWindow.ReturnObject() == null) break;
                    var toy = (Toy)inputWindow.ReturnObject();
                    _listObject.Add(toy);
                    ListObjectListBox.Items.Add(toy.ToString());
                    break;
                }
                case "Hide and Seek":
                {
                    var inputWindow = new InputWindow(_currentClass);
                    _windowParameters = inputWindow.CreateSpecialWindow();
                    _windowParameters.ShowDialog();
                    if (inputWindow.ReturnObject() == null) break;
                    var hideNSeek = (HideNSeek)inputWindow.ReturnObject();
                    _listObject.Add((hideNSeek));
                    ListObjectListBox.Items.Add(hideNSeek.ToString());
                    break;
                }
            }
        }

        private void RadioButton_OnChecked(object sender, RoutedEventArgs e)
        {
            if (sender is not RadioButton rdSender) return;
            _currentClass = Convert.ToString(rdSender.Content);
            ConsoleBox.Text = Convert.ToString(rdSender.Content);
        }

        private void MainWindow_OnClosing(object? sender, CancelEventArgs e)
        {
            _isClosing = true;
            Application.Current.Shutdown();
        }

        private void ListObjectListBox_OnPreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            _delegateList = new List<Func<string>>();
            ComboBoxMethod.IsEnabled = true;
            ComboBoxMethod.Items.Clear();
            var selectClass = (string)ListObjectListBox.Items[ListObjectListBox.SelectedIndex];
            selectClass = selectClass.Substring(9, 1);
            switch (selectClass)
            {
                case "0":
                { 
                    var variableClass = (Constructor)_listObject[ListObjectListBox.SelectedIndex];
                    _delegateList = variableClass.DelegateList();
                    break;
                }
                case "1":
                {
                    var variableClass = (Railway)_listObject[ListObjectListBox.SelectedIndex];
                    _delegateList = variableClass.DelegateList();
                    break;
                }
                case "2":
                {
                    var variableClass = (Doll)_listObject[ListObjectListBox.SelectedIndex];
                    _delegateList = variableClass.DelegateList();
                    break;
                }
                case "3":
                {
                    var variableClass = (BoardGame)_listObject[ListObjectListBox.SelectedIndex];
                    _delegateList = variableClass.DelegateList();
                    break;
                }
                case "4":
                {
                    var variableClass = (ComputerGame)_listObject[ListObjectListBox.SelectedIndex];
                    _delegateList = variableClass.DelegateList();
                    break;
                }
                case "5":
                {
                    var variableClass = (Toy)_listObject[ListObjectListBox.SelectedIndex];
                    _delegateList = variableClass.DelegateList();
                    break;
                }
                case "6":
                {
                    var variableClass = (HideNSeek)_listObject[ListObjectListBox.SelectedIndex];
                    _delegateList = variableClass.DelegateList();
                    break;
                }
                default: return;

            }
            foreach (var i in _delegateList)
            {
                var comboItem = new ComboBoxItem();
                comboItem.Content = i.Method.Name;
                ComboBoxMethod.Items.Add(comboItem);
            }
        }

        private void RunMethodClick(object sender, RoutedEventArgs e)
        {
            byte index = 0;
            if (ComboBoxMethod.Text == "" || ComboBoxMethod.IsEnabled == false)
            {
                ConsoleBox.Text = "You didn't choose the method";
                return;
            }
            for (byte i = 0; i < _delegateList.Count; i++)
            {
                if (_delegateList[i].Method.Name == ComboBoxMethod.Text)
                {
                    index = i;
                }
            }
            ConsoleBox.Text = _delegateList[index].Invoke();
        }

        private void SaveClickButton(object sender, RoutedEventArgs e)
        {
            var fs = new FileStream("KRSaveFile", FileMode.Create);
            var formatter = new BinaryFormatter();
            var union = new List<List<object>>();
            var ListBox = new List<object>();
            foreach (var i in ListObjectListBox.Items)
            {
                ListBox.Add(i);
            }
            union.Add(_listObject);
            union.Add(ListBox);
            formatter.Serialize(fs, union);
            fs.Close();
        }
        private void LoadFile()
        {
            var fileInfo = new FileInfo("KRSaveFile");
            if (!fileInfo.Exists)
            {
                fileInfo.Create();
                return;
            }
            if (fileInfo.Length != 0)
            {
                var union = new List<List<object>>();
                var fs = new FileStream("KRSaveFile", FileMode.Open);
                var formatter = new BinaryFormatter();
                union = (List<List<object>>)formatter.Deserialize(fs);
                foreach (var i in union[0])
                {
                    _listObject.Add(i);
                }
                foreach (var i in union[1])
                {
                    ListObjectListBox.Items.Add(i);
                }
                fs.Close();
            }
        }
    }
}