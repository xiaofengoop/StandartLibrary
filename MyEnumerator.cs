using System.Collections;

namespace StandartLibrary
{
    internal class MyEnumerator<T> : IEnumerator<T>
    {
        readonly MyList<T> _datas;

        int _position;

        private bool disposedValue;

        public T Current => _datas[_position];

        object IEnumerator.Current => Current!;

        public MyEnumerator(MyList<T> datas)
        {
            _position = -1;
            _datas = datas;
        }

        public bool MoveNext()
        {
            return ++_position < _datas.Count;
        }

        public void Reset()
        {
            _position = -1;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)
                    _datas.Dispose();
                }

                // TODO: 释放未托管的资源(未托管的对象)并重写终结器
                // TODO: 将大型字段设置为 null
                disposedValue = true;
            }
        }

        // // TODO: 仅当“Dispose(bool disposing)”拥有用于释放未托管资源的代码时才替代终结器
        // ~MyEnumerator()
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
