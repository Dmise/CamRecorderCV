using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System.Drawing.Imaging;
using CameraRecordLib;
using Newtonsoft.Json;
using System.Data;

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
            get { return _settings.SaveTo; }
            set { _settings.SaveTo = value; }
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
                InitVideoWriter(type);
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

        private int GetBackEndId()
        {
            Backend[] backends = CvInvoke.WriterBackends;
            int backend_idx = 0; //any backend;
            foreach (Backend be in backends)
            {
                if (be.Name.Equals("MSMF"))
                {
                    backend_idx = be.ID;
                    return backend_idx;
                }
            }
            return 0;
        }
        private void InitVideoWriter(RecordType type)
        {
            var size = ResolutionInfo.ResolutionToSize(_recordSettings.resolution);

            if (_capture != null)
            {
                //TODO: проверка максимального разрешения камеры.
                _capture.Set(CapProp.FourCC, _recordSettings.fourcc);
                _capture.Set(CapProp.Fps, _recordSettings.fps);
                // if more that webcam resolution set to webcam max                
                _capture.Set(CapProp.FrameHeight, size.Height);
                _capture.Set(CapProp.FrameWidth, size.Width);
                // set size as on the webcam/VideoCapture object
                size.Width = _capture.Width;
                size.Height = _capture.Height; 


                var filename = Path.Combine(_recordSettings.SaveTo, GetFileName(RecordType.Duty));
                if (!Directory.Exists(_recordSettings.SaveTo))
                {
                    Directory.CreateDirectory(_recordSettings.SaveTo);
                }
                if (type == RecordType.Duty)
                {
                    dutyRecording = true;
                    var bid = GetBackEndId();
                   // _dutywriter = new VideoWriter(filename, _recordSettings.fourcc, 24, new Size { Width = 1280, Height = 720 }, true); // работает без параметра fourcc
                    _dutywriter = new VideoWriter(filename, bid, _recordSettings.fourcc, _recordSettings.fps, new Size { Width = _capture.Width, Height = _capture.Height }, true); 
                }
                else
                {
                    fragmentRecording = true;
                    var bid = GetBackEndId();
                    _fragmentwriter = new VideoWriter(filename, bid, _recordSettings.fourcc, _recordSettings.fps, size, true);
                }
            }
        }
        /// <summary>
        /// read settings from settings file
        /// </summary>
        public void InitFromFile(string filename = "settings.json")
        {
            var text = File.ReadAllText(filename);
            var set = JsonConvert.DeserializeObject<Settings>(text);
            if (set != null)
            {
                _settings.Resolution = set.Resolution;
                _settings.SaveTo = set.SaveTo;
            }
            else
            {
                throw new DataException("Did not recognize Setting class from json settings file");
            }
        }

        /// <summary>
        /// Save settings to file
        /// </summary>
        public void SaveToFile(string filepath)
        {
            _settings.SaveTo = filepath;
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

                name += NameBuilder.GetDutyVideoName(_recordSettings.Suffix);
            }
            else
            {
                name += NameBuilder.GetFragmentVideoName(_recordSettings.Suffix);
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
