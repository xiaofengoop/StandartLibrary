using System.Collections;
using System.Runtime.InteropServices;

namespace StandartLibrary
{
    public class MyList<T> : IEnumerable<T>, IDisposable
    {
        //用来指向数组的指针
        IntPtr _p;
        //默认的数组基大小
        const int NumBase = 4;
        //数组的个数（可扩容）
        int _number;
        //数组的下标
        int _index;
        //数组内元素的内存大小
        readonly int _size;
        //IDispose参数
        private bool _disposedValue;

        private MyList<int>? _nullArray = null;

        //数组长度
        public int Length => _number;
        //元素数量
        public int Count => _index;

        public T this[int index]
        {
            set
            {
                //超出数组界限
                if (index < 0 || index >= _index)
                {
                    throw new IndexOutOfRangeException();
                }

                //定位修改元素位置
                IntPtr location = _p + index * _size;

                if(value is null)
                {
                    //若空数组为赋值
                    //赋值
                    _nullArray ??= new();

                    //添加该下标到空
                    _nullArray.Add(index);
                }
                else
                {
                    //修改值
                    Marshal.StructureToPtr(value, location, false);
                }

                //释放内存
                Marshal.FreeHGlobal(location);
            }

            get
            {
                if (index < 0 || index >= _index)
                {
                    throw new IndexOutOfRangeException();
                }

                //判断是否有空的值
                if(_nullArray is not null)
                {
                    //判断提取是否为空
                    foreach (var item in _nullArray)
                    {
                        //如果为空
                        if(item == index)
                        {
                            //返回null
                            return default!;
                        }
                    }
                }

                T? temp = Marshal.PtrToStructure<T>(_p + index * _size);
                if (temp is null)
                {
                    //储存信息错误
                    throw new ApplicationException("未知的错误！");
                }
                else
                {
                    return temp;
                }
            }
        }

        public MyList(int number)
        {
            _index = 0;
            _number = number > 0 ? number : NumBase;
            _size = Marshal.SizeOf(typeof(T));
            _p = Marshal.AllocHGlobal(_size * _number);
        }

        public MyList() : this(-1)
        {

        }

        public void Add(T[] datas)
        {
            //若将存入的数据大于剩余空间，则扩容
            if (_number - _index < datas.Length)
            {
                Capacity(_index + datas.Length);
            }
            for (int i = 0; i < datas.Length; i++)
            {
                T temp = datas[i];

                if (temp is null)
                {
                    //若空数组为赋值
                    //赋值
                    _nullArray ??= new();
                    _nullArray.Add(_index);
                }
                else
                {
                    Marshal.StructureToPtr(temp, _p + _index * _size, false);
                }
                _index++;
            }
        }

        public void Add(T data)
        {
            //若将存入的数据大于剩余空间，则扩容
            if (_number == _index)
            {
                Capacity(_index + 1);
            }

            T temp = data;

            if (temp is null)
            {
                //若空数组为赋值
                //赋值
                _nullArray ??= new();
                _nullArray.Add(_index);
            }
            else
            {
                Marshal.StructureToPtr(temp, _p + _index * _size, false);
            }
            _index++;
        }

        private void Capacity(int all)
        {
            int number = _number * 2;
            while (number < all)
            {
                number *= 2;
            }
            //重新开辟快新空间
            _p = Marshal.ReAllocHGlobal(_p, number * _size);
            _number = number;
        }

        public void Clear()
        {
            Marshal.FreeHGlobal(_p);
            if(_nullArray is not null)
            {
                _nullArray.Dispose();
                _nullArray = null;
            }
            _index = 0;
            _p = Marshal.AllocHGlobal(NumBase * _size);
        }

        private void IndexIsEffection(int index, int count)
        {
            if ((index + count - 1) >= _index) { throw new IndexOutOfRangeException(); }
            if (index < 0 || count <= 0) { throw new ArgumentOutOfRangeException(); }
        }

        public void RemoveAt(int index)
        {
            RemoveRange(index, 1);
        }

        public void RemoveRange(int index, int count)
        {
            IndexIsEffection(index, count);
            int nowindex = index + count;
            for(int i = nowindex; i < _index; i++)
            {
                if(_nullArray is not null)
                {
                    for(int j = 0; i < _nullArray.Count; j++)
                    {
                        //改变为null值的下标
                        _nullArray[j] = index;
                        continue;
                    }
                }
                T? temp = Marshal.PtrToStructure<T>(_p + i * _size);
                if(temp == null)
                {
                    throw new ApplicationException("未知错误！");
                }
                Marshal.StructureToPtr(temp, _p + index * _size, false);
                index++;
            }
            _index -= count;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new MyEnumerator<T>(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _nullArray?.Dispose();
                    Marshal.FreeHGlobal(_p);
                }

                // TODO: 释放未托管的资源(未托管的对象)并重写终结器
                // TODO: 将大型字段设置为 null
                _disposedValue = true;
            }
        }

        // // TODO: 仅当“Dispose(bool disposing)”拥有用于释放未托管资源的代码时才替代终结器
        // ~MyList()
        // {
        //     // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
