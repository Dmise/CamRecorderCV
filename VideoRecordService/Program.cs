using VideoRecordService;

IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService( options =>
    {
        options.ServiceName = ".NET Joke Service";
        
    })
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        services.AddSingleton<RecordManager>();
    })
    .Build();



await host.RunAsync();
