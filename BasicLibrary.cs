using StandartLibrary.MyResolutionClass;
using StandartLibrary.MyNativeAPIClass;
using StandartLibrary.MyExceptionClass;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using static StandartLibrary.HandleMethod;

namespace StandartLibrary
{
    public class BasicLibraryEstension
    {
        //基本类统计方法，多为user32方法，可作为SetLastError参数，用于获取报错信息
        public enum BasicLibraryMethods
        {
            ShowWindow,
            GetAsyncKeyState,
            MoveWindow,
            GetCursorPos,
            GetSystemMetrics,
            SetProcessAffinityMask
        }

        //用于获取windows--user32报错信息
        public static void SetLastError(BasicLibraryMethods blm)
        {
            Dictionary<int, string> keyValuePairs = new();
            foreach (int item in Enum.GetValues(typeof(BasicLibraryMethods)))
            {
                keyValuePairs.Add(item, Enum.GetName(typeof(BasicLibraryMethods), item)!);
            }
            foreach (KeyValuePair<int, string> item in keyValuePairs)
            {
                DllImportAttribute? dllAttribute = typeof(BasicLibrary)?.GetMethod(item.Value)?.
                    GetCustomAttribute<DllImportAttribute>();
                if (dllAttribute is not null && dllAttribute.SetLastError == true)
                {
                    dllAttribute.SetLastError = false;
                }
            }
            string? methodName = keyValuePairs[(int)blm];
            DllImportAttribute? nowdllAttribute = typeof(BasicLibrary)?.GetMethod(methodName)?.
                GetCustomAttribute<DllImportAttribute>();
            if (nowdllAttribute is not null)
            {
                nowdllAttribute.SetLastError = true;
            }
        }

        //指标--用于确认获取windows信息项
        public enum MetricsNIndex
        {
            //'X 屏幕尺寸
            SM_CXSCREEN = 0,
            //'y 屏幕尺寸
            SM_CYSCREEN = 1,
            //'X 垂直滚动条中箭头的大小。
            SM_CXVSCROLL = 2,
            //'Y 水平滚动条中箭头的大小
            SM_CYHSCROLL = 3,
            //'窗口高度标题
            SM_CYCAPTION = 4,
            //'无大小边界的宽度
            SM_CXBORDER = 5,
            //'不可调整的边界高度
            SM_CYBORDER = 6,
            //'对话框边框宽度
            SM_CXDLGFRAME = 7,
            //'对话框边框的高度
            SM_CYDLGFRAME = 8,
            //'水平滚动条上滚动框的高度
            SM_CYHTHUMB = 9,
            // ' 水平滚动条上滚动框的宽度
            SM_CXHTHUMB = 10,
            //'标准图标的宽度
            SM_CXICON = 11,
            //'标准图标的高度
            SM_CYICON = 12,
            //'标准光标宽度
            SM_CXCURSOR = 13,
            //'标准光标高度
            SM_CYCURSOR = 14,
            //'菜单高度
            SM_CYMENU = 15,
            //'最大化窗口的客户区宽度
            SM_CXFULLSCREEN = 16,
            //'最大化窗口的客户区域高度
            SM_CYFULLSCREEN = 17,
            //'汉字窗口高度
            SM_CYKANJIWINDOW = 18,
            //'真是存在鼠标
            SM_MOUSEPRESENT = 19,
            //'垂直滚动条中的箭头高度
            SM_CYVSCROLL = 20,
            //'垂直滚动条中的箭头宽度
            SM_CXHSCROLL = 21,
            //'如果窗口的去uging版本正在运行，则为真
            SM_DEBUG = 22,
            //'如果左右按钮互换，则为 true。
            SM_SWAPBUTTON = 23,
            //'窗口的最小宽度
            SM_CXMIN = 28,
            //'窗户的最小高度
            SM_CYMIN = 29,
            //'标题栏位图宽度
            SM_CXSIZE = 30,
            //'标题栏位图的高度
            SM_CYSIZE = 31,
            //'窗口的最小跟踪宽度
            SM_CXMINTRACK = 34,
            //'窗口的最小跟踪高度
            SM_CYMINTRACK = 35,
            //'双击宽度
            SM_CXDOUBLECLK = 36,
            //'双击高度
            SM_CYDOUBLECLK = 37,
            //'桌面图标之间的宽度
            SM_CXICONSPACING = 38,
            //'桌面图标之间的高度
            SM_CYICONSPACING = 39,
            //'零，如果弹出菜单与 memu 栏项的左侧对齐。如果它向右对齐，则为 True。
            SM_MENUDROPALIGNMENT = 40,
            //'笔窗口 DLL 的句柄（如果已加载）。
            SM_PENWINDOWS = 41,
            //'true（如果启用了双字节字符）
            SM_DBCSENABLED = 42,
            //'鼠标按钮数。
            SM_CMOUSEBUTTONS = 43,
            //'系统指标数
            SM_CMETRICS = 44,
            //'Windows 95 启动模式。0 = 正常，1 = 安全，2 = 网络安全
            SM_CLEANBOOT = 67,
            //'win95最大化窗口的默认宽度
            SM_CXMAXIMIZED = 61,
            //'调整 Win95 窗口大小时最大宽度
            SM_CXMAXTRACK = 59,
            //'菜单宽度复选标记位图
            SM_CXMENUCHECK = 71,
            //'菜单栏上按钮宽度 英寸
            SM_CXMENUSIZE = 54,
            //'矩形宽度，最小化的窗口必须适合该宽度。
            SM_CXMINIMIZED = 57,
            //'win95 最大化窗口的默认高度为
            SM_CYMAXIMIZED =  62,
            //'调整 Win95 窗口大小时最大宽度
            SM_CYMAXTRACK = 60,
            //'菜单高度复选标记位图
            SM_CYMENUCHECK = 72,
            //'菜单栏上按钮的高度 英寸
            SM_CYMENUSIZE = 55,
            //'矩形高度，最小化的窗户必须适合该矩形。
            SM_CYMINIMIZED = 58,
            //'高度的窗户95小标题
            SM_CYSMCAPTION = 51,
            //'Hebrw 和阿拉伯语启用 Windows 95
            SM_MIDEASTENABLED = 74,
            //如果存在网络，则设置 'bit o。
            SM_NETWORK = 63,
            //'如果 Windows 95 系统上存在安全性，则为 true
            SM_SECURE = 44,
            //'true，如果计算机运行 win95 太慢。
            SM_SLOWMACHINE = 73,
            //'如果启用了输入法管理器/输入法编辑器功能，则为非零值; 否则为 0。
            SM_IMMENABLED = 82,
            //'非零（如果启动了平板电脑输入服务）; 否则为 0
            SM_TABLETPC = 86,
            //'非零值（如果启动了平板电脑输入服务）; 否则为 0。
            SM_DIGITIZER = 94,
            //'非零（如果系统中有数字化器）; 否则为 0。
            SM_MAXIMUMTOUCHES = 95
        }
    }

    public class BasicLibrary
    {
        /// <summary>
        /// windows/user32.dll方法
        /// </summary>
        [GetMethodNumber(typeof(BasicLibraryEstension.BasicLibraryMethods))]
        public static class Win32Method
        {
            static Win32Method()
            {
                MethodNumber.JudgeMatch();
            }

            /// <summary>
            /// 设置进程核心
            /// </summary>
            /// <param name="point"></param>
            /// <param name="dwProcessAffinityMask"></param>
            /// <returns></returns>
            [DllImport("Kernel32.dll")]
            public static extern bool SetProcessAffinityMask(IntPtr point, int dwProcessAffinityMask);

            /// <summary>
            /// 获取系统指标
            /// </summary>
            /// <param name="nIndex"><seealso href="https://baike.baidu.com/item/GetSystemMetrics/5608817#1">索引值</seealso></param>
            /// <returns></returns>
            [DllImport("user32.dll")]
            public static extern int GetSystemMetrics(int nIndex);

            /// <summary>
            /// 获取鼠标相对屏幕坐标
            /// </summary>
            /// <param name="point"></param>
            /// <returns></returns>
            [DllImport("user32.dll")]
            public static extern bool GetCursorPos(out Point point);

            /// <summary>
            /// 控制窗体显示
            /// </summary>
            /// <param name="hWnd">要控制的控件</param>
            /// <param name="type">隐藏本dos窗体, 0: 后台执行；1:正常启动；2:最小化到任务栏；3:最大化</param>
            /// <returns></returns>
            [DllImport("user32.dll")]
            public static extern bool ShowWindow(IntPtr hWnd, int type);

            /// <summary>
            /// 确定用户当前是否按下了键盘上的一个键
            /// </summary>
            /// <param name="key">虚拟按键值</param>
            /// <returns>如果当前按下的键是你传递过的键，返回非零，否则返回零</returns>
            [DllImport("user32.dll")]
            public static extern int GetAsyncKeyState(int key);

            /// <summary>
            /// 设置目标窗体大小，位置
            /// </summary>
            /// <param name="hWnd">目标句柄</param>
            /// <param name="x">目标窗体新位置X轴坐标</param>
            /// <param name="y">目标窗体新位置Y轴坐标</param>
            /// <param name="nWidth">目标窗体新宽度</param>
            /// <param name="nHeight">目标窗体新高度</param>
            /// <param name="BRePaint">是否刷新窗体</param>
            /// <returns>如果函数成功，则返回值为非零值</returns>
            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            public static extern int MoveWindow(IntPtr hWnd, int x, int y, int nWidth, int nHeight, bool BRePaint);
        }

        /// <summary>
        /// 清除控制台上一行
        /// </summary>
        public static void ClearLine()
        {
            int currentLine = Console.GetCursorPosition().Top;
            Console.SetCursorPosition(0, currentLine - 1);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLine - 1);
        }

        /// <summary>
        /// 获取路径下所有文件信息
        /// </summary>
        /// <param name="path">目录路径</param>
        /// <returns>目录下所有文件信息</returns>
        public static IEnumerable<FileInfo> GetFullFile(string path)
        {
            //创建目录队列
            var dirList = new Queue<DirectoryInfo>();

            //创建文件列表
            var fileList = new List<FileInfo>();

            //创建path目录
            var dirInfo = new DirectoryInfo(path);
            dirList.Enqueue(dirInfo);

            //获取所有目录
            while(dirList.TryDequeue(out var dirTemp))
            {
                //获取目录下所有文件
                fileList.AddRange(dirTemp.GetFiles());

                //获取目录下所有目录
                foreach (var dir in dirTemp.GetDirectories())
                {
                    //加入队列
                    dirList.Enqueue(dir);
                }
            }

            return fileList;
        }

        /// <summary>
        /// Win32Mehtod-GetSystemMetrics的封装方法
        /// </summary>
        /// <param name="nindex">指标</param>
        /// <returns></returns>
        public static int GetSystemMetrics(BasicLibraryEstension.MetricsNIndex nindex)
        {
            return Win32Method.GetSystemMetrics((int)nindex);
        }

        /// <summary>
        /// 获取当前程序集中带有TypeSource特性的类的集合
        /// </summary>
        /// <param name="source">特性类型</param>
        /// <returns></returns>
        public static IEnumerable<Type>? GetClassWithTheAttribute(Type source)
        {
            //设置需要的type类型
            Type requirementType = typeof(Attribute);
            //如果不符合，则返回null
            if (!requirementType.IsInstanceOfType(source) && !requirementType.IsAssignableFrom(source)) { return null; }
            //获取包含目标特性的程序集
            Assembly? asm = source.Assembly;
            return asm.GetTypes().Where((t) =>
            {
                //获取程序集中所有类并获取类中特性
                foreach (Attribute item in t.GetCustomAttributes())
                {
                    //如果特性的类型和目标相同则返回true
                    if (item.GetType() == source)
                    {
                        return true;
                    }
                }
                return false;
            });
        }

        /// <summary>
        /// 截取应用图片
        /// ！！只能在window平台运行
        /// </summary>
        /// <param name="className"></param>
        /// <param name="appName"></param>
        /// <param name="path"></param>
        /// <param name="name"></param>
        public static void GetScreen(string className, string appName, string path, string name)
        {
            //非window平台直接报错
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                throw new PlatformNotSupportedException();
            }
            //根据窗口信息获取窗口句柄
            IntPtr p = FindWindow(className, appName);
            //如果没有句柄（无进程或其他情况）
            if (p == IntPtr.Zero)
            {
                throw new NoFindHandleExcept(Language.ZH_CN);
            }
            //获取位置坐标
            RECT fx = new();
            GetWindowRect(p, ref fx);
            //计算长度宽度
            int width = fx.Right - fx.Left;
            int height = fx.Bottom - fx.Top;

            //对图片进行截取保存
            using Bitmap bim = new(width, height);
            Graphics g = Graphics.FromImage(bim);
            //从屏幕截取图片
            g.CopyFromScreen(fx.Left, fx.Top, 0, 0, new Size(width, height));
            //保存
            bim.Save(Path.Combine(path, name + @".BMP"));
        }

        /// <summary>
        /// 改变分辨率
        /// </summary>
        /// <param name="size">分辨率大小</param>
        /// <exception cref="Exception">出现部分原因导致无法更改分辨率或必须重启才可生效</exception>
        public static void ChangeResolution(ResolutionSize size)
        {
            int width = size.Width, height = size.Height;
            // 初始化 DEVMODE结构
            DEVMODE devmode = new DEVMODE();
            devmode.dmDeviceName = new String(new char[32]);
            devmode.dmFormName = new String(new char[32]);
            devmode.dmSize = (short)Marshal.SizeOf(devmode);

            if (0 != NativeMethods.EnumDisplaySettings(null, NativeMethods.ENUM_CURRENT_SETTINGS, ref devmode))
            {
                devmode.dmPelsWidth = width;
                devmode.dmPelsHeight = height;

                // 改变屏幕分辨率
                int iRet = NativeMethods.ChangeDisplaySettings(ref devmode, NativeMethods.CDS_TEST);

                if (iRet == NativeMethods.DISP_CHANGE_FAILED)
                {
                    throw new Exception("不能执行您的请求");
                }
                else
                {
                    iRet = NativeMethods.ChangeDisplaySettings(ref devmode, NativeMethods.CDS_UPDATEREGISTRY);

                    switch (iRet)
                    {
                        // 成功改变
                        case NativeMethods.DISP_CHANGE_SUCCESSFUL:
                            {
                                break;
                            }
                        case NativeMethods.DISP_CHANGE_RESTART:
                            {
                                throw new Exception("你需要重新启动电脑设置才能生效");
                            }
                        default:
                            {
                                throw new Exception("改变屏幕分辨率失败");
                            }
                    }
                }
            }
        }

    }
}
