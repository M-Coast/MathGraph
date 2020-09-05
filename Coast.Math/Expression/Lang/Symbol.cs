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
    public enum SymbolType  //IDCategory
    {
        None,
        Variable,
        Parameter,
        Function,
    }

    [Serializable]
    public class Variable
    {
        public Identifier Id { get; set; }
        public Type DataType { get; set; } = typeof(double);  //Must Be Double
        public string Name { get { return Id == null ? string.Empty : Id.Name; } }
        public Symbol SymbolEntry;
    }


    [Serializable]
    public class Parameter
    {
        public Identifier Id { get; set; }
        public Type DataType { get; set; } = typeof(double);  //Must Be Double
        public string Name { get { return Id == null ? string.Empty : Id.Name; } }
        public Symbol SymbolEntry;
    }

    [Serializable]
    public class Function
    {
        public Identifier Id;
        public List<Parameter> Parameters;
        public Type ReturnType { get; set; } = typeof(double);  //Must Be Double
        public Expression Body { get; set; }
        public string Name { get { return Id == null ? string.Empty : Id.Name; } }
        public Symbol SymbolEntry;
    }
    
    [Serializable]
    public class Symbol
    {
        public string Name { set; get; }
        public SymbolType Type { set; get; }
        public object Value { set; get; }   //Variable,Function

        public int Adress { set; get; }
        public int Size { set; get; }

        public Symbol()
        {

        }

        public Symbol(string name, SymbolType type, object value)
        {
            Name = name;
            Type = type;
            Value = value;
        }

        public override string ToString()
        {
            return Name;
        }
    }

}