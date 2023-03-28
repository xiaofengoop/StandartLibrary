using StandartLibrary.MyExceptionClass;
using System.Reflection;

namespace StandartLibrary
{
    public static class MethodNumber
    {
        public static void JudgeMatch()
        {
            Type[]? types = BasicLibrary.GetClassWithTheAttribute(typeof(GetMethodNumberAttribute));
            foreach (Type item in types)
            {
                GetMethodNumberAttribute attClass = item.GetCustomAttribute<GetMethodNumberAttribute>();
                MethodInfo[] methods = item.GetMethods();
                int methodsNumber = methods.Length;
                if (attClass.IgnoreObject)
                {
                    foreach (MethodInfo method in methods)
                    {
                        if (method.DeclaringType == typeof(Object))
                        {
                            methodsNumber--;
                        }
                    }
                }
                if (attClass.IgnoreBase)
                {
                    foreach (MethodInfo method in methods)
                    {
                        if (method.DeclaringType == item.BaseType)
                        {
                            methodsNumber--;
                        }
                    }
                }
                foreach (MethodInfo method in methods)
                {
                    bool isCount = true;
                    if (method.IsDefined(typeof(MethodNotCountAttribute)))
                    {
                        MethodNotCountAttribute att = method.GetCustomAttribute<MethodNotCountAttribute>();
                        methodsNumber += att.GetNumber();
                        isCount = att.IsCount;
                    }
                    else if (method.IsDefined(typeof(SetMethodNumberAttribute)) && isCount)
                    {
                        SetMethodNumberAttribute att = method.GetCustomAttribute<SetMethodNumberAttribute>();
                        methodsNumber += att.GetNumber();
                    }
                }
                string[] memberNames = Enum.GetNames(item.GetCustomAttribute<GetMethodNumberAttribute>().EM);
                if (memberNames.Length != methodsNumber)
                {
                    List<string> methodNames = new();
                    foreach (MethodInfo info in methods)
                    {
                        methodNames.Add(info.Name);
                    }
                    throw new MethodsLMatchExcept()
                    {
                        ClassNameCollection = methodNames,
                        EnumMemberCollection = memberNames.ToList()
                    };
                }
            }
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class GetMethodNumberAttribute : Attribute
    {
        /// <summary>
        /// 是否无视object类的方法(重写除外)
        /// </summary>
        public bool IgnoreObject { get; set; } = true;

        /// <summary>
        /// 是否无视父类方法(重写除外)
        /// </summary>
        public bool IgnoreBase { get; set; } = false;

        private Type standType = typeof(Enum);
        private Type _em;

        /// <summary>
        /// 枚举类
        /// </summary>
        public Type EM
        {
            get => _em;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }
                if (!value.IsInstanceOfType(standType) && !value.IsSubclassOf(standType))
                {
                    throw new NoEnumExcept();
                }
                _em = value;
            }
        }

        /// <summary>
        /// 获取方法数量并判断是否与被枚举对象数量相同
        /// </summary>
        /// <param name="t">枚举对象类</param>
        public GetMethodNumberAttribute(Type t)
        {
            EM = t;
        }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class MethodNotCountAttribute : Attribute
    {
        public bool IsCount { get; set; } = false;
        public int GetNumber()
        {
            return IsCount ? 0 : -1;
        }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class SetMethodNumberAttribute : Attribute
    {
        private int _number = 1;
        public int Number
        {
            get => _number;
            set
            {
                if (value < 0)
                {
                    _number = 0;
                }
                else
                {
                    _number = value;
                }
            }
        }

        public int GetNumber()
        {
            return Number - 1;
        }
    }
}
