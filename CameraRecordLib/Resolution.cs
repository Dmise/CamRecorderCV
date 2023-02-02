using System.Drawing;

namespace CameraRecordLib
{
    public class Resolution
    {
        internal int Widht = 640;
        internal int Height = 480;

        public Size Size
        {
            get
            {
                return new Size(Widht, Height);
            }
        }
        public override string ToString()
        {
            float ratioF = (float)Widht / (float)Height;
            var ratio = (Math.Round(ratioF, 2) == 1.78) ? "16:9" : "4:3";
            return $"{Widht}x{Height} ({ratio})";
        }
    }
}
