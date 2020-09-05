/*****************************************************************************************

    MathGraph
    
    Copyright (C)  Coast


    AUTHOR      :  Coast   
    DATE        :  2020/9/3
    DESCRIPTION :  

 *****************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coast.Math.Expression
{
    public class Syntax : ModuleBase
    {

        private Dictionary<string, Symbol> _symtab = null;

        private ExpressionGroup _expressions = null;

        public ExpressionGroup Expressions { get { return _expressions; } }

        public Syntax()
        {

        }

        private Lex _lex = null;
        private Lex lex = null;

        public string SoureCode { get; set; }

        public void Parse()
        {
            _lex = new Lex();
            _lex.ModuleName = "Lexer";
            _lex.SourceCode = SoureCode;
            
            lex = _lex;

            Reset();

            ParseExpressionGroup();
        }

        public void Parse(string soureCode)
        {
            SoureCode = soureCode;
            Parse();
        }


        public override void Reset()
        {
            if (_expressions != null)
            {
                if (_expressions.SymbolTable != null) _expressions.SymbolTable.Clear();
                if (_expressions.Expressions != null) _expressions.Expressions.Clear();
                _expressions = null;
            }
            base.Reset();
        }

        private void ParsePrimaryExpression(ref Expression expr)
        {
            Token token = lex.Parse();
            if (Lex.TokenIsOperator(token, OperatorCode.LeftParenthesis))
            {
                Expression v = null;
                ParseAdditiveExpression(ref v);
                token = lex.Parse();
                if (Lex.TokenIsNotOperator(token, OperatorCode.RightParenthesis))
                {
                    //LogStatus(StatusLog.StateType_e.Error, "Paren expression wrong");
                    _errored = true;
                    return;
                }
                //expr = MakeNode(AstNodeType_e.Expression_Paren, v);
                expr = new ParenthesisExpression(v);
            }
            else if (Lex.TokenIsOperator(token, OperatorCode.LeftBrace))
            {
                //string text = lex.GetRawStringUntil('}');
                //token = lex.Parse();
                //ExpressionBrace v = new ExpressionBrace();
                //v.BracedText = MakeNode(AstNodeType_e.RawString, "{" + text + "}");
                //expr = MakeNode(AstNodeType_e.Expression_Brace, v);
                //lex.ConsumeAToken();
            }
            else if (Lex.TokenIs(token, TokenType.Identifier))
            {
                Identifier id = (Identifier)token.Value;
                Symbol sym = null;
                if (_symtab.ContainsKey(id.Name))
                {
                    sym = _symtab[id.Name];
                }
                else
                {
                    sym = new Symbol(id.Name, SymbolType.Variable, id);
                    _symtab.Add(id.Name, sym);
                }
                id.SymbolEntry = sym;
                expr = new IdentifierExpression(id);
            }
            else if (Lex.TokenIs(token, TokenType.Constant))
            {
                expr = new ConstantExpression((Constant)token.Value);
            }
            //else if (Lex.TokenIs(token, TokenType_e.Label))
            //{
            //    //need to check if label re-define
            //    Label label = (Label)token.Value;
            //    Symbol sym;
            //    if (_symtab.Contains(label.Name))
            //    {
            //        sym = _symtab[label.Name];
            //    }
            //    else
            //    {
            //        sym = new Symbol(label.Name, SymbolType_e.Label, label);
            //        _symtab.Add(sym);
            //    }
            //    expr = MakeNode(AstNodeType_e.Label, token.Value);
            //}
            //else
            //{
            //    LogStatus(StatusLog.StateType_e.Error, "Errored");
            //    _errored = true;
            //    return;
            //}
        }

        private List<Expression> ParseFunctionArgs(Function function)
        {
            Token token = lex.Parse();

            List<Expression> args = new List<Expression>();

            if (Lex.TokenIsNotOperator(token, OperatorCode.LeftParenthesis))
            {
                _errored = true;
                return null;
            }
            
            
            do
            {
                Expression arg = null;
                ParseAdditiveExpression(ref arg);
                args.Add(arg);
                token = lex.Parse();
            }
            while (Lex.TokenIsOperator(token, OperatorCode.Comma));


            if (Lex.TokenIsNotOperator(token, OperatorCode.RightParenthesis))
            {
                _errored = true;
                return null;
            }

            //Compare Args Count
            if(function.Parameters.Count != args.Count)
            {
                _errored = true;
                return null;
            }


            return args;
        }

        private void ParsePostfixExpression(ref Expression expr)
        {
            Expression primary = null;

            ParsePrimaryExpression(ref primary);

            if (_errored) return;
            if (primary == null) return;

            Token token = lex.PreviewNextToken();

            if (Lex.TokenIsOperator(token, OperatorCode.LeftParenthesis))
            {
                if (primary.Type != ExpressionType.IdentifierExpression)
                {
                    //LogStatus(StatusLog.StateType_e.Error, "");
                    _errored = true;
                    return;
                }

                Symbol symbol = ((IdentifierExpression)primary).Identifier.SymbolEntry;

                if (symbol.Type != SymbolType.Function)
                {
                    _errored = true;
                    return;
                }


                Function func = (Function)symbol.Value;
                List<Expression> args = ParseFunctionArgs(func);
                FuncCallExpression funcCall = new FuncCallExpression(func, args);
                

                expr = funcCall;
                return;

            }

            expr = primary;
        }

        
        private void ParseExponentialExpression(ref Expression expr)
        {
            Expression __base = null;

            ParsePostfixExpression(ref __base);

            if (_errored) return;
            if (__base == null) return;

            Token token = lex.PreviewNextToken();

            while (Lex.TokenIsOperator(token, OperatorCode.Xor))
            {
                lex.ConsumeAToken();
                Expression __power = null;
                ParsePostfixExpression(ref __power);

                if (__power == null)
                {
                    return;
                }

                Expression exprN = new ExponentialExpression(__base, __power);

                __base = exprN;
                exprN = null; __power = null;
                token = lex.PreviewNextToken();
            }
            expr = __base;
        }

        private void ParseUnaryExpression(ref Expression expr)
        {
            Token token = lex.PreviewNextToken();

            if (Lex.TokenIsOperator(token, OperatorCode.Minus))
            {
                lex.ConsumeAToken();
                if (lex.NextTokenIs(TokenType.Constant))
                {
                    token = lex.Parse();
                    Constant o = (Constant)token.Value;
                    Constant c = new Constant(o.Value * -1);
                    expr = new ConstantExpression(c);
                }
                else
                {
                    Expression exprN = null;
                    ParsePostfixExpression(ref exprN);
                    if (exprN == null) { return; }
                    expr = new NegativeExpression(exprN);
                }
            }
            else if (Lex.TokenIsOperator(token, OperatorCode.Plus))
            {
                lex.ConsumeAToken();
                if (lex.NextTokenIs(TokenType.Constant))
                {
                    token = lex.Parse();
                    Constant o = (Constant)token.Value;
                    Constant c = new Constant(o.Value * +1);
                    expr = new ConstantExpression(c);
                }
                else
                {
                    Expression exprN = null;
                    ParsePostfixExpression(ref exprN);
                    if (exprN == null) { return; }
                    expr = new PositiveExpression(exprN);
                }
            }
            else
            {
                ParsePostfixExpression(ref expr);
            }
        }

        private void ParseMultiplicativeExpression(ref Expression expr)
        {
            Expression left = null;

            ParseUnaryExpression(ref left);

            if (_errored) return;
            if (left == null) return;

            Token token = lex.PreviewNextToken();

            while (Lex.TokenIsOperator(token, OperatorCode.Mul) || Lex.TokenIsOperator(token, OperatorCode.Div))
            {
                lex.ConsumeAToken();
                Expression right = null;
                ParseUnaryExpression(ref right);

                if (right == null)
                {
                    return;
                }

                Expression exprN =
                    (OperatorCode)token.Value == OperatorCode.Mul ?
                    (Expression)new MultiplicationExpression(left, right) :
                    (Expression)new DivisionExpression(left, right);

                left = exprN;
                exprN = null; right = null;
                token = lex.PreviewNextToken();
            }
            expr = left;
        }

        private void ParseAdditiveExpression(ref Expression expr)
        {
            Expression left = null;

            ParseMultiplicativeExpression(ref left);

            if (_errored) return;
            if (left == null) return;

            Token token = lex.PreviewNextToken();

            while (Lex.TokenIsOperator(token, OperatorCode.Plus) || Lex.TokenIsOperator(token, OperatorCode.Minus))
            {
                lex.ConsumeAToken();
                Expression right = null;
                ParseMultiplicativeExpression(ref right);

                if(right==null)
                {
                    return;
                }

                Expression exprN =
                    (OperatorCode)token.Value == OperatorCode.Plus ?
                    (Expression)new AdditionExpression(left, right) :
                    (Expression)new SubtractionExpression(left, right);

                left = exprN;
                exprN = null; right = null;
                token = lex.PreviewNextToken();
            }
            expr = left;
        }


        private void ParseEquationExpression(ref Expression expr)
        {
            Expression left = null;
            Expression right = null;

            ParseAdditiveExpression(ref left);

            if (_errored) return;
            if (left == null) return;

            if (lex.NextTokenIsOperator(OperatorCode.Equals))
            {
                lex.ConsumeAToken();
                ParseAdditiveExpression(ref right);
                expr = new EquationExpression(left, right);
                return;
            }
            else
            {
                //LogStatus(StatusLog.StateType_e.Error, "Expression error in Not a Equation exprssion");
                _errored = true;
                expr = null;
                return;
            }


        }

        private void ParseExpressionGroup()
        {
            Token token = lex.PreviewNextToken();

            _expressions = new ExpressionGroup();

            //List<Function> bif = Statics.BuiltInFunction.ToList();

            string[] keys = Statics.BuiltInFunctions.Keys.ToArray();

            foreach(string key in keys)
            {
                _expressions.SymbolTable.Add(key, new Symbol() { Name = key, Type = SymbolType.Function, Value = Statics.BuiltInFunctions[key] });
            }

            _symtab = _expressions.SymbolTable;

            while (lex.NextTokenIsNot(TokenType.EOF))
            {
                Expression expr = null;
                ParseEquationExpression(ref expr);
                if (expr != null) _expressions.Expressions.Add(expr);
                if (_errored) { return; }
            }
        }






        public void DebugTest(string sourceCode)
        {
            Parse(sourceCode);
        }


        public string GenerateCode(Expression expr)
        {
            string s = string.Empty;
            switch (expr.Type)
            {
                case ExpressionType.IdentifierExpression:
                    {
                        IdentifierExpression e = (IdentifierExpression)expr;
                        s += e.Identifier.ToString();
                    }
                    break;
                case ExpressionType.ConstantExpression:
                    {
                        ConstantExpression e = (ConstantExpression)expr;
                        s += e.Constant.Value.ToString();
                    }
                    break;
                case ExpressionType.ParenthesisExpression:
                    {
                        ParenthesisExpression e = (ParenthesisExpression)expr;
                        s += "(" + GenerateCode(e.Expression) + ")";
                    }
                    break;
                case ExpressionType.FuncCallExpression: break;
                case ExpressionType.ExponentialExpression: break;
                case ExpressionType.NegativeExpression:
                    {
                        NegativeExpression e = (NegativeExpression)expr;
                        s += "-" + GenerateCode(e.Expression);
                    }
                    break;
                case ExpressionType.PositiveExpression:
                    {
                        PositiveExpression e = (PositiveExpression)expr;
                        s += "+" + GenerateCode(e.Expression);
                    }
                    break;
                case ExpressionType.AdditionExpression:
                case ExpressionType.SubtractionExpression:
                case ExpressionType.MultiplicationExpression:
                case ExpressionType.DivisionExpression:
                    {
                        BinaryExpression e = (BinaryExpression)expr;
                        s += GenerateCode(e.Left) + " " + Statics.OperatorTextTable[e.Operator] + " " + GenerateCode(e.Right);
                    }
                    break;
                case ExpressionType.EquationExpression:
                    {
                        EquationExpression e = (EquationExpression)expr;
                        s += GenerateCode(e.Left) + " " + Statics.OperatorTextTable[e.Operator] + " " + GenerateCode(e.Right);
                    }
                    break;
                case ExpressionType.ExpressionGroup:
                    {
                        ExpressionGroup e = (ExpressionGroup)expr;
                        foreach (Expression ie in e.Expressions)
                        {
                            s += GenerateCode(ie) + "\r\n";
                        }
                    }
                    break;
                default:
                    break;
            }
            return s;
        }


        private string _exprText = string.Empty;
        private int _indentLevel = -1;
        private void PrintIndent()
        {
            //_exprText;
        }

        public void PrintExpression(Expression expr)
        {
            _indentLevel++;
            PrintIndent();

            switch (expr.Type)
            {
                case ExpressionType.IdentifierExpression: _exprText += ((IdentifierExpression)expr).Identifier.Name;break;

                case ExpressionType.ConstantExpression:
                case ExpressionType.ParenthesisExpression:
                case ExpressionType.FuncCallExpression:
                case ExpressionType.ExponentialExpression:
                case ExpressionType.NegativeExpression:
                case ExpressionType.PositiveExpression:
                case ExpressionType.AdditionExpression:
                case ExpressionType.SubtractionExpression:
                case ExpressionType.MultiplicationExpression:
                case ExpressionType.DivisionExpression:
                case ExpressionType.EquationExpression:
                case ExpressionType.ExpressionGroup:
                    break;
                default:
                    break;
            }
            _indentLevel--;
        }

        public void PrintExpressionGroup(ExpressionGroup group)
        {
            _exprText = string.Empty;

               _indentLevel++;

        }

    }
}












//private void ParseExpressionPostfix(ref Expression expr)
//{
//}

//private void ParseConstNumberWithSign(ref Expression expr, int sign)
//{
//}

//private void ParseExpressionUnary(ref Expression expr)
//{

//}
//private void ParseExpressionMultiplicative(ref Expression expr)
//{
//}
//private void ParseExpressionAdditive(ref Expression expr)
//{
//}

//private void ParseStatementList(ref Expression stmtList)
//{
//    //Token token = lex.PreviewNextToken();

//    //StatementList valStmtList = new StatementList();
//    //valStmtList.List = new List<Expression>();

//    //while (lex.NextTokenIsNot(TokenType_e.EOF))
//    //{
//    //    Expression stmt = null;
//    //    ParseStatement(ref stmt);
//    //    if (stmt != null) valStmtList.List.Add(stmt);
//    //    if (_errored) { return; }
//    //}

//    //stmtList = MakeNode(AstNodeType_e.Statement_List, valStmtList);

//}



//int nodeCount = 0;
//private Expression MakeNode(ExpressionType nodeType, object value)
//{
//    Location location = new Location();
//    location.FileName = "";
//    location.Line = _lex.Line;
//    location.Column = _lex.Column;
//    Expression node = new Expression(nodeType, value);
//    node.Location = location;
//    nodeCount++;
//    return node;
//}




//int nodeCount = 0;
//private AstNode MakeNode(AstNodeType_e nodeType, object value)
//{
//    Location location = new Location();
//    location.FileName = "";
//    location.Line = _lex.Line;
//    location.Column = _lex.Column;
//    AstNode node = new AstNode(nodeType, value);
//    node.Location = location;
//    nodeCount++;
//    return node;
//}

//private void ParseExpressionPrimary(ref AstNode expr)
//{
//    Token token = lex.Parse();
//    if (Lex.TokenIsOperator(token, OperatorCode_e.LeftParen))
//    {
//        AstNode v = null;
//        ParseExpressionOr(ref v);
//        token = lex.Parse();
//        if (Lex.TokenIsNotOperator(token, OperatorCode_e.RightParen))
//        {
//            LogStatus(StatusLog.StateType_e.Error, "Paren expression wrong");
//            _errored = true;
//            return;
//        }
//        expr = MakeNode(AstNodeType_e.Expression_Paren, v);
//    }
//    else if (Lex.TokenIsOperator(token, OperatorCode_e.LeftBrace))
//    {
//        string text = lex.GetRawStringUntil('}');
//        token = lex.Parse();
//        ExpressionBrace v = new ExpressionBrace();
//        v.BracedText = MakeNode(AstNodeType_e.RawString, "{" + text + "}");
//        expr = MakeNode(AstNodeType_e.Expression_Brace, v);
//        //lex.ConsumeAToken();
//    }
//    else if (Lex.TokenIs(token, TokenType_e.Identifier))
//    {
//        Identifier id = (Identifier)token.Value;
//        Symbol sym;
//        if (_symtab.Contains(id.Name))
//        {
//            sym = _symtab[id.Name];
//        }
//        else
//        {
//            sym = new Symbol(id.Name, SymbolType_e.LocalVariable, id);
//            _symtab.Add(sym);
//        }
//        expr = MakeNode(AstNodeType_e.Identifier, token.Value);
//    }
//    else if (Lex.TokenIs(token, TokenType_e.Command))
//    {
//        ExpressionCommandFunction cmdfunc = new ExpressionCommandFunction();
//        cmdfunc.Command = MakeNode(AstNodeType_e.Command, (CommandCode)token.Value);
//        expr = MakeNode(AstNodeType_e.Expression_CommandFunction, cmdfunc);
//    }
//    else if (Lex.TokenIs(token, TokenType_e.Constant))
//    {
//        expr = MakeNode(AstNodeType_e.Constant, token.Value);
//    }
//    else if (Lex.TokenIs(token, TokenType_e.Label))
//    {
//        //need to check if label re-define
//        Label label = (Label)token.Value;
//        Symbol sym;
//        if (_symtab.Contains(label.Name))
//        {
//            sym = _symtab[label.Name];
//        }
//        else
//        {
//            sym = new Symbol(label.Name, SymbolType_e.Label, label);
//            _symtab.Add(sym);
//        }
//        expr = MakeNode(AstNodeType_e.Label, token.Value);
//    }
//    else
//    {
//        LogStatus(StatusLog.StateType_e.Error, "Errored");
//        _errored = true;
//        return;
//    }
//}

//private void ParseExpressionPostfix(ref AstNode expr)
//{
//    AstNode primary = null;

//    ParseExpressionPrimary(ref primary);

//    if (_errored) return;
//    if (primary == null) return;


//    if (primary.Type == AstNodeType_e.Expression_CommandFunction)
//    {
//        //already set something to expr
//        expr = primary;
//        ParseCommandFunction(ref expr);
//        return;
//    }

//    Token token = lex.PreviewNextToken();

//    if (Lex.TokenIsOperator(token, OperatorCode_e.LeftSquare))
//    {
//        if (primary.Type != AstNodeType_e.Identifier)
//        {
//            LogStatus(StatusLog.StateType_e.Error, "Array wrong");
//            _errored = true;
//            return;
//        }

//        lex.ConsumeAToken();

//        AstNode accessor = null;
//        ParseExpressionAdditive(ref accessor);
//        token = lex.Parse();
//        if (Lex.TokenIsNotOperator(token, OperatorCode_e.RightSquare))
//        {
//            LogStatus(StatusLog.StateType_e.Error, "Endless [ ]");
//            _errored = true;
//            return;
//        }
//        ExpressionArrayAccesor v = new ExpressionArrayAccesor();
//        v.Array = primary;
//        v.Accessor = accessor;
//        expr = MakeNode(AstNodeType_e.Expression_ArrayAccesor, v);

//        return;
//    }

//    expr = primary;
//}

//private void ParseConstNumberWithSign(ref AstNode expr, int sign)
//{
//    Token token = lex.Parse();

//    Constant c = (Constant)token.Value;

//    if (!c.IsNumber)
//    {
//        LogStatus(StatusLog.StateType_e.Error, "+/- Sign before a non number constant");
//        _errored = true; expr = null; return;
//    }

//    expr = MakeNode(AstNodeType_e.Constant, c);

//    if (sign == -1)
//    {
//        if (c.IsInteger)
//        {
//            //check *-1, and re-arrange
//            //like INI16 -32768 to +32767
//            long v = Convert.ToInt64(c.Value);
//            v = v * -1;
//            if (v >= int.MinValue && v <= int.MaxValue)
//            {
//                c.Value = (int)v;
//            }
//            else
//            {
//                c.Value = v;
//            }
//        }
//        else if (c.IsFloating)
//        {
//            double v = Convert.ToDouble(c.Value);
//            v = v * -1;
//            c.Value = v;
//        }
//    }

//}

//private void ParseExpressionUnary(ref AstNode expr)
//{
//    Token token = lex.PreviewNextToken();

//    if (Lex.TokenIsOperator(token, OperatorCode_e.Not))
//    {
//        lex.ConsumeAToken();
//        ExpressionNot val = new ExpressionNot();
//        ParseExpressionPostfix(ref val.Expression);
//        expr = MakeNode(AstNodeType_e.Expression_Not, val);
//    }
//    else if (Lex.TokenIsOperator(token, OperatorCode_e.Minus))
//    {
//        lex.ConsumeAToken();
//        if (lex.NextTokenIs(TokenType_e.Constant))
//        {
//            ParseConstNumberWithSign(ref expr, -1);
//        }
//        else
//        {
//            ExpressionNegative val = new ExpressionNegative();
//            ParseExpressionPostfix(ref val.Expression);
//            expr = MakeNode(AstNodeType_e.Expression_Negative, val);
//        }
//    }
//    else if (Lex.TokenIsOperator(token, OperatorCode_e.Plus))
//    {
//        lex.ConsumeAToken();
//        if (lex.NextTokenIs(TokenType_e.Constant))
//        {
//            ParseConstNumberWithSign(ref expr, +1);
//        }
//        else
//        {
//            ExpressionPositive val = new ExpressionPositive();
//            ParseExpressionPostfix(ref val.Expression);
//            expr = MakeNode(AstNodeType_e.Expression_Positive, val);
//        }
//    }
//    else
//    {
//        ParseExpressionPostfix(ref expr);
//    }
//}




//private void ParseExpressionMultiplicative(ref AstNode expr)
//{
//    AstNode left = null;

//    ParseExpressionUnary(ref left);

//    if (_errored) return;
//    if (left == null) return;

//    Token token = lex.PreviewNextToken();

//    while (Lex.TokenIsOperator(token, OperatorCode_e.Mult) || Lex.TokenIsOperator(token, OperatorCode_e.Div) ||
//        Lex.TokenIsOperator(token, OperatorCode_e.Mod))
//    {
//        lex.ConsumeAToken();
//        AstNode right = null;
//        ParseExpressionUnary(ref right);

//        OperatorCode_e opCode = (OperatorCode_e)token.Value;
//        ExpressionMultiplicative val = new ExpressionMultiplicative(opCode, left, right);
//        AstNode node = MakeNode(AstNodeType_e.Expression_Multiplicative, val);
//        left = node;
//        node = null; right = null;
//        token = lex.PreviewNextToken();
//    }
//    expr = left;

//}


//private void ParseExpressionAdditive(ref AstNode expr)
//{
//    AstNode left = null;

//    ParseExpressionMultiplicative(ref left);

//    if (_errored) return;
//    if (left == null) return;

//    Token token = lex.PreviewNextToken();

//    while (Lex.TokenIsOperator(token, OperatorCode_e.Minus) || Lex.TokenIsOperator(token, OperatorCode_e.Plus))
//    {
//        lex.ConsumeAToken();
//        AstNode right = null;
//        ParseExpressionMultiplicative(ref right);

//        OperatorCode_e opCode = (OperatorCode_e)token.Value;
//        ExpressionAdditive val = new ExpressionAdditive(opCode, left, right);
//        AstNode node = MakeNode(AstNodeType_e.Expression_Additive, val);
//        left = node;
//        node = null; right = null;
//        token = lex.PreviewNextToken();
//    }
//    expr = left;
//}

//private void ParseExpressionRelational(ref AstNode expr)
//{
//    AstNode left = null;

//    ParseExpressionAdditive(ref left);

//    if (_errored) return;
//    if (left == null) return;

//    Token token = lex.PreviewNextToken();

//    while (Lex.TokenIsOperator(token, OperatorCode_e.LessThan) || Lex.TokenIsOperator(token, OperatorCode_e.LessEqual) ||
//        Lex.TokenIsOperator(token, OperatorCode_e.GreaterThan) || Lex.TokenIsOperator(token, OperatorCode_e.GreaterEqual) ||
//        Lex.TokenIsOperator(token, OperatorCode_e.Equals) || Lex.TokenIsOperator(token, OperatorCode_e.NotEqual))
//    {
//        lex.ConsumeAToken();
//        AstNode right = null;
//        ParseExpressionAdditive(ref right);

//        OperatorCode_e opCode = (OperatorCode_e)token.Value;
//        ExpressionRelational val = new ExpressionRelational(opCode, left, right);
//        AstNode node = MakeNode(AstNodeType_e.Expression_Relational, val);
//        left = node;
//        node = null; right = null;
//        token = lex.PreviewNextToken();
//    }
//    expr = left;
//}

//private void ParseExpressionAnd(ref AstNode expr)
//{
//    AstNode left = null;

//    ParseExpressionRelational(ref left);

//    if (_errored) return;
//    if (left == null) return;

//    Token token = lex.PreviewNextToken();

//    while (Lex.TokenIsOperator(token, OperatorCode_e.And))
//    {
//        lex.ConsumeAToken();
//        AstNode right = null;
//        ParseExpressionRelational(ref right);

//        OperatorCode_e opCode = (OperatorCode_e)token.Value;
//        ExpressionAnd val = new ExpressionAnd(opCode, left, right);
//        AstNode node = MakeNode(AstNodeType_e.Expression_And, val);
//        left = node;
//        node = null; right = null;
//        token = lex.PreviewNextToken();
//    }
//    expr = left;
//}

//private void ParseExpressionOr(ref AstNode expr)
//{
//    AstNode left = null;

//    ParseExpressionAnd(ref left);

//    if (_errored) return;
//    if (left == null) return;

//    Token token = lex.PreviewNextToken();

//    while (Lex.TokenIsOperator(token, OperatorCode_e.Or))
//    {
//        lex.ConsumeAToken();
//        AstNode right = null;
//        ParseExpressionAnd(ref right);

//        OperatorCode_e opCode = (OperatorCode_e)token.Value;
//        ExpressionOr val = new ExpressionOr(opCode, left, right);
//        AstNode node = MakeNode(AstNodeType_e.Expression_Or, val);
//        left = node;
//        node = null; right = null;
//        token = lex.PreviewNextToken();
//    }
//    expr = left;
//}


//private void ParseExpressionAssignment(ref AstNode expr)
//{
//    AstNode left = null;
//    AstNode right = null;

//    ParseExpressionPostfix(ref left);

//    if (_errored) return;
//    if (left == null) return;

//    if (lex.NextTokenIsOperator(OperatorCode_e.Equals))
//    {
//        //assignment expression only allow identifier and array accessor 
//        if (!(left.Type == AstNodeType_e.Identifier || left.Type == AstNodeType_e.Expression_ArrayAccesor))
//        {
//            LogStatus(StatusLog.StateType_e.Error, "Assignment expression, can't be left value");
//            _errored = true; expr = null; return;
//        }

//        lex.ConsumeAToken();

//        ParseExpressionOr(ref right);
//        ExpressionAssignment val = new ExpressionAssignment(left, right);
//        expr = MakeNode(AstNodeType_e.Expression_Assignment, val);
//        return;
//    }

//    //Non-assignment expression, Only allow: Identifier,Label, CommandFunction,{xxx}, 
//    //{xxx} is just for debug, it's in CommandFunction args
//    if (left.Type == AstNodeType_e.Identifier)
//    {
//        Identifier id = (Identifier)left.Value;
//        Symbol sym = SymbolTable[id.Name];
//        if (sym == null)
//        {
//            LogStatus(StatusLog.StateType_e.Error, "Unknown Error 1");
//            _errored = true; expr = null; return;
//        }

//        if (sym.Type == SymbolType_e.GlobalVariable)
//        {
//            //if it's GlobalVariable, set 1
//            AstNode assignValue = MakeNode(AstNodeType_e.Constant, new Constant(1));
//            ExpressionAssignment val = new ExpressionAssignment(left, assignValue);
//            expr = MakeNode(AstNodeType_e.Expression_Assignment, val);
//        }
//        else if (sym.Type == SymbolType_e.LocalVariable)
//        {
//            //if it's LocalVariable, set 0
//            AstNode assignValue = MakeNode(AstNodeType_e.Constant, new Constant(0));
//            ExpressionAssignment val = new ExpressionAssignment(left, assignValue);
//            expr = MakeNode(AstNodeType_e.Expression_Assignment, val);
//        }
//        else
//        {
//            LogStatus(StatusLog.StateType_e.Error, "Unknown Error 2");
//            _errored = true; expr = null; return;
//        }
//    }
//    else if (left.Type == AstNodeType_e.Expression_CommandFunction)
//    {
//        expr = left;
//    }
//    else if (left.Type == AstNodeType_e.Label)
//    {
//        expr = left;
//    }
//    else if (left.Type == AstNodeType_e.Expression_Brace)
//    {
//        expr = left;
//    }
//    else
//    {
//        LogStatus(StatusLog.StateType_e.Error, "Expression error in non-assignment exprssion");
//        _errored = true; expr = null; return;
//    }

//}

//private bool IsConditionExpression(AstNode expr)
//{
//    switch (expr.Type)
//    {
//        case AstNodeType_e.Expression_Or:
//        case AstNodeType_e.Expression_And:
//        case AstNodeType_e.Expression_Relational:
//        case AstNodeType_e.Identifier:
//        case AstNodeType_e.Expression_ArrayAccesor:
//            return true;
//        case AstNodeType_e.Expression_Paren:
//            return IsConditionExpression((AstNode)expr.Value);
//        default:
//            return false;
//    }
//}
