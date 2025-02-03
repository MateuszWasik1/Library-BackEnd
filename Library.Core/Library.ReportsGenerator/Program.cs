using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Library.Core; 
using Library.ReportsGenerator.Reports;

class Program
{
    private readonly IServiceProvider serviceProvider;

    public Program(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    public void Run()
    {
        Timer timer = new Timer(Callback, null, 0, 60000);
    }

    private void Callback(object state)
    {
        var date = DateTime.Now;

        if (date.Hour == 20 && date.Minute == 0)
        {
            var generator = serviceProvider.GetRequiredService<DataSumReport>();
            generator.GenerateDataSumReport();
        }
    }

    public static void Main()
    {
        var serviceProvider = ConfigureServices();
        var program = new Program(serviceProvider);

        program.Run();

        Console.WriteLine("Program działa. Naciśnij Enter, aby zakończyć...");
        Console.ReadLine();
    }

    private static IServiceProvider ConfigureServices()
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddDbContext<DataContext>(options =>
            options.UseSqlServer("Server=.\\SqlExpress; Database=Library; Trusted_Connection=true; TrustServerCertificate=true;"));

        serviceCollection.AddTransient<DataSumReport>();

        return serviceCollection.BuildServiceProvider();
    }
}