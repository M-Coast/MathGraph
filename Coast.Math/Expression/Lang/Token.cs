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
using System.Data;
using System.Collections;

namespace Coast.Math.Expression
{
    public enum TokenType
    {
        None,
        Constant,
        Operator,
        Identifier,
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
        public TokenType Type { set; get; }
        public object Value { set; get; }
        public Location Location { set; get; }

        public Token()
        {
            Type = TokenType.None;
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

        public string ToString_S()
        {
            if (Type == TokenType.Operator)
            {
                return Statics.GetOperatorText((OperatorCode)Value);
            }
            else if (Type == TokenType.Constant)
            {
                return Value.ToString();
            }
            else if (Type == TokenType.Identifier)
            {
                return Value.ToString();
            }

            return string.Empty;
        }
    }


}