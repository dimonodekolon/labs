using System;
using System.Collections.Generic;
namespace Solution
{
    class Program
    {
        static void Main(string[] args)
        {
            List<object> result = RPN(GetInput().Replace(" ", string.Empty));
            Console.WriteLine($"выражение в ОПЗ: {string.Join(" ", result)}");
            Console.WriteLine($"числа в выражении: {string.Join(", ", ListOfNumbers(result))}");
            Console.WriteLine($"операторы в выражении: {string.Join(", ", ListOfOperators(result))}");
            Console.WriteLine($"значение выражения: {Calculation(result)}");
        }
        static string GetInput()
        {
            Console.Write("ваше выражение: ");
            return Console.ReadLine();
        }
        public static int GetPriority(char op)
        {
            switch (op)
            {
                case '(': return 0;
                case ')': return 0;
                case '-': return 1;
                case '+': return 1;
                case '*': return 2;
                case '/': return 2;
                default: return 3;
            }
        }
        public static bool IsOperator(string c)
        {
            bool check_operator;
            string string_operators = "+-*/()";
            return check_operator = (string_operators.Contains(Convert.ToString(c))) ? true : false;
        }
        public static List<object> RPN(string input)
        {
            List<object> rpn_output = new List<object>();
            Stack<char> operators = new Stack<char>();
            string number = string.Empty;

            for (int i = 0; i < input.Length; i++)
            {
                if (char.IsDigit(input[i]))
                {
                    number += input[i];
                }

                if (IsOperator(input[i].ToString()))
                {
                    rpn_output.Add(number);
                    number = string.Empty;
                    if (input[i] == '(')
                    {
                        operators.Push(input[i]);
                    }
                    else if (input[i] == ')')
                    {
                        while (operators.Peek() != '(')
                        {
                            rpn_output.Add(operators.Peek());
                            operators.Pop();
                        }
                        operators.Pop();
                    }
                    else
                    {
                        if (operators.Count > 0)
                        {
                            if (GetPriority(input[i]) <= GetPriority(operators.Peek()))
                            {
                                rpn_output.Add(operators.Peek());
                                operators.Pop();
                            }

                            operators.Push(input[i]);
                        }
                        else
                        {
                            operators.Push(input[i]);
                        }
                    }
                }
            }
            rpn_output.Add(number);

            while (operators.Count != 0)
            {
                rpn_output.Add(operators.Peek());
                operators.Pop();
            }

            while (rpn_output.Contains(string.Empty)) rpn_output.Remove(string.Empty);
            return rpn_output;
        }
        public static List<int> ListOfNumbers(List<object> rpn_output)
        {
            List<int> numbers = new List<int>();

            for (int i = 0; i < rpn_output.Count; i++)
            {
                if (!IsOperator(rpn_output[i].ToString()))
                {
                    numbers.Add(int.Parse(rpn_output[i].ToString()));
                }
            }

            return numbers;
        }
        public static List<char> ListOfOperators(List<object> rpn_output)
        {
            List<char> operators = new List<char>();

            for (int i = 0; i < rpn_output.Count; i++)
            {
                if (IsOperator(rpn_output[i].ToString()))
                {
                    operators.Add(Convert.ToChar(rpn_output[i]));
                }
            }

            return operators;
        }
        public static int Calculation(List<object> rpn_caclc)
        {
            Stack<int> temp_calc = new Stack<int>();
            int result = 0;

            for (int i = 0; i < rpn_caclc.Count; i++)
            {
                if (!IsOperator(rpn_caclc[i].ToString()))
                {
                    temp_calc.Push(int.Parse(rpn_caclc[i].ToString()));
                }
                else
                {
                    int first = temp_calc.Pop();
                    int second = temp_calc.Pop();

                    switch (Convert.ToChar(rpn_caclc[i]))
                    {
                        case '+': result = first + second; break;
                        case '-': result = second - first; break;
                        case '*': result = first * second; break;
                        case '/': result = second / first; break;
                    }
                    temp_calc.Push(result);
                }
            }
            return temp_calc.Peek();
        }
    }
}