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
using AdInfinitum.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Data;
using System.Runtime;
using System.Text.RegularExpressions;
using Emgu.CV.Reg;
using System.Reflection;

namespace VideoRecordService
{
    public class RecordManager : IRecordManager
    {
        private Settings _appsettings;
        private VideoWriter _videoWriter;
        private ILogger<RecordManager> _logger;
        private IConfiguration _configuration;
        private Mat mat = new Mat();
        private int counter = 0;
        /// <summary>
        /// camera
        /// </summary>
        private VideoCapture _camera;
        private RecordSettings _recordSettings;
        private bool _dutyRecording = false;
        private bool _fragmentRecording = false;

        // TODO : IOptionsSnapshot
        public RecordManager(ILogger<RecordManager> logger, IConfiguration config, IOptions<Settings> settings) 
        {           
            _recordSettings = new RecordSettings();
            _appsettings = settings.Value;            
            _logger = logger;
            _configuration = config;            
        }
        
        public string SaveTo 
        { 
            get => _appsettings.SaveTo; 
            set => _appsettings.SaveTo = value;
        }

        /// <summary>
        /// load values from appsettings file
        /// </summary>
        public void InitFromFile(string filename)
        {

            var text = File.ReadAllText(filename);
            var set = JsonConvert.DeserializeObject<Settings>(text);
            if (set != null)
            {
                _appsettings.Resolution = set.Resolution;
                _appsettings.SaveTo = set.SaveTo;                
            }
            else
            {
                throw new DataException("Did not recognize Setting class from json settings file");
            }
        }

        //стопает и сохраняет текущую запись. Начинает новую.
        public void Start(RecordType type)
        {
            try
            {               
                _logger.InfoLine("Start recording");
                if (_dutyRecording)
                {
                    _camera.ImageGrabbed -= _capture_ImageGrabbed;
                    _videoWriter.Dispose();
                    _dutyRecording = false;
                    Start(RecordType.Duty);
                    return;                    
                }
                if (FindCamera())
                {
                    var filename = NameBuilder.GetDutyVideoName();
                    var fullFileName = SaveTo + filename;
                    var bid = GetBackEndId();
                    _logger.InfoLine($"try to create VideoWriter");
                    
                    // эти штуки крутил. на выходе видео всеравно жирное
                    var vrset = new Tuple<VideoWriter.WriterProperty, int>[]
                    {
                        Tuple.Create(VideoWriter.WriterProperty.Quality, 50),
                        Tuple.Create(VideoWriter.WriterProperty.Framebytes, 100),
                        Tuple.Create(VideoWriter.WriterProperty.IsColor, 0),
                        
                    };
                    _videoWriter = new VideoWriter(fullFileName,
                        (int)VideoCapture.API.Ffmpeg, // Msmf - работает но не сжимает // при Any выбирает Ffmpeg // VFW - требует установить кодек
                        _recordSettings.fourcc,
                        _recordSettings.fps,
                        new Size { Width = _recordSettings.resolution.Width, Height = _recordSettings.resolution.Height },
                        vrset);
                    _logger.InfoLine($"VideoWriterCreated");
                    
                    _dutyRecording = true; 
                    
                    _camera.ImageGrabbed += _capture_ImageGrabbed;
                }
                else
                {
                    _logger.InfoLine("Cannot find camera");
                }
            }
            catch (Exception ex) { _logger.InfoLine($"{ex.Message}"); }
        }

        private void _capture_ImageGrabbed(object? sender, EventArgs e)
        {
            counter++;
            try
            {                
                    _camera.Retrieve(mat);
                    if (_dutyRecording && _videoWriter != null)
                    {
                        _videoWriter.Write(mat);
                    
                                               
                    }                          
            }
            catch (Exception ex) { }
        }

        public bool FindCamera()
        {
            if (_camera == null)
            {
                var capture = new VideoCapture(0);
                if (capture.IsOpened)
                {
                    _camera = capture;
                    
                    //_camera.Set(CapProp.FourCC, _recordSettings.fourcc);
                    _camera.Set(CapProp.Fps, _recordSettings.fps);
                    // if more that webcam resolution set to webcam max                
                    _camera.Set(CapProp.FrameHeight, _recordSettings.resolution.Height);
                    _camera.Set(CapProp.FrameWidth, _recordSettings.resolution.Width);
                    _recordSettings.resolution.Width = _camera.Width;
                    _recordSettings.resolution.Height = _camera.Height;
                    _camera.Start();
                    return true;
                }
                else
                {
                    _logger.InfoLine("Can not find camera");
                    return false;
                }
            }
            return true;
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
    }
}
