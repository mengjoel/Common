using System;
using System.Collections.Generic;
using System.Reflection;
namespace Tools
{
    /// <summary>
    /// 枚举帮助类
    /// </summary>
    public class EnumService
    {
        /// <summary>
        /// 将枚举转换为字符串
        /// </summary>
        /// <param name="typ"></param>
        /// <returns></returns>
        public static string GetStringAttribute<T>(T o)
        {
            try
            {
                Type objType = o.GetType();
                string s = o.ToString();
                StringAttributeAttribute[] objStringAttributeAttribute = (StringAttributeAttribute[])objType.GetField(s).GetCustomAttributes(typeof(StringAttributeAttribute), false);
                if (objStringAttributeAttribute != null && objStringAttributeAttribute.Length == 1)
                {
                    return objStringAttributeAttribute[0].StringName;
                }

                return s;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
        
        /// <summary>
        /// 将枚举转换为字符串
        /// </summary>
        /// <param name="typ"></param>
        /// <returns></returns>
        public static string GetStringAttribute<T>(int? i)
        {
            T o = (T)Enum.ToObject(typeof(T), i);
            return GetStringAttribute<T>(o);
        }

        /// <summary>
        /// 绑下拉框
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="ddl">下拉框</param>
        public static List<EnumModel> ToModelList(Type tp)
        {
            EnumModel model = null;
            List<EnumModel> list = new List<EnumModel>();
            foreach (string s in Enum.GetNames(tp))
            {
                model = new EnumModel();
                StringAttributeAttribute[] objStringAttributeAttribute = (StringAttributeAttribute[])tp.GetField(s).GetCustomAttributes(typeof(StringAttributeAttribute), false);
                if (objStringAttributeAttribute != null && objStringAttributeAttribute.Length == 1)
                {
                    model.Attribute = objStringAttributeAttribute[0].StringName;
                    model.Int = Convert.ToInt32(Enum.Format(tp, Enum.Parse(tp, s), "d"));
                    model.String = s;
                }
                list.Add(model);
            }
            return list;
        }

        /// <summary>
        /// 根据属性获取值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="attribute"></param>
        /// <returns></returns>
        public static int GetIntByAttribute<T>(string attribute)
        {
            List<EnumModel> list = ToModelList(typeof(T));
            foreach (EnumModel item in list)
            {
                if (item.Attribute == attribute)
                {
                    return item.Int;
                }
            }
            return -1;
        }

        /// <summary>
        /// 根据文本获取描述文字
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetAttributeByString<T>(string str)
        {
            List<EnumModel> list = ToModelList(typeof(T));
            foreach (EnumModel item in list)
            {
                if (item.String == str)
                {
                    return item.Attribute;
                }
            }
            return "";
        }
        /// <summary>
        /// 将枚举转换成Dictionary类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Dictionary<int, string> EnumParseDictionary(Type type)
        {
            Dictionary<int, string> dic = new Dictionary<int, string>();
            string[] names = Enum.GetNames(type);
            int[] values = (int[])Enum.GetValues(type);
            for (int i = 0; i < names.Length; i++)
            {
                dic.Add(values[i], names[i]);
            }

            return dic;
        }

        /// <summary>
        /// 获取所有枚举对象
        /// </summary>
        /// <param name="tp"></param>
        /// <returns></returns>
        public static List<EnumModel> GetEnumModelList(Type tp)
        {
            EnumModel model = null;
            List<EnumModel> list = new List<EnumModel>();
            foreach (string s in Enum.GetNames(tp))
            {
                model = new EnumModel();
                StringAttributeAttribute[] objStringAttributeAttribute = 
                    (StringAttributeAttribute[])tp.GetField(s).GetCustomAttributes(typeof(StringAttributeAttribute), false);
                if (objStringAttributeAttribute != null && objStringAttributeAttribute.Length == 1)
                {
                    model.Attribute = objStringAttributeAttribute[0].StringName;
                }
                model.Int = Convert.ToInt32(Enum.Format(tp, Enum.Parse(tp, s), "d"));
                model.String = s;
                list.Add(model);
            }
            return list;
        }


        /// <summary>
        /// 将枚举转换为字符串
        /// </summary>
        /// <param name="typ"></param>
        /// <returns></returns>
        public static string GetStringAttribute<T>(int i)
        {
            T o = (T)Enum.ToObject(typeof(T), i);
            return GetStringAttribute<T>(o);
        }

    }
    /// <summary>
    /// 枚举对象
    /// </summary>
    public class EnumModel
    {
        public int Int { get; set; }
        public string String { get; set; }
        public string Attribute { get; set; }
    }

    [System.AttributeUsage(System.AttributeTargets.All, AllowMultiple = false)]
    public class StringAttributeAttribute : Attribute
    {
        protected string _StringName;
        //返回属性的内容
        public string StringName
        {
            get { return _StringName; }
            set { _StringName = value; }
        }

        public StringAttributeAttribute(string strAttributeName)
        {
            _StringName = strAttributeName;
        }

        public StringAttributeAttribute()
        {
            _StringName = string.Empty;
        }
    }
}
