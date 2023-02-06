using CameraRecordLib;
using Emgu.CV;
using System.Threading;

namespace VideoRecordService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;                       
        private RecordManager _recordManager;
        
        private Timer _timer;
        
        public Worker(ILogger<Worker> logger, RecordManager manager)                       
        {
            _logger = logger;             
            _timer = new Timer(new TimerCallback(timerProcess), null, TimeSpan.Zero, TimeSpan.FromMinutes(1));   
            _recordManager = manager;
        }
     
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Проверка на наличие директории
            
            if (!Directory.Exists(_recordManager.SaveTo)) Directory.CreateDirectory(_recordManager.SaveTo);

            

            while (!stoppingToken.IsCancellationRequested)
            {
                                          
            }
        }

        // проверка по таймеру. Сохраняем текущую запись, начинаем новую.
        public void timerProcess(object state)
        {
            _recordManager.Start(RecordType.Duty);
        }
    }
}