using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coast.Math.Expression
{
    //class Lex// : ModuleBase
    //{
    //    private string _sourceCode = null;
    //    private char[] _source = null;

    //    bool _errored;

    //    public string SourceCode
    //    {
    //        get { return _sourceCode; }
    //        set
    //        {
    //            _sourceCode = value;
    //            if (_sourceCode == null || _sourceCode.Length <= 0)
    //            {
    //                _errored = true; return;
    //            }
    //            _source = _sourceCode.ToCharArray();
    //            //_errored = true; return;
    //            InitLineSections();
    //        }
    //    }

    //    private SymbolTable _symtab = null;

    //    public SymbolTable SymbolTable
    //    {
    //        get { return _symtab; }
    //        set { _symtab = value; }
    //    }


    //    private int pos = 0;
    //    private Stack<int> posStack = new Stack<int>();


    //    List<int> lineSections = new List<int>();
    //    private void InitLineSections()
    //    {
    //        if (_source == null) return;
    //    }

    //    public int Line
    //    {
    //        get
    //        {
    //            if (_source == null) return -1;
    //            int i;
    //            int line = 0;
    //            for (i = 0; i < pos; i++)
    //            {
    //                if (i > _source.Length - 1) break;
    //                if (_source[i] == '\n') line++;
    //            }
    //            return line;
    //        }
    //    }
    //    public int Column
    //    {
    //        get
    //        {
    //            if (_source == null) return -1;
    //            int i;
    //            int column = 0;
    //            for (i = pos; i >= 0; i--)
    //            {
    //                if (i > _source.Length - 1) continue;
    //                if (_source[i] == '\n') break;
    //                column++;
    //            }
    //            return column;
    //        }
    //    }


    //    public Token Parse()
    //    {
    //        if (_errored) return null;

    //        SkipBlank();

    //        Token token = new Token();

    //        token.Location.Line = Line;
    //        token.Location.Column = Column;
    //        token.Type = TokenType_e.None;

    //        if (_EOF()) { token.Type = TokenType_e.EOF; return token; }


    //        switch (_source[pos])
    //        {
    //            #region Case Identifier
    //            case 'A':
    //            case 'B':
    //            case 'C':
    //            case 'D':
    //            case 'E':
    //            case 'F':
    //            case 'G':
    //            case 'H':
    //            case 'I':
    //            case 'J':
    //            case 'K':
    //            case 'L':
    //            case 'M':
    //            case 'N':
    //            case 'O':
    //            case 'P':
    //            case 'Q':
    //            case 'R':
    //            case 'S':
    //            case 'T':
    //            case 'U':
    //            case 'V':
    //            case 'W':
    //            case 'X':
    //            case 'Y':
    //            case 'Z':
    //            case 'a':
    //            case 'b':
    //            case 'c':
    //            case 'd':
    //            case 'e':
    //            case 'f':
    //            case 'g':
    //            case 'h':
    //            case 'i':
    //            case 'j':
    //            case 'k':
    //            case 'l':
    //            case 'm':
    //            case 'n':
    //            case 'o':
    //            case 'p':
    //            case 'q':
    //            case 'r':
    //            case 's':
    //            case 't':
    //            case 'u':
    //            case 'v':
    //            case 'w':
    //            case 'x':
    //            case 'y':
    //            case 'z':
    //            case '_':
    //                #endregion
    //                ParseName(token);
    //                break;

    //            #region Case Const Number
    //            case '0':
    //            case '1':
    //            case '2':
    //            case '3':
    //            case '4':
    //            case '5':
    //            case '6':
    //            case '7':
    //            case '8':
    //            case '9':
    //            case '$':
    //            case '.':
    //                #endregion
    //                ParseConstNumber(token);
    //                break;

    //            case '"': ParseConstString(token); break;
    //            case '#': ParseLabel(token); break;
    //            case '\'': ParseComment(token); break;

    //            #region Case Operators
    //            case '\r': token.Type = TokenType_e.Operator; token.Value = OperatorCode_e.Return; pos++; break;
    //            case '\n': token.Type = TokenType_e.Operator; token.Value = OperatorCode_e.NewLine; pos++; break;

    //            case '!': token.Type = TokenType_e.Operator; token.Value = OperatorCode_e.Not; pos++; break;
    //            case '%': token.Type = TokenType_e.Operator; token.Value = OperatorCode_e.Mod; pos++; break;
    //            case '*': token.Type = TokenType_e.Operator; token.Value = OperatorCode_e.Mult; pos++; break;
    //            case '+': token.Type = TokenType_e.Operator; token.Value = OperatorCode_e.Plus; pos++; break;
    //            case '-': token.Type = TokenType_e.Operator; token.Value = OperatorCode_e.Minus; pos++; break;
    //            case '/': token.Type = TokenType_e.Operator; token.Value = OperatorCode_e.Div; pos++; break;
    //            case '=': token.Type = TokenType_e.Operator; token.Value = OperatorCode_e.Equals; pos++; break;

    //            case '<':
    //                token.Type = TokenType_e.Operator; token.Value = OperatorCode_e.LessThan; pos++;
    //                if (NextCharIs('=')) { token.Value = OperatorCode_e.LessEqual; pos++; }
    //                else if (NextCharIs('>')) { token.Value = OperatorCode_e.NotEqual; pos++; }
    //                break;

    //            case '>':
    //                token.Type = TokenType_e.Operator; token.Value = OperatorCode_e.GreaterThan; pos++;
    //                if (NextCharIs('=')) { token.Value = OperatorCode_e.GreaterEqual; pos++; }
    //                break;

    //            case '&': token.Type = TokenType_e.Operator; token.Value = OperatorCode_e.And; pos++; break;
    //            case '|': token.Type = TokenType_e.Operator; token.Value = OperatorCode_e.And; pos++; break;

    //            case ',': token.Type = TokenType_e.Operator; token.Value = OperatorCode_e.Comma; pos++; break;
    //            case ';': token.Type = TokenType_e.Operator; token.Value = OperatorCode_e.SemiColon; pos++; break;

    //            case '(': token.Type = TokenType_e.Operator; token.Value = OperatorCode_e.LeftParen; pos++; break;
    //            case ')': token.Type = TokenType_e.Operator; token.Value = OperatorCode_e.RightParen; pos++; break;
    //            case '[': token.Type = TokenType_e.Operator; token.Value = OperatorCode_e.LeftSquare; pos++; break;
    //            case ']': token.Type = TokenType_e.Operator; token.Value = OperatorCode_e.RightSquare; pos++; break;
    //            case '{': token.Type = TokenType_e.Operator; token.Value = OperatorCode_e.LeftBrace; pos++; break;
    //            case '}': token.Type = TokenType_e.Operator; token.Value = OperatorCode_e.RightBrace; pos++; break;

    //            case '\\': SkipTo('\n'); break;

    //            #endregion

    //            default:
    //                //case '\\':
    //                MissShot();
    //                break;


    //        }

    //        return token;
    //    }

    //    private void MissShot()
    //    {
    //        LogStatus(StatusLog.StateType_e.Error, "Illegal Operator");
    //    }


    //    public override void LogStatus(StatusLog.StateType_e state, string description)
    //    {
    //        description = description + ", At Location: " + Line.ToString() + "," + Column.ToString();
    //        base.LogStatus(state, description);
    //    }


    //    private bool _EOF()
    //    {
    //        if (pos >= _source.Length)
    //        {
    //            return true;
    //        }
    //        else
    //        {
    //            return false;
    //        }
    //    }

    //    private void SkipBlank()
    //    {
    //        while (!_EOF() && (_source[pos] == ' ' || _source[pos] == '\t' || _source[pos] == '\r'))
    //        {
    //            pos++;
    //        }
    //    }

    //    private void SkipTo(char until)
    //    {
    //        while (!_EOF() && _source[pos] != until)
    //        {
    //            pos++;
    //        }
    //        pos++;
    //    }

    //    private bool NextCharIs(char c)
    //    {
    //        if (_EOF()) return false;
    //        if (_source[pos] == c)
    //        {
    //            return true;
    //        }
    //        else
    //        {
    //            return false;
    //        }
    //    }

    //    private static bool CharIsIdName(char c)
    //    {
    //        if (c >= 'A' && c <= 'Z' || c >= 'a' && c <= 'z' || c >= '0' && c <= '9' || c == '_')
    //        {
    //            return true;
    //        }
    //        else
    //        {
    //            return false;
    //        }
    //    }

    //    private static bool CharIsLetter(char c)
    //    {
    //        if (c >= 'A' && c <= 'Z' || c >= 'a' && c <= 'z')
    //        {
    //            return true;
    //        }
    //        else
    //        {
    //            return false;
    //        }
    //    }

    //    private static bool CharIsUpperCaseLetter(char c)
    //    {
    //        if (c >= 'A' && c <= 'Z')
    //        {
    //            return true;
    //        }
    //        else
    //        {
    //            return false;
    //        }
    //    }

    //    private static bool CharIsLowerCaseLetter(char c)
    //    {
    //        if (c >= 'A' && c <= 'Z')
    //        {
    //            return true;
    //        }
    //        else
    //        {
    //            return false;
    //        }
    //    }

    //    private static bool CharIsHexNumber(char c)
    //    {
    //        if (c >= 'A' && c <= 'F' || c >= 'a' && c <= 'f' || c >= '0' && c <= '9')
    //        {
    //            return true;
    //        }
    //        else
    //        {
    //            return false;
    //        }
    //    }

    //    private static bool CharIsDecNumber(char c)
    //    {
    //        if (c >= '0' && c <= '9')
    //        {
    //            return true;
    //        }
    //        else
    //        {
    //            return false;
    //        }
    //    }

    //    private void ParseName(Token token)
    //    {
    //        string name;
    //        Symbol sym;
    //        int i;

    //        string text = "";

    //        while (!_EOF() && CharIsIdName(_source[pos]))
    //        {
    //            text = text + _source[pos];
    //            pos++;
    //        }

    //        //check precedence
    //        //  KeyWord
    //        //  GlobalVariable
    //        //  Command
    //        //  Identifier


    //        //check if keyword
    //        //check if GlobalVariable
    //        name = text;
    //        sym = _symtab[name];
    //        if (sym != null)
    //        {
    //            if (sym.Type == SymbolType_e.Keyword)
    //            {
    //                //keyword
    //                token.Type = TokenType_e.Keyword;
    //                token.Value = ((KeywordInfo)sym.Value).Code;
    //                return;
    //            }

    //            else if (sym.Type == SymbolType_e.GlobalVariable)
    //            {
    //                //GlobalVariable
    //                token.Type = TokenType_e.Identifier;
    //                token.Value = new Identifier(name);
    //                return;
    //            }
    //        }


    //        //Check if Command
    //        int unUsedChars = 0;
    //        for (i = text.Length; i >= 2; i--)
    //        {
    //            name = text.Substring(0, i);
    //            sym = _symtab[name];
    //            if (sym != null)
    //            {
    //                if (sym.Type == SymbolType_e.Command)
    //                {
    //                    token.Type = TokenType_e.Command;
    //                    token.Value = ((CommandInfo)sym.Value).Code;
    //                    //return to current position
    //                    pos = pos - unUsedChars;
    //                    return;
    //                }
    //            }
    //            unUsedChars++;
    //        }

    //        //Identifier
    //        name = text;
    //        token.Type = TokenType_e.Identifier;
    //        token.Value = new Identifier(name);
    //        return;


    //    }


    //    private void ParseConstNumber(Token token)
    //    {
    //        //if (_EOF()) return;

    //        int n_pos = pos;
    //        bool isFloat = false;

    //        string text = "";
    //        while (!_EOF() && (CharIsDecNumber(_source[pos]) || _source[pos] == '.'))
    //        {
    //            if (_source[pos] == '.')
    //            {
    //                if (!isFloat)
    //                    isFloat = true;
    //                else
    //                {
    //                    LogStatus(StatusLog.StateType_e.Error, "Too many floating dots");
    //                    _errored = true;
    //                    return;
    //                }
    //            }
    //            text = text + _source[pos];

    //            pos++;
    //        }

    //        if (isFloat)
    //        {
    //            double val = 0;
    //            if (!double.TryParse(text, out val))
    //            {
    //                LogStatus(StatusLog.StateType_e.Error, "Parse double value error");
    //                _errored = true;
    //                return;
    //            }
    //            token.Type = TokenType_e.Constant;
    //            token.Value = new Constant(val);
    //        }
    //        else
    //        {
    //            long val = 0;
    //            if (!long.TryParse(text, out val))
    //            {
    //                LogStatus(StatusLog.StateType_e.Error, "Parse integer value error");
    //                _errored = true;
    //                return;
    //            }
    //            if (val >= int.MinValue && val <= int.MaxValue)
    //            {
    //                token.Value = new Constant((int)val);
    //            }
    //            else
    //            {
    //                token.Value = new Constant(val);
    //            }
    //            token.Type = TokenType_e.Constant;
    //        }

    //    }

    //    private void ParseConstString(Token token)
    //    {
    //        // "\""
    //        pos++;

    //        if (_EOF()) return;

    //        string text = "";


    //        while (!_EOF() && _source[pos] != '\"')
    //        {

    //            text = text + _source[pos];
    //            pos++;
    //        }

    //        if (_EOF())
    //        {
    //            LogStatus(StatusLog.StateType_e.Error, "Parse constant string error");
    //            _errored = true;
    //            return;
    //        }

    //        token.Type = TokenType_e.Constant;
    //        token.Value = new Constant(text);
    //    }

    //    private void ParseLabel(Token token)
    //    {
    //        pos++;
    //        if (_EOF()) return;

    //        string text = "";

    //        while (!_EOF() && CharIsIdName(_source[pos]))
    //        {
    //            text = text + _source[pos];
    //            pos++;
    //        }

    //        if (text == "")
    //        {
    //            LogStatus(StatusLog.StateType_e.Error, "Parse Label error");
    //            _errored = true;
    //            return;
    //        }

    //        token.Type = TokenType_e.Label;
    //        token.Value = new Label("#" + text, Line);

    //    }

    //    private void ParseComment(Token token)
    //    {
    //        pos++;

    //        if (_EOF()) return;

    //        string text = "";

    //        while (!_EOF() && _source[pos] != '\n')
    //        {

    //            text = text + _source[pos];
    //            pos++;
    //        }

    //        token.Type = TokenType_e.Comment;
    //        token.Value = text;

    //    }

    //    public string GetRawStringUntil(char until)
    //    {
    //        string text = "";
    //        bool inQuotation = false;
    //        bool finished = false;

    //        while (!_EOF())
    //        {
    //            if (_source[pos] == '\"')
    //            {
    //                inQuotation = !inQuotation;
    //            }

    //            if (_source[pos] == until && !inQuotation)
    //            {
    //                finished = true;
    //                break;
    //            }
    //            text = text + _source[pos];
    //            pos++;
    //        }

    //        if (!finished)
    //        {
    //            LogStatus(StatusLog.StateType_e.Error, "Get raw string error");
    //            _errored = true;
    //            return null;
    //        }
    //        return text;
    //    }

    //    public string GetRawStringUntil(List<char> until, bool ignoreError = true)
    //    {
    //        string text = "";
    //        bool inQuotation = false;
    //        bool finished = false;

    //        while (!_EOF())
    //        {
    //            if (_source[pos] == '\"')
    //            {
    //                inQuotation = !inQuotation;
    //            }

    //            foreach (char c in until)
    //            {
    //                if (_source[pos] == c && !inQuotation)
    //                {
    //                    finished = true;
    //                }
    //            }
    //            if (finished) break;
    //            text = text + _source[pos];
    //            pos++;
    //        }


    //        if (!ignoreError && !finished)
    //        {
    //            LogStatus(StatusLog.StateType_e.Error, "Get raw string error");
    //            _errored = true;
    //            return null;
    //        }
    //        return text;
    //    }






    //    public Token PreviewNextToken()
    //    {
    //        posStack.Push(pos);
    //        Token token = Parse();
    //        pos = posStack.Pop();
    //        return token;
    //    }

    //    public void ConsumeAToken()
    //    {
    //        Parse();
    //    }

    //    public static bool TokenIs(Token token, TokenType_e type)
    //    {
    //        if (token.Type == type) return true;
    //        return false;
    //    }
    //    public static bool TokenIsNot(Token token, TokenType_e type)
    //    {
    //        return !TokenIs(token, type);
    //    }

    //    public static bool TokenIsOperator(Token token, OperatorCode_e opCpde)
    //    {
    //        if (token.Type != TokenType_e.Operator) return false;
    //        if ((OperatorCode_e)(token.Value) != opCpde) return false;
    //        return true;
    //    }
    //    public static bool TokenIsNotOperator(Token token, OperatorCode_e opCpde)
    //    {
    //        return !TokenIsOperator(token, opCpde);
    //    }

    //    public static bool TokenIsKeyword(Token token, KeywordCode_e kwCpde)
    //    {
    //        if (token.Type != TokenType_e.Keyword) return false;
    //        if ((KeywordCode_e)(token.Value) != kwCpde) return false;
    //        return true;
    //    }
    //    public static bool TokenIsNotKeyword(Token token, KeywordCode_e kwCpde)
    //    {
    //        return !TokenIsKeyword(token, kwCpde);
    //    }


    //    public bool NextTokenIs(TokenType_e type)
    //    {
    //        Token token = PreviewNextToken();
    //        if (token == null) return false;
    //        if (token.Type == type)
    //        {
    //            return true;
    //        }
    //        else
    //        {
    //            return false;
    //        }
    //    }

    //    public bool NextTokenIsNot(TokenType_e type)
    //    {
    //        Token token = PreviewNextToken();
    //        if (token == null) return false;
    //        if (token.Type != type)
    //        {
    //            return true;
    //        }
    //        else
    //        {
    //            return false;
    //        }
    //    }

    //    public bool NextTokenIsOperator(OperatorCode_e opCode)
    //    {
    //        Token token = PreviewNextToken();
    //        if (token == null) return false;
    //        if (token.Type != TokenType_e.Operator) return false;
    //        if (((OperatorCode_e)token.Value) != opCode) return false;
    //        return true;
    //    }
    //    public bool NextTokenIsNotOperator(OperatorCode_e opCode)
    //    {
    //        return !NextTokenIsOperator(opCode);
    //    }

    //    public bool NextTokenIsKeyword(KeywordCode_e kwCode)
    //    {
    //        Token token = PreviewNextToken();
    //        if (token == null) return false;
    //        if (token.Type != TokenType_e.Keyword) return false;
    //        if (((KeywordCode_e)token.Value) != kwCode) return false;
    //        return true;
    //    }
    //    public bool NextTokenIsNotKeyword(KeywordCode_e kwCode)
    //    {
    //        return !NextTokenIsKeyword(kwCode);
    //    }





    //}

}
