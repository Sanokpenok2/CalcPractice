using System;
using System.Windows;
using System.Windows.Controls;

namespace CalcPractice
{
    public partial class MainWindow : Window
    {
        private double _firstNumber;
        private double _secondNumber;
        private string _operation;
        private bool _isNewNumber;
        private bool _isFirstNumber;

        public MainWindow()
        {
            InitializeComponent();
            ResetCalculator();
        }

        private void ResetCalculator()
        {
            _firstNumber = 0;
            _secondNumber = 0;
            _operation = "";
            _isNewNumber = true;
            _isFirstNumber = true;
            Display.Text = "0";
            OperationDisplay.Text = "";
        }

        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string number = button.Content.ToString();

            if (Display.Text == "0" || _isNewNumber)
            {
                Display.Text = number;
                _isNewNumber = false;
            }
            else
            {
                Display.Text += number;
            }
        }

        private void BtnDecimal_Click(object sender, RoutedEventArgs e)
        {
            if (!Display.Text.Contains(","))
            {
                Display.Text += ",";
                _isNewNumber = false;
            }
        }

        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            if (_isNewNumber && (_operation != "" || _isFirstNumber) && button.Content.ToString() == "-")
            {
                if (_isFirstNumber)
                {
                    _firstNumber = double.Parse(Display.Text) * (-1);
                }
                Display.Text = "-";
                _isNewNumber = false;
                return;
            }

            _firstNumber = double.Parse(Display.Text);
            _operation = button.Content.ToString();

            OperationDisplay.Text = _operation;
            _isNewNumber = true;
            _isFirstNumber = false;
        }

        private void BtnEquals_Click(object sender, RoutedEventArgs e)
        {
            if (!_isNewNumber)
            {
                _secondNumber = double.Parse(Display.Text);
                double result = 0;

                switch (_operation)
                {
                    case "+":
                        result = _firstNumber + _secondNumber;
                        break;
                    case "-":
                        result = _firstNumber - _secondNumber;
                        break;
                    case "×":
                        result = _firstNumber * _secondNumber;
                        break;
                    case "/":
                        if (_secondNumber != 0)
                            result = _firstNumber / _secondNumber;
                        else
                            MessageBox.Show("Нельзя делить на ноль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;
                    case "%":
                        result = _firstNumber * _secondNumber / 100;
                        break;
                    case "^":
                        result = Math.Pow(_firstNumber, _secondNumber);
                        break;
                }

                Display.Text = result.ToString();
                _isNewNumber = true;
                _isFirstNumber = false;
                _operation = "";
                OperationDisplay.Text = "";
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            ResetCalculator();
        }

        private void BtnBackspace_Click(object sender, RoutedEventArgs e)
        {
            if (Display.Text.Length > 1)
            {
                Display.Text = Display.Text.Substring(0, Display.Text.Length - 1);
            }
            else
            {
                Display.Text = "0";
                _isNewNumber = true;
            }
        }
    }
}