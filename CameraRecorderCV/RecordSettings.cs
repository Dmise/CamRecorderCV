using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CameraRecorderCV
{
    internal class RecordSettings
    {
        public int fourcc;
        public Resolution resolution;
        public VideoCapture videoCapture;
        public string folderPath;
        public bool isDuty;        

    }
}
