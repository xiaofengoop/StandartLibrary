using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.Versioning;

namespace StandartLibrary
{
    [SupportedOSPlatform("windows")]
    public class ImageDeal
    {
        private Size _size;
        private float _opacity = 1;

        public Image Image { get; private set; }
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
                if (value > 1 || value <= 0 || value == Opacity) { return; }
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
    }
}
