using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;

namespace Coast.Math.Expression
{
    public class Lex : ModuleBase
    {
        private string _sourceCode = null;
        private char[] _source = null;
        private int _position = 0;
        private Stack<int> _stack = new Stack<int>();


        public string SourceCode
        {
            get { return _sourceCode; }
            set
            {
                _sourceCode = value;
                if (_sourceCode == null || _sourceCode.Length <= 0)
                {
                    _errored = true; return;
                }
                _source = _sourceCode.ToCharArray();
                //_errored = true; return;
                //InitLineSections();
            }
        }



   

        //List<int> lineSections = new List<int>();
        //private void InitLineSections()
        //{
        //    if (_source == null) return;
        //}

        public int Position { get { return _position; } }
        public int Line
        {
            get
            {
                if (_source == null) return -1;
                int i;
                int line = 0;
                for (i = 0; i < _position; i++)
                {
                    if (i > _source.Length - 1) break;
                    if (_source[i] == '\n') line++;
                }
                return line;
            }
        }
        public int Column
        {
            get
            {
                if (_source == null) return -1;
                int i;
                int column = 0;
                for (i = _position; i >= 0; i--)
                {
                    if (i > _source.Length - 1) continue;
                    if (_source[i] == '\n') break;
                    column++;
                }
                return column;
            }
        }







        public Token Parse()
        {
            if (_errored) return null;

            SkipBlank();

            Token token = new Token();

            token.Location.Line = Line;
            token.Location.Column = Column;
            token.Type = TokenType.None;

            if (_EOF()) { token.Type = TokenType.EOF; return token; }


            switch (_source[_position])
            {
                #region Case Name
                case 'A':
                case 'B':
                case 'C':
                case 'D':
                case 'E':
                case 'F':
                case 'G':
                case 'H':
                case 'I':
                case 'J':
                case 'K':
                case 'L':
                case 'M':
                case 'N':
                case 'O':
                case 'P':
                case 'Q':
                case 'R':
                case 'S':
                case 'T':
                case 'U':
                case 'V':
                case 'W':
                case 'X':
                case 'Y':
                case 'Z':
                case 'a':
                case 'b':
                case 'c':
                case 'd':
                case 'e':
                case 'f':
                case 'g':
                case 'h':
                case 'i':
                case 'j':
                case 'k':
                case 'l':
                case 'm':
                case 'n':
                case 'o':
                case 'p':
                case 'q':
                case 'r':
                case 's':
                case 't':
                case 'u':
                case 'v':
                case 'w':
                case 'x':
                case 'y':
                case 'z':
                case '_':
                    #endregion
                    ParseName(token);
                    break;

                #region Case Const Number
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                case '$':
                case '.':
                    #endregion
                    ParseConstantNumber(token);
                    break;

                //case '"': ParseConstString(token); break;
                //case '#': ParseLabel(token); break;
                //case '\'': ParseComment(token); break;

                #region Case Operators
                case '\r': token.Type = TokenType.Operator; token.Value = OperatorCode.Return; _position++; break;
                case '\n': token.Type = TokenType.Operator; token.Value = OperatorCode.NewLine; _position++; break;

                case '!': token.Type = TokenType.Operator; token.Value = OperatorCode.Not; _position++; break;
                case '%': token.Type = TokenType.Operator; token.Value = OperatorCode.Mod; _position++; break;
                case '*': token.Type = TokenType.Operator; token.Value = OperatorCode.Mul; _position++; break;
                case '+': token.Type = TokenType.Operator; token.Value = OperatorCode.Plus; _position++; break;
                case '-': token.Type = TokenType.Operator; token.Value = OperatorCode.Minus; _position++; break;
                case '/': token.Type = TokenType.Operator; token.Value = OperatorCode.Div; _position++; break;
                case '=': token.Type = TokenType.Operator; token.Value = OperatorCode.Equals; _position++; break;

                case '<':
                    token.Type = TokenType.Operator; token.Value = OperatorCode.LessThan; _position++;
                    if (NextCharIs('=')) { token.Value = OperatorCode.LessEqual; _position++; }
                    else if (NextCharIs('>')) { token.Value = OperatorCode.NotEqual; _position++; }
                    break;

                case '>':
                    token.Type = TokenType.Operator; token.Value = OperatorCode.GreaterThan; _position++;
                    if (NextCharIs('=')) { token.Value = OperatorCode.GreaterEqual; _position++; }
                    break;

                case '&': token.Type = TokenType.Operator; token.Value = OperatorCode.And; _position++; break;
                case '|': token.Type = TokenType.Operator; token.Value = OperatorCode.And; _position++; break;

                case ',': token.Type = TokenType.Operator; token.Value = OperatorCode.Comma; _position++; break;
                case ';': token.Type = TokenType.Operator; token.Value = OperatorCode.SemiColon; _position++; break;

                case '(': token.Type = TokenType.Operator; token.Value = OperatorCode.LeftParenthesis; _position++; break;
                case ')': token.Type = TokenType.Operator; token.Value = OperatorCode.RightParenthesis; _position++; break;
                case '[': token.Type = TokenType.Operator; token.Value = OperatorCode.LeftSquare; _position++; break;
                case ']': token.Type = TokenType.Operator; token.Value = OperatorCode.RightSquare; _position++; break;
                case '{': token.Type = TokenType.Operator; token.Value = OperatorCode.LeftBrace; _position++; break;
                case '}': token.Type = TokenType.Operator; token.Value = OperatorCode.RightBrace; _position++; break;

                case '\\': SkipTo('\n'); break;

                #endregion

                default:
                    //case '\\':
                    MissShot();
                    break;


            }

            return token;
        }
        
        private bool _EOF()
        {
            if (_position >= _source.Length)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void SkipBlank()
        {
            while (!_EOF() && (_source[_position] == ' ' || _source[_position] == '\t' || _source[_position] == '\r'))
            {
                _position++;
            }
        }

        private void SkipTo(char until)
        {
            while (!_EOF() && _source[_position] != until)
            {
                _position++;
            }
            _position++;
        }

        private void MissShot()
        {
            //LogStatus(StatusLog.StateType_e.Error, "Illegal Operator");
        }

        #region Charactor Judgement

        private bool NextCharIs(char c)
        {
            if (_EOF()) return false;
            if (_source[_position] == c)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool CharIsIdName(char c)    //Identifier/Keyword character
        {
            if (c >= 'A' && c <= 'Z' || c >= 'a' && c <= 'z' || c >= '0' && c <= '9' || c == '_')
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool CharIsLetter(char c)
        {
            if (c >= 'A' && c <= 'Z' || c >= 'a' && c <= 'z')
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool CharIsUpperCaseLetter(char c)
        {
            if (c >= 'A' && c <= 'Z')
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool CharIsLowerCaseLetter(char c)
        {
            if (c >= 'A' && c <= 'Z')
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool CharIsHexNumber(char c) //Hex charactor
        {
            if (c >= 'A' && c <= 'F' || c >= 'a' && c <= 'f' || c >= '0' && c <= '9')
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool CharIsDecNumber(char c) //Decimal character
        {
            if (c >= '0' && c <= '9')
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        private void ParseName(Token token)
        {
            string text = string.Empty;

            while (!_EOF() && CharIsIdName(_source[_position]))
            {
                text = text + _source[_position];
                _position++;
            }

            //check precedence
            //  KeyWord
            //  Identifier

            if(Statics.KeywordTable.ContainsKey(text))
            {
                token.Type = TokenType.Keyword;
                token.Value = Statics.KeywordTable[text];
                return;
            }

            token.Type = TokenType.Identifier;
            token.Value = new Identifier(text);
            return;
        }
        
        private void ParseConstantNumber(Token token)
        {
            string text = string.Empty;
            double value = 0.0;

            while (!_EOF() && (CharIsDecNumber(_source[_position]) || _source[_position] == '.'))
            {
                text = text + _source[_position];
                _position++;
            }

            if (!double.TryParse(text, out value))
            {
                _errored = true;
                return;
            }

            token.Type = TokenType.Constant;
            token.Value = new Constant(value);

        }
        

        #region Token Operations

        public Token PreviewNextToken()
        {
            _stack.Push(_position);
            Token token = Parse();
            _position = _stack.Pop();
            return token;
        }

        public void ConsumeAToken()
        {
            Parse();
        }

        public static bool TokenIs(Token token, TokenType type)
        {
            if (token.Type == type) return true;
            return false;
        }
        public static bool TokenIsNot(Token token, TokenType type)
        {
            return !TokenIs(token, type);
        }

        public static bool TokenIsOperator(Token token, OperatorCode opCpde)
        {
            if (token.Type != TokenType.Operator) return false;
            if ((OperatorCode)(token.Value) != opCpde) return false;
            return true;
        }
        public static bool TokenIsNotOperator(Token token, OperatorCode opCpde)
        {
            return !TokenIsOperator(token, opCpde);
        }

        //public static bool TokenIsKeyword(Token token, KeywordCode_e kwCpde)
        //{
        //    if (token.Type != TokenType_e.Keyword) return false;
        //    if ((KeywordCode_e)(token.Value) != kwCpde) return false;
        //    return true;
        //}
        //public static bool TokenIsNotKeyword(Token token, KeywordCode_e kwCpde)
        //{
        //    return !TokenIsKeyword(token, kwCpde);
        //}


        public bool NextTokenIs(TokenType type)
        {
            Token token = PreviewNextToken();
            if (token == null) return false;
            if (token.Type == type)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool NextTokenIsNot(TokenType type)
        {
            Token token = PreviewNextToken();
            if (token == null) return false;
            if (token.Type != type)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool NextTokenIsOperator(OperatorCode opCode)
        {
            Token token = PreviewNextToken();
            if (token == null) return false;
            if (token.Type != TokenType.Operator) return false;
            if (((OperatorCode)token.Value) != opCode) return false;
            return true;
        }
        public bool NextTokenIsNotOperator(OperatorCode opCode)
        {
            return !NextTokenIsOperator(opCode);
        }

        //public bool NextTokenIsKeyword(KeywordCode_e kwCode)
        //{
        //    Token token = PreviewNextToken();
        //    if (token == null) return false;
        //    if (token.Type != TokenType_e.Keyword) return false;
        //    if (((KeywordCode_e)token.Value) != kwCode) return false;
        //    return true;
        //}
        //public bool NextTokenIsNotKeyword(KeywordCode_e kwCode)
        //{
        //    return !NextTokenIsKeyword(kwCode);
        //}

        #endregion






            



        public List<Token> DebugTest(string sourceCode)
        {
            SourceCode = sourceCode;
            List<Token> tokenList = new List<Token>();

            while(!_EOF())
            {
                Token t = Parse();
                tokenList.Add(t);
                Debug.Print(t.ToString());
                
            }

            return tokenList;
        }







    }
}















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
//            case '\r': token.Type = TokenType_e.Operator; token.Value = OperatorCode.Return; pos++; break;
//            case '\n': token.Type = TokenType_e.Operator; token.Value = OperatorCode.NewLine; pos++; break;

//            case '!': token.Type = TokenType_e.Operator; token.Value = OperatorCode.Not; pos++; break;
//            case '%': token.Type = TokenType_e.Operator; token.Value = OperatorCode.Mod; pos++; break;
//            case '*': token.Type = TokenType_e.Operator; token.Value = OperatorCode.Mult; pos++; break;
//            case '+': token.Type = TokenType_e.Operator; token.Value = OperatorCode.Plus; pos++; break;
//            case '-': token.Type = TokenType_e.Operator; token.Value = OperatorCode.Minus; pos++; break;
//            case '/': token.Type = TokenType_e.Operator; token.Value = OperatorCode.Div; pos++; break;
//            case '=': token.Type = TokenType_e.Operator; token.Value = OperatorCode.Equals; pos++; break;

//            case '<':
//                token.Type = TokenType_e.Operator; token.Value = OperatorCode.LessThan; pos++;
//                if (NextCharIs('=')) { token.Value = OperatorCode.LessEqual; pos++; }
//                else if (NextCharIs('>')) { token.Value = OperatorCode.NotEqual; pos++; }
//                break;

//            case '>':
//                token.Type = TokenType_e.Operator; token.Value = OperatorCode.GreaterThan; pos++;
//                if (NextCharIs('=')) { token.Value = OperatorCode.GreaterEqual; pos++; }
//                break;

//            case '&': token.Type = TokenType_e.Operator; token.Value = OperatorCode.And; pos++; break;
//            case '|': token.Type = TokenType_e.Operator; token.Value = OperatorCode.And; pos++; break;

//            case ',': token.Type = TokenType_e.Operator; token.Value = OperatorCode.Comma; pos++; break;
//            case ';': token.Type = TokenType_e.Operator; token.Value = OperatorCode.SemiColon; pos++; break;

//            case '(': token.Type = TokenType_e.Operator; token.Value = OperatorCode.LeftParen; pos++; break;
//            case ')': token.Type = TokenType_e.Operator; token.Value = OperatorCode.RightParen; pos++; break;
//            case '[': token.Type = TokenType_e.Operator; token.Value = OperatorCode.LeftSquare; pos++; break;
//            case ']': token.Type = TokenType_e.Operator; token.Value = OperatorCode.RightSquare; pos++; break;
//            case '{': token.Type = TokenType_e.Operator; token.Value = OperatorCode.LeftBrace; pos++; break;
//            case '}': token.Type = TokenType_e.Operator; token.Value = OperatorCode.RightBrace; pos++; break;

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

//        string text = string.Empty;

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

//        string text = string.Empty;
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
//        // "\string.Empty
//        pos++;

//        if (_EOF()) return;

//        string text = string.Empty;


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

//        string text = string.Empty;

//        while (!_EOF() && CharIsIdName(_source[pos]))
//        {
//            text = text + _source[pos];
//            pos++;
//        }

//        if (text == string.Empty)
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

//        string text = string.Empty;

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
//        string text = string.Empty;
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
//        string text = string.Empty;
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

//    public static bool TokenIsOperator(Token token, OperatorCode opCpde)
//    {
//        if (token.Type != TokenType_e.Operator) return false;
//        if ((OperatorCode)(token.Value) != opCpde) return false;
//        return true;
//    }
//    public static bool TokenIsNotOperator(Token token, OperatorCode opCpde)
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

//    public bool NextTokenIsOperator(OperatorCode opCode)
//    {
//        Token token = PreviewNextToken();
//        if (token == null) return false;
//        if (token.Type != TokenType_e.Operator) return false;
//        if (((OperatorCode)token.Value) != opCode) return false;
//        return true;
//    }
//    public bool NextTokenIsNotOperator(OperatorCode opCode)
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
