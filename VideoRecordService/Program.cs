using Emgu.CV.DepthAI;
using Newtonsoft.Json;
using NLog.Extensions.Logging;
using NLog;
using System.ComponentModel.Design;
using VideoRecordService;
using System.Configuration;
using CameraRecordLib;
using Microsoft.Extensions.Hosting;

var configuration = new ConfigurationBuilder()
        //.Sources.Clear()
        //.AddEnvironmentVariables()
        //.AddCommandLine(args)
        //.SetBasePath(Environment.ProcessPath)
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .Build();


using IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService(options =>
    {
        options.ServiceName = "Video Record Service";
    })   
    .ConfigureHostConfiguration(conf =>
    {
        conf.Sources.Clear();
        conf.AddJsonFile("appsettings.json");       
    })
    //.ConfigureAppConfiguration((builderContext, config) =>
    //{
    //    config.AddConfiguration(configuration);
    //})
    .ConfigureServices((hostContext, services) =>
    {
        services.AddLogging();
        services.AddSingleton<RecordManager>();
        services.AddHostedService<Worker>();

        // add settings as singleton
        //IConfiguration configuration = hostContext.Configuration;
        //Settings settings = configuration.GetSection(nameof(Settings)).Get<Settings>();
        //services.AddSingleton(settings);

        // add settings as IOption
        services.AddOptions();
        services.Configure<Settings>(hostContext.Configuration.GetSection(nameof(Settings)));       
    })
    .ConfigureLogging(logging =>
    {
        logging.ClearProviders();
        logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
        logging.AddNLog();
    })
    .Build();


LogManager.Configuration = new NLogLoggingConfiguration(host.Services.GetService<HostBuilderContext>().Configuration.GetSection("NLog"));
var logger = LogManager.GetCurrentClassLogger();
logger.Info($"Starting with config:{Environment.NewLine}{JsonConvert.SerializeObject(configuration)}");

await host.RunAsync();