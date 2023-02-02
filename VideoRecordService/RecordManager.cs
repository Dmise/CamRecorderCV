using CameraRecordLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using CameraRecordLib;

namespace VideoRecordService
{
    internal class RecordManager : IRecordManager
    {
        private Settings _appsettings;
        private VideoWriter _videoWriter;
        /// <summary>
        /// camera
        /// </summary>
        private VideoCapture _camera;
        private RecordSettings _recordSettings;
        private bool _recording;

        RecordManager() 
        {           
            _recordSettings = new RecordSettings();
            _appsettings= new Settings();
            _appsettings.InitFromFile();
        }
        public string SaveTo { get { return _appsettings.saveTo; } }

        string IRecordManager.SaveTo 
        { 
            get => _appsettings.saveTo; 
            set => _appsettings.saveTo = value; 
        }

        public void InitFromFile()
        {
            throw new NotImplementedException();
        }

        //стопает и сохраняет текущую запись. Начинает новую.
        public async void Start(RecordType type)
        {
            _videoWriter = new VideoWriter(SaveTo, _recordSettings.fourcc , 25, _recordSettings.resolution.Size, true);

        }
    }
}
