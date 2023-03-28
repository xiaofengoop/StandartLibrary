using System.Runtime.InteropServices;

namespace StandartLibrary.MyConsoleClass
{
#pragma warning disable CS8602 // 解引用可能出现空引用。
    public class ConsoleClass
    {
        public Action? CTRL_C_EVENT { get; set; } = null;
        public Action? CTRL_BREAK_EVENT { get; set; } = null;
        public Action? CTRL_CLOSE_EVENT { get; set; } = null;
        public Action? CTRL_LOGOFF_EVENT { get; set; } = null;
        public Action? CTRL_SHUTDOWN_EVENT { get; set; } = null;

        public delegate bool ControlCtrlDelegate(int CtrlType);

        /// <summary>
        /// 从调用进程的处理程序函数列表中添加或删除应用程序定义的 HandlerRoutine 函数
        /// </summary>
        /// <param name="HandlerRoutine"> 控制台进程使用此函数来处理由进程接收的控制信号。
        /// 收到信号后，系统会在进程中创建一个新线程来执行该函数
        ///     <para>CTRL_C_EVENT : 0 : 从键盘输入或从GenerateConsoleCtrlEvent函数生成的信号接收到了CTRL+C信号。</para>
        ///     <para>CTRL_BREAK_EVENT : 1 : 从键盘输入或由GenerateConsoleCtrlEvent生成的信号接收到CTRL+BREAK信号。</para>
        ///     <para>CTRL_CLOSE_EVENT : 2 : 当用户关闭控制台时，系统会向附加到控制台的所有进程发送信号，
        ///     (方法是：单击控制台窗口的窗口菜单上的 " 关闭 " 或单击 "任务管理器") 中的 " 结束任务 " 按钮命令。</para>
        ///     <para>CTRL_LOGOFF_EVENT : 5 : 当用户注销时系统发送给所有控制台进程的信号。 此信号不指示哪个用户正在注销，因此不能进行假设。
        ///     (请注意，此信号仅由服务接收。 交互式应用程序将在注销时终止，因此当系统发送此信号时，它们不会出现。)</para>
        ///     <para>CTRL_SHUTDOWN_EVENT : 6 : 系统关闭时系统发送的信号。 系统发送此信号时不会出现交互式应用程序，因此，在这种情况下，只能接收到服务。
        ///     服务还具有用于关闭事件的自己的通知机制。</para>
        /// </param>
        /// <param name="Add"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public static extern bool SetConsoleCtrlHandler(ControlCtrlDelegate HandlerRoutine, bool Add);

        public ControlCtrlDelegate GetControlCtrlDelegate() => HandlerRoutine;

        private bool HandlerRoutine(int CtrlType)
        {
            switch (CtrlType)
            {
                case 0:
                    //Ctrl+C关闭
                    CTRL_C_EVENT();
                    break;
                case 1:
                    //CTRL+BREAK关闭
                    CTRL_BREAK_EVENT();
                    break;
                case 2:
                    //按控制台关闭按钮关闭
                    CTRL_CLOSE_EVENT();
                    break;
                case 5:
                    CTRL_LOGOFF_EVENT();
                    break;
                case 6:
                    CTRL_SHUTDOWN_EVENT();
                    break;
            }
            Console.ReadLine();
            return false;
        }
    }
#pragma warning restore CS8602 // 解引用可能出现空引用。
}
