using System.Collections;
using System;
using System.Collections.Generic;

public abstract class Token
{
}

public class Parenthesis : Token
{
    public char Value;
}

public class Number : Token
{
    public double Value;
}

public class Operation : Token
{
    public char Value;
    public int priority;
}

class Program
{
    public static List<Token> Parse(string input, Dictionary<char, int> index)
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
                else
                {
                    tokenList.Add(new Operation { Value = input[i], priority = index[input[i]] });
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

    static void Main()
    {
        Dictionary<char, int> index = new Dictionary<char, int>();
        index.Add('+', 1);
        index.Add('-', 1);
        index.Add('*', 2);
        index.Add('/', 2);
        List<Token> tokenList = Parse("12+6*  (4+3)+10*5+3", index);
        /* foreach (Token token in tokenList)
             Console.Write($"{token.GetType} ");
         Console.WriteLine();*/
        List<Token> rpn = ToRPN(tokenList, index);
        /*foreach (Token elements in rpn)
            Console.Write($"{elements} ");*/
        Console.WriteLine(Calculate(rpn));
    }

    public static List<Token> ToRPN(List<Token> tokenList, Dictionary<char, int> operations)
    {
        Stack<Token> stack = new Stack<Token>();
        List<Token> RPN = new List<Token>();

        for (int i = 0; i < tokenList.Count; i++)
        {
            if (tokenList[i] is Number)
            {
                RPN.Add(tokenList[i]);
            }

            else if (tokenList[i] is Operation)
            {
                if ((stack.Count == 0) || (stack.Peek() is Parenthesis) || ((Operation)tokenList[i]).priority >= ((Operation)stack.Peek()).priority)
                {
                    stack.Push(tokenList[i]);
                }
                else
                {
                    while (((Operation)tokenList[i]).priority < ((Operation)stack.Peek()).priority)
                    {
                        if (stack.Count == 0)
                        {
                            break;
                        }
                        else
                        {
                            RPN.Add(stack.Pop());
                        }
                    }

                    stack.Push(tokenList[i]);
                }
            }

            else //if (tokenList[i] is Parenthesis)
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

    public static double Calculate(List<Token> RPN)
    {
        Stack<double> stack = new Stack<double>();

        for (int i = 0; i < RPN.Count; i++)
        {
            if (RPN[i] is Number)
            {
                stack.Push(((Number)RPN[i]).Value);
            }

            else
            {
                switch ((char)((Operation)RPN[i]).Value)
                {
                    case '+':
                        stack.Push(stack.Pop() + stack.Pop());
                        continue;
                    case '-':
                        stack.Push(-stack.Pop() + stack.Pop());
                        continue;
                    case '*':
                        stack.Push(stack.Pop() * stack.Pop());
                        continue;
                    case '/':
                        double divisor = stack.Pop();
                        stack.Push(stack.Pop() / divisor);
                        continue;
                }
            }
        }

        double result = stack.Pop();
        return result;
    }
}