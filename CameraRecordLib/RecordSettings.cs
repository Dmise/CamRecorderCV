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
        
        public int fourcc = FourCC.H264; // XVID -.avi . // MP4V = .mp4 // MP42 - .avi // IYUV  // H264 - .mp4
                                        
        public Resolution resolution = ResolutionInfo.ResolutionDict[ResolutionInfo.ResolutionEnum._1920_1080_169];
        public RecordType recordType = RecordType.Duty;
        //public VideoCapture videoCapture;
        public int fps = 24;
        public string? SaveTo { get; set; }    
        public string Suffix 
        { 
            // TODO _suffix depends on recordType
            get 
            {
                switch (fourcc)
                {
                    case FourCC.MP4V:
                        return ".mp4";
                    case FourCC.XVID:
                        return ".avi";
                    case FourCC.MP42:
                        return ".avi";
                    default: return".mp4";
                }
                
            }             
        }
    }
}
