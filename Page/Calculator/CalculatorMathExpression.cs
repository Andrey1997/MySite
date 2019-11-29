using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace MyWebApplication.Page.Calculate
{
    public class ClassStackOperation
    {
        private List<string> ListOperation;
        private ClassOperationPriority OperationPriority;

        public ClassStackOperation()
        {
            ListOperation = new List<string>();
            OperationPriority = new ClassOperationPriority();
        }

        public bool IsEmpty()
        {
            if (ListOperation.Count() == 0) return true;
            else return false;
        }

        public void AddOperation(string operation)
        {
            ListOperation.Add(operation);
        }

        public bool СomparisonOperation(string operation)
        {
            if (OperationPriority.GetOperationPriority(ListOperation.Last()) < OperationPriority.GetOperationPriority(operation))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public string GetLastOperation()
        {
            return ListOperation.Last();
        }

        public void DeleteLastOperation()
        {
            ListOperation.RemoveAt(ListOperation.Count() - 1);
        }

        public int GetSizeStackOperation()
        {
            return ListOperation.Count();
        }
    }

    public class ClassStackValue
    {
        private List<double> ListValue;

        public ClassStackValue()
        {
            ListValue = new List<double>();
        }

        public void AddValue(double value)
        {
            ListValue.Add(value);
        }

        public double GetLastValue()
        {
            return ListValue.Last();
        }

        public double GetPenultimateValue()
        {
            return ListValue[ListValue.Count() - 2];
        }

        public void DeleteLastValue()
        {
            ListValue.RemoveAt(ListValue.Count() - 1);
        }

    }

    public class ClassOperationPriority
    {
        private Dictionary<string, int> OperationPriority;

        public ClassOperationPriority()
        {
            OperationPriority = new Dictionary<string, int>();

            OperationPriority.Add("+", 1);
            OperationPriority.Add("-", 1);
            OperationPriority.Add("*", 2);
            OperationPriority.Add("/", 2);
            OperationPriority.Add("^", 3);
        }

        public int GetOperationPriority(string operation)
        {
            int priority;
            OperationPriority.TryGetValue(operation, out priority);
            return priority;
        }
    }



    public class ClassCalculatorMath
    {
        private ClassStackOperation StackOperation;
        private ClassStackValue StackValue;

        private double result;
        public ClassCalculatorMath()
        {
            StackOperation = new ClassStackOperation();
            StackValue = new ClassStackValue();
        }

        public double GetResult()
        {
            return this.result;
        }

        public bool Calculate(string Expression)
        {
            try
            {
                result = 0;

                Expression = Expression.Replace(" ", "");

                string[] TokenExpression = Regex.Split(Expression, @"(\d+[.]\d+)|(\d+)|([*\-+\/\)\(])").Where(st => st != String.Empty).ToArray();

                for (int i = 0; i < TokenExpression.Count(); i++)
                {
                    double ValueToken;
                    bool IsDouble = Double.TryParse(TokenExpression[i], out ValueToken);

                    if (IsDouble == true)
                    {
                        StackValue.AddValue(ValueToken);
                    }
                    else
                    {
                        if (StackOperation.IsEmpty() == true)
                        {
                            StackOperation.AddOperation(TokenExpression[i]);
                        }
                        else
                        {
                            while (true)
                            {
                                if (StackOperation.IsEmpty() == true) break;
                                if (StackOperation.СomparisonOperation(TokenExpression[i]) == true)
                                {
                                    Calculation();
                                }
                                else
                                {
                                    break;
                                }

                            }

                            StackOperation.AddOperation(TokenExpression[i]);
                        }
                    }
                }

                int CountOperation = StackOperation.GetSizeStackOperation();
                for (int i = 0; i < CountOperation; i++)
                {
                    Calculation();
                }

                result = StackValue.GetLastValue();
                StackValue.DeleteLastValue();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void Calculation()
        {
            double a = StackValue.GetPenultimateValue();
            double b = StackValue.GetLastValue();

            switch (StackOperation.GetLastOperation())
            {
                case "+":
                    {
                        double c = a + b;
                        StackValue.DeleteLastValue();
                        StackValue.DeleteLastValue();
                        StackValue.AddValue(c);

                        StackOperation.DeleteLastOperation();
                        break;
                    }
                case "-":
                    {
                        double c = a - b;
                        StackValue.DeleteLastValue();
                        StackValue.DeleteLastValue();
                        StackValue.AddValue(c);

                        StackOperation.DeleteLastOperation();
                        break;
                    }
                case "*":
                    {
                        double c = a * b;
                        StackValue.DeleteLastValue();
                        StackValue.DeleteLastValue();
                        StackValue.AddValue(c);

                        StackOperation.DeleteLastOperation();
                        break;
                    }
                case "/":
                    {
                        double c = a / b;
                        StackValue.DeleteLastValue();
                        StackValue.DeleteLastValue();
                        StackValue.AddValue(c);

                        StackOperation.DeleteLastOperation();
                        break;
                    }
                case "^":
                    {
                        double c = Math.Pow(a, b);
                        StackValue.DeleteLastValue();
                        StackValue.DeleteLastValue();
                        StackValue.AddValue(c);

                        StackOperation.DeleteLastOperation();
                        break;
                    }
            }
        }

    }