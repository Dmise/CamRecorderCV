using CameraRecordLib;
using Emgu.CV;
using Microsoft.Extensions.Options;
using System.Threading;
using AdInfinitum.Logging;
using System.Diagnostics;

namespace VideoRecordService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;                       
        private RecordManager _recordManager;
        
        private Timer _timer;
        
        public Worker(ILogger<Worker> logger, RecordManager manager, IOptions<Settings> settings)                       
        {
            _logger = logger;
#if DEBUG
            _timer = new Timer(new TimerCallback(timerProcess), null, TimeSpan.Zero, TimeSpan.FromSeconds(10));
#else
            _timer = new Timer(new TimerCallback(timerProcess), null, TimeSpan.Zero, TimeSpan.FromMinutes(settings.Value.VideoLength));   
#endif
            _recordManager = manager;
        }
     
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
                     
            if (!Directory.Exists(_recordManager.SaveTo)) Directory.CreateDirectory(_recordManager.SaveTo);
            
            while (!stoppingToken.IsCancellationRequested)
            {
                                          
            }
        }


        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            // Stop called without start
            if (base.ExecuteTask == null)
            {
                return;
            }

            try
            {
                _recordManager.StopOldRecord();
                // Signal cancellation to the executing method
                base.Dispose();
            }
            finally
            {
                // Wait until the task completes or the stop token triggers
                
                await Task.WhenAny(base.ExecuteTask, Task.Delay(Timeout.Infinite, cancellationToken)).ConfigureAwait(false);
            }

        }

        public void timerProcess(object state)
        {
            try
            {
                _recordManager.Start(RecordType.Duty);
            }
            catch (Exception ex)
            {
                _logger.ErrorLine(ex.Message);
                Environment.Exit(1);
            }
        }
    }
}