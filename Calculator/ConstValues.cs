using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Calculator
{
    /// <summary>
    /// ConstantValues class
    /// </summary>
    class ConstValues
    {
        internal const int InputUpperLimit = 9;
        internal const int InputLowerLimit = 1;
        /// <summary>
        /// result screen length.
        /// </summary>
        internal const int ResultLengthLimit = 12;
        /// <summary>
        /// Max value possible.
        /// </summary>
        internal const int ResultValueLimit = 999999999;
        internal const int ResultValueLimitSigned = -999999999;
        internal const decimal DefaultValue = 0;
        /// <summary>
        /// History max characters.
        /// </summary>
        internal const int HistoryLimit = 40;
        internal const string Zero = "0";
        internal const string One = "1";
        internal const string Two  = "2";
        internal const string Three = "3";
        internal const string Four = "4";
        internal const string Five  = "5";
        internal const string Six = "6";
        internal const string Seven = "7";
        internal const string Eight = "8";
        internal const string Nine = "9";
        internal const string Decimal = ".";
        internal const string Plus = "+";
        internal const string Minus = "-";
        internal const string Multiply = "*";
        internal const string Divide = "/";
        internal const string Equal = "=";        
        internal const string DivideByZeroException = "Cannot divide by 0";
        internal const string Overflow = " Result is out of Limit";
        internal const string FormateType = "0.############"; 


    }
}
