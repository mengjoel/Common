using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Tools
{
    public class ObjectConverter
    {
        public static float Object2float(object obj)
        {
            if (obj == null) return 0;
            float result;
            if (float.TryParse(obj.ToString(), out result))
                return result;
            else
                return 0;
        }
        public static decimal Object2decimal(object obj)
        {
            if (obj == null) return 0;
            decimal result;
            if (decimal.TryParse(obj.ToString(), out result))
                return result;
            else
                return 0;
        }
        public static int Bool2int(bool b1)
        {
            return b1 ? 1 : 0;
        }
        public static bool Object2bool(object obj)
        {
            if (obj == null) return false;
            try
            {
                bool bRtn;
                bRtn = (bool)obj;

                return bRtn;
            }
            catch
            {

                return false;
            }
        }
        /// <summary>
        /// 安全将对象转成字符串。null或错误值被当成0。
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int Object2int(object obj)
        {
            if (obj == null)
                return 0;
            int result;
            if (int.TryParse(obj.ToString(), out result))
                return result;
            else
                return 0;
        }
        /// <summary>
        /// 安全将对象转成字符串。null或错误值被当成-1。
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int Object2int2(object obj)
        {
            if (obj == null)
                return -1;
            int result;
            if (int.TryParse(obj.ToString(), out result))
                return result;
            else
                return -1;
        }
        public static double Object2Double(object objValue)
        {
            double ret = 0;
            try
            {
                ret = double.Parse(objValue.ToString());
            }
            catch
            {
            }
            return ret;
        }
       
        /// <summary>
        /// 安全将对象转成字符串。null被当成空字符串。
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string Object2string(object obj)
        {
            string ret = "";
            try
            {
                if (obj != null && obj != DBNull.Value)
                { ret = obj.ToString().Trim(); }
            }
            catch
            {
            }
            return ret;
        }
        /// <summary>
        /// 安全将对象转成指定的字符串。 
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <param name="defaultString">指定的字符串</param>
        /// <returns></returns>
        public static string Object2string(object obj, string defaultString)
        {
            string ret = defaultString;
            try
            {
                if (obj != null && obj != DBNull.Value)
                { ret = obj.ToString().Trim(); }
            }
            catch
            {
            }
            return ret;
        }
        /// <summary>
        /// 安全将对象转成指定的字符串
        /// </summary>
        /// <param name="obj">要转换的对象</param> 
        /// <param name="defaultString">指定的字符串</param>
        /// <param name="removeString">要排除的字符串</param>
        /// <returns></returns>
        public static string Object2string(object obj, string defaultString, string removeString)
        {
            string ret = defaultString;
            try
            {
                if (obj != null && obj != DBNull.Value && obj.ToString().Trim() != removeString)
                { ret = obj.ToString().Trim(); }
            }
            catch
            {
            }
            return ret;
        }
        /// <summary>
        /// 类型转换,专门处理可空的值类型转换
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="convertsionType">要转换的类型</param>
        /// <returns></returns>
        public static object ChanageType(object value, Type convertsionType)
        {
            //判断convertsionType类型是否为泛型，因为nullable是泛型类,
            if (convertsionType.IsGenericType &&
                //判断convertsionType是否为nullable泛型类
                convertsionType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null || value.ToString().Length == 0)
                {
                    return null;
                }

                //如果convertsionType为nullable类，声明一个NullableConverter类，该类提供从Nullable类到基础基元类型的转换
                NullableConverter nullableConverter = new NullableConverter(convertsionType);
                //将convertsionType转换为nullable对的基础基元类型
                convertsionType = nullableConverter.UnderlyingType;
            }
            return Convert.ChangeType(value, convertsionType);
        }



        #region 时间格式处理

        /// <summary>
        /// 安全将对象转成日期。null被当成最小日期。
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static DateTime Object2DateTime(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return default(DateTime);
            DateTime result;
            if (DateTime.TryParse(obj.ToString(), out result))
                return result;
            else
                return default(DateTime);
        }
        /// <summary>
        /// 将日期转换为字符串
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string DateTime2DBstring(DateTime dt)
        {
            if (dt == DateTime.MinValue)
                return "null";
            else
                return "'" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "'";
        }

        /// <summary>
        /// 将日期转换为日期和时间格式字符串
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string DateAndTime2String(DateTime dt)
        {
            if (dt == DateTime.MinValue)
                return "null";
            else
                return dt.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objValue"></param>
        /// <param name="formate">yyyy-MM-dd yyyy-MM-dd HH:mm:ss</param>
        /// <returns></returns>
        public static string Date2String(object objValue)
        {
            string formate = "yyyy-MM-dd";
            string ret = "";
            try
            {
                if (objValue != null)
                {
                    DateTime dt = DateTime.Parse(objValue.ToString());
                    if (dt != DateTime.MinValue && dt != DateTime.Parse("1900-01-01"))
                    { ret = dt.ToString(formate); }
                }
            }
            catch
            {
            }
            return ret;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objValue"></param>
        /// <param name="formate">yyyy-MM-dd yyyy-MM-dd HH:mm:ss</param>
        /// <returns></returns>
        public static string Time2String(object objValue)
        {
            string formate = "HH:mm";
            string ret = "";
            try
            {
                if (objValue != null)
                {
                    DateTime dt = DateTime.Parse(objValue.ToString());
                    if (dt != DateTime.MinValue && dt != DateTime.Parse("1900-01-01"))
                    { ret = dt.ToString(formate); }
                }
            }
            catch
            {
            }
            return ret;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objValue"></param>
        /// <param name="formate">yyyy-MM-dd </param>
        /// <returns></returns>
        public static string DateTime2String(object objValue)
        {
            string formate = "yyyy-MM-dd HH:mm";
            string ret = "";
            try
            {
                if (objValue != null)
                {
                    DateTime dt = DateTime.Parse(objValue.ToString());
                    if (dt != DateTime.MinValue && dt != DateTime.Parse("1900-01-01"))
                    { ret = dt.ToString(formate); }
                }
            }
            catch
            {
            }
            return ret;
        }

        #endregion


    }
}
