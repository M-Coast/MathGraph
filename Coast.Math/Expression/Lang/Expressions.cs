using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;

namespace Coast.Math.Expression
{
    public enum ExpressionType
    {
        None,

        Expression,


        //PrimaryExpression,

        IdentifierExpression,
        ConstantExpression,
        ParenthesisExpression,

        //PostfixExpression,

        FuncCallExpression,
        ExponentialExpression,

        //UnaryExpression,
        NegativeExpression,
        PositiveExpression,

        //BinaryExpression,
        AdditionExpression,
        SubtractionExpression,
        MultiplicationExpression,
        //MulticationExpression,
        DivisionExpression,

        //eq
        EquationExpression,

        ExpressionGroup,

    }


    [Serializable]
    public class Constant
    {
        public double Value { get; set; }

        public Constant(double value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
    
    [Serializable]
    public class Identifier
    {
        public string Name { set; get; }
        public Symbol SymbolEntry { set; get; }

        public Identifier()
        {
            Name = null; SymbolEntry = null;
        }

        public Identifier(string name)
        {
            Name = name; SymbolEntry = null;
        }

        public override string ToString()
        {
            return Name;
        }
    }
    

    [Serializable]
    public abstract class Expression //GenericExpression
    {
        public ExpressionType Type;
        public List<Location> Locations;

        public Expression()
        {

        }
        public Expression(ExpressionType type)
        {
            Type = type;
        }
    }

    [Serializable]
    public abstract class PrimaryExpression : Expression
    {
        public PrimaryExpression() { }
        public PrimaryExpression(ExpressionType type) : base(type) { }
    }

    [Serializable]
    public class IdentifierExpression : PrimaryExpression
    {
        public Identifier Identifier { get; set; }

        public IdentifierExpression() : base(ExpressionType.IdentifierExpression)
        {

        }

        public IdentifierExpression(Identifier identifier) : base(ExpressionType.IdentifierExpression)
        {
            Identifier = identifier;
        }


        public override string ToString()
        {
            return Identifier.ToString();
        }
    }

    [Serializable]
    public class ConstantExpression: PrimaryExpression
    {
        public Constant Constant { get; set; }

        public ConstantExpression() : base(ExpressionType.ConstantExpression)
        {

        }

        public ConstantExpression(Constant constant) : base(ExpressionType.ConstantExpression)
        {
            Constant = constant;
        }

        public override string ToString()
        {
            return Constant.ToString();
        }
    }

    [Serializable]
    public class ParenthesisExpression: PrimaryExpression
    {
        public Expression Expression { get; set; }

        public ParenthesisExpression() : base(ExpressionType.ParenthesisExpression)
        {

        }

        public ParenthesisExpression(Expression expression) : base(ExpressionType.ParenthesisExpression)
        {
            Expression = expression;
        }

        public override string ToString()
        {
            return "(" + Expression.ToString() + ")";
        }
    }

    [Serializable]
    public class PostfixExpression: PrimaryExpression
    {
        //public PostfixExpression() { }
        public PostfixExpression(ExpressionType type) : base(type) { }
    }

    [Serializable]
    public class FuncCallExpression : PostfixExpression
    {
        public Function Function { get; set; }
        public List<Expression> Arguments { get; set; }

        public FuncCallExpression() : base(ExpressionType.FuncCallExpression)
        {

        }

        public FuncCallExpression(Function function, List<Expression> arguments) : base(ExpressionType.FuncCallExpression)
        {
            Function = function;
            Arguments = arguments;
        }
    }

    //[Serializable]
    //public class NativeFuncCallExpression : Expression
    //{
    //    public Function Function { get; set; }
    //    public List<Expression> Arguments { get; set; }

    //    public NativeFuncCallExpression() : base(ExpressionType.FuncCallExpression)
    //    {

    //    }

    //    public NativeFuncCallExpression(Function function, List<Expression> arguments) : base(ExpressionType.FuncCallExpression)
    //    {
    //        Function = function;
    //        Arguments = arguments;
    //    }
    //}

    [Serializable]
    public class ExponentialExpression : PostfixExpression
    {
        public Expression Base { get; set; }
        public Expression Power { get; set; }

        public ExponentialExpression() : base(ExpressionType.ExponentialExpression)
        {

        }

        public ExponentialExpression(Expression @base, Expression power) : base(ExpressionType.ExponentialExpression)
        {
            Base = @base;
            Power = power;
        }
    }

    [Serializable]
    public abstract class UnaryExpression : Expression
    {
        public OperatorCode Operator { get; set; }
        public Expression Expression { get; set; }

        public UnaryExpression(ExpressionType type) : base(type) { }

        public UnaryExpression(ExpressionType type, OperatorCode @operator, Expression expression) : base(type)
        {
            Operator = @operator;
            Expression = expression;
        }

        public override string ToString()
        {
            return Operator.ToString() + " " + Expression.ToString();
        }
    }

    [Serializable]
    public class NegativeExpression : UnaryExpression
    {
        public NegativeExpression()
            : base(ExpressionType.NegativeExpression, OperatorCode.Minus,null)
        {
            
        }
        public NegativeExpression(Expression expression) 
            : base(ExpressionType.NegativeExpression,OperatorCode.Minus,expression)
        {

        }
    }

    [Serializable]
    public class PositiveExpression : UnaryExpression
    {
        public PositiveExpression()
            : base(ExpressionType.NegativeExpression, OperatorCode.Plus, null)
        {
            
        }
        public PositiveExpression(Expression expression) 
            : base(ExpressionType.NegativeExpression, OperatorCode.Plus, expression)
        {

        }
    }

    [Serializable]
    public class BinaryExpression : Expression
    {
        public OperatorCode Operator { get; set; }
        public Expression Left { get; set; }
        public Expression Right { get; set; }

        public BinaryExpression(ExpressionType type, OperatorCode @operator, Expression left, Expression right) : base(type)
        {
            Operator = @operator;
            Left = left;
            Right = right;
        }
        public BinaryExpression(ExpressionType type): base(type)
        {

        }

        public override string ToString()
        {
            return Left.ToString() + " " + Operator.ToString() + " " + Right.ToString();
        }

    }

    [Serializable]
    public class AdditionExpression : BinaryExpression
    {
        public AdditionExpression()
            : base(ExpressionType.AdditionExpression, OperatorCode.Plus, null, null)
        {

        }

        public AdditionExpression(Expression left, Expression right)
           : base(ExpressionType.AdditionExpression, OperatorCode.Plus, left, right)
        {

        }
    }

    [Serializable]
    public class SubtractionExpression : BinaryExpression
    {
        public SubtractionExpression()
            : base(ExpressionType.SubtractionExpression, OperatorCode.Minus, null, null)
        {

        }

        public SubtractionExpression(Expression left, Expression right)
            : base(ExpressionType.SubtractionExpression, OperatorCode.Minus, left, right)
        {

        }
    }

    [Serializable]
    public class MultiplicationExpression : BinaryExpression
    {
        public MultiplicationExpression()
            : base(ExpressionType.MultiplicationExpression, OperatorCode.Mul, null, null)
        {

        }

        public MultiplicationExpression(Expression left, Expression right) :
            base(ExpressionType.MultiplicationExpression, OperatorCode.Mul, left, right)
        {

        }
    }

    [Serializable]
    public class DivisionExpression : BinaryExpression
    {
        public DivisionExpression()
            : base(ExpressionType.DivisionExpression, OperatorCode.Div, null, null)
        {

        }

        public DivisionExpression(Expression left, Expression right) :
            base(ExpressionType.DivisionExpression, OperatorCode.Div, left, right)
        {

        }
    }

    [Serializable]
    public class EquationExpression : BinaryExpression
    {
        public EquationExpression()
            : base(ExpressionType.EquationExpression, OperatorCode.Equals, null, null)
        {

        }

        public EquationExpression(Expression left, Expression right) :
            base(ExpressionType.EquationExpression, OperatorCode.Equals, left, right)
        {

        }
    }

    [Serializable]
    public class ExpressionGroup : Expression
    {
        public List<Expression> Expressions { get; private set; }

        public Dictionary<string, Symbol> SymbolTable { get; private set; }

        public ExpressionGroup() : base(ExpressionType.ExpressionGroup)
        {
            Expressions = new List<Expression>();
            SymbolTable = new Dictionary<string, Symbol>();
        }

        public string Name { get; set; }
        public string Version { get; set; }

    }


}