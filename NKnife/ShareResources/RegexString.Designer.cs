﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace NKnife.ShareResources {
    using System;
    
    
    /// <summary>
    ///   一个强类型的资源类，用于查找本地化的字符串等。
    /// </summary>
    // 此类是由 StronglyTypedResourceBuilder
    // 类通过类似于 ResGen 或 Visual Studio 的工具自动生成的。
    // 若要添加或移除成员，请编辑 .ResX 文件，然后重新运行 ResGen
    // (以 /str 作为命令选项)，或重新生成 VS 项目。
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class RegexString {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal RegexString() {
        }
        
        /// <summary>
        ///   返回此类使用的缓存的 ResourceManager 实例。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("NKnife.ShareResources.RegexString", typeof(RegexString).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   重写当前线程的 CurrentUICulture 属性
        ///   重写当前线程的 CurrentUICulture 属性。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   查找类似 \r\n 的本地化字符串。
        /// </summary>
        public static string RegexStr_Br {
            get {
                return ResourceManager.GetString("RegexStr_Br", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 ((^((1[8-9]\d{2})|([2-9]\d{3}))([-\/\._])(10|12|0?[13578])([-\/\._])(3[01]|[12][0-9]|0?[1-9])$)|(^((1[8-9]\d{2})|([2-9]\d{3}))([-\/\._])(11|0?[469])([-\/\._])(30|[12][0-9]|0?[1-9])$)|(^((1[8-9]\d{2})|([2-9]\d{3}))([-\/\._])(0?2)([-\/\._])(2[0-8]|1[0-9]|0?[1-9])$)|(^([2468][048]00)([-\/\._])(0?2)([-\/\._])(29)$)|(^([3579][26]00)([-\/\._])(0?2)([-\/\._])(29)$)|(^([1][89][0][48])([-\/\._])(0?2)([-\/\._])(29)$)|(^([2-9][0-9][0][48])([-\/\._])(0?2)([-\/\._])(29)$)|(^([1][89][2468][048])([-\/\._])(0?2)([-\/\._])( [字符串的其余部分被截断]&quot;; 的本地化字符串。
        /// </summary>
        public static string RegexStr_Date {
            get {
                return ResourceManager.GetString("RegexStr_Date", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 ^([a-zA-Z]:)?[^:]+$ 的本地化字符串。
        /// </summary>
        public static string RegexStr_FileName {
            get {
                return ResourceManager.GetString("RegexStr_FileName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 (?&lt;email&gt;[a-zA-Z][a-zA-Z0-9-_.]+\@[a-zA-Z][a-zA-Z0-9-_]+\.(?(?=[a-zA-Z]{2}\.)([a-zA-Z0-9-_]{2}\.[a-zA-Z0-9-_]{2})|([a-zA-Z0-9-_]{2,3}))) 的本地化字符串。
        /// </summary>
        public static string RegexStr_FindEmail {
            get {
                return ResourceManager.GetString("RegexStr_FindEmail", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 ^(http|https)\://([a-zA-Z0-9\.\-]+(\:[a-zA-Z0-9\.&amp;amp;%\$\-]+)*@)*((25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])|localhost|([a-zA-Z0-9\-]+\.)*[a-zA-Z0-9\-]+\.(com|edu|gov|int|mil|net|org|biz|arpa|info|name|pro|aero|coop|museum|[a-zA-Z]{1,10}))(\:[0-9]+)*(/($|[a-zA-Z0-9\.\,\?\&apos;\\\+&amp;amp;%\$#\=~_\-]+))*$ 的本地化字符串。
        /// </summary>
        public static string RegexStr_HttpUrl {
            get {
                return ResourceManager.GetString("RegexStr_HttpUrl", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 ([+-]?)([^0][0-9]{1,}\.[0-9]{1,})E([+-]?)(\d+) 的本地化字符串。
        /// </summary>
        public static string RegexStr_ScientificNotation {
            get {
                return ResourceManager.GetString("RegexStr_ScientificNotation", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 ^([0-9a-zA-Z]+[-._+&amp;amp;])*[0-9a-zA-Z]+@([-0-9a-zA-Z]+[.])+[a-zA-Z]{2,4}$ 的本地化字符串。
        /// </summary>
        public static string RegexStr_SimpleEmail {
            get {
                return ResourceManager.GetString("RegexStr_SimpleEmail", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 ^(^\d{15}$|^\d{18}$|^\d{17}(\d|X|x))$ 的本地化字符串。
        /// </summary>
        public static string RegexStr_SimpleIdCard {
            get {
                return ResourceManager.GetString("RegexStr_SimpleIdCard", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 ^((([0-1]?[0-9])|(2[0-3])):([0-5]?[0-9])(:[0-5]?[0-9])?)$ 的本地化字符串。
        /// </summary>
        public static string RegexStr_Time {
            get {
                return ResourceManager.GetString("RegexStr_Time", resourceCulture);
            }
        }
    }
}
