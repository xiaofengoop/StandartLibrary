using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace StandartLibrary
{
    public static class HandleMethod
    {
        //获取窗口标题--1
        [DllImport("user32.dll")]
        private static extern int GetWindowText(
            IntPtr hWnd,//窗口句柄
            StringBuilder lpString,//标题
            int nMaxCount //最大值
            );

        //获取窗口标题--1的封装方法
        /// <summary>
        /// 获取窗口标题
        /// </summary>
        /// <param name="hWnd">窗口句柄</param>
        /// <returns></returns>
        public static string GetHandleTitle(IntPtr hWnd)
        {
            StringBuilder sb = new();
            GetWindowText(hWnd, sb, sb.Capacity);
            return sb.ToString();
        }

        //获取窗口标题--2
        [DllImport("user32.dll")]
        private static extern int GetWindowText(
            IntPtr hWnd,//窗口句柄
            IntPtr lpString,//标题
            int nMaxCount //最大值
            );

        //获取类的名字
        [DllImport("user32.dll")]
        private static extern int GetClassName(
            IntPtr hWnd,//句柄
            StringBuilder lpString, //类名
            int nMaxCount //最大值
            );

        //获取窗口类名--获取类的名字的封装方法
        /// <summary>
        ///  获取窗口类名
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        public static string GetHandleClass(IntPtr hWnd)
        {
            StringBuilder sb = new();
            GetClassName(hWnd, sb, sb.Capacity);
            return sb.ToString();
        }

        //通过类名和名称获取句柄（className和appName填一个即可）
        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        public extern static IntPtr FindWindow(string? className, string? appName);

        /// <summary>
        /// 根据坐标获取窗口句柄
        /// </summary>
        /// <param name="Point">坐标</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern IntPtr WindowFromPoint(Point Point);

        //位置坐标
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left; //最左坐标
            public int Top; //最上坐标
            public int Right; //最右坐标
            public int Bottom; //最下坐标
        }

        //通过句柄获取窗口坐标
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);

        //遍历子窗口并处理委托
        private delegate bool EnumWindowsProc(int hWnd, int lParam);
        //子窗口句柄和标题名字典
        private static Dictionary<IntPtr, string> _pointChildren = new();

        //遍历子窗口
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool EnumChildWindows(IntPtr hwndParent, EnumWindowsProc lpEnumFunc, IntPtr lParam);

        //委托具体实现方法
        private static bool EnumWindowsMethod(int hWnd, int lParam)
        {
            IntPtr lpString = Marshal.AllocHGlobal(200);
            GetWindowText(hWnd, lpString, 200);//这里获得控件text
            var text = Marshal.PtrToStringAnsi(lpString);
            if (!string.IsNullOrWhiteSpace(text))
            {
                _pointChildren.Add(hWnd, text);//添加到list，如果要获得句柄就新建list添加hWnd
            }   
            return true;
        }

        /// <summary>
        /// 遍历子窗口方法
        /// </summary>
        /// <param name="point">父窗口句柄</param>
        /// <returns></returns>
        public static Dictionary<IntPtr, string> GetChildrenPtr(IntPtr point)
        {
            _pointChildren.Clear();
            EnumChildWindows(point, EnumWindowsMethod, IntPtr.Zero);
            return _pointChildren;
        }
    }
}
