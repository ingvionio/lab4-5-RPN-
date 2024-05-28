using System.Collections;
using System;
using System.Collections.Generic;
using RPN.Logic;


namespace RPN.ConsoleApp
{
    class Program
    {
        static void Main()
        {
            string input = "5-6*x+ctg1 ";
            double[] x = { 5 };

            //List<Token> tokenList = RpnCalculator.Parse(input, index);

            //foreach (Token token in tokenList)
            //    Console.Write(token.ToString() + " ");
            //Console.WriteLine();

            //List<Token> rpn = RpnCalculator.ToRPN(tokenList, index);

            //foreach (Token elements in rpn)
            //    Console.Write(elements.ToString() + " ");
            //Console.WriteLine();

            Console.WriteLine(RpnCalculator.PerformСalculation(input, x));
        }
    }   
}