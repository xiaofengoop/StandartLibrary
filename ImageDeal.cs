using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.Versioning;

namespace StandartLibrary
{
    [SupportedOSPlatform("windows")]
    public class ImageDeal : IDisposable
    {
        private Size _size;
        private float _opacity = 1;
        private bool disposedValue;

        private Image Image { get; set; }
        public Size Size
        {
            get => _size;
            set
            {
                if (value != _size)
                {
                    var bim = new Bitmap(value.Width, value.Height);
                    using var g = Graphics.FromImage(bim);
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.DrawImage(Image, 0, 0, bim.Width, bim.Height);
                    Image.Dispose();
                    Image = bim;
                    _size = value;
                }
            }
        }
        public float Opacity
        {
            get => _opacity;
            set
            {
                if (value > 1 || value <= 0 || value == Opacity) { throw new ArgumentOutOfRangeException("值只能在0(大于)到1(小于)之间"); }
                var bim = new Bitmap(Size.Width, Size.Height);
                using var g = Graphics.FromImage(bim);
                var matrix = new ColorMatrix() { Matrix33 = value };
                using var attributes = new ImageAttributes();
                attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                var rect = new Rectangle(0, 0, bim.Width, bim.Height);
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(Image, rect, 0, 0, bim.Width, bim.Height, GraphicsUnit.Pixel, attributes);
                Image.Dispose();
                Image = bim;
            }
        }

        public ImageDeal(Image img)
        {
            var bim = new Bitmap(img.Width, img.Height);
            using var g = Graphics.FromImage(bim);
            g.DrawImage(img, 0, 0, bim.Width, bim.Height);
            Image = bim;
            _size = new Size(img.Width, img.Height);
        }

        public void SmoothMedian(int k)
        {
            if (k % 2 == 0) { k -= 1; }
            if (k < 1) { throw new ArgumentOutOfRangeException("值错误"); }
            var t1 = BitmapExtension.ToImage<Bgr, byte>((Bitmap)Image);
            Image.Dispose();
            using var t2 = t1.SmoothMedian(k);
            t1.Dispose();
            Image = t2.ToBitmap();

        }

        public Image Save()
        {
            var bim = new Bitmap(Image.Width, Image.Height);
            using var g = Graphics.FromImage(bim);
            g.DrawImage(Image, 0, 0, bim.Width, bim.Height);
            return bim;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)
                    Image.Dispose();
                }

                // TODO: 释放未托管的资源(未托管的对象)并重写终结器
                // TODO: 将大型字段设置为 null
                disposedValue = true;
            }
        }

        // // TODO: 仅当“Dispose(bool disposing)”拥有用于释放未托管资源的代码时才替代终结器
        // ~ImageDeal()
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
