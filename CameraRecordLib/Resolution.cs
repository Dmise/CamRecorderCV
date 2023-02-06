using System.Drawing;

namespace CameraRecordLib
{
    public class Resolution
    {
        public int Width { get; set; } = 1280;  
        public int Height { get; set; } = 720;

        public Size Size
        {
            get
            {
                return new Size(Width, Height);
            }
        }
        public override string ToString()
        {
            float ratioF = (float)Width / (float)Height;
            var ratio = (Math.Round(ratioF, 2) == 1.78) ? "16:9" : "4:3";
            return $"{Width}x{Height} ({ratio})";
        }
    }
}
