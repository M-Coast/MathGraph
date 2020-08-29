using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Coast.Controls.Converters
{
    //Use for All Basic Value Types

    public class GenericFormatMultiConverter : IMultiValueConverter
    {

        //value[0] DoubleValue
        //value[1] StringFormat

        private string __format__ = string.Empty;   //Store format

        public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.Length < 2)
            {
                if (value[0] != null) return value[0].ToString();
                return string.Empty;
            }

            TypeCode __typecode = Type.GetTypeCode(value[0].GetType());

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

            if (value[1] == null) return value[0].ToString();

            if (!(value[1] is string)) return string.Empty;

            object __value = (double)value[0];
            string __format = (string)value[1];
            string __text = string.Empty;

            __format__ = __format;


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
                __text = String.Format(__format, __value);
                return __text;
            }
            catch(Exception e)
            {
                //return string.Empty;
                return e.Message;
            }

        }

        public object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture)
        {
            object[] __out = new object[] { null, __format__ };    //Remain format when transfer back

            if (value == null) return __out;
            if (!(value is string)) return __out;
            if (targetType == null) return __out;
            if (targetType.Length < 2) return __out;

            string __value = (string)value;

            TypeCode __typecode = Type.GetTypeCode(targetType[0]);


            switch (__typecode)
            {
                case TypeCode.Boolean:
                    {
                        Boolean __temp;
                        if (Boolean.TryParse(__value, out __temp)) { __out[0] = (Boolean)__temp; return __out; }
                    }
                    break;
                case TypeCode.Char:
                    {
                        __out[0] = __value; return __out;
                    }
                    break;
                case TypeCode.SByte:
                    {
                        SByte __temp;
                        if (SByte.TryParse(__value, out __temp)) { __out[0] = (SByte)__temp; return __out; }
                    }
                    break;
                case TypeCode.Byte:
                    {
                        Byte __temp;
                        if (Byte.TryParse(__value, out __temp)) { __out[0] = (Byte)__temp; return __out; }
                    }
                    break;
                case TypeCode.Int16:
                    {
                        Int16 __temp;
                        if (Int16.TryParse(__value, out __temp)) { __out[0] = (Int16)__temp; return __out; }
                    }
                    break;
                case TypeCode.UInt16:
                    {
                        UInt16 __temp;
                        if (UInt16.TryParse(__value, out __temp)) { __out[0] = (UInt16)__temp; return __out; }
                    }
                    break;
                case TypeCode.Int32:
                    {
                        Int32 __temp;
                        if (Int32.TryParse(__value, out __temp)) { __out[0] = (Int32)__temp; return __out; }
                    }
                    break;
                case TypeCode.UInt32:
                    {
                        UInt32 __temp;
                        if (UInt32.TryParse(__value, out __temp)) { __out[0] = (UInt32)__temp; return __out; }
                    }
                    break;
                case TypeCode.Int64:
                    {
                        Int64 __temp;
                        if (Int64.TryParse(__value, out __temp)) { __out[0] = (Int64)__temp; return __out; }
                    }
                    break;
                case TypeCode.UInt64:
                    {
                        UInt64 __temp;
                        if (UInt64.TryParse(__value, out __temp)) { __out[0] = (UInt64)__temp; return __out; }
                    }
                    break;
                case TypeCode.Single:
                    {
                        Single __temp;
                        if (Single.TryParse(__value, out __temp)) { __out[0] = (Single)__temp; return __out; }
                    }
                    break;
                case TypeCode.Double:
                    {
                        Double __temp;
                        if (Double.TryParse(__value, out __temp)) { __out[0] = (Double)__temp; return __out; }
                    }
                    break;
                case TypeCode.Decimal:
                    {
                        Decimal __temp;
                        if (Decimal.TryParse(__value, out __temp)) { __out[0] = (Decimal)__temp; return __out; }
                    }
                    break;
                case TypeCode.DateTime:
                    {
                        DateTime __temp;
                        if (DateTime.TryParse(__value, out __temp)) { __out[0] = (DateTime)__temp; return __out; }
                    }
                    break;
                case TypeCode.String:
                    {
                        __out[0] = __value; return __out;
                    }
            }

            return null;
        }
    }
}



