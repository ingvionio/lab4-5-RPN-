using System;
using System.Collections;

class Program
{

    public static List<object> Parse(string input)
    {
        List<object> tokenList = new List<object>();
        for (int i = 0; i < input.Length; i++)
        {
            if ((char.IsDigit(input[i]) == false) && (input[i] != ' '))
                tokenList.Add(input[i]);

            else if ((char.IsDigit(input[i]) == true) && (input[i]) != ' ')
            {
                string number = null;

                while (char.IsDigit(input[i]) == true)
                {
                    number += input[i];
                    i++;
                    if (i == input.Length)
                        break;
                }

                tokenList.Add(Convert.ToInt32(number));
                i--;

            }
        }

        return tokenList;
    }

    static void Main()
    {
        List<object> tokenList = Parse("12+6*  (4+3)+10*5+3");
        foreach (object token in tokenList)
            Console.Write($"{token} ");
        Console.WriteLine();
        Hashtable index = new Hashtable();
        index.Add('+', 1);
        index.Add('-', 1);
        index.Add('*', 2);
        index.Add('/', 2);
        index.Add('(', 10);
        index.Add(')', 0);

        List<object> rpn = ToRPN(tokenList, index);
        foreach (object elements in rpn)
            Console.Write($"{elements} ");
        Console.WriteLine(Calculate(rpn));


    }

    public static List<object> ToRPN(List<object> tokenList, Hashtable operations)
    {
        var stack = new Stack<object>();
        var RPN = new List<object>();

        for (int i = 0; i < tokenList.Count; i++)
        {
            int a = Convert.ToInt32(operations[tokenList[i]]);
            if (tokenList[i].GetType() == typeof(Int32) || tokenList[i].GetType() == typeof(double))
                RPN.Add(tokenList[i]);
            else if ((stack.Count == 0) || (Convert.ToInt32(operations[tokenList[i]]) >= Convert.ToInt32(operations[stack.Peek()])) || (Convert.ToInt32(operations[stack.Peek()]) == 10))
            {
                stack.Push(tokenList[i]);
            }
            else
            {
                while (Convert.ToInt32(operations[tokenList[i]]) < Convert.ToInt32(operations[stack.Peek()]))
                {

                    if (stack.Count == 0)
                        break;
                    if (Convert.ToInt32(operations[stack.Peek()]) == 10)
                    {
                        stack.Pop();
                        break;
                    }
                    else
                        RPN.Add(stack.Pop());
                }
                if (Convert.ToInt32(operations[tokenList[i]]) != 0)
                    stack.Push(tokenList[i]);
            }
        }
        while (stack.Count > 0)
            RPN.Add(stack.Pop());
        return RPN;
    }

    public static double Calculate(List<object> RPN)
    {
        for (int i = 0; i < RPN.Count; i++)
        {
            if (Convert.ToChar(RPN[i]) == '+')
            {
                RPN[i - 1] = Convert.ToDouble(RPN[i - 2]) + Convert.ToDouble(RPN[i - 1]);
                RPN.RemoveAt(i);
                RPN.RemoveAt(i - 2);
                i -= 2;
                continue;
            }
            if (Convert.ToChar(RPN[i]) == '-')
            {
                RPN[i - 1] = Convert.ToDouble(RPN[i - 2]) - Convert.ToDouble(RPN[i - 1]);
                RPN.RemoveAt(i);
                RPN.RemoveAt(i - 2);
                i -= 2;
                continue;
            }
            if (Convert.ToChar(RPN[i]) == '*')
            {
                RPN[i - 1] = Convert.ToDouble(RPN[i - 2]) * Convert.ToDouble(RPN[i - 1]);
                RPN.RemoveAt(i);
                RPN.RemoveAt(i - 2);
                i -= 2;
                continue;
            }
            if (Convert.ToChar(RPN[i]) == '/')
            {
                RPN[i - 1] = Convert.ToDouble(RPN[i - 2]) / Convert.ToDouble(RPN[i - 1]);
                RPN.RemoveAt(i);
                RPN.RemoveAt(i - 2);
                i -= 2;
                continue;
            }
        }
        double result = Convert.ToDouble(RPN[0]);
        return result;
    }
}