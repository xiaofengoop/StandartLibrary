﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StandartLibrary.异常类
{
    public class MethodsLMatchExcept :ApplicationException
    {
        /// <summary>
        /// 类方法名称合集
        /// </summary>
        public List<string> ClassNameCollection { get; set; } = new();

        /// <summary>
        /// 枚举成员名称合集
        /// </summary>
        public List<string> EnumMemberCollection { get; set; } = new();

        public MethodsLMatchExcept() : base("方法与结构体数量不匹配！请检查") { }
    }
}
