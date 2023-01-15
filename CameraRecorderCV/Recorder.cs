using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CameraRecorderCV
{
    
    internal class Recorder
    {
        private VideoWriter _vwriter;
        private VideoCapture _capture;
        private int fourcc = VideoWriter.Fourcc('m', 'p', '4', 'v'); // mpg4
        public delegate void Frame(Bitmap btm);
        public event Frame NewFrame;
        public CancellationToken _recordToken = new CancellationToken();
        public void Start(RecordSettings a)
        {
            Task.Run(() => StartRecord(a));
        }
        private async void StartRecord(RecordSettings settings)
        {
            var size = ResolutionInfo.GetSize(settings.resolution);
            _capture = settings.videoCapture;
            var filename = Path.Combine(settings.folderPath,GetFileName());
            try
            {
                var size1 = new Size(width: 480, height: 640);
                _vwriter = new VideoWriter(filename, fourcc, 24, size, true);
                //_vwriter = new VideoWriter(filename, 24, size1, true);
            }
            catch (NullReferenceException nrx)
            {
                throw nrx;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            while (!_recordToken.IsCancellationRequested)
            {
                try
                {
                    var queryframe = _capture.QueryFrame();
                    var imageCv = queryframe.ToImage<Bgr, Byte>();

                    var bitmap = new Bitmap(queryframe.Width,
                                            queryframe.Height,
                                            queryframe.Step,
                                            PixelFormat.Format24bppRgb,
                                            queryframe.DataPointer);
                    if (_vwriter.IsOpened)
                    {
                        _vwriter.Write(queryframe);
                    }
                    if (NewFrame != null)
                    {
                        NewFrame.Invoke(bitmap);
                    }
                }
            }
        }

        public void Stop() 
        { 
            _recordToken.ThrowIfCancellationRequested();
            _capture.Stop();  
            _capture.Dispose();
            _vwriter.Dispose();
        }

        private string GetFileName()
        {
            return "duty.mp4";
        }
    }
}
