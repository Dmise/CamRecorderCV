using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CameraRecordLib
{
    public static class ResolutionInfo
    {
        public enum ResolutionEnum
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
            { ResolutionEnum._640_480, new Resolution{ Height = 480, Width = 640} },
            { ResolutionEnum._1024_768, new Resolution{ Height = 768, Width = 1024} },
            { ResolutionEnum._1280_960, new Resolution{ Height = 960, Width = 1280 } },
            { ResolutionEnum._1600_1200, new Resolution{ Height = 1200, Width = 1600} },
            { ResolutionEnum._1280_720_169, new Resolution{ Height = 720, Width = 1280} },
            { ResolutionEnum._1920_1080_169, new Resolution{ Height = 1080, Width = 1920} },
        };

        public static List<Resolution> ResolutionList = new List<Resolution>()
        {
            new Resolution{ Height = 480, Width = 640},
            new Resolution{ Height = 768, Width = 1024},
            new Resolution{ Height = 960, Width = 1280},
            new Resolution{ Height = 1200, Width = 1600},
            new Resolution{ Height = 720, Width= 1280},
            new Resolution{ Height = 1080, Width = 1920},
        };

        /// <summary>
        /// Resolution to Size
        /// </summary>
        /// <param name="resol"></param>
        /// <returns></returns>
        public static Size ResolutionToSize(Resolution resol)
        {
            return new Size() { Height = resol.Height, Width = resol.Width };
        }

    }
}
