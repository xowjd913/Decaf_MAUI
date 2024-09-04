using System;
using System.Diagnostics;
using Decaf.Base.MVVM;

namespace Decaf.OnBoarding.ViewModels
{
    public class OnBoardingPageViewModel : ViewModelBase
    {
        public OnBoardingPageViewModel(
                INavigationService navigationService
            )
            : base(navigationService)
        {
            Debug.WriteLine($"[{nameof(OnBoardingPageViewModel)}] [L] : Initialize....");
        }
    }
}

