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
            {
                tokenList.Add(input[i]);
            }

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

                tokenList.Add(Convert.ToDouble(number));
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
        Dictionary<char, int> index = new Dictionary<char, int>();
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

    public static List<object> ToRPN(List<object> tokenList, Dictionary<char, int> operations)
    {
        Stack<object> stack = new Stack<object>();
        List<object> RPN = new List<object>();

        for (int i = 0; i < tokenList.Count; i++)
        {

            if (tokenList[i] is double)
            {
                RPN.Add(tokenList[i]);
            }

            else if ((stack.Count == 0) || ((operations[(char)tokenList[i]]) >= operations[(char)stack.Peek()]) || (operations[(char)stack.Peek()] == 10))
            {
                stack.Push(tokenList[i]);
            }

            else
            {
                while (operations[(char)tokenList[i]] < operations[(char)stack.Peek()])
                {

                    if (stack.Count == 0)
                    {
                        break;
                    }
                    if (operations[(char)stack.Peek()] == 10)
                    {
                        stack.Pop();
                        break;
                    }
                    else
                        RPN.Add(stack.Pop());
                }
                if (operations[(char)tokenList[i]] != 0)
                {
                    stack.Push(tokenList[i]);
                }
            }
        }
        while (stack.Count > 0)
            RPN.Add(stack.Pop());
        return RPN;
    }



    public static double Calculate(List<object> RPN)
    {
        Stack stack = new Stack();

        for (int i = 0; i < RPN.Count; i++)
        {
            if (RPN[i] is double)
            {
                stack.Push(RPN[i]);
            }
            else
            {
                switch ((char)RPN[i])
                {
                    case '+':
                        stack.Push((double)stack.Pop() + (double)stack.Pop());
                        continue;
                    case '-':
                        stack.Push((double)stack.Pop() - (double)stack.Pop());
                        continue;
                    case '*':
                        stack.Push((double)stack.Pop() * (double)stack.Pop());
                        continue;
                    case '/':
                        stack.Push((double)stack.Pop() / (double)stack.Pop());
                        continue;

                }
            }
        }

        double result = (double)stack.Pop();
        return result;
    }
}