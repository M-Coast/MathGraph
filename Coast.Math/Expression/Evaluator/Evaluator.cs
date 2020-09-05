using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coast.Math.Expression
{
    public class Evaluator
    {
        public void Solve(ExpressionGroup expressions, Dictionary<string, double> valueTable)
        {
            Dictionary<string, Symbol> symtab = expressions.SymbolTable;

            //bool symbol_missed = false;
            //foreach(Symbol s in symtab)
            //{
            //    if(s )
            //}

            _evStack.Clear();
            //_operations.Clear();
            _valueTable = valueTable;

            Solve(expressions);
        }

        private void Solve(Expression expr)
        {
            if (expr == null) return;
            string s = string.Empty;
            switch (expr.Type)
            {
                case ExpressionType.IdentifierExpression:
                    {
                        IdentifierExpression e = (IdentifierExpression)expr;
                        double v = _valueTable[e.Identifier.Name];
                        _evStack.Push(v);
                    }
                    break;
                case ExpressionType.ConstantExpression:
                    {
                        ConstantExpression e = (ConstantExpression)expr;
                        _evStack.Push(e.Constant.Value);
                    }
                    break;
                case ExpressionType.ParenthesisExpression:
                    {
                        ParenthesisExpression e = (ParenthesisExpression)expr;
                        Solve(e.Expression);
                    }
                    break;
                case ExpressionType.FuncCallExpression:
                    {
                        FuncCallExpression e = (FuncCallExpression)expr;

                        foreach(Expression arg in e.Arguments)
                        {
                            Solve(arg);
                        }

                        List<double> vArgs = new List<double>();
                        for(int i=0;i< e.Arguments.Count;i++)
                        {
                            vArgs.Add(_evStack.Pop());
                        }
                        vArgs.Reverse();

                        double v = FuncCall(e.Function, vArgs);
                        _evStack.Push(v);
                    }
                    break;
                case ExpressionType.ExponentialExpression: break;
                case ExpressionType.NegativeExpression:
                    {
                        NegativeExpression e = (NegativeExpression)expr;

                        Solve(e.Expression);
                        double v = _evStack.Pop() * -1;
                        _evStack.Push(v);
                    }
                    break;
                case ExpressionType.PositiveExpression:
                    {
                        PositiveExpression e = (PositiveExpression)expr;

                        Solve(e.Expression);
                        double v = _evStack.Pop() * -1;
                        _evStack.Push(v);
                    }
                    break;
                case ExpressionType.AdditionExpression:
                    {
                        AdditionExpression e = (AdditionExpression)expr;

                        Solve(e.Left);
                        Solve(e.Right);
                        double r = _evStack.Pop();
                        double l = _evStack.Pop();
                        _evStack.Push(l + r);
                    }
                    break;
                case ExpressionType.SubtractionExpression:
                    {
                        SubtractionExpression e = (SubtractionExpression)expr;

                        Solve(e.Left);
                        Solve(e.Right);
                        double r = _evStack.Pop();
                        double l = _evStack.Pop();
                        _evStack.Push(l - r);
                    }
                    break;
                case ExpressionType.MultiplicationExpression:
                    {
                        MultiplicationExpression e = (MultiplicationExpression)expr;

                        Solve(e.Left);
                        Solve(e.Right);
                        double r = _evStack.Pop();
                        double l = _evStack.Pop();
                        _evStack.Push(l * r);
                    }
                    break;
                case ExpressionType.DivisionExpression:
                    {
                        DivisionExpression e = (DivisionExpression)expr;

                        Solve(e.Left);
                        Solve(e.Right);
                        double r = _evStack.Pop();
                        double l = _evStack.Pop();
                        _evStack.Push(l / r);
                    }
                    break;
                case ExpressionType.EquationExpression:
                    {
                        EquationExpression e = (EquationExpression)expr;

                        Solve(e.Right);
                        double v = _evStack.Pop();
                        if (e.Left is IdentifierExpression)
                        {
                            _valueTable[((IdentifierExpression)e.Left).Identifier.Name] = v;
                        }
                    }
                    break;
                case ExpressionType.ExpressionGroup:
                    {
                        ExpressionGroup e = (ExpressionGroup)expr;
                        foreach (Expression ie in e.Expressions)
                        {
                            Solve(ie);
                        }
                    }
                    break;
                default:
                    break;
            }
        }


        private double FuncCall(Function func, List<double> args)
        {
            double result = double.NaN;

            switch (func.Name)
            {
                case "sin":
                case "Sin":
                case "SIN":
                    result = System.Math.Sin(args[0]);
                    break;

                case "cos":
                case "Cos":
                case "COS":
                    result = System.Math.Cos(args[0]);
                    break;

                case "tan":
                case "Tan":
                case "TAN":
                    result = System.Math.Tan(args[0]);
                    break;

                case "pow":
                case "Pow":
                case "POW":
                    result = System.Math.Pow(args[0], args[1]);
                    break;

                case "exp":
                case "Exp":
                case "EXP":
                    result = System.Math.Exp(args[0]);
                    break;
                    
                case "log":
                case "Log":
                case "LOG":
                    result = System.Math.Log(args[0]);
                    break;


            }

            return result;
        }


        private Stack<double> _evStack = new Stack<double>();
        private Dictionary<string, double> _valueTable;

        public Stack<double> EVStack { get { return _evStack; } }

        //private List<string> _operations = new List<string>();
        //private int _opIndex = 0;
        //public List<string> Operations { get { return _operations; } }
        //_operations.Add(_operations.Count.ToString("D4") + "Push " + v.ToString() + "  - " + e.ToString());
        //_operations.Add(_operations.Count.ToString("D4") + "Push " + e.Constant.Value + "  - " + "Conatant");
    }

    public class EVStack<T> : Stack<T>
    {
        private List<string> _operations = new List<string>();
        public List<string> Operations { get { return _operations; } }


        public new void Clear()
        {
            base.Clear();
            _operations.Clear();
        }

        public new void Push(T item)
        {
            base.Push(item);
            _operations.Add(_operations.Count.ToString("D4") + ": " + "Push " + item.ToString());
        }

        public new T Pop()
        {
            T item = base.Pop();
            _operations.Add(_operations.Count.ToString("D4") + ": " + "Pop  " + item.ToString());
            return item;
        }
    }
}
