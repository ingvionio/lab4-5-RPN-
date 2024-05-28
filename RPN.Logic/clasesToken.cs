using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPN.Logic
{
    public abstract class Token
    {

    }

    public class Variable : Token
    {
        public string symbol;
        public override string ToString()
        {
            return symbol.ToString();
        }
    }

    public class Parenthesis : Token
    {
        public char Value;

        public override string ToString()
        {
            return Value.ToString();
        }
    }

    public class Number : Token
    {
        public double Value;
        public override string ToString()
        {
            return Value.ToString();
        }
    }

}
