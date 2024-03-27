﻿using System.Collections;
using System;
using System.Collections.Generic;
using RPN.Logic;


namespace RPN.ConsoleApp
{
    class Program
    {
        static void Main()
        {
            string input = "12+6*  (4+3)+10*5+3";

            Dictionary<char, int> index = new Dictionary<char, int>();
            index.Add('+', 1);
            index.Add('-', 1);
            index.Add('*', 2);
            index.Add('/', 2);

            List<Token> tokenList = RpnCalculator.Parse(input, index);

            foreach (Token token in tokenList)
                Console.Write(token.ToString() + " ");
            Console.WriteLine();

            List<Token> rpn = RpnCalculator.ToRPN(tokenList, index);

            foreach (Token elements in rpn)
                Console.Write(elements.ToString() + " ");
            Console.WriteLine();

            Console.WriteLine(RpnCalculator.Calculate(rpn));
        }
    }   
}