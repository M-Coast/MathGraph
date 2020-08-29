/*****************************************************************************************

    MathGraph
    
    Copyright (C)  Coast


    AUTHOR      :  Coast   
    DATE        :  2020/8/27
    DESCRIPTION :  

 *****************************************************************************************/

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Coast.Controls.Converters
{
    //Use for All Basic Value Types

    public class GenericFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TypeCode __typecode = Type.GetTypeCode(value.GetType());

            switch (__typecode)
            {
                case TypeCode.Boolean:
                case TypeCode.Char:
                case TypeCode.SByte:
                case TypeCode.Byte:
                case TypeCode.Int16:
                case TypeCode.UInt16:
                case TypeCode.Int32:
                case TypeCode.UInt32:
                case TypeCode.Int64:
                case TypeCode.UInt64:
                case TypeCode.Single:
                case TypeCode.Double:
                case TypeCode.Decimal:
                case TypeCode.DateTime:
                case TypeCode.String:
                    break;
                default:
                    return string.Empty;
            }

            if (parameter == null) return value.ToString();

            if (!(parameter is string)) return string.Empty;


            object __value = value;
            string __format = (string)parameter;


            if (!__format.Contains("{"))
            {
                //if not include '{' try ToString(format) function first 
                try
                {
                    switch (__typecode)
                    {
                        case TypeCode.Boolean: return ((Boolean)__value).ToString();
                        case TypeCode.Char: return ((Char)__value).ToString();
                        case TypeCode.SByte: return ((SByte)__value).ToString(__format);
                        case TypeCode.Byte: return ((Byte)__value).ToString(__format);
                        case TypeCode.Int16: return ((Int16)__value).ToString(__format);
                        case TypeCode.UInt16: return ((UInt16)__value).ToString(__format);
                        case TypeCode.Int32: return ((Int32)__value).ToString(__format);
                        case TypeCode.UInt32: return ((UInt32)__value).ToString(__format);
                        case TypeCode.Int64: return ((Int64)__value).ToString(__format);
                        case TypeCode.UInt64: return ((UInt64)__value).ToString(__format);
                        case TypeCode.Single: return ((Single)__value).ToString(__format);
                        case TypeCode.Double: return ((Double)__value).ToString(__format);
                        case TypeCode.Decimal: return ((Decimal)__value).ToString(__format);
                        case TypeCode.DateTime: return ((DateTime)__value).ToString(__format);
                        case TypeCode.String: return value;
                    }
                }
                catch (Exception e)
                {
                    //return string.Empty;
                    return e.Message;
                }
            }


            //String.Format() 格式规范的完整形式：{ index[, width][:formatstring]}
            try
            {
                return String.Format(__format, __value);
            }
            catch (Exception e)
            {
                //return string.Empty;
                return e.Message;
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;
            if (!(value is string)) return null;

            string __value = (string)value;
            TypeCode __typecode = Type.GetTypeCode(targetType);


            switch (__typecode)
            {
                case TypeCode.Boolean:
                    {
                        Boolean __out;
                        if (Boolean.TryParse(__value, out __out)) return __out;
                    }
                    break;
                case TypeCode.Char:
                    {
                        return __value;
                    }
                    break;
                case TypeCode.SByte:
                    {
                        SByte __out;
                        if (SByte.TryParse(__value, out __out)) return __out;
                    }
                    break;
                case TypeCode.Byte:
                    {
                        Byte __out;
                        if (Byte.TryParse(__value, out __out)) return __out;
                    }
                    break;
                case TypeCode.Int16:
                    {
                        Int16 __out;
                        if (Int16.TryParse(__value, out __out)) return __out;
                    }
                    break;
                case TypeCode.UInt16:
                    {
                        UInt16 __out;
                        if (UInt16.TryParse(__value, out __out)) return __out;
                    }
                    break;
                case TypeCode.Int32:
                    {
                        Int32 __out;
                        if (Int32.TryParse(__value, out __out)) return __out;
                    }
                    break;
                case TypeCode.UInt32:
                    {
                        UInt32 __out;
                        if (UInt32.TryParse(__value, out __out)) return __out;
                    }
                    break;
                case TypeCode.Int64:
                    {
                        Int64 __out;
                        if (Int64.TryParse(__value, out __out)) return __out;
                    }
                    break;
                case TypeCode.UInt64:
                    {
                        UInt64 __out;
                        if (UInt64.TryParse(__value, out __out)) return __out;
                    }
                    break;
                case TypeCode.Single:
                    {
                        Single __out;
                        if (Single.TryParse(__value, out __out)) return __out;
                    }
                    break;
                case TypeCode.Double:
                    {
                        Double __out;
                        if (Double.TryParse(__value, out __out)) return __out;
                    }
                    break;
                case TypeCode.Decimal:
                    {
                        Decimal __out;
                        if (Decimal.TryParse(__value, out __out)) return __out;
                    }
                    break;
                case TypeCode.DateTime:
                    {
                        DateTime __out;
                        if (DateTime.TryParse(__value, out __out)) return __out;
                    }
                    break;
                case TypeCode.String:
                    {
                        return __value;
                    }
            }

            return null;
        }
    }
}



