using CameraRecordLib;
using Emgu.CV;
using System.Threading;

namespace VideoRecordService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private bool _dutyRecording = false;               
        private RecordManager _recordManager;
        
        private Timer _timer;
        
        internal Worker(ILogger<Worker> logger, RecordManager manager)                       
        {
            _logger = logger;             
            _timer = new Timer(new TimerCallback(timerProcess), null, TimeSpan.Zero, TimeSpan.FromHours(3));   
            _recordManager = manager;
        }
     
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            
            while (!stoppingToken.IsCancellationRequested)
            {
                // Проверка на наличие директории
                if (!Directory.Exists(_recordManager.SaveTo)) Directory.CreateDirectory(_recordManager.SaveTo);
                // Начинаем запись в новый файл
                
                
            }
        }

        // проверка по таймеру. Сохраняем текущую запись, начинаем новую.
        public void timerProcess(object state)
        {
            _recordManager.Start(RecordType.Duty);
        }
    }
}