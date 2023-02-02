using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using CameraRecordLib;

namespace TestForm
{

    
    public class RecordManager : IRecordManager
    {
        private Settings _settings = new Settings();
        private VideoWriter? _dutywriter;
        private VideoWriter? _fragmentwriter;
        private VideoCapture? _capture;
        private Mat mat = new Mat();
        private RecordSettings _recordSettings = new RecordSettings();
        
        public RecordSettings RecordSettings 
        {   get { return _recordSettings; }
            set { _recordSettings = value; }
        }
        public bool dutyRecording = false;
        public bool fragmentRecording = false;        
        public delegate void Frame(Bitmap btm);

        public string SaveTo
        {
            get { return _settings.saveTo; }
            set { _settings.saveTo = value; }
        }

        public VideoCapture VideoCapture
        {
            get { return _capture; }
            set { _capture = value; }
        }

        public event Frame NewFrame;
        public event EventHandler UpdateStatus;

        
        public void Start(RecordType type)
        {                   
            try
            {
                // create/init videoWriter
                InitVideoWriter(RecordSettings, type);
                if (_capture == null)
                {
                    throw new NullReferenceException("Capture does not defined");
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

        private void InitVideoWriter(RecordSettings settings, RecordType type)
        {
            var size = ResolutionInfo.ResolutionToSize(settings.resolution);

            if (_capture != null)
            {
                //TODO: проверка максимального разрешения камеры.
                _capture.Set(CapProp.FourCC, settings.fourcc);
                // if more that webcam resolution set to webcam max                
                _capture.Set(CapProp.FrameHeight, size.Height);
                _capture.Set(CapProp.FrameWidth, size.Width);
                // set size as on the webcam/VideoCapture object
                size.Width = _capture.Width;
                size.Height = _capture.Height;
            }

            var filename = Path.Combine(settings.SaveTo, GetFileName(RecordType.Duty));
            if (!Directory.Exists(settings.SaveTo)) 
            { 
                Directory.CreateDirectory(settings.SaveTo);
            }
            if (type == RecordType.Duty)
            {
                dutyRecording = true;
                _dutywriter = new VideoWriter(filename, RecordSettings.fourcc, RecordSettings.fps, size, true);                
            }
            else
            {
                fragmentRecording = true;
                _fragmentwriter = new VideoWriter(filename, RecordSettings.fourcc, RecordSettings.fps, size, true);
            }
        }
        /// <summary>
        /// read settings from settings file
        /// </summary>
        public void InitFromFile()
        {
            _settings.InitFromFile();
        }

        /// <summary>
        /// Save settings to file
        /// </summary>
        public void SaveToFile()
        {
            _settings.SaveToFile();
        }
        private void _capture_ImageGrabbed(object? sender, EventArgs e)
        {
            try
            {
                _capture.Retrieve(mat);

                if (dutyRecording && _dutywriter != null)
                {
                    _dutywriter.Write(mat);
                    
                }
                if (fragmentRecording && _fragmentwriter != null)
                {
                    _fragmentwriter.Write(mat);
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

                //WHAT залипает при диспозе. Так что ставлю ссылку в нулл
                //if (_dutywriter != null) _dutywriter.Dispose();
                _dutywriter.Dispose();
                _dutywriter = null;
                
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
            if (_fragmentwriter != null)
            {
                fragmentRecording = false;
                _fragmentwriter.Dispose();
                _fragmentwriter = null;
            }
            UpdateStatus.Invoke(this, new EventArgs());
        }

        private string GetFileName(RecordType type)
        {
            var name = String.Empty;            
            if (type == RecordType.Duty)
            {
                name += NameBuilder.GetDutyVideoName();
            }
            else
            {
                name += NameBuilder.GetFragmentVideoName();
            }
            return name;
        }

        public void Dispose()
        {
            
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
