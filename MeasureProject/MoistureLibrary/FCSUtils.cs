using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Moisture.Lib
{
    public static class FCSUtils
    {
    }

    public static class UnitConverter
    {
        public static double InchToMillimeter(double fInch) => fInch * 25.4;

        public static double MillimeterToInch(double fMillimeter) => fMillimeter / 25.4;
    }

    public static class ObjectExtension
    {
        public static T ConvertDBNull<T>(this object value, Func<object, T> conversionFunction) => value.ConvertDBNull(conversionFunction,0);

        public static T ConvertDBNull<T>(this object value, Func<object, T> conversionFunction, object defaultValue)
        {
            try
            {
                return conversionFunction(value == DBNull.Value || value == null || "".Equals(value) || "null".Equals(value) || "*null*".Equals(value) || "0".Equals(value) ? defaultValue : value);
            }
            catch(System.FormatException fe)
            {
                _ = fe;
                return conversionFunction(0);
            }
            catch(System.NullReferenceException nre)
            {
                _ = nre;
                return conversionFunction(0);
            }
        }

        public static T TryConvertDBNull<T>(this object value, Func<object, T> conversionFunction) => value.TryConvertDBNull(conversionFunction, 0);

        public static T TryConvertDBNull<T>(this object value, Func<object, T> conversionFunction, object defaultValue)
        {
            T ret;

            try
            {
                ret = value.ConvertDBNull(conversionFunction, defaultValue);
            }
            catch (System.FormatException fe)
            {
                _ = fe;

                ret = (T)defaultValue;
            }
            catch (System.NullReferenceException nre)
            {
                _ = nre;

                ret = (T)defaultValue;
            }

            return ret;
        }
    }

    public static class DictionaryExtension
    {
        public static TValue TryGetValue<TKey, TValue>(this Dictionary<TKey, TValue> dic, TKey key, TValue defaultValue = default)
        {
            TValue ret = defaultValue;

            try
            {
                ret = dic[key];
            }
            catch(KeyNotFoundException knfe)
            {
                _ = knfe;
            }

            return ret;
        }
    }

    public static class StringExtension
    {
        public static bool IsEmptyLine(this string line)
        {
            return line == null || string.IsNullOrEmpty(line) || line.Trim().Length == 0 || line.StartsWith("--") || line == Environment.NewLine || line == "\r" || line == "\n";
        }

        public static string Left(this string s, int Length)
        {
            string preStr = s;
            int len = s.Length;
            if (Length > len)
            {
                return preStr;
            }

            string rtnStr = preStr.Substring(0, Length);

            return rtnStr;
        }

        public static string Right(this string s, int Length)
        {
            string preStr = s;
            int len = s.Length;
            if (Length > len)
            {
                return preStr;
            }

            string rtnStr = preStr.Substring(len - Length);

            return rtnStr;
        }

        public static dynamic ToNumber(this string s)
        {
            if (string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s) || s.Equals("0")) return 0;

            if (!double.TryParse(s,out double doubleToCheckDecimal))
            {
                return null;
            }

            s = doubleToCheckDecimal.ToString();

            if (sbyte.TryParse(s,out sbyte sbyteParseResult))
            {
                return sbyteParseResult;
            }

            if(int.TryParse(s,out int intParseResult))
            {
                return intParseResult;
            }

            if (long.TryParse(s, out long longParseResult))
            {
                return longParseResult;
            }

            if (float.TryParse(s, out float floatParseResult))
            {
                return floatParseResult;
            }

            if (double.TryParse(s, out double doubleParseResult))
            {
                return doubleParseResult;
            }

            return null;
        }

        public static decimal ToDecimal(object Expression)
        {
            bool isNum;
            isNum = decimal.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, 
                System.Globalization.NumberFormatInfo.InvariantInfo, out decimal retNum);
            if (isNum)
            {
                return retNum;
            }
            else
            {
                return 0;
            }
        }

        public static int ToInt(this string s)
        {
            if (string.IsNullOrEmpty(s)) return 0;

            return int.Parse(s);
        }

        public static double ToDouble(this string s)
        {
            if (string.IsNullOrEmpty(s)) return 0;

            return double.Parse(s);
        }

        public static float ToFloat(this string s)
        {
            if (string.IsNullOrEmpty(s)) return 0;
            return float.Parse(s);
        }

        public static string ToDateStr(this string s, string dateFormat)
        {
            string wyyyy, wmonth, wday;

            if(string.IsNullOrEmpty(s))
            {
                return "";
            }
            if(string.IsNullOrWhiteSpace(s))
            {
                return "";
            }
            try
            {
                string wdate = s.Replace("-", "").Replace("/", "").Replace(".", "") + "0101";
                wyyyy = wdate.Substring(0, 4);
                wmonth = wdate.Substring(4, 2);
                wday = wdate.Substring(6, 2);

                string rdate = dateFormat;

                rdate = rdate.Replace("yyyy", wyyyy);
                rdate = rdate.Replace("MM", wmonth);
                rdate = rdate.Replace("dd", wday);

                DateTime dt = DateTime.ParseExact(rdate, dateFormat, null);

                return rdate;
            }
            catch (ArgumentNullException ex)
            {
                _ = ex;
                //Mhelper.Show("ERR3006");
                //return s;
                MessageBox.Show("Cannot parse to date (" + s + ")" + "\n" + ex.Message, "ERROR:AQDateBox.ToDateStr()", MessageBoxButtons.OK);
                return null;
            }
            catch (FormatException ex)
            {
                _ = ex;
                //Mhelper.Show("ERR3006");
                //return s;
                MessageBox.Show("Cannot parse to date (" + s + ")" + "\n" + ex.Message, "ERROR:AQDateBox.ToDateStr()", MessageBoxButtons.OK);
                return null;
            }
        }

        public static DateTime ToDate(this string s)
        {
            s = s.Trim();
            if(s.Length ==0)
            {
                return DateTime.Now;
            }

            if(s.Length == 4)
            {
                s = $"{s}0101";
            }

            DateTime dt;
            try
            {
                if(DateTime.TryParse(s,out dt))
                {
                    return dt;
                }

                if(s.Contains("-") || s.Contains("/") || s.Contains("."))
                {
                    string[] buf = (s + "///").Split(new char[] { '-', '/', '.', ' ' });
                    s = buf[0] + "-" + buf[1] + "-" + buf[2];
                }
                else
                {
                    dt = DateTime.Today;
                    string ReplaceText = s.Replace("-", "").Replace("/", "").Replace(".", "");

                    if (Regex.IsMatch(ReplaceText, @"^(19|20)\d{2}(0[1-9]|1[012])(0[1-9]|[12][0-9]|3[0-1])$"))
                    {
                        s = ReplaceText;
                    }

                    // 년월일  981225
                    else if (Regex.IsMatch(ReplaceText, @"^\d{2}(0[1-9]|1[012])(0[1-9]|[12][0-9]|3[0-1])$"))
                    {
                        s = dt.Year.ToString().Left(2) + ReplaceText;
                    }

                    // 월일 0205
                    else if (Regex.IsMatch(ReplaceText, @"^(0[1-9]|1[012])(0[1-9]|[12][0-9]|3[0-1])$"))
                    {
                        s = dt.Year.ToString() + ReplaceText;
                    }

                    // 일 05
                    else if(Regex.IsMatch(ReplaceText, @"^(0[1-9]|[12][0-9]|3[0-1])$"))
                    {
                        s = dt.Year.ToString() + dt.Month.ToString("00") + ReplaceText;
                    }

                    // 일 5
                    else if(ReplaceText.Length.ToNumber() == 1)
                    {
                        s = dt.Year.ToString() + dt.Month.ToString("00") + (ReplaceText.Length == 1 ? "0" + ReplaceText : ReplaceText);
                    }

                    else if (string.IsNullOrEmpty(ReplaceText))
                    {
                        s = "";
                    }

                    else
                    {
                        s = "99999999";
                    }
                }
                s = s.Left(4) + "-" + s.Substring(4, 2) + "-" + s.Substring(6, 2);

                return DateTime.TryParse(s, out dt) ? dt : DateTime.Now;
            }
            catch(FormatException fe)
            {
                _ = fe;

                dt = DateTime.Now;
            }
            finally
            {

            }

            return dt;
        }

        public static string Substr(this string s, int startIndex)
        {
            return Substr(s, startIndex, s.Length);
        }

        public static string Substr(this string s, int startIndex, int length)
        {
            if (startIndex < 0) startIndex = 0;
            if (length < 0) length = 0;

            if (startIndex > s.Length) return string.Empty;
            if (startIndex + length > s.Length) return s.Substring(startIndex);

            return s.Substring(startIndex, length);
        }

        public static bool NotEquals(this string s, string value)
        {
            if(!s.Equals(value))
            {
                return true;
            }

            return false;
        }

        public static bool NotEquals(this string s, string value, StringComparison comparisonType)
        {
            if(!s.Equals(value,comparisonType))
            {
                return true;
            }

            return false;
        }

        public static bool NotEquals(this string s, object obj)
        {
            if(!s.Equals(obj))
            {
                return true;
            }

            return false;
        }

        public static bool NotContains(this string s, string value)
        {
            if(value == null)
            {
                return false;
            }

            if(!s.Contains(value))
            {
                return true;
            }

            return false;
        }

    }

    public static class DataRowExtension
    {
        public static dynamic ToNumber(this object o)
        {
            return o.ToString().ToNumber();
        }
    }

    public static class DataTableExtension
    {
        public static DataTable Pivot(this DataTable tbl, string first_column = "")
        {
            var tblPivot = new DataTable();

            tblPivot.Columns.Add(new DataColumn
            {
                ColumnName = string.Format("{0}", (string.IsNullOrEmpty(first_column)) ? tbl.Columns[0].ColumnName : first_column),
                Caption = string.Format("{0}",(string.IsNullOrEmpty(first_column)) ? tbl.Columns[0].ColumnName : first_column)
            });

            for(int i = 1; i <= tbl.Rows.Count; i++)
            {
                DataRow dr = tbl.Rows[i - 1];

                DataColumn dc = new DataColumn
                {
                    ColumnName = string.Format("C{0}", dr[tbl.Columns[0].ColumnName]).Replace(" ","_"),
                    Caption = string.Format("C{0}", dr[tbl.Columns[0].ColumnName])
                };

                tblPivot.Columns.Add(dc);
            }

            DataTable dtCloned = tblPivot.Clone();

            for(int col = 1; col < tbl.Columns.Count; col++)
            {
                var r = tblPivot.NewRow();

                r[0] = (!string.IsNullOrEmpty(tbl.Columns[col].Caption)) ? tbl.Columns[col].Caption : tbl.Columns[col].ToString();

                for(int j = 1; j <= tbl.Rows.Count; j++)
                {
                    DataColumn dc = dtCloned.Columns[j];

                    try
                    {
                        if(dc.DataType != (Nullable.GetUnderlyingType(tbl.Rows[j - 1][col].GetType()) ?? tbl.Rows[j - 1][col].GetType()))
                        {
                            dc.DataType = Nullable.GetUnderlyingType(tbl.Rows[j - 1][col].GetType()) ?? tbl.Rows[j - 1][col].GetType();

                            if(dc.DataType == typeof(bool))
                            {
                                dc.DefaultValue = false;
                            }
                            else if(dc.DataType == typeof(int) | dc.DataType == typeof(double) | dc.DataType == typeof(decimal))
                            {
                                dc.DefaultValue = 0;
                            }
                            else if(dc.DataType ==(Nullable.GetUnderlyingType(tbl.Rows[j-1][col].GetType()) ?? tbl.Rows[j-1][col].GetType()))
                            {

                            }
                            else
                            {
                                dc.DefaultValue = "";
                            }
                        }
                    }
                    catch(NullReferenceException nre)
                    {
                        _ = nre;

                        dc.DataType = typeof(string);
                        dc.DefaultValue = "";
                    }
                    catch(ArgumentException ae)
                    {
                        _ = ae;
                    }

                    r[j] = tbl.Rows[j - 1][col];
                }

                tblPivot.Rows.Add(r);
            }

            for(int j = 0; j < tblPivot.Rows.Count; j++)
            {
                DataRow row = tblPivot.Rows[j];

                dtCloned.ImportRow(row);
            }

            return dtCloned;
        }

        public static TOutType Field<TInType, TOutType>(this DataRow dataRow, string columnName)
        {
            return (TOutType)Convert.ChangeType(dataRow.Field<TInType>(columnName: columnName), typeof(TOutType));
        }
    }
    
    public static class DateTimeExtension
    {
        public static DateTime ToFirstDay(this DateTime dt) => new DateTime(dt.Year, dt.Month, 1);

        public static DateTime ToLastDay(this DateTime dt) => dt.ToFirstDay().AddMonths(1).AddDays(-1);
    }

    public static class SocketExtension
    {
        public static bool IsConnect(this System.Net.Sockets.Socket @this)
        {
            return !(@this.Poll(1, System.Net.Sockets.SelectMode.SelectRead) && @this.Available == 0);
        }
    }
}
