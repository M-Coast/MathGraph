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