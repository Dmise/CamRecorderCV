 using Emgu.CV;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CameraRecordLib
{
    public class RecordSettings
    {
        public int fourcc = FourCC.MP4V; // XVID -.avi
        public Resolution resolution = ResolutionInfo.ResolutionDict[ResolutionInfo.ResolutionEnum._1920_1080_169];
        //public VideoCapture videoCapture;
        public int fps = 24;
        public string? SaveTo { get; set; }        
        public RecordType recordType = RecordType.Duty;
    }
}
