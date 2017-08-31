using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Reflection;
using System.Configuration;
using System.IO;
using System.Collections.Specialized;

namespace Tools
{
    public class Utils
    {
        public static bool IsInt(object obj)
        {
            try
            {
                Convert.ToInt64(obj);
                return true;
            }
            catch { return false; }
        }
        public static bool IsDecimal(object obj)
        {
            try
            {
                Convert.ToDecimal(obj);
                return true;
            }
            catch { return false; }
        }
        public static bool IsBoolean(object obj)
        {
            try
            {
                Convert.ToBoolean(obj);
                return true;
            }
            catch { return false; }
        }
        public static bool IsEmail(object obj)
        {
            try
            {
                string strEamilFarmat = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
                return System.Text.RegularExpressions.Regex.IsMatch(obj.ToString(), strEamilFarmat);
            }
            catch { return false; }
        }
        public static bool IsPostCode(object obj)
        {
            try
            {
                string strEamilFarmat = @"([0-9]{3})+.([0-9]{4})+";
                return System.Text.RegularExpressions.Regex.IsMatch(obj.ToString(), strEamilFarmat);
            }
            catch { return false; }
        }
        public static bool IsPhone(object obj)
        {
            try
            {
                string strEamilFarmat = @"^(0[0-9]{2,3}\-)?([2-9][0-9]{6,7})+(\-[0-9]{1,4})?$";
                return System.Text.RegularExpressions.Regex.IsMatch(obj.ToString(), strEamilFarmat);
            }
            catch { return false; }

        }
        public static bool IsMobile(object obj)
        {
            try
            {
                string strEamilFarmat = @"^((\(\d{3}\))|(\d{3}\-))?13[0-9]\d{8}|15[89]\d{8}";
                return System.Text.RegularExpressions.Regex.IsMatch(obj.ToString(), strEamilFarmat);
            }
            catch { return false; }
        }
        /// <summary>
        /// 获取枚举类型用于显示对应的备注
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public static DataTable GetEnumTable(Type en, string valueName, string dispName)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Columns.Add(valueName, typeof(System.Int32));
            dt.Columns.Add(dispName, typeof(System.String));
            if (en.BaseType.FullName != "System.Enum")
            {
                return dt;
            }
            FieldInfo[] fields = en.GetFields();
            foreach (FieldInfo f in fields)
            {
                if (f.Name == "value__")
                {
                    continue;
                }
                object fieldValue = Enum.Parse(en, f.Name);
                object[] attrs = f.GetCustomAttributes(true);

                foreach (object attr in attrs)
                {

                    try
                    {
                        Type ty = attr.GetType();
                        if (ty.FullName == "System.ComponentModel.DescriptionAttribute")
                        {
                            System.Data.DataRow dr = dt.NewRow();
                            dr[valueName] = (int)fieldValue;
                            dr[dispName] = ((System.ComponentModel.DescriptionAttribute)attr).Description;
                            dt.Rows.Add(dr);
                            break;
                        }
                    }
                    catch { }
                }
            }
            return dt;
        }
        /// <summary>
        /// 获取枚举类型用于显示对应的备注
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public static Hashtable GetEnumHashtable(Type en)
        {
            Hashtable ht = new Hashtable();

            if (en.BaseType.FullName != "System.Enum")
            {
                return ht;
            }
            FieldInfo[] fields = en.GetFields();
            foreach (FieldInfo f in fields)
            {
                if (f.Name == "value__")
                {
                    continue;
                }
                object fieldValue = Enum.Parse(en, f.Name);
                object[] attrs = f.GetCustomAttributes(true);

                foreach (object attr in attrs)
                {

                    try
                    {
                        Type ty = attr.GetType();
                        if (ty.FullName == "System.ComponentModel.DescriptionAttribute")
                        {
                            ht.Add((int)fieldValue,
                            ((System.ComponentModel.DescriptionAttribute)attr).Description);
                            break;
                        }
                    }
                    catch { }
                }
            }
            return ht;
        }
        /// <summary>
        /// 获取枚举值的描述
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public static string GetEnumDescription(Enum en)
        {
            Type type = en.GetType();
            MemberInfo[] memInfo = type.GetMember(en.ToString());
            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
                if (attrs != null && attrs.Length > 0)
                    return ((System.ComponentModel.DescriptionAttribute)attrs[0]).Description;
            }
            return en.ToString();
        }
        public static string __sqlStandard(string _sql)
        {
            try
            {
                return _sql.Replace("'", "");
            }
            catch { return ""; }
        }
        public static string SqlFilter(string inputStr)
        {
            if ((inputStr != null) && (inputStr != string.Empty))
            {
                //inputStr = inputStr.ToLower().Replace(",", "");
                //inputStr = inputStr.ToLower().Replace("<", "&lt;");
                //inputStr = inputStr.ToLower().Replace(">", "&gt;");
                //inputStr = inputStr.ToLower().Replace("%", "");
                //inputStr = inputStr.ToLower().Replace(".", "");
                //inputStr = inputStr.ToLower().Replace(":", "");
                //inputStr = inputStr.ToLower().Replace("#", "");
                //inputStr = inputStr.ToLower().Replace("&", "");
                //inputStr = inputStr.ToLower().Replace("$", "");
                //inputStr = inputStr.ToLower().Replace("^", "");
                //inputStr = inputStr.ToLower().Replace("*", "");
                //inputStr = inputStr.ToLower().Replace("`", "");
                //inputStr = inputStr.ToLower().Replace(" ", "");
                //inputStr = inputStr.ToLower().Replace("~", "");
                //inputStr = inputStr.ToLower().Replace("or", "");
                //inputStr = inputStr.ToLower().Replace("and", "");

                inputStr = inputStr.Replace("'", "''");
            }
            return inputStr;
        }
        struct LASTINPUTINFO
        {
            [MarshalAs(UnmanagedType.U4)]
            public int cbSize;
            [MarshalAs(UnmanagedType.U4)]
            public uint dwTime;
        }
        [DllImport("user32.dll")]
        static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);
        /// <summary>
        /// 获取程序闲置时间
        /// </summary>
        /// <returns></returns>
        public static long GetLastInputTime()
        {
            LASTINPUTINFO vLastInputInfo = new LASTINPUTINFO();
            vLastInputInfo.cbSize = Marshal.SizeOf(vLastInputInfo);
            if (!GetLastInputInfo(ref vLastInputInfo)) return 0;
            return Environment.TickCount - (long)vLastInputInfo.dwTime;
        }
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0X0112;
        public const int SC_MOVE = 0XF010;
        public const int HTCAPTION = 0X0002;
        public static string CreateReportDirectory()
        {
            string reportPath = ConfigurationManager.AppSettings["ReportPath"].ToString();
            bool isExists = Directory.Exists(reportPath);
            if (isExists == false)
            {
                Directory.CreateDirectory(reportPath);
            }
            string savePath = reportPath;
            if (!Directory.Exists(savePath + "\\LI"))
            {
                Directory.CreateDirectory(savePath + "\\LI");
            }
            if (!Directory.Exists(savePath + "\\LC"))
            {
                Directory.CreateDirectory(savePath + "\\LC");
            }
            return reportPath;
        }
        public static DataTable ListToDataTable<T>(List<T> list)
        {
            Type type = typeof(T);
            PropertyInfo[] infos = type.GetProperties();
            DataTable dt = new DataTable();
            foreach (PropertyInfo info in infos)
            {
                dt.Columns.Add(info.Name, info.PropertyType);
            }
            if (list != null)
            {
                foreach (T t in list)
                {
                    DataRow dr = dt.NewRow();
                    foreach (PropertyInfo info in infos)
                    {
                        dr[info.Name] = info.GetValue(t, null);
                    }
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }

        #region V1
        /// <summary>
        /// 获取随机数
        /// </summary>
        /// <param name="beg"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static int GetRedom(int beg, int end)
        {
            Random rd = new Random();
            return rd.Next(beg, end);
        }
        /// <summary>
        /// 截取固定数字的字符串,剩下的以省略号表示
        /// </summary>
        /// <param name="objValue"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string Substring(object objValue, int length)
        {
            string ret = "";
            string value = objValue == null ? "" : objValue.ToString();
            try
            {
                ret = value.Length > length ? value.Substring(0, length - 1) + "..." : value;
            }
            catch
            {

            }
            return ret;
        }
        #region  获取汉子拼音首字母
        /// <summary>
        /// 获取汉字首字母（可包含多个汉字）
        /// </summary>
        /// <param name="strText"></param>
        /// <returns></returns>
        public static string GetChineseSpell(string strText)
        {
            int len = strText.Length;
            string myStr = "";
            for (int i = 0; i < len; i++)
            {
                myStr += GetSpell(strText.Substring(i, 1));
            }
            return myStr;
        }

        public static string GetSpell(string cnChar)
        {
            byte[] arrCN = Encoding.Default.GetBytes(cnChar);
            if (arrCN.Length > 1)
            {
                int area = (short)arrCN[0];
                int pos = (short)arrCN[1];
                int code = (area << 8) + pos;
                int[] areacode = { 45217, 45253, 45761, 46318, 46826, 47010, 47297, 47614, 48119, 48119, 49062, 49324, 49896, 50371, 50614, 50622, 50906, 51387, 51446, 52218, 52698, 52698, 52698, 52980, 53689, 54481 };
                for (int i = 0; i < 26; i++)
                {
                    int max = 55290;
                    if (i != 25) max = areacode[i + 1];
                    if (areacode[i] <= code && code < max)
                    {
                        return Encoding.Default.GetString(new byte[] { (byte)(65 + i) });
                    }
                }
                return "*";
            }
            else
                return cnChar;
        }

        #endregion
        /// <summary>
        /// 判断是否英文和数字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public bool IsLetterNumber(string str)
        {
            System.Text.RegularExpressions.Regex reg1 = new System.Text.RegularExpressions.Regex(@"^[A-Za-z0-9]+$");
            return reg1.IsMatch(str);
        }
        #endregion

    }
}
