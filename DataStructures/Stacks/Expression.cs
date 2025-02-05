﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructures.Stacks
{
    public class Expression
    {
        public string PostfixToInfix(string input)
        {
            var stk = new Stack<string>();
            foreach(var c in input)
            {
                if (Char.IsLetterOrDigit(c))
                    stk.Push(c.ToString());
                else if(IsOperator(c))
                {
                    var first = stk.Pop();
                    var second = stk.Pop();
                    var temp = "(" + second + c + first + ")";
                    stk.Push(temp);
                }
            }

            return stk.Pop();
        }

        public string PostfixToPrefix(string input)
        {
            var stk = new Stack<string>();
            foreach(var c in input)
            {
                if (Char.IsLetterOrDigit(c))
                    stk.Push(c.ToString());
                else if(IsOperator(c))
                {
                    var first = stk.Pop();
                    var second = stk.Pop();
                    var temp = c + second + first;
                    stk.Push(temp);
                }
            }

            return stk.Pop();
        }

        public string PrefixToPostfix(string input)
        {
            var stk = new Stack<string>();
            for(int i = input.Length - 1; i >= 0; i --)
            {
                var c = input[i];
                if (Char.IsLetter(c))
                    stk.Push(c.ToString());
                else if(IsOperator(c))
                {
                    var first = stk.Pop();
                    var second = stk.Pop();
                    var temp = first + second + c;
                    stk.Push(temp);
                }
            }

            return stk.Pop();
        }

        public string PrefixToInfix(string input)
        {
            var stk = new Stack<string>();

            for (int i = input.Length - 1; i >= 0; i--)
            {
                var c = input[i];
                if (Char.IsLetterOrDigit(c))
                    stk.Push(c.ToString());
                else if (IsOperator(c))
                {
                    var first = stk.Pop();
                    var second = stk.Pop();
                    var temp = "(" + first + c + second + ")";
                    stk.Push(temp);
                }
            }

            return stk.Pop();
        }

        public bool IsOperator(char c)
        {
            switch (c)
            {
                case '+':
                case '-':
                case '*':
                case '/':
                case '^':
                    return true;
            }
            return false;
        }

        public string InfixToPrefix(string input)
        {
            var arr = input.ToCharArray();
            Array.Reverse(arr);
            for(int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == '(')
                    arr[i] = ')';
                else if (arr[i] == ')')
                    arr[i] = '(';
            }

            var temp = InfixToPostfix(arr).ToCharArray();
            Array.Reverse(temp);
            
            return new string(temp);
        }

        //Modified version for InfixToPrefix
        private string InfixToPostfix(char[] input)
        {
            var builder = new StringBuilder();
            var stk = new Stack<char>();
            foreach(var c in input)
            {
                if (Char.IsLetterOrDigit(c))
                    builder.Append(c);
                else if (c == '(')
                    stk.Push(c);
                else if (c == ')')
                {
                    while (stk.Count > 0 && stk.Peek() != '(')
                        builder.Append(stk.Pop());
                    stk.Pop();
                }
                else if(IsOperator(c))
                {
                    if(c == '^')
                    {
                        while(stk.Count > 0 && Prec(stk.Peek()) >= Prec(c)) //Here is the difference ">=" when ^
                            builder.Append(stk.Pop());
                        stk.Push(c);
                    }
                    else
                    {
                        while (stk.Count > 0 && Prec(stk.Peek()) > Prec(c))
                            builder.Append(stk.Pop());
                        stk.Push(c);
                    }
                }
            }

            while (stk.Count > 0)
                builder.Append(stk.Pop());

            return builder.ToString();
        }

        public string InfixToPostfix(string input)
        {
            var stk = new Stack<char>();
            var builder = new StringBuilder();
            foreach (var c in input)
            {
                if (Char.IsLetterOrDigit(c))
                    builder.Append(c);
                else if (c == '(')
                    stk.Push(c);
                else if (c == ')')
                {
                    while (stk.Count > 0 && stk.Peek() != '(')
                        builder.Append(stk.Pop());
                    stk.Pop();
                }
                else
                {
                    while (stk.Count > 0 && Prec(stk.Peek()) >= Prec(c)) //needs >=. Also '(' returns -1 from GetWeight() when it is hit 
                        builder.Append(stk.Pop());
                    stk.Push(c);
                }
            }

            while (stk.Count > 0)
                builder.Append(stk.Pop());

            return builder.ToString();
        }

        public int Prec(char c)
        {
            switch (c)
            {
                case '+':
                case '-':
                    return 1;
                case '*':
                case '/':
                    return 2;
                case '^':
                    return 3;
                default:
                    return -1;
            }
        }

        public bool IsBalanced(string input)
        {
            var dictionary = new Dictionary<char, char>()
            {
                { '{', '}'},
                { '[', ']'},
                { '(', ')'},
                { '<', '>'}
            };

            var stk = new Stack<char>();

            foreach(var i in input)
            {
                if (dictionary.ContainsKey(i))
                    stk.Push(i);
                
                if(dictionary.ContainsValue(i))
                {
                    if (stk.Count == 0)
                        return false;

                    var top = stk.Pop();
                    if (dictionary[top] != i)
                        return false;
                }
            }

            return stk.Count == 0;
        }
    }
}
