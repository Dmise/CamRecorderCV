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
        private VideoWriter? _vwriter;
        private VideoCapture? _capture;        
        private int fourcc = FourCC.MP4V;
        private Mat mat = new Mat();
        private int fps = 24;
        
        public delegate void Frame(Bitmap btm);
        public event Frame NewFrame;     
        
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
                //_capture.Set(CapProp.Fps, fps);
                //_capture.Set(CapProp.XiWidth, size.Width);
                //_capture.Set(CapProp.XiHeight, size.Height);

                // set size as on the webcam
                size.Width = _capture.Width;
                size.Height = _capture.Height;
                var filename = Path.Combine(settings.folderPath, GetFileName());
                _vwriter = new VideoWriter(filename, fourcc, fps, size, true);

                _capture.Start();
                _capture.ImageGrabbed += _capture_ImageGrabbed;
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
            _capture.Retrieve(mat);            
            
            if (_vwriter != null)
            {
                _vwriter.Write(mat);                
            }            
           NewFrame.Invoke(mat.ToImage<Bgr, byte>().Flip(FlipType.Horizontal).ToBitmap());
        }

        public void Stop() 
        {
            try {
                _capture.ImageGrabbed -= _capture_ImageGrabbed;
                _vwriter.Dispose();
                _vwriter = null;
                _capture.Pause();
                _capture.Stop();
                _capture.Dispose();
                //_capture = null;
                //
            }
            catch (Exception ex) { }
        }

        private string GetFileName()
        {
            return "duty.mp4";
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
