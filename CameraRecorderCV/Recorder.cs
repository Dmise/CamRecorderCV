using Emgu.CV;
using Emgu.CV.CvEnum;
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
        private VideoWriter? _vdutywriter;
        private VideoWriter? _vfragmentwriter;
        private VideoCapture? _capture;        
        private int fourcc = FourCC.MP4V;
        private Mat mat = new Mat();
        private int fps = 24;
        public bool dutyRecording = false;
        public bool fragmentRecording = false;

        public delegate void Frame(Bitmap btm);
        public event Frame NewFrame; 
        public event EventHandler UpdateStatus;
        
        public void Start(RecordSettings settings)
        {                   
            try
            {
                // create/init videoWriter
                var size = ResolutionInfo.GetSize(settings.resolution);
                
                _capture = settings.videoCapture;
                _capture.Set(CapProp.FourCC, fourcc);

                // if more that webcam resolution set to webcam max
                _capture.Set(CapProp.FrameHeight, size.Height); 
                _capture.Set(CapProp.FrameWidth, size.Width);                

                // set size as on the webcam/VideoCapture object
                size.Width = _capture.Width;
                size.Height = _capture.Height;
                var filename = Path.Combine(settings.folderPath, GetFileName(settings));
                if (settings.isDuty)
                {
                    dutyRecording = true;
                    _vdutywriter = new VideoWriter(filename, fourcc, fps, size, true);
                }
                else
                {
                    fragmentRecording = true;
                    _vfragmentwriter = new VideoWriter(filename, fourcc, fps, size, true);
                }

                _capture.Start();
                _capture.ImageGrabbed += _capture_ImageGrabbed;
                UpdateStatus.Invoke(this, new EventArgs());
            }
            catch (NullReferenceException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
       
        private void _capture_ImageGrabbed(object? sender, EventArgs e)
        {
            try
            {
                _capture.Retrieve(mat);

                if (dutyRecording && _vdutywriter != null)
                {
                    _vdutywriter.Write(mat);
                }
                if (fragmentRecording && _vfragmentwriter != null)
                {
                    _vfragmentwriter.Write(mat);
                }
                NewFrame.Invoke(mat.ToImage<Bgr, byte>().Flip(FlipType.Horizontal).ToBitmap());
            }
            catch (Exception ex) { }
        }

        public void StopDuty() 
        {
            try 
            {
                dutyRecording = false;
                _capture.ImageGrabbed -= _capture_ImageGrabbed;
                _vdutywriter.Dispose();
                _vdutywriter = null;
                
                if (!fragmentRecording)
                {
                    _capture.Pause();
                    _capture.Stop();
                    _capture.Dispose();
                }
                UpdateStatus.Invoke(this, new EventArgs());
            }
            catch (Exception ex) { }
        }

        public void StopFragment()
        {
            if (_vfragmentwriter != null)
            {
                fragmentRecording = false;
                _vfragmentwriter.Dispose();
                _vfragmentwriter = null;
            }
            UpdateStatus.Invoke(this, new EventArgs());
        }

        private string GetFileName(RecordSettings settings)
        {
            var name = String.Empty;
            var res = ".mp4";
            if (settings.isDuty)
            {
                name += Namer.GetDutyVideoName();
            }
            else
            {
                name += Namer.GetFragmentVideoName();
            }
            return name + res;
        }
    }

    class Converter
    {
        public static Bitmap MatToBitmap(Mat mat)
        {
            return new Bitmap(mat.Width,
                              mat.Height,
                              mat.Step,
                              PixelFormat.Format24bppRgb,
                              mat.DataPointer);
        }
    }
}
