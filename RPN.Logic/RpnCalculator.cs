using System.ComponentModel.Design;
using System.Text;

namespace RPN.Logic
{
    public abstract class Token
    {

    }

    public class Variable : Token
    {
        public char symbol;
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

    class TokenCreate
    {
        private static List<Operation> _availableOperations;

        private TokenCreate ()
        {
            _availableOperations = null;
        }

        //private static Token CreateOperation<T>(T value)
        //{          


        //}

        public static Operation FindAvilableByName(string name)
        {
            if (_availableOperations == null)
            {
                var parent = typeof(Operation);
                var allAssemblies = AppDomain.CurrentDomain.GetAssemblies();
                var types = allAssemblies.SelectMany(assembly => assembly.GetTypes());
                var inheritingTypes = types.Where(t => parent.IsAssignableFrom(t) && !t.IsAbstract).ToList();

                _availableOperations = inheritingTypes.Select(type => (Operation)Activator.CreateInstance(type)).ToList();
            }

            return _availableOperations.FirstOrDefault(op => op.Name.Equals(name));

        }
    }

    public class RpnCalculator
    {
        private readonly List<Token> _rpn;
        private readonly List<char> _variableNames = new() { 'x' } ;
   
        public static List<Token> Parse(string input, Dictionary<char, int> index)
        {
            List<Token> tokenList = new List<Token>();
            for (int i = 0; i < input.Length; i++)
            {
                if ((char.IsDigit(input[i]) == false) && (input[i] != ' '))
                {
                    if (char.IsAsciiLetter(input[i]) && input[i] == 'x')
                    {
                        tokenList.Add(new Variable { symbol = input[i] });
                    }
                    else if (input[i] == '(' || input[i] == ')')
                    {
                        tokenList.Add(new Parenthesis { Value = input[i] });
                    }
                    else if (char.IsAsciiLetter(input[i]) && input[i] != 'x')
                    {
                        StringBuilder operation = new StringBuilder();
                        while (i!= input.Length-1 && input[i] != ' '&& input[i] != 'x'&& (char.IsDigit(input[i]) == false))
                        {
                            operation.Append(input[i]);
                            i++;
                        }

                        i--;
                        tokenList.Add(TokenCreate.FindAvilableByName(Convert.ToString(operation)));
                    }
                    else
                    {
                        tokenList.Add(TokenCreate.FindAvilableByName(Convert.ToString(input[i])));
                    }
                }

                else if ((char.IsDigit(input[i]) == true) && (input[i] != ' '))
                {
                    string number = null;

                    while (char.IsDigit(input[i]) == true)
                    {
                        number += input[i];
                        i++;
                        if (i == input.Length)
                            break;
                    }

                    tokenList.Add(new Number { Value = Convert.ToDouble(number) });
                    i--;
                }
            }

            return tokenList;
        }

        //public static double PerformСalculation(string input, double variableValue)
        //{
        //    Dictionary<char, int> index = new Dictionary<char, int>();
        //    index.Add('+', 1);
        //    index.Add('-', 1);
        //    index.Add('*', 2);
        //    index.Add('/', 2);

        //    List<Token> rpn = ToRPN(Parse(input, index), index);


        //    return Calculate(rpn, variableValue);
        //}

        public static List<Token> ToRPN(List<Token> tokenList, Dictionary<char, int> operations)
        {
            Stack<Token> stack = new Stack<Token>();
            List<Token> RPN = new List<Token>();

            for (int i = 0; i < tokenList.Count; i++)
            {
                if (tokenList[i] is Number || tokenList[i] is Variable)
                {
                    RPN.Add(tokenList[i]);
                }

                //else if (tokenList[i] is Operation)
                //{
                //    if ((stack.Count == 0) || (stack.Peek() is Parenthesis) || ((Operation)tokenList[i]).priority >= ((Operation)stack.Peek()).priority)
                //    {
                //        stack.Push(tokenList[i]);
                //    }
                //    else
                //    {
                //        while (stack.Count != 0 && ((Operation)tokenList[i]).priority < ((Operation)stack.Peek()).priority)
                //        {
                //            RPN.Add(stack.Pop());
                //        }

                //        stack.Push(tokenList[i]);
                //    }
                //}

                else
                {
                    if (((Parenthesis)tokenList[i]).Value == '(')
                    {
                        stack.Push(tokenList[i]);
                    }
                    else
                    {
                        while (stack.Peek() is Parenthesis == false)
                        {
                            RPN.Add(stack.Pop());
                        }

                        stack.Pop();
                    }
                }
            }

            while (stack.Count > 0)
            {
                RPN.Add(stack.Pop());
            }

            return RPN;
        }

        public static double Calculate(List<Token> RPN, double variableValue)
        {
            Stack<double> stack = new Stack<double>();

            for (int i = 0; i < RPN.Count; i++)
            {
                if (RPN[i] is Number)
                {
                    stack.Push(((Number)RPN[i]).Value);
                }

                else if (RPN[i] is Variable)
                {
                    stack.Push(variableValue);
                }

                //else
                //{
                //    switch ((char)((Operation)RPN[i]).Value)
                //    {
                //        case '+':
                //            stack.Push(stack.Pop() + stack.Pop());
                //            continue;
                //        case '-':
                //            stack.Push(-stack.Pop() + stack.Pop());
                //            continue;
                //        case '*':
                //            stack.Push(stack.Pop() * stack.Pop());
                //            continue;
                //        case '/':
                //            double divisor = stack.Pop();
                //            stack.Push(stack.Pop() / divisor);
                //            continue;
                //    }
                //}
            }

            double result = stack.Pop();
            return result;
        }
    }
}