using PropertyChanged;
using static Calculator.MVVM.Views.CalculatorView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using System;
using System.Collections;
using System.Globalization;


namespace Calculator.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class CalculatorViewModel : INotifyPropertyChanged
    {
        int currentState = 1;
        string operatorMath;
        public double firstNum { get; set; } = 0;

        public double secondNum { get; set; } = 0;

        public string result { get; set; } = "0";

        public string operation { get; set; } = "";

        public string Operator { get; set; } = null;

        public string Operation
        {
            get
            { return operation; }
            set
            {
                operation = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Operation"));
            }
            
        }


        public ICommand Insert_digitCommand => new Command<string>(Insert_digit);
        public ICommand OperatorCommand => new Command<string>(OperatorMath);
        public ICommand PercentageCommand => new Command<string>(Percentage);
        public ICommand BackspaceCommand => new Command(Backspace);
        public ICommand ClearCommand => new Command(Clear);
        public ICommand CalcCommand => new Command(Calc);

        public event PropertyChangedEventHandler PropertyChanged;

        private void Insert_digit(string parameterValue)
        {
            Operation += parameterValue;


            if (Operation.StartsWith("00"))
            {
                Operation = Operation.Substring(1);
            }


            if (Operation.StartsWith("."))
            {
                Operation = "0,";
            }
        }

        private void OperatorMath(string parameterValue)
        {
            Operator = parameterValue;
            Operation += parameterValue;
        }

        private void Percentage(string parameterValue)
        {
            string number;


                if (Operator == "" || Operator == null)
                {
                    number = Operation;
                    Operation = (Convert.ToDouble(number) / 100).ToString();
                }
                else
                {
                    number = Operation.Substring(Operation.LastIndexOf(Operator) + 1);
                    Operation = Operation.Substring(0, Operation.LastIndexOf(Operator) + 1) + (Convert.ToDouble(number) / 100).ToString();
                }


        }

        private void Backspace()
        {
            if (Operation.Length > 1 || Operation != "")
            {
                Operation = Operation.Substring(0, Operation.Length - 1);
            }
        }

        private void Clear()
        {
            Operation = String.Empty;
            Operator = null;
            firstNum = 0;
            secondNum = 0;
            result = "0";
        }

        private void Calc()
        {
 
                double result = 0;

            if (String.IsNullOrEmpty(Operation))
            {
                return;
            }


            if (Operation.LastIndexOf(Operator) > -1)
            {
                if (firstNum == 0)
                {
                    firstNum = Convert.ToDouble(Operation.Substring(0, Operation.LastIndexOf(Operator)));
                }

                secondNum = Convert.ToDouble(Operation.Substring(Operation.LastIndexOf(Operator) + 1));
            }

            switch (Operator)
                {
                    case "/":
                        result = firstNum / secondNum;
                        break;
                    case "*":
                        result = firstNum * secondNum;
                        break;
                    case "-":
                        result = firstNum - secondNum;
                        break;
                    case "+":
                        result = firstNum + secondNum;
                        break;

                    default:
                        break;
                }
                Operation = result.ToString();

        }
    }
}
