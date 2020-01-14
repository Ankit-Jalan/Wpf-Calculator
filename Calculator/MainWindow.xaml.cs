using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        #region private members

        //Stores result after calculation
        private decimal resultValue = ConstValues.DefaultValue;          
  
        //check previous value inputed is operand or not.
        private bool isPreviousOperand = true;

        // store previous operator value.
        private string previousOperator = ConstValues.Equal;

        //store repeating opeation operand.
        private decimal recentCalculationValue;

        //Store repeating  operation operator.
        private string recentCalculationOperator = string.Empty;

        //Notify is there any incorrect state.
        private bool isIncorrectState = false;

        #endregion  

        /// <summary>
        /// Main method
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        #region  event

        /// <summary>
        /// Operand event handler.
        /// All operand values is taken care.
        /// </summary>
        /// <param name="sender"> Button object</param>
        /// <param name="e"> RoutedEventArgs object</param>
        private void operand_Click(object sender, RoutedEventArgs e)
        {
            Button operand = sender as Button;            
            OperandInput(operand.Content.ToString());
        }

        /// <summary>
        /// Clear result Screen and make it zero.
        /// </summary>
        /// <param name="sender"> Button object</param>
        /// <param name="e"> RoutedEventArgs object</param>
        private void clear_Click(object sender, RoutedEventArgs e)
        {
            ClearOperation();
        }

        /// <summary>
        /// Clear one character when the operator is not  pressed.
        /// </summary>
        /// <param name="sender"> Button object</param>
        /// <param name="e"> RoutedEventArgs object</param>
        private void backspace_Click(object sender, RoutedEventArgs e)
        {
            BackSpaceOperation();
        }

        /// <summary>
        /// Operators event handler.
        /// All operation is handled here.
        /// </summary>
        /// <param name="sender"> Button object</param>
        /// <param name="e"> RoutedEventArgs object</param>
        private void operator_Click(object sender, RoutedEventArgs e)
        {
            Button operatorButton = sender as Button;            
            OperatorInput(operatorButton.Content.ToString());
        }

        /// <summary>
        /// Assiment task is handled.
        /// Related functionality is mantianed.
        /// </summary>
        /// <param name="sender"> Button object</param>
        /// <param name="e"> RoutedEventArgs object</param>
        private void equal_Click(object sender, RoutedEventArgs e)
        {
            EqualOperation();
        }

        /// <summary>
        /// All button keys is handled
        /// </summary>
        /// <param name="sender">sender object of Windows</param>
        /// <param name="e"> KeyEventArgs object</param>
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (Keyboard.Modifiers)
            {
                case ModifierKeys.None:

                    switch (e.Key)
                    {
                        //operand input upto decimal case.
                        case Key.D1:
                        case Key.NumPad1:
                            OperandInput(ConstValues.One);
                            break;

                        case Key.D2:
                        case Key.NumPad2:
                            OperandInput(ConstValues.Two);
                            break;

                        case Key.D3:
                        case Key.NumPad3:
                            OperandInput(ConstValues.Three);
                            break;

                        case Key.D4:
                        case Key.NumPad4:
                            OperandInput(ConstValues.Four);
                            break;

                        case Key.D5:
                        case Key.NumPad5:
                            OperandInput(ConstValues.Five);
                            break;

                        case Key.D6:
                        case Key.NumPad6:
                            OperandInput(ConstValues.Six);
                            break;

                        case Key.D7:
                        case Key.NumPad7:
                            OperandInput(ConstValues.Seven);
                            break;

                        case Key.D8:
                        case Key.NumPad8:
                            OperandInput(ConstValues.Eight);
                            break;

                        case Key.D9:
                        case Key.NumPad9:
                            OperandInput(ConstValues.Nine);
                            break;

                        case Key.D0:
                        case Key.NumPad0:
                            OperandInput(ConstValues.Zero);
                            break;

                        case Key.Decimal:
                        case Key.OemPeriod:
                            OperandInput(ConstValues.Decimal);
                            break;

                        // Operator input.
                        case Key.Divide:
                        case Key.OemQuestion:
                            OperatorInput(ConstValues.Divide);
                            break;

                        case Key.Subtract:
                        case Key.OemMinus:
                            OperatorInput(ConstValues.Minus);
                            break;

                        case Key.Enter:
                        case Key.OemPlus:
                            EqualOperation();
                            break;

                        case Key.Add:
                            OperatorInput(ConstValues.Plus);
                            break;

                        case Key.Multiply:
                            OperatorInput(ConstValues.Multiply);
                            break;

                        //Delete result Screen value and make it zero.
                        case Key.Delete:
                            ClearOperation();
                            break;

                        // Remove one character from input.
                        case Key.Back:
                            BackSpaceOperation();
                            break;
                        // Reset to Default state of program.
                        case Key.Escape:
                            ResetToDefaultState();
                            break;

                    }
                    break;

                case ModifierKeys.Shift:
                    if (e.Key == Key.D8)
                    {
                        OperatorInput(ConstValues.Multiply);
                    }
                    else if (e.Key == Key.OemPlus)
                    {
                        OperatorInput(ConstValues.Plus);
                    }
                    break;

            }

        }

        #endregion

        #region methods

        /// <summary>
        /// Manage Light Screen or History Screen. 
        /// </summary>
        /// <param name="operatorStr"> Parameter contains operator Value</param>
        private void HistoryScreenDisplay(string operatorStr)
        {
            //Operand Case
            if (isPreviousOperand)
            {
                historyScreen.Text += resultScreen.Text + operatorStr;
                while (historyScreen.Text.Length > ConstValues.HistoryLimit)
                {
                    int index;
                    char[] operators = { '+', '-', '*', '/' };
                    index = historyScreen.Text.IndexOfAny(operators);
                    historyScreen.Text = historyScreen.Text.Remove(0, index + 1);
                }
            }
            else
            {
                if (previousOperator != ConstValues.Equal)
                {
                    historyScreen.Text = historyScreen.Text.Remove(historyScreen.Text.Length - 1);
                    historyScreen.Text += operatorStr;
                }
                else
                {
                    historyScreen.Text = resultScreen.Text + operatorStr;
                }
            }                            
        }        

        /// <summary>
        /// Perform Addition
        /// </summary>
        /// <param name="operandOne"> First Operand value</param>
        /// <param name="operandTwo">Second Operand value </param>
        /// <returns></returns>
        private decimal Add(decimal operandOne, decimal operandTwo)
        {                       
            return operandOne + operandTwo;                        
        }

        /// <summary>
        /// Perform Substration
        /// </summary>
        /// <param name="operandOne"> First Operand value</param>
        /// <param name="operandTwo">Second Operand value </param>
        /// <returns></returns>
        private decimal Subtract(decimal operandOne, decimal operandTwo)
        {            
            return  operandOne - operandTwo;
        }

        /// <summary>
        /// Perform Multiplication
        /// </summary>
        /// <param name="operandOne"> First Operand value</param>
        /// <param name="operandTwo">Second Operand value </param>
        /// <returns></returns>
        private decimal Multiply(decimal operandOne , decimal operandTwo)
        {
            return operandOne * operandTwo;
        }

        /// <summary>
        /// Perform Division
        /// </summary>
        /// <param name="operandOne"> First Operand value</param>
        /// <param name="operandTwo">Second Operand value </param>
        /// <returns></returns>
        private decimal Divide(decimal operandOne , decimal operandTwo)
        {
            return operandOne / operandTwo;
        }        

        /// <summary>
        /// Take operand input.
        /// </summary>
        /// <param name="operandInputValue">operand</param>
        private void OperandInput(String operandInputValue)
        {
            if (!isIncorrectState)
            {
                //Condition allow up to 9 digit which can contain one decimal symbol.
                if ((resultScreen.Text.Length < ConstValues.InputUpperLimit && !(resultScreen.Text.Contains(ConstValues.Decimal) && operandInputValue == ConstValues.Decimal)) || !isPreviousOperand)
                {
                    if (!isPreviousOperand)
                    {
                        resultScreen.Text = ConstValues.Zero;
                    }
                    if ( resultScreen.Text == ConstValues.Zero && operandInputValue != ConstValues.Decimal)
                    {
                        resultScreen.Text = operandInputValue;                      
                    }
                    else
                    {
                        resultScreen.Text +=  operandInputValue;
                    }                                        
                    isPreviousOperand = true;
                }
            }
            else
            {
                ResetToDefaultState();
                OperandInput(operandInputValue);
            }
        }

        /// <summary>
        ///Take operator value and call operator method
        /// </summary>
        /// <param name="operatorInputValue">Operator</param>
        private void OperatorInput(String operatorInputValue)
        {
            if (!isIncorrectState)
            {
                resultScreen.Text = decimal.Parse(resultScreen.Text).ToString(ConstValues.FormateType);
                HistoryScreenDisplay(operatorInputValue);
                if (isPreviousOperand)
                {
                    switch (previousOperator)
                    {
                        case ConstValues.Plus:
                            resultValue = Add(resultValue, decimal.Parse(resultScreen.Text));
                            break;

                        case ConstValues.Minus:
                            resultValue = Subtract(resultValue, decimal.Parse(resultScreen.Text));
                            break;

                        case ConstValues.Multiply:
                            resultValue = Multiply(resultValue, decimal.Parse(resultScreen.Text));
                            break;

                        case ConstValues.Divide:
                            try
                            {
                                resultValue = Divide(resultValue, decimal.Parse(resultScreen.Text));
                            }
                            catch (DivideByZeroException)
                            {
                                historyScreen.Text = string.Empty;
                                resultScreen.Text = ConstValues.DivideByZeroException;
                                isIncorrectState = true;
                            }
                            break;

                        case ConstValues.Equal:
                            resultValue = decimal.Parse(resultScreen.Text);
                            break;
                    }
                }                
                resultValue = ResultLimitCheck(resultValue);
                if (!isIncorrectState)
                {
                    resultScreen.Text = resultValue.ToString(ConstValues.FormateType);
                }                                                                                                                                              
                isPreviousOperand = false;
                previousOperator = operatorInputValue;                
            }
        }

        /// <summary>
        /// Perform equal operation
        /// </summary>
        private void EqualOperation()
        {            
            if (!isIncorrectState)
            {
                resultScreen.Text = decimal.Parse(resultScreen.Text).ToString(ConstValues.FormateType);
                if (previousOperator != ConstValues.Equal)
                {
                    recentCalculationOperator = previousOperator;
                    recentCalculationValue = decimal.Parse(resultScreen.Text);
                }
                switch (previousOperator)
                {
                    case ConstValues.Plus:
                        resultValue = Add(resultValue, decimal.Parse(resultScreen.Text));
                        break;

                    case ConstValues.Minus:
                        resultValue = Subtract(resultValue, decimal.Parse(resultScreen.Text));
                        break;

                    case ConstValues.Multiply:
                        resultValue = Multiply(resultValue, decimal.Parse(resultScreen.Text));
                        break;

                    case ConstValues.Divide:
                        try
                        {
                            resultValue = Divide(resultValue, decimal.Parse(resultScreen.Text));
                        }
                        catch (DivideByZeroException)
                        {
                            historyScreen.Text = string.Empty;
                            resultScreen.Text = ConstValues.DivideByZeroException;
                            isIncorrectState = true;
                        }
                        break;

                    case ConstValues.Equal:

                        if (recentCalculationOperator == ConstValues.Plus)
                        {
                            resultValue = Add(decimal.Parse(resultScreen.Text), recentCalculationValue);                            
                        }
                        else if (recentCalculationOperator == ConstValues.Minus)
                        {
                            resultValue = Subtract(decimal.Parse(resultScreen.Text), recentCalculationValue);                            
                        }
                        else if (recentCalculationOperator == ConstValues.Multiply)
                        {
                            resultValue = Multiply(decimal.Parse(resultScreen.Text), recentCalculationValue);                            
                        }
                        else if (recentCalculationOperator == ConstValues.Divide)
                        {
                            try
                            {
                                resultValue = Divide(decimal.Parse(resultScreen.Text), recentCalculationValue);
                            }
                            catch (DivideByZeroException)
                            {
                                historyScreen.Text = string.Empty;
                                resultScreen.Text = ConstValues.DivideByZeroException;
                                isIncorrectState = true;
                            }
                        }
                        else
                        {
                            resultValue = decimal.Parse(resultScreen.Text);
                        }
                        break;

                }
                resultValue = ResultLimitCheck(resultValue);

                if (!isIncorrectState)
                {
                    resultScreen.Text = resultValue.ToString(ConstValues.FormateType);
                }                                                     
                isPreviousOperand = false;
                previousOperator = ConstValues.Equal;
                historyScreen.Text = string.Empty;
            }
            else
            {
                ResetToDefaultState();
            }
        }

        /// <summary>
        /// Clear one character form input
        /// </summary>
        private void BackSpaceOperation()
        {
            if (!isIncorrectState)
            {
                if (isPreviousOperand)
                {
                    if (resultScreen.Text.Length > ConstValues.InputLowerLimit)
                    {
                        resultScreen.Text = resultScreen.Text.Remove(resultScreen.Text.Length - 1);
                    }
                    else if (resultScreen.Text.Length == ConstValues.InputLowerLimit)
                    {
                        resultScreen.Text = ConstValues.Zero;
                    }
                }
            }
            else
            {
                ResetToDefaultState();
            }
        }

        /// <summary>
        /// Clear input or result Screen.
        /// </summary>
        private void ClearOperation()
        {
            if (!isIncorrectState)
            {
                resultScreen.Text = ConstValues.Zero;
                if (previousOperator == ConstValues.Equal)
                {                                        
                    resultValue = ConstValues.DefaultValue;
                }                               
            }
            else 
            {
                ResetToDefaultState();
            }
        }

        /// <summary>
        /// Check that result value is under limit value and if not than pass overflow statement.
        /// Check result length and Round up value up to length limit.
        /// </summary>
        /// <param name="resultChekingValue">Calculated result value</param>
        /// <returns>return updated resutl Value</returns>
        private decimal ResultLimitCheck(decimal resultChekingValue)
        {
            //If result value is out of limit.
            if (resultChekingValue > ConstValues.ResultValueLimit || resultChekingValue < ConstValues.ResultValueLimitSigned)
            {
                resultScreen.Text = ConstValues.Overflow;                                                              
                historyScreen.Text = string.Empty;
                isIncorrectState = true;
            }
            else
            {
                if (resultChekingValue.ToString().Length > ConstValues.ResultLengthLimit)
                {
                    int index = resultChekingValue.ToString().IndexOf(ConstValues.Decimal) + 1;                   
                    resultChekingValue = Math.Round(resultChekingValue,ConstValues.ResultLengthLimit - index);                    
                }                
            }
            return resultChekingValue;
        }        

        /// <summary>
        /// Set all by default value.
        /// </summary>
        private void ResetToDefaultState()
        {
            resultScreen.Text = ConstValues.Zero;
            resultValue = ConstValues.DefaultValue;
            isPreviousOperand = true;
            previousOperator = ConstValues.Equal;
            recentCalculationOperator = string.Empty;
            recentCalculationValue = ConstValues.DefaultValue;            
            historyScreen.Text= string.Empty;
            isIncorrectState = false;
        }
        
        #endregion
    }
}