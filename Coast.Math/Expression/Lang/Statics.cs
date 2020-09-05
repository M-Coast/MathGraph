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
    public static class Statics
    {
        public static Dictionary<string, OperatorCode> OperatorTable { get; private set; }
        public static Dictionary<OperatorCode, string> OperatorTextTable { get; private set; }

        public static Dictionary<string, KeywordCode> KeywordTable { get; private set; }
        public static Dictionary<KeywordCode, string> KeywordTextTable { get; private set; }

        public static Dictionary<string,Function> BuiltInFunctions { get; private set; }
        public static Dictionary<string, Function> BuiltInSymbols { get; private set; }

        static Statics()
        {
            InitOperatorTable();
            InitKeywordTable();
            InitBuiltInFunctions();
        }




        public static string GetOperatorText(OperatorCode code)
        {
            if (OperatorTextTable.ContainsKey(code)) return OperatorTextTable[code];
            return string.Empty;
        }

        public static void InitOperatorTable()
        {
            OperatorTextTable = new Dictionary<OperatorCode, string>();

            Dictionary<OperatorCode, string> __tab = OperatorTextTable;

            __tab.Add(OperatorCode.None, "");
            __tab.Add(OperatorCode.NewLine, "");
            __tab.Add(OperatorCode.Return, "");
            __tab.Add(OperatorCode.Not, "!");
            __tab.Add(OperatorCode.DoubleQuotation, "\"");
            __tab.Add(OperatorCode.Hash, "#");
            __tab.Add(OperatorCode.Currency, "$");
            __tab.Add(OperatorCode.Mod, "%");
            __tab.Add(OperatorCode.And, "&");
            __tab.Add(OperatorCode.SingleQuotation, "'");
            __tab.Add(OperatorCode.LeftParenthesis, "(");
            __tab.Add(OperatorCode.RightParenthesis, ")");
            __tab.Add(OperatorCode.Mul, "*");
            __tab.Add(OperatorCode.Plus, "+");
            __tab.Add(OperatorCode.Comma, ",");
            __tab.Add(OperatorCode.Minus, "-");
            __tab.Add(OperatorCode.Dot, ".");
            __tab.Add(OperatorCode.Div, "/");
            __tab.Add(OperatorCode.Colon, ":");
            __tab.Add(OperatorCode.SemiColon, ";");
            __tab.Add(OperatorCode.LessThan, "<");
            __tab.Add(OperatorCode.Equals, "=");
            __tab.Add(OperatorCode.GreaterThan, ">");
            __tab.Add(OperatorCode.Query, "?");
            __tab.Add(OperatorCode.At, "@");
            __tab.Add(OperatorCode.LeftSquare, "[");
            __tab.Add(OperatorCode.Backslash, "\\");
            __tab.Add(OperatorCode.RightSquare, "]");
            __tab.Add(OperatorCode.Xor, "^");
            __tab.Add(OperatorCode.UnderScore, "_");
            __tab.Add(OperatorCode.LeftBrace, "{");
            __tab.Add(OperatorCode.Or, "|");
            __tab.Add(OperatorCode.RightBrace, "}");
            __tab.Add(OperatorCode.Invert, "~");
            __tab.Add(OperatorCode.LessEqual, "<=");
            __tab.Add(OperatorCode.NotEqual, "<>");
            __tab.Add(OperatorCode.GreaterEqual, ">=");
        }


        public static void InitKeywordTable()
        {
            KeywordTable = new Dictionary<string, KeywordCode>();
        }


        public static void InitBuiltInFunctions()
        {
            BuiltInFunctions = new Dictionary<string, Function>();
            BuiltInFunctions.Add("sin", new Function() { Id = new Identifier("sin"), Parameters = new List<Parameter>() { new Parameter() } });
            BuiltInFunctions.Add("cos", new Function() { Id = new Identifier("cos"), Parameters = new List<Parameter>() { new Parameter() } });
            BuiltInFunctions.Add("tan", new Function() { Id = new Identifier("tan"), Parameters = new List<Parameter>() { new Parameter() } });
            BuiltInFunctions.Add("pow", new Function() { Id = new Identifier("pow"), Parameters = new List<Parameter>() { new Parameter(), new Parameter() } });
            BuiltInFunctions.Add("exp", new Function() { Id = new Identifier("exp"), Parameters = new List<Parameter>() { new Parameter() } });
            BuiltInFunctions.Add("log", new Function() { Id = new Identifier("log"), Parameters = new List<Parameter>() { new Parameter() } });
        }
    }

}
