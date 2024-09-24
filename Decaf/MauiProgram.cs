using Decaf.Main.ViewModels;
using Decaf.Main.Views;

using Decaf.OnBoarding.Views;
using Decaf.OnBoarding.ViewModels;

using Microsoft.Extensions.Logging;
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

                    fonts.AddFont("SUIT-ExtraLight.ttf", "Suit_200");
                    fonts.AddFont("SUIT-Light.ttf", "Suit_300");
                    fonts.AddFont("SUIT-Regular.ttf", "Suit_400");
                    fonts.AddFont("SUIT-Medium.ttf", "Suit_500");
                    fonts.AddFont("SUIT-SemiBold.ttf", "Suit_600");
                    fonts.AddFont("SUIT-Bold.ttf", "Suit_700");
                    fonts.AddFont("SUIT-ExtraBold.ttf", "Suit_800");
                    fonts.AddFont("SUIT-Heavy.ttf", "Suit_900");
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
                        containerRegistry.RegisterForNavigation<OnBoardingPage, OnBoardingPageViewModel>();
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
                        var navigationResult = await navigationService.NavigateAsync("/" + nameof(NavigationPage) + "/" + nameof(OnBoardingPage));
                    });
                })
                .Build();
        }
    }
}
