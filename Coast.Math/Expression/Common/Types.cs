using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;

namespace Coast.Math.Expression
{
    public enum TokenType_e
    {
        None,
        Identifier,
        Constant,
        Operator,
        Keyword,
        EOF,
    }
    
    [Serializable]
    public class Location
    {
        public string FileName { set; get; }
        public int Line { set; get; }
        public int Column { set; get; }
        public int Length { set; get; }
        public int Index { set; get; }

        public override string ToString()
        {
            return "[" + Line.ToString() + ", " + Column.ToString() + "]";
        }
    }

    [Serializable]
    public class Token
    {
        public TokenType_e Type { set; get; }
        public object Value { set; get; }
        public Location Location { set; get; }

        public Token()
        {
            Type = TokenType_e.None;
            Value = null;
            Location = new Location();
        }

        public override string ToString()
        {
            string info = "";
            info = Type.ToString();
            if (Value != null) info = info + ", " + Value.ToString();
            if (Location != null) info = info + ", " + Location.ToString();
            return info;
        }
    }


    public enum SymbolType_e
    {
        None,
        Keyword,
        GlobalVariable,
        LocalVariable,
        TempVariable,
        Function,
    }

    //[Serializable]
    //public class Symbolm
    //{
    //    public string Name { set; get; }
    //    public SymbolType_e Type { set; get; }
    //    public int Adress { set; get; }
    //    public int Size { set; get; }
    //    public object Value { set; get; }

    //    public Symbol()
    //    {

    //    }
    //    public Symbol(string name, SymbolType_e type, object value)
    //    {
    //        Name = name;
    //        Type = type;
    //        Value = value;
    //    }

    //    public override string ToString()
    //    {
    //        return Name;
    //    }
    //}




    [Serializable]
    public class Identifier
    {
        public string Name { set; get; }
        public object SymbolEntry { set; get; }

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

    public class Constant
    {

    }

    public class Function
    {

    }



    public enum AstNodeType_e_G
    {
        Constant,
        Identifier,

        Expression_Paren,
        Expression_Brace,

        Expression_ArrayAccesor,
        Expression_CommandFunction, //command function

        Expression_Not,

        Expression_Negative,
        Expression_Positive,

        Expression_Multiplicative,
        Expression_Additive,
        Expression_Relational,
        Expression_And,
        Expression_Or,
        Expression_Assignment,
        Expression_Jump,
        Expression,

        Statement_Line,
        Statement_If,
        Statement,
        Statement_List,

        //MultipleExpression,
        
    }

    //AstNode 改为Expression

    public enum AstNodeType_e
    {
        Constant,
        Identifier,
        
        Expression_Paren,
        Expression_Brace,

        Expression_ArrayAccesor,
        Expression_CommandFunction, //command function

        Expression_Not,

        Expression_Negative,
        Expression_Positive,

        Expression_Multiplicative,
        Expression_Additive,
        Expression_Relational,
        Expression_And,
        Expression_Or,
        Expression_Assignment,
        Expression_Jump,
        Expression,

        Statement_Line,
        Statement_If,
        Statement,
        Statement_List,
    }

    [Serializable]
    public class AstNode
    {
        public AstNodeType_e Type;
        public object Value;
        public Location Location;

        public AstNode()
        {

        }
        public AstNode(AstNodeType_e nodeType)
        {
            Type = nodeType;
            Value = null;
            Location = null;
        }
        public AstNode(AstNodeType_e nodeType, object value)
        {
            Type = nodeType;
            Value = value;
            Location = null;
        }
    }

    [Serializable]
    public class ExpressionArrayAccesor
    {
        public AstNode Array;
        public AstNode Accessor;
    }












    public enum OperatorCode
    {
        None,
        NewLine,
        Return,
        Not,
        DoubleQuotation,
        Hash,
        Currency,
        Mod,
        And,
        SingleQuotation,
        LeftParen,
        RightParen,
        Mult,
        Plus,
        Comma,
        Minus,
        Dot,
        Div,
        Colon,
        SemiColon,
        LessThan,
        Equals,
        GreaterThan,
        Query,
        At,
        LeftSquare,
        Backslash,
        RightSquare,
        Xor,
        UnderScore,
        LeftBrace,
        Or,
        RightBrace,
        Invert,
        LessEqual,
        NotEqual,
        GreaterEqual,
    }

    public enum ExpressionType
    {
        None,

        Expression,



        //PrimaryExpression,

        IdentifierExpression,
        ConstantExpression,
        ParenExpression,
        
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
        MulticationExpression,
        DivisionExpression,
        EquationExpression,

    }

    [Serializable]
    public abstract class Expression //GenericExpression
    {
        public ExpressionType Type;
        public List<Location> Locations;

        public Expression()
        {

        }
        public Expression(ExpressionType nodeType)
        {
            Type = nodeType;
        }
    }

    public abstract class PrimaryExpression : Expression
    {
        //public PrimaryExpression() { }
        public PrimaryExpression(ExpressionType type) : base(type) { }
    }

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
    }

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
    }

    public class ParenExpression: PrimaryExpression
    {
        public Expression Expression { get; set; }

        public ParenExpression() : base(ExpressionType.ParenExpression)
        {

        }

        public ParenExpression(Expression expression) : base(ExpressionType.ParenExpression)
        {
            Expression = expression;
        }
    }

    public class PostfixExpression: PrimaryExpression
    {
        //public PostfixExpression() { }
        public PostfixExpression(ExpressionType type) : base(type) { }
    }

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


    public abstract class UnaryExpression : Expression
    {
        public OperatorCode Operator { get; set; }
        public Expression Expression { get; set; }

        public UnaryExpression(ExpressionType type) : base(type) { }

        //public UnaryExpression(ExpressionType type, OperatorCode @operator, Expression expression) : base(type)
        //{
        //    Operator = @operator;
        //    Expression = expression;
        //}
    }

    public class NegativeExpression : UnaryExpression
    {
        public NegativeExpression()
            : base(ExpressionType.NegativeExpression)
        {
            Operator = OperatorCode.Minus;
        }
        public NegativeExpression(Expression expression) : base(ExpressionType.NegativeExpression)
        {
            Operator = OperatorCode.Minus;
            Expression = expression;
        }
    }

    public class PositiveExpression : UnaryExpression
    {
        public PositiveExpression()
            : base(ExpressionType.PositiveExpression)
        {
            Operator = OperatorCode.Minus;
        }
        public PositiveExpression(Expression expression) : base(ExpressionType.PositiveExpression)
        {
            Operator = OperatorCode.Minus;
            Expression = expression;
        }
    }

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
    }

    //public class AdditionExpression : BinaryExpression
    //{
    //    public AdditionExpression()
    //        : base(ExpressionType.AdditionExpression, OperatorCode.Plus, null, null)
    //    {
    //        Operator = OperatorCode.Minus;
    //    }

    //    public AdditionExpression(Expression expression) : base(ExpressionType.PositiveExpression)
    //    {
    //        Operator = OperatorCode.Minus;
    //        Expression = expression;
    //    }
    //}

    //public class SubtractionExpression : BinaryExpression
    //{

    //}

    //public class MultiplicationExpression : BinaryExpression
    //{

    //}

    //public class MulticationExpression : BinaryExpression
    //{

    //}

    //public class DivisionExpression : BinaryExpression
    //{

    //}
    
    //public class EquationExpression : BinaryExpression
    //{

    //}








    [Serializable]
    public class TableList
    {
        [Serializable]
        struct IndexedKeyValue
        {
            public string Key;// { set; get; }
            public int Index;// { set; get; }
            public object Value;// { set; get; }
            public IndexedKeyValue(string key, int index, object value)
            {
                Key = key;
                Value = value;
                Index = index;
            }
        }

        private Hashtable hashTable;// { set; get; }
        private List<string> keyList;// { set; get; }
        private List<object> valueList;// { set; get; }
        public int Count { set; get; }

        public TableList()
        {
            hashTable = new Hashtable();
            keyList = new List<string>();
            valueList = new List<object>();
            Count = 0;
        }

        public virtual int Add(string key, object value)
        {
            object r = hashTable[key];
            if (r != null) return -1;

            IndexedKeyValue iv = new IndexedKeyValue(key, Count, value);
            hashTable.Add(key, iv);
            keyList.Add(key);
            valueList.Add(value);
            Count++;
            return Count - 1;
        }
        public virtual void Remove(string key)
        {

        }

        public virtual object this[string key]
        {
            get
            {
                object iv = hashTable[key];
                if (iv == null) return null;
                return ((IndexedKeyValue)iv).Value;
            }
        }

        public virtual object this[int index]
        {
            get
            {
                if (index < 0 || index > Count - 1) return null;
                return valueList[index];
            }
        }

        public int GetIndex(string key)
        {
            object iv = hashTable[key];
            if (iv == null) return -1;
            return ((IndexedKeyValue)iv).Index;
        }

        public bool Contains(string key)
        {
            return hashTable.Contains(key);
        }
    }


    //public enum SymbolType_e
    //{
    //    None,
    //    Keyword,
    //    GlobalVariable,
    //    LocalVariable,
    //    TempVariable,
    //    Label,
    //    Function,
    //    Command,
    //}

    //[Serializable]
    //public class Symbol
    //{
    //    public string Name { set; get; }
    //    public SymbolType_e Type { set; get; }
    //    public int Adress { set; get; }
    //    public int Size { set; get; }
    //    public object Value { set; get; }

    //    public Symbol()
    //    {

    //    }
    //    public Symbol(string name, SymbolType_e type, object value)
    //    {
    //        Name = name;
    //        Type = type;
    //        Value = value;
    //    }

    //    public override string ToString()
    //    {
    //        return Name;
    //    }
    //}

    //[Serializable]
    //public class SymbolTable : TableList
    //{
    //    public void Add(Symbol symbol)
    //    {
    //        if (symbol == null) return;
    //        base.Add(symbol.Name, symbol);
    //    }
    //    public new Symbol this[string key]
    //    {
    //        get { return (Symbol)base[key]; }
    //    }
    //    public new Symbol this[int index]
    //    {
    //        get { return (Symbol)base[index]; }
    //    }
    //}




}
