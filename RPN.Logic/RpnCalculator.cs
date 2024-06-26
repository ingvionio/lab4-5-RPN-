﻿using Microsoft.Win32.SafeHandles;
using System.ComponentModel.Design;
using System.Globalization;
using System.Text;

namespace RPN.Logic
{
    public class RpnCalculator
    {
        private readonly List<Token> _rpn;
        private static readonly string[] _variableNames = { "x" } ;
   
        private static List<Token> Parse(string input)
        {
            List<Token> tokenList = new List<Token>();
            for (int i = 0; i < input.Length; i++)
            {
                if ((char.IsDigit(input[i]) == false) && (input[i] != ' '))
                {
                    if (input[i] == '(' || input[i] == ')')
                    {
                        tokenList.Add(new Parenthesis { Value = input[i] });
                    }
                    else if (char.IsAsciiLetter(input[i]))
                    {
                        StringBuilder operation = new StringBuilder();
                        while (i!= input.Length && char.IsAsciiLetter(input[i]))
                        {
                            operation.Append(input[i]);
                            i++;
                        }
                        i--;

                        if (_variableNames.Contains(operation.ToString()))
                        {
                            tokenList.Add(new Variable { symbol = operation.ToString() });
                        }

                        else
                        {
                            tokenList.Add(TokenCreate.CreateOperation(operation.ToString()));
                        }

                    }

                    else
                    {
                        tokenList.Add(TokenCreate.CreateOperation(Convert.ToString(input[i])));
                    }
                }

                else if ((char.IsDigit(input[i]) == true) && (input[i] != ' '))
                {
                    string number = null;

                    while (char.IsDigit(input[i]) == true || input[i] == ',' || input[i] == '.')
                    {
                        number += input[i];
                        i++;
                        if (i == input.Length)
                            break;
                    }

                    tokenList.Add(new Number { Value = Convert.ToDouble(number, CultureInfo.InvariantCulture) });
                    i--;
                }
            }

            return tokenList;
        }

        public static double PerformСalculation(string input, double[] variableValue)
        {
            List<Token> rpn = ToRPN(Parse(input));
            return Calculate(rpn, variableValue);
        }

        private static List<Token> ToRPN(List<Token> _rpn)
        {
            Stack<Token> stack = new Stack<Token>();
            List<Token> RPN = new List<Token>();

            for (int i = 0; i < _rpn.Count; i++)
            {
                if (_rpn[i] is Number || _rpn[i] is Variable)
                {
                    RPN.Add(_rpn[i]);
                }

                else if (_rpn[i] is Operation)
                {
                    if ((stack.Count == 0) || (stack.Peek() is Parenthesis) || ((Operation)_rpn[i]).Priority >= ((Operation)stack.Peek()).Priority)
                    {
                        stack.Push(_rpn[i]);
                    }
                    else
                    {
                        while (stack.Count != 0 && ((Operation)_rpn[i]).Priority < ((Operation)stack.Peek()).Priority)
                        {
                            RPN.Add(stack.Pop());
                        }

                        stack.Push(_rpn[i]);
                    }
                }

                else
                {
                    if (((Parenthesis)_rpn[i]).Value == '(')
                    {
                        stack.Push(_rpn[i]);
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

        public static double Calculate(List<Token> RPN, double[] variableValue)
        {
            Stack<Number> stack = new Stack<Number>();

            for (int i = 0; i < RPN.Count; i++)
            {
                if (RPN[i] is Number)
                {
                    stack.Push((Number)RPN[i]);
                }

                else if (RPN[i] is Variable)
                {
                    int index = Array.IndexOf(_variableNames, ((Variable)RPN[i]).symbol );
                    stack.Push(new Number { Value = variableValue[index] });
                }

                else
                {
                    Number[] args = new Number[((Operation)RPN[i]).ArgsCount];

                    for (int k = 0; k < args.Length; k++)
                    {
                        args[k] = stack.Pop();
                    }

                    stack.Push(((Operation)RPN[i]).Execute(args));
                }
            }

            Number result = stack.Pop();
            return result.Value;
        }
    }
}