using Decaf.Main.ViewModels;
using Decaf.Main.Views;
using Microsoft.Extensions.Logging;
using Prism.Ioc;
using System.Diagnostics;

namespace Decaf
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            return MauiApp.CreateBuilder()
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .UsePrism(prism => 
                {
                    prism.ConfigureServices(services =>
                    {
                        Debug.WriteLine($"[{nameof(MauiProgram)}] [L] - ConfigureServices");
                    })
                    .ConfigureLogging(builder =>
                    {
                        builder.AddDebug();
                    })
                    .RegisterTypes(containerRegistry => 
                    {
                        Debug.WriteLine($"[{nameof(MauiProgram)}] [L] - Register Types");

                        containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
                    })
                    .OnInitialized(containerProvider => 
                    {
                        var eventAggregator = containerProvider.Resolve<IEventAggregator>();
                        eventAggregator?.GetEvent<NavigationRequestEvent>().Subscribe(context =>
                        {
                            Debug.WriteLine("\nNavigation Service");
                            Debug.WriteLine("Uri = " + context.Uri);
                            Debug.WriteLine("Parameters = " + context.Parameters);
                            Debug.WriteLine("Type = " + context.Type);
                            Debug.WriteLine("Cancelled = " + context.Cancelled);
                            Debug.WriteLine("NAVIGATIONSERVICE RESULT");
                            Debug.WriteLine("Success = " + context.Result.Success);
                            Debug.WriteLine("Context = " + context.Result.Context);

                            var exc = context.Result.Exception;
                            if (exc != null)
                            {
                                Debug.WriteLine("Navigation Service Exception");
                                Debug.WriteLine("Exception = " + exc);
                                Debug.WriteLine("Data = " + exc.Data);
                                Debug.WriteLine("HelpLink = " + exc.HelpLink);
                                Debug.WriteLine("HResult = " + exc.HResult);
                                Debug.WriteLine("InnerException = " + exc.InnerException);
                                Debug.WriteLine("Message = " + exc.Message);
                                Debug.WriteLine("Source = " + exc.Source);
                                Debug.WriteLine("StackTrace = " + exc.StackTrace);
                                Debug.WriteLine("TargetSite = " + exc.TargetSite);
                            }
                        });
                    })
                    .CreateWindow(async navigationService => 
                    {
                        Debug.WriteLine($"[{nameof(MauiProgram)}] [L] - Create window");
                        var navigationResult = await navigationService.NavigateAsync("/" + nameof(NavigationPage) + "/" + nameof(MainPage));
                    });
                })
                .Build();
        }
    }
}
