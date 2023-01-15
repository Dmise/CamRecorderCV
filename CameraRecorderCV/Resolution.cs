using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CameraRecorderCV
{
    internal class Resolution
    {
        internal int Widht;
        internal int Height;

        public override string ToString()
        {
            float ratioF = (float)Widht / (float)Height;
            var ratio = (Math.Round(ratioF, 2) == 1.78) ? "16:9" : "4:3";
            return $"{Widht}x{Height} ({ratio})";
        }
    }


    internal class ResolutionInfo
    {
        internal enum ResolutionEnum
        {
            _640_480,
            _1024_768,
            _1280_960,
            _1600_1200,
            _1280_720_169,
            _1920_1080_169
        };

        public static Dictionary<ResolutionEnum, Resolution> ResolutionDict = new Dictionary<ResolutionEnum, Resolution>()
        {
            { ResolutionEnum._640_480, new Resolution{ Height = 480, Widht = 640} },
            { ResolutionEnum._1024_768, new Resolution{ Height = 768, Widht = 1024} },
            { ResolutionEnum._1280_960, new Resolution{ Height = 960, Widht = 1280 } },
            { ResolutionEnum._1600_1200, new Resolution{ Height = 1200, Widht = 1600} },
            { ResolutionEnum._1280_720_169, new Resolution{ Height = 720, Widht = 1280} },
            { ResolutionEnum._1920_1080_169, new Resolution{ Height = 1080, Widht = 1920} },
        };

        public static List<Resolution> ResolutionList = new List<Resolution>()
        {
            new Resolution{ Height = 480, Widht = 640},
            new Resolution{ Height = 768, Widht = 1024},
            new Resolution{ Height = 960, Widht = 1280},
            new Resolution{ Height = 1200, Widht = 1600},
            new Resolution{ Height = 720, Widht = 1280},
            new Resolution{ Height = 1080, Widht = 1920},
        };

        public static Size GetSize(Resolution resol)
        {
            return new Size() { Height = resol.Height, Width = resol.Widht };
        }

    }
    
    
 
}
