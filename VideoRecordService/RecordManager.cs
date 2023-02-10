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
using AdInfinitum.Processes;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;

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
        private SystemProcess _dutyRecorder;
        private Process _recordingProc;
        private string _ffmpegTemplate = "-f dshow -rtbufsize 10M -vcodec mjpeg -i video=\"{0}\" -r 24 -vcodec libx264 -s 1280x720 {1}"; // 0 - cameraName, 1 - outputfile name  | -vcodec h264  \\ тут 
        private int programmWay = 1;
            
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
        /// load values from settings file
        /// </summary>
        public void InitFromFile(string filename)
        {

            var text = File.ReadAllText(filename);
            var set = JsonConvert.DeserializeObject<Settings>(text);
            if (set != null)
            {
                _appsettings.Resolution = set.Resolution;
                _appsettings.SaveTo = set.SaveTo;      
                _appsettings.CameraName = set.CameraName;
                if (set.FFmpegArgs != String.Empty)
                    _appsettings.FFmpegArgs = set.FFmpegArgs;
            }
            else
            {
                throw new DataException("Did not recognize Setting class from json settings file");
            }
        }

        //Начинает новую. стопает и сохраняет текущую запись. 
        public void Start(RecordType type)
        {
            try
            {
                // Стопаем старый. 
                StopOldRecord();
                // создаем и старутем новый процесс
                _logger.InfoLine("Start recording");                
                StartRecordProcess();
                
            }
            catch (Exception ex) { _logger.InfoLine($"{ex.Message}"); }
        }

        private void StartRecordProcess()
        {        
            try
            {
                _logger.InfoLine("Start record process");

                // меняем имя
                var outputName = String.Concat(_appsettings.SaveTo, NameBuilder.GetDutyVideoName());
                
                if (_appsettings.FFmpegArgs != string.Empty)
                {
                    _logger.InfoLine("FFmpegArgs in settings file not empthy");
                    var args = String.Concat(_appsettings.FFmpegArgs,$" {outputName}");
                    StartFFmpegProcess(args);
                }
                else
                {                   
                    _logger.InfoLine("Use default FFmpegArgs template");
                    var args = String.Format(_ffmpegTemplate, _appsettings.CameraName, outputName);
                    StartFFmpegProcess(args);
                }
            }
            catch (Exception ex)
            {
                _logger.ErrorLine($"{ex}");
            }
            void StartFFmpegProcess(string args)
            {
                
                var ffpath = Path.Combine(_appsettings.FFmpegBinPath, "ffmpeg");
                switch (programmWay)
                {
                    case 1:
                        if (_recordingProc == null)
                        {
                            _recordingProc = new Process();
                            _recordingProc.StartInfo.FileName = ffpath;
                            _recordingProc.StartInfo.CreateNoWindow = true;
                            _recordingProc.StartInfo.UseShellExecute = false;
                            //_recordingProc.StartInfo.RedirectStandardError = true;
                            _recordingProc.StartInfo.RedirectStandardInput = true;
                            _recordingProc.StartInfo.RedirectStandardOutput = true;
                            _recordingProc.OutputDataReceived += OutputHandler;
                            _recordingProc.ErrorDataReceived += ErrorHandler;
                        }
                        _recordingProc.StartInfo.Arguments = args;
                        _recordingProc.Start();
                        break;
                    case 2:

                        if (_dutyRecorder == null)
                        {
                            _dutyRecorder = new SystemProcess();
                            _dutyRecorder.CreateRunNoWindow(ffpath, args, OutputHandler, ErrorHandler);
                            _dutyRecorder.Process.StartInfo.WorkingDirectory = _appsettings.FFmpegBinPath;
                            _dutyRecorder.RunAsync();
                            AttachConsole((uint)_dutyRecorder.Process.Id);
                        }
                        _dutyRecorder.RunAsync();
                        break;
                }
            }
        }
        public void StopOldRecord()
        {        
                switch (programmWay)
                {
                    case 1:
                         if (_recordingProc != null)
                         {                        
                            _recordingProc.StandardInput.WriteLine('q');
                            _recordingProc.WaitForExit();
                         }
                         break;
                    case 2:
                        _dutyRecorder.Process.StandardInput.WriteLine('q');
                        var ffmpegProc = Process.GetProcessesByName("ffmpeg")[0];
                        ffmpegProc.StandardInput.WriteLine('q');
                        break;
                }                   
        }

        

        public bool FindCamera()
        {
            // TODO: автопоиск. сейчас ищем в cmd:  ffmpeg -list_devices true -f dshow -i dummy
            return false;
        }

        private void OutputHandler(object sender, DataReceivedEventArgs args)
        {
            _logger.InfoLine($"[{sender}] Process output: {args.Data}");
        }

        private void ErrorHandler(object sender, DataReceivedEventArgs args)
        {
            _logger.ErrorLine($"[{sender}] Process error: {args.Data}");
        }

        internal const int CTRL_C_EVENT = 0;
        const int CTRL_BREAK_EVENT = 1;
        
        [DllImport("kernel32.dll")]
        internal static extern bool GenerateConsoleCtrlEvent(uint dwCtrlEvent, uint dwProcessGroupId);

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool AttachConsole(uint dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        internal static extern bool FreeConsole();

        [DllImport("kernel32.dll")]
        static extern bool SetConsoleCtrlHandler(ConsoleCtrlDelegate HandlerRoutine, bool Add);
        // Delegate type to be used as the Handler Routine for SCCH
        delegate Boolean ConsoleCtrlDelegate(uint CtrlType);

        
    }
}
