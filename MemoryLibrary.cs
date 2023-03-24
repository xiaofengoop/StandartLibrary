using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace StandartLibrary
{
    //获取进程内存和运行时间
    public static partial class MemoryLibrary
    {
        private static Stopwatch _swatch = new();

        public static double GetProcessMemory(Process process)
        {
            return (double)process.PrivateMemorySize64 / (1024 * 1024);
        }

        public static void ShowProcessMemory(Process process)
        {
            Console.WriteLine("----------------------------------------------------------------");
            Console.WriteLine($"\"{process.ProcessName}\"内存为:{GetProcessMemory(process)}MB");
            Console.WriteLine("----------------------------------------------------------------");
        }

        public static void StartTimer()
        {
            _swatch.Start();
        }

        public static TimeSpan StopTimer()
        {
            _swatch.Stop();
            return _swatch.Elapsed;
        }

        public static void ReStartTimer()
        {
            _swatch.Restart();
        }
    }

    //获得程序类/结构体内存
    public static partial class MemoryLibrary
    {
        public static int GetMemberSize(Type t)
        {
            int size = -1;
            StructLayoutAttribute? slAttribute = TypeDescriptor.GetAttributes(t).OfType<StructLayoutAttribute>().FirstOrDefault();
            if(slAttribute is null)
            {
                TypeDescriptor.AddAttributes(t, new StructLayoutAttribute(LayoutKind.Sequential));
                slAttribute = TypeDescriptor.GetAttributes(t).OfType<StructLayoutAttribute>().FirstOrDefault();
                Console.WriteLine(slAttribute is null);
            }
            else
            {
                if(slAttribute.Value == LayoutKind.Auto)
                {
                    return size;
                }
            }
            return size;
        }
    }
}
